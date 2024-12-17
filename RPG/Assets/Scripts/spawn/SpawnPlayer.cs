using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject jugador;
    // Start is called before the first frame update
    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player");
        var posJugador = jugador.GetComponent<CharacterController>();
        posJugador.enabled = false;
        posJugador.transform.position = transform.position+new Vector3(0,1.55f,0);
        posJugador.enabled = true;
    }

}
