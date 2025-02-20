using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    public Animator animEnemigo; // Referencia al Animator del enemigo

    void Start()
    {
        if (animEnemigo == null)
        {
            animEnemigo = GetComponent<Animator>();
        }
 
    }

    // Método para activar la animación de daño
    public void ActivarAnimacionDano()
    {
        animEnemigo.SetBool("quemado", true);
    }

    // Método para detener la animación de daño
    public void DetenerAnimacionDano()
    {
        animEnemigo.SetBool("quemado", false);
    }
}
