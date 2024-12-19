using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFigther : Fighter
{
    // Variables de estadísticas
    public float vida, vidaMax;
    public int nivel, xp, ataque, defensa, espiritu;
    public int baseXP = 50;

    void Awake()
    {
        // Calcular estadísticas al inicio
        CalcularEstadisticas();
        this.stats = new Stats(nivel, vida, vidaMax, ataque, defensa, espiritu);
    }

    // Método para calcular las estadísticas basadas en el nivel
    private void CalcularEstadisticas()
    {
        vida = 20 + (nivel * 10);  // Vida basada en el nivel
        vidaMax = vida;            // La vida máxima es igual a la vida actual
        ataque = 10 + (nivel * 5); // Ataque basado en el nivel
        defensa = 5 + (nivel * 3); // Defensa basada en el nivel
        espiritu = 10 + (nivel * 5); // Espíritu basado en el nivel
        xp = (int)(50 * System.Math.Pow(nivel, 1.5)); // XP según la fórmula
    }

    // Método para calcular la XP que un enemigo da a un jugador
    public int CalculateXP(int playerLevel)
    {
        float levelDifference = nivel - playerLevel;  // Diferencia de niveles
        float multiplier = 1.0f;

        // Aumenta un 10% por cada nivel superior, y disminuye un 5% por nivel inferior
        if (levelDifference > 0)
        {
            multiplier += levelDifference * 0.1f;
        }
        else if (levelDifference < 0)
        {
            multiplier += levelDifference * 0.05f;
        }

        // Nunca debe ser menor al 50% del XP base
        multiplier = Mathf.Max(0.5f, multiplier);

        return Mathf.RoundToInt(baseXP * multiplier);
    }

    // Método que inicializa el turno del enemigo
    public override void InitTurn()
    {
        StartCoroutine(IA()); // Iniciar la IA para seleccionar y ejecutar una habilidad
    }

    // Corutina para la IA del enemigo
    IEnumerator IA()
    {
        yield return new WaitForSeconds(1f); // Espera para simular el tiempo de decisión

        // Seleccionar una habilidad aleatoria
        Skill skill = this.skills[Random.Range(0, this.skills.Length)];

        // Seleccionar un objetivo aleatorio que esté vivo
        Fighter target = null;
        do
        {
            Fighter[] players = this.combatManager.GetOpposingTeam();
            target = players[Random.Range(0, players.Length)];
        }
        while (target.isAlive == false); // Asegurarse de que el objetivo esté vivo

        // Ejecutar la habilidad seleccionada sobre el objetivo
        skill.TomarEmisorAndReceptor(this, target);
        this.combatManager.OnFigtherSkill(skill); // Ejecutar la habilidad a través del CombatManager
    }
}
