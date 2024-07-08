using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comprobador : MonoBehaviour
{
    public Material comparar;
    public PuertaAparecer Puerta;
    public CambiarColor color;
  
    // Update is called once per frame
    private void Start()
    {
    
    }
    void Update()
    {
        CompararColor();
    }

    public void CompararColor()
    {
        var material = GetComponent<MeshRenderer>().material;
        Puerta.cantidad=color.contador;
    }
}
