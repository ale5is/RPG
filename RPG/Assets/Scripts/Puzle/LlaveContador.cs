using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlaveContador : MonoBehaviour
{
    public Material comparar;
    public Puerta Puerta;
    private bool activo;
    // Update is called once per frame
    private void Start()
    {
        activo=false;
    }
    void Update()
    {
        CompararColor();
    }

    public void CompararColor()
    {
        var material = GetComponent<MeshRenderer>().material;
        if (material.color == comparar.color && !activo) 
        {
            activo= true;
            Puerta.cantidad++;
        }
    }
}
