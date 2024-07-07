using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContador : MonoBehaviour
{
    // Start is called before the first frame update
    public Puerta Puerta;
    private bool activo;
    // Update is called once per frame
    private void Start()
    {
        activo = false;
    }

    public void ItemGuardado()
    {
        if (!activo)
        {
            activo = true;
            Puerta.cantidad++;
        }
    }
}
