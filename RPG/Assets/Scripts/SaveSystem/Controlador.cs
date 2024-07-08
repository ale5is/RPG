using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Controlador : MonoBehaviour
{
    public GameObject jugador;
    public int vidaJugador;
    public int vidaMaxJugador;
    public string archivoDeGuardado;
    public Datos datos=new Datos();
    public Datos nuevosDatos = new Datos();
    public int activar=0;
    public bool combate=false;
    public static Controlador Instance;
    private void Awake()
    {
        archivoDeGuardado = Application.dataPath + "/Scripts/SaveSystem/datosJuego.json";

        jugador = GameObject.FindGameObjectWithTag("Player");
        if (Controlador.Instance == null) 
        {
            Controlador.Instance = this;
            DontDestroyOnLoad(this.gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    private void Update()
    {
        
        if (activar==1)
        {
            activar = 0;
            GuardarDatos();
        }
        
        if (Input.GetKeyDown(KeyCode.G)) 
        {
            GuardarDatos();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            CargarDatos();
        }
    }
    public void CargarDatos()
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
            jugador.GetComponent<vida>().vidaActual = datos.vida;


        }
        else
        {
            Debug.Log("El archivo no Existe");
        }
    }

    public void GuardarDatos()
    {
        if (combate) 
        {
            nuevosDatos.vida = (int)jugador.GetComponent<PlayerFigther>().vida;
        }
        else 
        {
            nuevosDatos.vida = jugador.GetComponent<vida>().vidaActual;
            nuevosDatos.posicion = jugador.transform.position;
        }

        
        
        vidaJugador = nuevosDatos.vida;
        string cadenaJSON=JsonUtility.ToJson(nuevosDatos);

        File.WriteAllText(archivoDeGuardado, cadenaJSON);

        Debug.Log("Archivo Guardado");
        
    }
}
