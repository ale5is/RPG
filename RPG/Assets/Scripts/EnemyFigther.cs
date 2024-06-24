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

        skill.TomarEmisorAndReceptor(
            this, this.combatManager.GetOpposingFigther());

        this.combatManager.OnFigtherSkill(skill);
    }
}
