using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
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
            gameObject.SetActive(false);
        }
    }
}
