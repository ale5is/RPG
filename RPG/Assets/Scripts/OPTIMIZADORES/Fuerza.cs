using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuerza : MonoBehaviour
{
    public float minForce = 5f; // Fuerza mnima a aplicar
    public float maxForce = 15f; // Fuerza mxima a aplicar

    private void OnCollisionEnter(Collision collision)
    {
        // Verificar si el objeto con el que colisionamos tiene la etiqueta "Objeto"
        if (collision.gameObject.CompareTag("Objeto"))
        {
            // Obtener el componente Rigidbody del objeto
            Rigidbody rb = GetComponent<Rigidbody>();

            // Verificar si el objeto tiene un Rigidbody
            if (rb != null)
            {
                // Generar una direccin aleatoria
                Vector3 randomDirection = Random.onUnitSphere;

                // Aplicar fuerza aleatoria en la direccin aleatoria generada
                float randomForce = Random.Range(minForce, maxForce);
                rb.AddForce(randomDirection * randomForce, ForceMode.Impulse);
            }
        }
    }

}
