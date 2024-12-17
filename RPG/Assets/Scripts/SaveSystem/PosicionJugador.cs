using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosicionJugador : MonoBehaviour
{
    public GameObject jugador;
    public GameObject save;
    // Start is called before the first frame update
    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player");
        save= GameObject.FindGameObjectWithTag("Save");
        var posJugador = jugador.GetComponent<CharacterController>();
        posJugador.enabled = false;
        posJugador.transform.position = transform.position;
        posJugador.enabled = true;
    }
}
