using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim : MonoBehaviour
{
    public Animator animBatalla;
    public EnemyAnim enemy1,enemy2;
    public int id;
    private string animacionSeleccionada = ""; // Almacena la animación seleccionada

    void Start()
    {
        animBatalla = GetComponent<Animator>();
    }

    // Método para seleccionar la animación de ataque
    public void SeleccionarAtacar()
    {
        animacionSeleccionada = "atacar";
    }

    // Método para seleccionar la animación de magia
    public void SeleccionarMagia()
    {
        animacionSeleccionada = "magia";
        
    }

    // Método para iniciar la animación seleccionada
    public void IniciarAnimacion()
    {
        
        if (!string.IsNullOrEmpty(animacionSeleccionada))
        {
            
            ResetAnimaciones(); // Asegurarse de que otras animaciones estén desactivadas
            animBatalla.SetBool(animacionSeleccionada, true);

        }
        if (id == 1)
        {
            enemy1.ActivarAnimacionDaño();
        }
        if (id == 2)
        {
            enemy2.ActivarAnimacionDaño();
        }


    }

    // Método para detener todas las animaciones
    public void DetenerAnimaciones()
    {
        ResetAnimaciones();
        animacionSeleccionada = ""; // Reiniciar la selección
        
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
