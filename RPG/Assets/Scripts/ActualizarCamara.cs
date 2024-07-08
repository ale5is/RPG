using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualizarCamara : MonoBehaviour
{
    public new Transform camera;
    public GameObject jugador;
    // Start is called before the first frame update
    void Start()
    {
        camera= transform;
        jugador = GameObject.FindGameObjectWithTag("Player");
        jugador.GetComponent<Controller_Player>().camera = camera;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
