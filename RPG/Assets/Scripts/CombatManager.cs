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

    private SaveSystem saveSystem;
    public static Controller_Player player;

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
        saveSystem = FindObjectOfType<SaveSystem>();
        player = FindAnyObjectByType<Controller_Player>();
    }
    private void Update()
    {
        if (victoria)
        {
            SceneManager.LoadScene(0);
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
                        this.isCombatActive = false;

                        LogPanel.write("Victoria");
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