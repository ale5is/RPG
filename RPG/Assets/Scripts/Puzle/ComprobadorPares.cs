using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComprobadorPares : MonoBehaviour
{
    public PuertaAparecer Puerta;
    public ControllerPared color;

    void Update()
    {
        CompararColor();
    }

    public void CompararColor()
    {
        Puerta.cantidad = color.contador;
    }
}
