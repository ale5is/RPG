using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualizarEscena : MonoBehaviour
{
    public GameObject controlador,jugador;
    private void Awake()
    {
        controlador = GameObject.FindGameObjectWithTag("Save");
        jugador = GameObject.FindGameObjectWithTag("Player");
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        var chequeo = controlador.GetComponent<Controlador>();
        var save= jugador.GetComponent<Controller_Player>();
        
        if (chequeo.activar == 2||chequeo.activar==4)
        {

            chequeo.activar = 0;
            chequeo.jugador= jugador;
            chequeo.CargarDatos();

        }
    }
}
