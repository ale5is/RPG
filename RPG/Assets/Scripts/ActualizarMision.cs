using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActualizarMision : MonoBehaviour
{
    public Canvas canvas;
    public GameObject texto;
    public TMP_Text mision;
    void Start()
    {
        
        texto = canvas.transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).gameObject;
        //mision=texto.GetComponent<TextMeshPro>(texto);
        Debug.Log(mision);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
