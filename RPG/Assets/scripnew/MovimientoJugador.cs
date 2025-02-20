using System.Collections;
using System.Collections.Generic;
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
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        //controlador = GameObject.FindGameObjectWithTag("Save");
        //vidaJ1 = GetComponent<vida>();


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
        velocidadInicial=velocidad;
        velocidadMax = velocidad + 5;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
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
            if (hor > 0)
            {
                transform.localScale = new Vector3(-2, 2, 2);
            }
            else
            {
                transform.localScale = new Vector3(2, 2, 2);
            }
            Vector3 forward = camera.forward;
            forward.y = 0;
            forward.Normalize();

            Vector3 rigth = camera.right;
            rigth.y = 0;
            rigth.Normalize();

            Vector3 direccion = camera.forward * ver + camera.right * hor;
            direccion.Normalize();
            movimiento = direccion * velocidad * Time.deltaTime;
            //anim.SetBool("caminando", true);
        }
        else
        {
            //anim.SetBool("caminando", false);
        }
        movimiento.y += gravedad * Time.deltaTime;
        controller.Move(movimiento);
    }
}
