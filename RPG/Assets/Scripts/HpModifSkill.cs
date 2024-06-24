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

    public override void OnRun()
    {
        float cantidad = this.GetModificacion();
        this.receptor.ModifyHp(cantidad);
    }
    public float GetModificacion()
    {
        if (this.modifType==HpModifType.STAT_BASED)
        {
            Stats emisorStats = this.emisor.GetCurrentStats();
            Stats receptorStats = this.receptor.GetCurrentStats();

            float daño=(((2*emisorStats.nivel)/5)+2)*this.cantidad*(emisorStats.ataque/receptorStats.defensa);
            return ((daño / 50) + 2);
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
