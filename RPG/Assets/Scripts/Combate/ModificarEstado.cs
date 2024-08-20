using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusModType
{
    ATTACK_MOD,
    DEFFENSE_MOD
}
public class ModificarEstado : MonoBehaviour
{
    public StatusModType tipo;
    public float cantidad;

    public Stats Aplicar(Stats stats)
    {
        Stats modedStats = stats.clone();

        if (this.tipo == StatusModType.ATTACK_MOD)
        {
            modedStats.ataque += this.cantidad;
        }
        else if(this.tipo == StatusModType.DEFFENSE_MOD)
        {
            modedStats.defensa += this.cantidad;
        }
        return modedStats;
    }

    
}
