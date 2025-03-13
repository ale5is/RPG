using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ComportamientoEnemigo : MonoBehaviour
{
    public Transform[] puntosDePatrulla;
    public float rangoDeDeteccion = 8f;
    public float rangoDeAtaque = 2f;
    public float tiempoEntreAtaques = 1.5f;
    public float tiempoDeEspera = 2f;
    public int danoDeAtaque = 10;

    public float velocidadDePatrulla = 2f;
    public float velocidadDePersecucion = 5f;
    public float velocidadDeAtaque = 0f;

    private int puntoActual = 0;
    private Transform jugador;
    private NavMeshAgent agente;
    private Animator anim;
    private float tiempoUltimoAtaque;
    private bool persiguiendo = false;
    private bool atacando = false;
    private bool esperando = false;

    private void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        agente = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        if (puntosDePatrulla.Length > 0)
        {
            agente.SetDestination(puntosDePatrulla[puntoActual].position);
        }
    }

    private void Update()
    {
        float distanciaAlJugador = Vector3.Distance(transform.position, jugador.position);

        if (distanciaAlJugador <= rangoDeDeteccion && !atacando)
        {
            persiguiendo = true;
        }
        else if (distanciaAlJugador > rangoDeDeteccion)
        {
            persiguiendo = false;
            atacando = false;
        }

        if (persiguiendo && distanciaAlJugador > rangoDeAtaque)
        {
            PerseguirJugador();
        }
        else if (distanciaAlJugador <= rangoDeAtaque && !atacando)
        {
            StartCoroutine(AtacarJugador());
        }
        else
        {
            if (!esperando && !atacando)
            {
                Patrullar();
            }
        }

        anim.SetFloat("Velocidad", agente.velocity.magnitude);
    }

    private void Patrullar()
    {
        if (!persiguiendo && !agente.pathPending && agente.remainingDistance < 0.5f)
        {
            if (Random.value < 0.3f)
            {
                StartCoroutine(EsperarAntesDeMoverse());
            }
            else
            {
                puntoActual = (puntoActual + 1) % puntosDePatrulla.Length;
                agente.SetDestination(puntosDePatrulla[puntoActual].position);
            }
        }
        agente.speed = velocidadDePatrulla;
    }

    private IEnumerator EsperarAntesDeMoverse()
    {
        esperando = true;
        anim.SetBool("Quieto", true);
        agente.isStopped = true;
        yield return new WaitForSeconds(tiempoDeEspera);
        agente.isStopped = false;
        anim.SetBool("Quieto", false);
        esperando = false;
        puntoActual = (puntoActual + 1) % puntosDePatrulla.Length;
        agente.SetDestination(puntosDePatrulla[puntoActual].position);
    }

    private void PerseguirJugador()
    {
        if (!atacando)
        {
            agente.isStopped = false;
            agente.SetDestination(jugador.position);
            agente.speed = velocidadDePersecucion;
        }
    }

    private IEnumerator AtacarJugador()
    {
        if (atacando) yield break; // Evita múltiples ataques al mismo tiempo

        atacando = true;
        agente.isStopped = true;
        agente.velocity = Vector3.zero;
        agente.speed = 0f; // Detiene el movimiento

        // Obtener dirección hacia el jugador sin modificar la altura (eje Y)
        Vector3 direccionHaciaJugador = (jugador.position - transform.position).normalized;
        direccionHaciaJugador.y = 0; // Evita inclinaciones en X y Z

        // Rotación gradual hacia el jugador
        float duracionRotacion = 0.5f; // Tiempo en segundos para girar
        float tiempoInicio = Time.time;
        Quaternion rotacionInicial = transform.rotation;
        Quaternion rotacionFinal = Quaternion.LookRotation(direccionHaciaJugador); // Solo gira en Y

        while (Time.time < tiempoInicio + duracionRotacion)
        {
            transform.rotation = Quaternion.Slerp(rotacionInicial, rotacionFinal, (Time.time - tiempoInicio) / duracionRotacion);
            yield return null; // Espera al siguiente frame
        }

        anim.SetBool("Atacar", true); // Inicia animación de ataque

        yield return new WaitForSeconds(1f); // Ajusta al tiempo de animación de ataque

        if (Vector3.Distance(transform.position, jugador.position) <= rangoDeAtaque)
        {
            jugador.GetComponent<VidaJugador>().RecibirDanio(danoDeAtaque);
        }

        StartCoroutine(EsperarAntesDeMoverse());
        anim.SetBool("Atacar", false); // Finaliza animación de ataque
        yield return new WaitForSeconds(tiempoEntreAtaques);

        // Reactivar el agente después de atacar
        agente.isStopped = false;
        atacando = false;
    }


}
