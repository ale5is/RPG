using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElegirEscena : MonoBehaviour
{
    public int numeroAsignado;
   

    private void Start()
    {
        
    }
    public void SetNumero(int numero)
    {
        numeroAsignado = numero;
        
        Debug.Log($"{gameObject.name} tiene el número: {numeroAsignado}");
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(numeroAsignado);
            
        }
    }
}
