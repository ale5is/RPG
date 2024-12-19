using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim : MonoBehaviour
{
    public Animator animBatalla;
    public EnemyAnim enemy1,enemy2;
    public int id;
    private string animacionSeleccionada = ""; // Almacena la animaci�n seleccionada

    void Start()
    {
        animBatalla = GetComponent<Animator>();
    }

    // M�todo para seleccionar la animaci�n de ataque
    public void SeleccionarAtacar()
    {
        animacionSeleccionada = "atacar";
    }

    // M�todo para seleccionar la animaci�n de magia
    public void SeleccionarMagia()
    {
        animacionSeleccionada = "magia";
        
    }

    // M�todo para iniciar la animaci�n seleccionada
    public void IniciarAnimacion()
    {
        
        if (!string.IsNullOrEmpty(animacionSeleccionada))
        {
            
            ResetAnimaciones(); // Asegurarse de que otras animaciones est�n desactivadas
            animBatalla.SetBool(animacionSeleccionada, true);

        }
        if (id == 1)
        {
            enemy1.ActivarAnimacionDa�o();
        }
        if (id == 2)
        {
            enemy2.ActivarAnimacionDa�o();
        }


    }

    // M�todo para detener todas las animaciones
    public void DetenerAnimaciones()
    {
        ResetAnimaciones();
        animacionSeleccionada = ""; // Reiniciar la selecci�n
        
    }

    // Reinicia todas las animaciones
    private void ResetAnimaciones()
    {
        
        animBatalla.SetBool("atacar", false);
        animBatalla.SetBool("magia", false);
    }

    public void idEnemy1()
    {
        id = 1;
    }
    public void idEnemy2()
    {
        id = 2;
    }

}
