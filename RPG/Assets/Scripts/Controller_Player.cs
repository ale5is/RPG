using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller_Player : MonoBehaviour
{
    public float speed = 5;
    private CharacterController controller;
    public new Transform camera;
    public float gravedad;
    private Vector3 direccion;
    public static Controller_Player Instance;
    public int numEscena;
    public GameObject controlador;
    public CanvasController canvasController;
    public Animator anim;
    public TMP_Text vida,nivel;
    public vida vidaJ1;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        controlador = GameObject.FindGameObjectWithTag("Save");
        vidaJ1= GetComponent<vida>();


        if (Controller_Player.Instance == null)
        {
            Controller_Player.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (controlador.GetComponent<Controlador>().combate==0)
        {
            Movement();
            vida.text=vidaJ1.vidaActualJ1.ToString()+"/"+vidaJ1.vidaMaxJ1.ToString();
            nivel.text = "N." + vidaJ1.nivelJ1.ToString();
        }
        
    }
    private void Movement()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 movimiento = Vector3.zero;
        if( hor != 0 || ver != 0)
        {
            if( hor > 0)
            {
                transform.localScale=new Vector3(-2,2,2);
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

            Vector3 direccion=camera.forward*ver+camera.right*hor;
            direccion.Normalize();
            movimiento=direccion * speed * Time.deltaTime;
            anim.SetBool("caminando", true);
        }
        else
        {
            anim.SetBool("caminando", false);
        }
        movimiento.y += gravedad * Time.deltaTime;
        controller.Move( movimiento );
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemigo"))
        {
            controlador.GetComponent<Controlador>().activar = 1;
            
            SceneManager.LoadScene(1);
            //controlador.GetComponent<Controlador>().combate = true;
            //canvasController.ModoBatalla();
            //canvasController.Cargar();
        }
        else if(other.gameObject.CompareTag("Puerta"))
        {
            Debug.Log("a");
            numEscena=other.GetComponent<CambiarEscena>().escena;
            controlador.GetComponent<Controlador>().CargarDatos();
            //controlador.GetComponent<Controlador>().activar = 1;

            SceneManager.LoadScene(numEscena);
        }
        else { }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spawn"))
        {
            controlador.GetComponent<Controlador>().GuardarDatos();
        }
    }
}
