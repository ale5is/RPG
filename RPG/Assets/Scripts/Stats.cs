using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    public float Hp, maxHp,ataque, defensa, espiritu;
    public int nivel;

    public Stats(int nivel,float maxHp,float ataque,float defensa,float espiritu)
    {
        this.nivel = nivel;
        this.maxHp = maxHp;
        this.Hp = maxHp;
        this.ataque = ataque;
        this.defensa = defensa;
        this.espiritu = espiritu;
    }

    public Stats clone()
    {
        return new Stats(this.nivel, this.maxHp, this.ataque, this.defensa, this.espiritu);
    }
}
