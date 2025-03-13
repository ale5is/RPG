using System.Collections;
using TMPro;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public float velocidad = 5, velocidadInicial, velocidadMax;
    private CharacterController controller;
    public new Transform camera;
    public float gravedad;
    private Vector3 direccion;
    public static MovimientoJugador Instance;
    public int numEscena;
    public GameObject controlador;
    public CanvasController canvasController;
    public Animator anim;
    public TMP_Text vida, nivel;
    public vida vidaJ1;

    public int vidaMaxima = 100;
    private int vidaActual;
    private bool puedeMoverse = true;
    private bool puedeAtacar = true;

    public float impulsoAtaque = 3f; // Fuerza del impulso hacia adelante
    private bool estaAtacando = false; // Evita cualquier otro movimiento durante el ataque

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        if (MovimientoJugador.Instance == null)
        {
            MovimientoJugador.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        velocidadInicial = velocidad;
        velocidadMax = velocidad + 5;
        vidaActual = vidaMaxima;
        ActualizarUI();
    }

    void Update()
    {
        if (puedeMoverse && !estaAtacando) // Bloqueamos movimiento si está atacando
        {
            Movement();
        }

        if (Input.GetButtonDown("Fire1") && puedeAtacar)
        {
            StartCoroutine(Atacar());
        }

        // 🔥 Enviar la velocidad al Animator
        anim.SetFloat("velocidad", (!estaAtacando && puedeMoverse) ? controller.velocity.magnitude : 0);
    }

    private void Movement()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 movimiento = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            velocidad = velocidadMax;
        }
        else
        {
            velocidad = velocidadInicial;
        }

        if (hor != 0 || ver != 0)
        {
            Vector3 forward = camera.forward;
            forward.y = 0;
            forward.Normalize();

            Vector3 right = camera.right;
            right.y = 0;
            right.Normalize();

            Vector3 direccion = forward * ver + right * hor;
            direccion.Normalize();
            movimiento = direccion * velocidad * Time.deltaTime;

            // 🔄 Aplicamos la rotación en lugar de cambiar el scale
            Vector3 direccionMovimiento = new Vector3(movimiento.x, 0, movimiento.z);
            if (direccionMovimiento != Vector3.zero)
            {
                Quaternion nuevaRotacion = Quaternion.LookRotation(direccionMovimiento);
                transform.rotation = Quaternion.Slerp(transform.rotation, nuevaRotacion, Time.deltaTime * 10f);
            }
        }

        movimiento.y += gravedad * Time.deltaTime;
        controller.Move(movimiento);
    }

    IEnumerator Atacar()
    {
        if (estaAtacando) yield break; // Evita que se active si ya está atacando

        puedeAtacar = false;
        puedeMoverse = false;
        estaAtacando = true;
        anim.SetBool("Atacando", true); // 🔄 Activamos el bool en el Animator

        // Guardamos la velocidad antes del ataque para restaurarla después
        float velocidadGuardada = velocidad;
        velocidad = 0; // 🔴 Bloqueamos el movimiento

        yield return null; // Esperamos un frame para actualizar la animación

        // 💥 Impulso hacia adelante al inicio del ataque
        Vector3 impulso = transform.forward * impulsoAtaque;
        float tiempoImpulso = 0.3f; // Aplica impulso solo al inicio

        float tiempoPasado = 0;
        while (tiempoPasado < tiempoImpulso)
        {
            controller.Move(impulso * Time.deltaTime);
            tiempoPasado += Time.deltaTime;
            yield return null;
        }

        // Esperar el resto de la animación sin mover al personaje
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length - tiempoImpulso);

        // Restauramos la velocidad y el control del jugador
        velocidad = velocidadGuardada;
        puedeMoverse = true;
        puedeAtacar = true;
        estaAtacando = false;
        anim.SetBool("Atacando", false); // 🔄 Desactivamos el bool
    }




    public void RecibirDanio(int cantidad)
    {
        vidaActual -= cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);
        anim.SetTrigger("RecibirDanio"); // Asegúrate de tener un trigger "RecibirDanio" en el Animator

        if (vidaActual <= 0)
        {
            Morir();
        }

        ActualizarUI();
    }

    private void Morir()
    {
        Debug.Log("Jugador ha muerto");
        anim.SetTrigger("Morir"); // Debes agregar una animación de muerte en el Animator
        // Aquí podrías reiniciar el nivel, mostrar un mensaje de "Game Over", etc.
    }

    private void ActualizarUI()
    {
        if (vida != null)
        {
            vida.text = "Vida: " + vidaActual;
        }
    }
}
