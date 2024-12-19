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

    // M�todo para activar la animaci�n de da�o
    public void ActivarAnimacionDa�o()
    {
        animEnemigo.SetBool("quemado", true);
    }

    // M�todo para detener la animaci�n de da�o
    public void DetenerAnimacionDa�o()
    {
        animEnemigo.SetBool("quemado", false);
    }
}
