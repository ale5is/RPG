using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusPanel : MonoBehaviour
{
    public TMP_Text nombreLabel,nivelLabel,HpLabel;
    public Slider hpSlider;
    public Image hpSliderBar;
   

    public void setStats(string nombre, Stats stats)
    {
        this.nombreLabel.text = nombre;
        this.nivelLabel.text = $"N. {stats.nivel}";
        this.SetHP(stats.Hp, stats.maxHp);
    }
    public void SetHP(float Hp, float maxHp)
    {
        this.HpLabel.text=$"{Mathf.RoundToInt(Hp)}/{Mathf.RoundToInt(maxHp)}";
        
        float porcentaje=Hp/maxHp;

        this.hpSlider.value = porcentaje;

        if (porcentaje < 0.33f)
        {
            this.hpSliderBar.color = Color.red;
        }
    }
}
