using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CombatStatus
{
    WAITING_FOR_FIGTHER,
    FIGTHER_ACTION,
    CHECK_ACTION_MESSAGES,
    CHECK_FOR_VICTORY,
    NEXT_TURN
}
public class CombatManager : MonoBehaviour
{
    public Fighter[] PlayerTeam;
    public Fighter[] EnemyTeam;

    private Fighter[] fighters;

    private int fighterIndex;

    private bool isCombatActive;

    private CombatStatus combatStatus;
    private Skill currentFigtherSkill;

    private bool victoria=false;

    public static Controller_Player player;
    public GameObject jugador, save;

    //public GameObject controlador;
    private void Awake()
    {
        //jugador = GameObject.FindGameObjectWithTag("Player");
        //PlayerTeam[0] = jugador.GetComponent<PlayerFigther>();


    }
    void Start()
    {
        
        LogPanel.write("Batalla Iniciada");

        this.fighters = new Fighter[]
        {
            
            this.PlayerTeam[0],this.PlayerTeam[1],
            this.EnemyTeam[0],this.EnemyTeam[1],
        };
        
        

        foreach (var fgtr in fighters)
        {
            fgtr.combatManager = this;
        }
        this.combatStatus = CombatStatus.NEXT_TURN;

        this.fighterIndex = -1;
        this.isCombatActive = true;
        StartCoroutine(this.CombatLoop());
        player = FindAnyObjectByType<Controller_Player>();
        save = GameObject.FindGameObjectWithTag("Save");
        
    }
    private void Update()
    {
        
            //controlador.GetComponent<Controlador>().combate = true;
            //controlador.GetComponent<Controlador>().GuardarDatos();
            //controlador.GetComponent<Controlador>().combate = false;
            
            //controlador.GetComponent<Controlador>().activar = 2;

       
    }

    IEnumerator CombatLoop()
    {
        while (this.isCombatActive)
        {
            if(this.combatStatus==CombatStatus.WAITING_FOR_FIGTHER)
            { 
                yield return null;
            }
            else if (this.combatStatus == CombatStatus.FIGTHER_ACTION)
            {
                LogPanel.write($"{this.fighters[this.fighterIndex].name} uso {currentFigtherSkill.name}.");

                yield return null;
                //Ejecuta la skill del jugador
                currentFigtherSkill.Run();

                //Espera la animacion de la skill
                yield return new WaitForSeconds(currentFigtherSkill.animationDuration);
                this.combatStatus = CombatStatus.CHECK_ACTION_MESSAGES;
                
            }
            else if(this.combatStatus == CombatStatus.CHECK_ACTION_MESSAGES)
            {
                string nextMessage = this.currentFigtherSkill.TomarSiguienteMensaje();
                if(nextMessage != null)
                {
                    LogPanel.write(nextMessage);
                    yield return new WaitForSeconds(2f);
                }
                else
                {
                    this.currentFigtherSkill= null;
                    this.combatStatus = CombatStatus.CHECK_FOR_VICTORY;
                    yield return null;
                }
            }
            else if (this.combatStatus == CombatStatus.CHECK_FOR_VICTORY)
            {
                bool arePlayerAlive = false;
                foreach(var figther in this.PlayerTeam)
                {
                    arePlayerAlive |= figther.isAlive;
                }

                bool areEnemyAlive = false;
                foreach (var figther in this.EnemyTeam)
                {
                    areEnemyAlive |= figther.isAlive;
                }
                
                bool victory = areEnemyAlive == false;
                bool defeat=arePlayerAlive== false;
                if (victory)
                {
                    LogPanel.write("Victoria");
                    this.isCombatActive = false;
                    save.GetComponent<Controlador>().GuardarDatos();
                    SceneManager.LoadScene(0);
                    save.GetComponent<Controlador>().CargarDatos();

                }
                if (defeat)
                {
                    LogPanel.write("Derrota");
                    this.isCombatActive = false;
                    save.GetComponent<Controlador>().GuardarDatos();
                    SceneManager.LoadScene(0);
                    save.GetComponent<Controlador>().CargarDatos();
                }

                if(this.isCombatActive)
                {
                    this.combatStatus = CombatStatus.NEXT_TURN;
                }

                yield return null;
            }
            else if (this.combatStatus == CombatStatus.NEXT_TURN)
            {
                yield return new WaitForSeconds(1f);

                
                Fighter current = null;

                do
                {
                    this.fighterIndex = (this.fighterIndex + 1) % this.fighters.Length;

                    current = this.fighters[this.fighterIndex];
                }
                while(current.isAlive==false);
                LogPanel.write($"{current.name} tiene el turno.");
                
                current.InitTurn();
                
                this.combatStatus = CombatStatus.WAITING_FOR_FIGTHER;
            }
        }
    }

    public Fighter GetOpposingFigther()
    {
        if (this.fighterIndex == 0)
        {
            return this.fighters[1];
        }
        else
        {
            return this.fighters[0];
        }
    }
    public Fighter[] GetOpposingTeam()
    {
        if (this.fighterIndex == 0 || this.fighterIndex == 1)
        {
            return this.EnemyTeam;
        }
        else
        {
            return this.PlayerTeam;
        }
    }

    public void OnFigtherSkill(Skill skill)
    {
        this.currentFigtherSkill= skill;
        this.combatStatus = CombatStatus.FIGTHER_ACTION;
    }

    
}
