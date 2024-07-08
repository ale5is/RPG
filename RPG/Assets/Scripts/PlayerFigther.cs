using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFigther : Fighter
{
    public float vida;
    public float vidaMax;
    public GameObject Save;
    [Header("UI")]
    public PlayerSkillPanel skillPanel;
    void Awake()
    {
        Save = GameObject.FindGameObjectWithTag("Save");
        vida=Save.GetComponent<Controlador>().vidaJugador;
        this.stats = new Stats(21, vida, 50, 45, 20);
        

    }
    public void VidaActual()
    {
        vida = stats.Hp;
        vidaMax = stats.maxHp;
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
