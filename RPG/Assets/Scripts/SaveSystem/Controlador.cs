using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Controlador : MonoBehaviour
{
    public GameObject jugador,jugadorBatalla1,jugadorBatalla2;
    public int vidaJugador1,vidaJugador2;
    public int vidaMaxJugador1,vidaMaxJugador2;
    public string archivoDeGuardado;
    public Datos datos=new Datos();
    public Datos nuevosDatos = new Datos();
    public int activar=0;
    public int combate=0;
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

        if (combate==1)
        {
            jugadorBatalla1 = GameObject.FindGameObjectWithTag("Batalla");
            jugadorBatalla2 = GameObject.FindGameObjectWithTag("Player2");
            jugadorBatalla1.GetComponent<PlayerFigther>().nivel = nuevosDatos.nivelJ1;
            jugadorBatalla2.GetComponent<PlayerFigther>().nivel = nuevosDatos.nivelJ2;
            //jugadorBatalla1.GetComponent<vida>().vidaActualJ1 = vidaJugador1;


            combate = 2;
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
            jugador.GetComponent<vida>().vidaActualJ1 = datos.vidaJ1;
            jugador.GetComponent<vida>().vidaActualJ2 = datos.vidaJ2;
            jugador.GetComponent<vida>().nivelJ1 = datos.nivelJ1;
            jugador.GetComponent<vida>().nivelJ2 = datos.nivelJ2;
            jugador.GetComponent<vida>().actualizar=true;


        }
        else
        {
            Debug.Log("El archivo no Existe");
        }
    }

    public void GuardarDatos()
    {
         if (combate == 0)
        {
            nuevosDatos.vidaJ1 = jugador.GetComponent<vida>().vidaActualJ1;
            
            nuevosDatos.vidaJ2 = jugador.GetComponent<vida>().vidaActualJ2;
            vidaMaxJugador1 = jugador.GetComponent<vida>().vidaMaxJ1;
            vidaMaxJugador2 = jugador.GetComponent<vida>().vidaMaxJ2;
            nuevosDatos.posicion = jugador.transform.position;
            nuevosDatos.nivelJ1= jugador.GetComponent<vida>().nivelJ1;
            nuevosDatos.nivelJ2 = jugador.GetComponent<vida>().nivelJ2;
        }

        else if (combate==2) 
        {
            
            
            nuevosDatos.vidaJ1 = (int)jugadorBatalla1.GetComponent<PlayerFigther>().vidaAct();
            nuevosDatos.vidaJ2 = (int)jugadorBatalla2.GetComponent<PlayerFigther>().vidaAct();
            nuevosDatos.nivelJ1 = jugadorBatalla1.GetComponent<PlayerFigther>().nivel;
            nuevosDatos.nivelJ2 = jugadorBatalla2.GetComponent<PlayerFigther>().nivel;
            combate = 0;
        }


        vidaJugador1 = nuevosDatos.vidaJ1;
        
        vidaJugador2 = nuevosDatos.vidaJ2;

        string cadenaJSON=JsonUtility.ToJson(nuevosDatos);

        File.WriteAllText(archivoDeGuardado, cadenaJSON);

        Debug.Log("Archivo Guardado");
        
    }
}
