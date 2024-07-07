using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Controlador : MonoBehaviour
{
    public GameObject jugador;
    public string archivoDeGuardado;
    public Datos datos=new Datos();
    private void Awake()
    {
        archivoDeGuardado = Application.dataPath + "/Scripts/SaveSystem/datosJuego.json";

        jugador = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) 
        {
            GuardarDatos();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            CargarDatos();
        }
    }
    private void CargarDatos()
    {
        if (File.Exists(archivoDeGuardado)) 
        {
            var posJugador = jugador.GetComponent<CharacterController>();
            string contenido=File.ReadAllText(archivoDeGuardado);
            datos=JsonUtility.FromJson<Datos>(contenido);
            Debug.Log("Posicion Jugador: " + datos.posicion);
            posJugador.enabled = false;
            posJugador.transform.position = datos.posicion;
            posJugador.enabled = true;


        }
        else
        {
            Debug.Log("El archivo no Existe");
        }
    }

    private void GuardarDatos()
    {
        Datos nuevosDatos = new Datos()
        {
            posicion=jugador.transform.position
        };

        string cadenaJSON=JsonUtility.ToJson(nuevosDatos);

        File.WriteAllText(archivoDeGuardado, cadenaJSON);

        Debug.Log("Archivo Guardado");
        
    }
}
