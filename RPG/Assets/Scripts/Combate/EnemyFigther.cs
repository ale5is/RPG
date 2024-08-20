using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFigther : Fighter
{
    void Awake()
    {
        this.stats = new Stats(20, 50, 40, 30, 60);
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
