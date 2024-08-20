using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModificarEstadoSkill : Skill
{
    [Header("Modificador de estados")]
    public string message;

    protected ModificarEstado mod;

    protected override void OnRun()
    {
        if(this.mod == null)
        {
            this.mod = this.GetComponent<ModificarEstado>();
        }
        this.messages.Enqueue(this.message.Replace("{receiver}", this.receptor.nombre));
        this.receptor.modificarEstados.Add(this.mod); 
    }
}
