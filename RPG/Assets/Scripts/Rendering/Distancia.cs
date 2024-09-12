using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Distancia : MonoBehaviour
{
    public Transform player;
    public float distancia=50f;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        foreach (GameObject obj in FindObjectsOfType<GameObject>().Where(x => x.tag!= "Limite"))
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                float distanciaDelJugador=Vector3.Distance(obj.transform.position, player.position);

                if (distanciaDelJugador > distancia)
                {
                    renderer.enabled = false;
                }
                else
                {
                    renderer.enabled = true;
                }
            }

           

        }
    }
}
