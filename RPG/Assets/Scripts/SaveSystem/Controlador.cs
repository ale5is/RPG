using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Controlador : MonoBehaviour
{
    // Referencias a objetos del jugador y de combate
    public GameObject jugador, jugadorBatalla1, jugadorBatalla2;

    // Variables de vida y control
    public int vidaJugador1, vidaJugador2;
    public int vidaMaxJugador1, vidaMaxJugador2;

    // Sistema de guardado
    public string archivoDeGuardado;
    public Datos datos = new Datos();
    public Datos nuevosDatos = new Datos();

    // Variables de control
    public int activar = 0;
    public int combate = 0;

    // Instancia única del controlador (Singleton)
    public static Controlador Instance;

    private void Awake()
    {
        // Ruta para guardar el archivo JSON
        archivoDeGuardado = Application.dataPath + "/Scripts/SaveSystem/datosJuego.json";

        // Encontrar al jugador en la escena
        jugador = GameObject.FindGameObjectWithTag("Player");

        // Implementar patrón Singleton
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
        // Verificar si se activa manualmente el guardado
        if (activar == 1)
        {
            activar = 0;
            GuardarDatos();
        }

        // Guardar datos con la tecla "G"
        if (Input.GetKeyDown(KeyCode.G))
        {
            GuardarDatos();
        }

        // Cargar datos con la tecla "C"
        if (Input.GetKeyDown(KeyCode.C))
        {
            CargarDatos();
        }

        // Actualizar datos durante el combate
        if (combate == 1)
        {
            jugadorBatalla1 = GameObject.FindGameObjectWithTag("Batalla");
            jugadorBatalla2 = GameObject.FindGameObjectWithTag("Player2");

            //jugadorBatalla1.GetComponent<PlayerFigther>().nivel = nuevosDatos.nivelJ1;
            //jugadorBatalla2.GetComponent<PlayerFigther>().nivel = nuevosDatos.nivelJ2; 
            //jugadorBatalla2.GetComponent<PlayerFigther>().experienciaRequerida = nuevosDatos.xpRequerido;
            jugadorBatalla1.GetComponent<vida>().nivelJ1 = nuevosDatos.nivelJ1;
            jugadorBatalla2.GetComponent<vida>().nivelJ2 = nuevosDatos.nivelJ2;


            combate = 2; // Cambiar el estado del combate
        }
    }

    public void CargarDatos()
    {
        // Verificar si existe un archivo de guardado
        if (File.Exists(archivoDeGuardado))
        {
            var posJugador = jugador.GetComponent<CharacterController>();
            string contenido = File.ReadAllText(archivoDeGuardado);

            // Leer los datos desde el archivo JSON
            datos = JsonUtility.FromJson<Datos>(contenido);

            Debug.Log("Posición del Jugador: " + datos.posicion);

            // Actualizar posición del jugador
            posJugador.enabled = false;
            posJugador.transform.position = datos.posicion;
            posJugador.enabled = true;

            // Actualizar estadísticas del jugador
            var vidaJugador = jugador.GetComponent<vida>();
            vidaJugador.vidaActualJ1 = datos.vidaJ1;
            vidaJugador.vidaActualJ2 = datos.vidaJ2;
            vidaJugador.nivelJ1 = datos.nivelJ1;
            vidaJugador.nivelJ2 = datos.nivelJ2;
            vidaJugador.actualizar = true;
        }
        else
        {
            Debug.Log("El archivo de guardado no existe.");
        }
    }

    public void GuardarDatos()
    {
        if (combate == 0) // Guardar fuera de combate
        {
            var vidaJugador = jugador.GetComponent<vida>();

            nuevosDatos.vidaJ1 = vidaJugador.vidaActualJ1;
            nuevosDatos.vidaJ2 = vidaJugador.vidaActualJ2;
            vidaMaxJugador1 = vidaJugador.vidaMaxJ1;
            vidaMaxJugador2 = vidaJugador.vidaMaxJ2;

            nuevosDatos.posicion = jugador.transform.position;
            nuevosDatos.nivelJ1 = vidaJugador.nivelJ1;
            nuevosDatos.nivelJ2 = vidaJugador.nivelJ2;
        }
        else if (combate == 2) // Guardar durante el combate
        {
            nuevosDatos.vidaJ1 = (int)jugadorBatalla1.GetComponent<PlayerFigther>().vidaAct();
            nuevosDatos.vidaJ2 = (int)jugadorBatalla2.GetComponent<PlayerFigther>().vidaAct();

            nuevosDatos.nivelJ1 = jugadorBatalla1.GetComponent<PlayerFigther>().nivel;
            nuevosDatos.nivelJ2 = jugadorBatalla2.GetComponent<PlayerFigther>().nivel;

            combate = 0; // Reiniciar estado del combate
        }

        // Actualizar variables globales
        vidaJugador1 = nuevosDatos.vidaJ1;
        vidaJugador2 = nuevosDatos.vidaJ2;

        // Guardar en archivo JSON
        string cadenaJSON = JsonUtility.ToJson(nuevosDatos);
        File.WriteAllText(archivoDeGuardado, cadenaJSON);

        Debug.Log("Archivo guardado correctamente.");
    }
}
