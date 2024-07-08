using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualizarEscenaBatalla : MonoBehaviour
{
    public GameObject controlador, jugador;

    // Start is called before the first frame update
    void Start()
    {
        controlador = GameObject.FindGameObjectWithTag("Save");
        jugador = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        var chequeo = controlador.GetComponent<Controlador>();
        if (chequeo.activar == 0)
        {
            chequeo.jugador = jugador;
        }
    }
}
