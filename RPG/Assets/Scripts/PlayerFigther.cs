using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerFigther : Fighter
{
    [Header("UI")]
    public PlayerSkillPanel skillPanel;
    void Awake()
    {
        this.stats = new Stats(21, 60, 50, 45, 20);
    }

    
    public override void InitTurn()
    {
        this.skillPanel.Mostrar();
        for (int i = 0; i < this.skills.Length; i++)
        {
            this.skillPanel.ConfigurarBotones(i, this.skills[i].name);
        }
    }

    public void EjecturSkill(int index)
    {
        this.skillPanel.Ocultar();
        Skill skill=this.skills[index];
        skill.TomarEmisorAndReceptor(
            this, this.combatManager.GetOpposingFigther());

        this.combatManager.OnFigtherSkill(skill);
        Debug.Log($"uso la habilidad {skill.name}");
    }
    
}
