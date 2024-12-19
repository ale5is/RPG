using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class vida : MonoBehaviour
{
    public int nivelJ1,nivelJ2;
    public int vidaActualJ1, vidaActualJ2;
    public int vidaMaxJ1,vidaMaxJ2;
    public bool actualizar;

    private void Start()
    {
        vidaActualJ1 = 20 + (nivelJ1 * 10);
        vidaActualJ2 = 20 + (nivelJ2 * 10);
        vidaMaxJ1 = vidaActualJ1;
        vidaMaxJ2 = vidaActualJ2;
    }
    private void Update()
    {
        if (actualizar)
        {
            actualizar = false;
            vidaMaxJ1 = 20 + (nivelJ1 * 10);
            vidaMaxJ2 = 20 + (nivelJ2 * 10);
           
        }
        if (vidaActualJ1 == 0)
        {
            vidaActualJ1 = 1;
        }
        if (vidaActualJ2 == 0)
        {
            vidaActualJ2 = 1;
        }
    }

    // Start is called before the first frame update
    private void VidaActual(int Pvida)
    {
        Pvida=vidaActualJ1;
    }
}
