using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarColor : MonoBehaviour
{
    public Material color1, color2, color3, color4;
    public int contador;
    public bool cambiar;
    private void Start()
    {
        cambiar = true;
        Cambiar();   
    }
    // Start is called before the first frame updat 
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player") && cambiar)
        {

            cambiar = false;

        }
        else if (other.gameObject.CompareTag("Player") && !cambiar)
        {
            cambiar = true;
            Cambiar();
        }
    }
    public void Cambiar()
    {
        if (cambiar)
        {
            var material = GetComponent<MeshRenderer>().material;

            if (material.color == color4.color)
            {
                material.color = color1.color;
                contador = 1;
            }
            else if (material.color == color1.color)
            {
                material.color = color2.color;
                contador = 2;
            }
            else if (material.color == color2.color)
            {
                material.color = color3.color;
                contador = 3;
            }
            else if (material.color == color3.color)
            {
                material.color = color4.color;
                contador = 4;
            }
            Invoke("Cambiar", 1.5f);
        }
        
    }
}
