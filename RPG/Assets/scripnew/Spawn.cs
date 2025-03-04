using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(TeletransportarPlayer());
    }

    IEnumerator TeletransportarPlayer()
    {
        GameObject player = null;

        // Esperar hasta que el Player esté en la escena
        while (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            yield return null; // Espera un frame antes de volver a buscar
        }

        // Mover al Player al punto de Spawn
        player.transform.position = transform.position;
    }

}
