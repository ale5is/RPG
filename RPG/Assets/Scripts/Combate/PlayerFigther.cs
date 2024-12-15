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
    public EnemiesPanel enemiesPanel;

    private Skill skillToBeExecuted;
    void Awake()
    {
        Save = GameObject.FindGameObjectWithTag("Save");
        Save.GetComponent<Controlador>().combate = true;
        vida= Save.GetComponent<Controlador>().vidaJugador;
        this.stats = new Stats(21, vida, 50, 45, 20);
    }
 

    public void VidaActual()
    {
        vida = stats.Hp;
        vidaMax = stats.maxHp;
    }
    public override void InitTurn()
    {

        this.skillPanel.MostrarJugador(this);
        

        for (int i = 0; i < this.skills.Length; i++)
        {
            this.skillPanel.ConfigurarBotones(i, this.skills[i].skillName);
        }
    }

    public void EjecturSkill(int index)
    {
        this.skillToBeExecuted=this.skills[index];

        if(this.skillToBeExecuted.selfInflicted)
        {
            this.skillToBeExecuted.TomarEmisorAndReceptor(this, this);
            this.combatManager.OnFigtherSkill(this.skillToBeExecuted);
            this.skillPanel.Ocultar();
        }
        else
        {
            Fighter[] enemies = this.combatManager.GetOpposingTeam();
            Debug.Log(enemies[0].name);
            this.enemiesPanel.Show(this,enemies);
        }
        
        
    }
    public void SetTargetAndAttack(Fighter enemyFigther)
    {
        this.skillToBeExecuted.TomarEmisorAndReceptor(this, enemyFigther);
        this.combatManager.OnFigtherSkill(this.skillToBeExecuted);

        this.skillPanel.Ocultar();
        this.enemiesPanel.Hide();
    }

}
