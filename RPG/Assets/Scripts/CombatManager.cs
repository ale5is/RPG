using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CombatStatus
{
    WAITING_FOR_FIGTHER,
    FIGTHER_ACTION,
    CHECK_FOR_VICTORY,
    NEXT_TURN
}
public class CombatManager : MonoBehaviour
{
    public Fighter[] fighters;
    private int fighterIndex;

    private bool isCombatActive;

    private CombatStatus combatStatus;
    private Skill currentFigtherSkill;

    private bool victoria=false;

    public static Controller_Player player;

    public GameObject controlador;
    private void Awake()
    {
        controlador = GameObject.FindGameObjectWithTag("Save");
    }
    void Start()
    {
        
        LogPanel.write("Batalla Iniciada");

        foreach (var fgtr in fighters)
        {
            fgtr.combatManager = this;
        }
        this.combatStatus = CombatStatus.NEXT_TURN;

        this.fighterIndex = -1;
        this.isCombatActive = true;
        StartCoroutine(this.CombatLoop());
        player = FindAnyObjectByType<Controller_Player>();
    }
    private void Update()
    {
        if (victoria)
        {
            controlador.GetComponent<Controlador>().combate = true;
            controlador.GetComponent<Controlador>().GuardarDatos();
            controlador.GetComponent<Controlador>().combate = false;
            SceneManager.LoadScene(0);
            controlador.GetComponent<Controlador>().activar = 2;

        }
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
                this.combatStatus = CombatStatus.CHECK_FOR_VICTORY;
                currentFigtherSkill = null;
            }
            else if (this.combatStatus == CombatStatus.CHECK_FOR_VICTORY)
            {
                foreach (var fgtr in fighters)
                {
                    if (fgtr.isAlive == false)
                    {
                        
                        Debug.Log(fgtr.id);
                        if(fgtr.id == 1)
                        {
                            
                            LogPanel.write("Victoria");
                            fighters[0].GetComponent<PlayerFigther>().VidaActual();
                        }
                        else
                        {
                            LogPanel.write("Derrota");
                        }

                        
                        this.isCombatActive = false;                       
                        victoria = true;
                    }
                    else
                    {
                        this.combatStatus = CombatStatus.NEXT_TURN;
                    }
                }

                yield return null;
            }
            else if (this.combatStatus == CombatStatus.NEXT_TURN)
            {
                yield return new WaitForSeconds(1f);
                this.fighterIndex = (this.fighterIndex + 1) % this.fighters.Length;
                var currentTurn = this.fighters[this.fighterIndex];
                LogPanel.write($"{currentTurn.name} tiene el turno.");
                
                currentTurn.InitTurn();
                
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

    public void OnFigtherSkill(Skill skill)
    {
        this.currentFigtherSkill= skill;
        this.combatStatus = CombatStatus.FIGTHER_ACTION;
    }

    
}
