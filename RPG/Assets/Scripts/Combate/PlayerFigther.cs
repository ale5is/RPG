using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFigther : Fighter
{
    // Variables de jugador
    public float vida, vidaMax;
    public int nivel, xp, ataque, defensa, espiritu, xpActual;
    public GameObject Save;

    [Header("UI")]
    public PlayerSkillPanel skillPanel;
    public EnemiesPanel enemiesPanel;

    private Skill skillToBeExecuted;

    // Awake se llama cuando el script se inicializa
    void Awake()
    {
        Save = GameObject.FindGameObjectWithTag("Save");
        Save.GetComponent<Controlador>().combate = 1;

        // Cargar datos dependiendo del jugador
        if (id == 0)
        {
            CargarDatosJugador(1);
        }
        else if (id == 1)
        {
            CargarDatosJugador(2);
        }

        // Calcular estadísticas con los valores cargados
        CalcularEstadisticas();
        
    }

    // Cargar los datos de un jugador específico
    private void CargarDatosJugador(int jugadorId)
    {
        if (jugadorId == 1)
        {
            vida = Save.GetComponent<Controlador>().vidaJugador1;
            vidaMax = Save.GetComponent<Controlador>().vidaMaxJugador1;
            nivel = Save.GetComponent<Controlador>().nuevosDatos.nivelJ1;
        }
        else
        {
            vida = Save.GetComponent<Controlador>().vidaJugador2;
            vidaMax = Save.GetComponent<Controlador>().vidaMaxJugador2;
            nivel = Save.GetComponent<Controlador>().nuevosDatos.nivelJ2;
        }
        xpActual = Save.GetComponent<Controlador>().nuevosDatos.xpRequerido;
        if (xpActual != 0)
        {
            subirNivel(xpActual);
        }

        // En caso de que la vida esté en cero, inicializar con un valor mínimo
        if (vida == 0) vida = 1;

        // Crear una instancia de Stats para el jugador
        stats = new Stats(nivel, vida, vidaMax, ataque, defensa, espiritu);
    }

    // Método para subir de nivel al jugador
    public void subirNivel(int xpGanada)
    {
        xp += xpGanada; // Sumar la XP ganada

        // Subir de nivel mientras tengamos suficiente XP
        while (xp >= CalcularXPRequeridaParaSiguienteNivel())
        {
            int xpRequerida = CalcularXPRequeridaParaSiguienteNivel(); // XP requerida para el nivel actual
            xp -= xpRequerida; // Consumir XP para subir al siguiente nivel
            nivel++; // Subir al siguiente nivel
            CalcularEstadisticas(); // Recalcular las estadísticas del jugador
        }

        // Guardar la experiencia requerida actual y la XP sobrante
        int experienciaRequerida = CalcularXPRequeridaParaSiguienteNivel();
        xpActual = xp;
        Save.GetComponent<Controlador>().nuevosDatos.xpRequerido = xpActual;
        Debug.Log($"Nivel: {nivel}, XP actual: {xp}, XP requerida para el próximo nivel: {experienciaRequerida}");
    }

    // Calcula la XP requerida para subir al siguiente nivel
    private int CalcularXPRequeridaParaSiguienteNivel()
    {
        return (int)(50 * Math.Pow(nivel, 1.5)); // Fórmula para XP requerida (ajustable si es necesario)
    }

    // Actualiza la vida del jugador con los valores de stats
    public void ActualizarVida()
    {
        vida = stats.Hp;
        vidaMax = stats.maxHp;
    }

    // Calcula y actualiza las estadísticas basadas en el nivel
    private void CalcularEstadisticas()
    {
        ataque = 10 + (nivel * 5);
        defensa = 5 + (nivel * 3);
        espiritu = 10 + (nivel * 5);
        // No se debe actualizar xp aquí. La XP debería ser gestionada en otro lugar.
    }

    // Inicializa el turno del jugador y muestra las habilidades en el UI
    public override void InitTurn()
    {
        this.skillPanel.MostrarJugador(this);

        // Configurar los botones de habilidades en el panel
        for (int i = 0; i < this.skills.Length; i++)
        {
            this.skillPanel.ConfigurarBotones(i, this.skills[i].skillName);
        }
    }

    // Ejecuta la habilidad seleccionada por el jugador
    public void EjecutarSkill(int index)
    {
        this.skillToBeExecuted = this.skills[index];

        if (this.skillToBeExecuted.selfInflicted)
        {
            // Si la habilidad es autocast (se usa sobre sí mismo)
            this.skillToBeExecuted.TomarEmisorAndReceptor(this, this);
            this.combatManager.OnFigtherSkill(this.skillToBeExecuted);
            this.skillPanel.Ocultar();
        }
        else
        {
            // Si la habilidad se usa sobre un enemigo
            Fighter[] enemies = this.combatManager.GetOpposingTeam();
            Debug.Log(enemies[0].name);
            this.enemiesPanel.Show(this, enemies);
        }
    }

    // Configura el objetivo de la habilidad y la ejecuta
    public void SetTargetAndAttack(Fighter enemyFigther)
    {
        this.skillToBeExecuted.TomarEmisorAndReceptor(this, enemyFigther);
        this.combatManager.OnFigtherSkill(this.skillToBeExecuted);

        // Ocultar los paneles después de ejecutar la habilidad
        this.skillPanel.Ocultar();
        this.enemiesPanel.Hide();
    }
}
