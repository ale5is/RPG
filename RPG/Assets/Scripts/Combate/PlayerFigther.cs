using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFigther : Fighter
{
    public float vida, vidaMax;
    public int nivel, xp, ataque, defensa, espiritu;
    public GameObject Save;

    [Header("UI")]
    public PlayerSkillPanel skillPanel;
    public EnemiesPanel enemiesPanel;

    private Skill skillToBeExecuted;
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

        // En caso de que la vida esté en cero, inicializar con un valor mínimo
        if (vida == 0) vida = 1;

        stats = new Stats(nivel, vida, vidaMax, ataque, defensa, espiritu);
    }
    public void subirNivel(int xpGanada)
    {
        xp += xpGanada; // Sumar la XP ganada

        // Subir de nivel mientras tengamos suficiente XP
        while (xp >= CalcularXPRequeridaParaSiguienteNivel())
        {
            xp -= CalcularXPRequeridaParaSiguienteNivel(); // Consumir XP para subir al siguiente nivel
            nivel++;  // Subir al siguiente nivel
            CalcularEstadisticas();  // Recalcular las estadísticas del jugador
        }
    }

    // Calcula la XP requerida para subir al siguiente nivel
    private int CalcularXPRequeridaParaSiguienteNivel()
    {
        return (int)(50 * Math.Pow(nivel, 1.5)); // Fórmula para XP requerida (se puede ajustar si es necesario)
    }

    public void ActualizarVida()
    {
        vida = stats.Hp;
        vidaMax = stats.maxHp;
    }

    private void CalcularEstadisticas()
    {
        ataque = 10 + (nivel * 5);
        defensa = 5 + (nivel * 3);
        espiritu = 10 + (nivel * 5);
        // No se debe actualizar xp aquí. La XP debería ser gestionada en otro lugar.
    }
    public override void InitTurn()
    {

        this.skillPanel.MostrarJugador(this);
        

        for (int i = 0; i < this.skills.Length; i++)
        {
            this.skillPanel.ConfigurarBotones(i, this.skills[i].skillName);
        }
    }

    public void EjecturSkill(int index)
    {
        this.skillToBeExecuted=this.skills[index];

        if(this.skillToBeExecuted.selfInflicted)
        {
            this.skillToBeExecuted.TomarEmisorAndReceptor(this, this);
            this.combatManager.OnFigtherSkill(this.skillToBeExecuted);
            this.skillPanel.Ocultar();
        }
        else
        {
            Fighter[] enemies = this.combatManager.GetOpposingTeam();
            Debug.Log(enemies[0].name);
            this.enemiesPanel.Show(this,enemies);
        }
        
        
    }
    public void SetTargetAndAttack(Fighter enemyFigther)
    {
        this.skillToBeExecuted.TomarEmisorAndReceptor(this, enemyFigther);
        this.combatManager.OnFigtherSkill(this.skillToBeExecuted);

        this.skillPanel.Ocultar();
        this.enemiesPanel.Hide();
    }

}
