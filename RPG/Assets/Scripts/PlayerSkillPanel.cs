using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSkillPanel : MonoBehaviour
{
    public GameObject [] skillBotones;
    public TMP_Text[] skillBotonesLabel;

    void Awake()
    {
        this.Ocultar();
        foreach(var btn in this.skillBotones)
        {
            btn.SetActive(false);
        }
    }

    public void ConfigurarBotones(int index,string skillNombre)
    {
        this.skillBotones[index].SetActive(true);
        this.skillBotonesLabel[index].text = skillNombre;
    }

    public void Mostrar()
    {
        this.gameObject.SetActive(true);
    }

    public void Ocultar()
    {
        this.gameObject.SetActive(false);
    }
}
