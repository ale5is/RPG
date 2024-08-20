using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comprobador : MonoBehaviour
{
    public PuertaAparecer Puerta;
    public CambiarColor color;
  
    void Update()
    {
        CompararColor();
    }

    public void CompararColor()
    {
        Puerta.cantidad=color.contador;
    }
}
