using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSkillPanel : MonoBehaviour
{
    public GameObject [] skillBotones;
    public TMP_Text[] skillBotonesLabel;

    private PlayerFigther targetFigther;

    void Awake()
    {
        foreach (var btn in this.skillBotones)
        {
            btn.SetActive(false);
        }
        this.Ocultar();
        
    }

    public void ConfigurarBotones(int index,string skillNombre)
    {
        this.skillBotones[index].SetActive(true);
        this.skillBotonesLabel[index].text = skillNombre;
    }
    public void OnSkillButtonClick(int index)
    {
        this.targetFigther.EjecturSkill(index);
    }
    public void MostrarJugador(PlayerFigther newTarget)
    {
        
        this.gameObject.SetActive(true);
        this.targetFigther=newTarget;
    }

    public void Ocultar()
    {
        this.gameObject.SetActive(false);
        foreach(var btn in this.skillBotones)
        {
            btn.SetActive(false);
        }
    }
}
