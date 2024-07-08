using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaAparecer : MonoBehaviour
{
    public int cantidad;
    public int num;
    void Update()
    {
        Abrir();
    }

    public void Abrir()
    {
        if (cantidad == num)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
