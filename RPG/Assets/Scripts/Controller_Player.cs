using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller_Player : MonoBehaviour
{
    public float speed = 5;
    private CharacterController controller;
    public new Transform camera;
    public float gravedad;
    private Vector3 direccion;
    public static Controller_Player instance;
    public int numEscena;

    public GameObject controlador;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        controlador = GameObject.FindGameObjectWithTag("Save");
    }

    private void Update()
    {
        Movement();
    }
    private void Movement()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 movimiento = Vector3.zero;
        if( hor != 0 || ver != 0)
        {
            Vector3 forward = camera.forward;
            forward.y = 0;
            forward.Normalize();

            Vector3 rigth = camera.right;
            rigth.y = 0;
            rigth.Normalize();

            Vector3 direccion=camera.forward*ver+camera.right*hor;
            direccion.Normalize();
            movimiento=direccion * speed * Time.deltaTime;

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
        }
        else if(other.gameObject.CompareTag("Puerta"))
        {
            Debug.Log("a");
            numEscena=other.GetComponent<CambiarEscena>().escena;
            //controlador.GetComponent<Controlador>().activar = 1;
            
            SceneManager.LoadScene(numEscena);
        }
        else { }
    }
    
}
