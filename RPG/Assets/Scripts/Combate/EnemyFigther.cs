using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFigther : Fighter
{
    public float vida;
    public float vidaMax;
    public int nivel, xp, ataque, defensa, espiritu;
    public int baseXP = 50;
    void Awake()
    {
        CalcularEstadisticas();
        this.stats = new Stats(nivel, vida, vidaMax, ataque, defensa, espiritu);

    }
    private void CalcularEstadisticas()
    {
        vida = 20 + (nivel * 10);
        vidaMax = vida;
        ataque = 10 + (nivel * 5);
        defensa = 5 + (nivel * 3);
        espiritu = 10 + (nivel * 5);
        xp = (int)(50 * System.Math.Pow(nivel, 1.5)); // Fórmula para calcular la XP requerida
    }
    public int CalculateXP(int playerLevel)
    {
        float levelDifference = nivel - playerLevel;
        float multiplier = 1.0f;

        if (levelDifference > 0)
        {
            multiplier += levelDifference * 0.1f; // Aumenta un 10% por nivel superior.
        }
        else if (levelDifference < 0)
        {
            multiplier += levelDifference * 0.05f; // Reduce un 5% por nivel inferior.
        }

        multiplier = Mathf.Max(0.5f, multiplier); // Nunca menos del 50% del baseXP.
        return Mathf.RoundToInt(baseXP * multiplier);
    }

    public override void InitTurn()
    {
        StartCoroutine(this.IA());
    }
    IEnumerator IA()
    {
        yield return new WaitForSeconds(1f);

        Skill skill = this.skills[Random.Range(0, this.skills.Length)];
        Fighter target = null;
        do {
            Fighter[] players = this.combatManager.GetOpposingTeam();
            target = players[Random.Range(0, players.Length)];
        }
        while(target.isAlive==false);


        skill.TomarEmisorAndReceptor(
            this, target
            );

        this.combatManager.OnFigtherSkill(skill);
        
    }

}
