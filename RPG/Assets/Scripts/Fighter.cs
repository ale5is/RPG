using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Fighter : MonoBehaviour
{
    public string nombre;
    public int id;
    public StatusPanel statusPanel;
    public CombatManager combatManager;
    public List<ModificarEstado> modificarEstados;

    protected Stats stats;
    protected Skill[] skills;
    
    public bool isAlive
    {
        get=> this.stats.Hp> 0;
    }
    protected virtual void Start()
    {
        this.statusPanel.setStats(this.nombre, this.stats);

        this.skills=this.GetComponentsInChildren<Skill>();

        this.modificarEstados = new List<ModificarEstado>();
    }

    public void ModifyHp(float cantidad)
    {
        this.stats.Hp = Mathf.Clamp(this.stats.Hp + cantidad, 0f, this.stats.maxHp);
        this.stats.Hp=Mathf.Round(this.stats.Hp);
        this.statusPanel.SetHP(this.stats.Hp, this.stats.maxHp);
    }

    public Stats GetCurrentStats()
    {
        Stats modedStast = this.stats;
        foreach(var mod in this.modificarEstados)
        {
            modedStast = mod.Aplicar(modedStast);
        }
        return modedStast; 
    }

    public abstract void InitTurn();
}
