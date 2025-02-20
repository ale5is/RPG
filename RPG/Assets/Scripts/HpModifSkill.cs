using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class HpModifSkill : Skill
{
    public enum HpModifType
    {
        STAT_BASED,FIXED,PERCENTAGE
    }

    [Header("Health Mod")]
    public float cantidad;
    public HpModifType modifType;

    [Range(0f, 1f)]
    public float critChance=0;

    protected override void OnRun()
    {
        float cantidad = this.GetModificacion();
        float dice=Random.Range(0f,1f);
        if (dice <= this.critChance)
        {
            cantidad *= 2f;
            this.messages.Enqueue("Daño crú‘ico!");
        }
        this.receptor.ModifyHp(cantidad);
    }
    public float GetModificacion()
    {
        if (this.modifType==HpModifType.STAT_BASED)
        {
            Stats emisorStats = this.emisor.GetCurrentStats();
            Stats receptorStats = this.receptor.GetCurrentStats();

            float dato=(((2*emisorStats.nivel)/5)+2)*this.cantidad*(emisorStats.ataque/receptorStats.defensa);
            return ((dato / 50) + 2);
        }
        else if(this.modifType == HpModifType.FIXED)
        {
            return this.cantidad;
        }
        else if (this.modifType == HpModifType.PERCENTAGE)
        {
            Stats receptorStats = this.receptor.GetCurrentStats();
            return receptorStats.maxHp * this.cantidad;
        }
        throw new System.InvalidOperationException("HpModifSkill::GetDaño. Unreachable!");
    }
}
