using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Enemigo : MonoBehaviour
{
    private int rutina;
    private float cronometro;
    private Quaternion angulo;
    private float grado;
    public int caminar;
    public int correr;
    public int distancia;
    public GameObject target;
    void Start()
    {
        target = GameObject.Find("Xion");
    }

    // Update is called once per frame
    void Update()
    {
        Comportamiento();
    }
    public void Comportamiento()
    {
        if(Vector3.Distance(transform.position, target.transform.position) > distancia)
        {
            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }
            if (rutina == 0) { }
            else if (rutina == 1)
            {
                grado = Random.Range(0, 360);
                angulo = Quaternion.Euler(0, grado, 0);
                rutina++;
            }
            else if (rutina == 2)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                transform.Translate(Vector3.forward * caminar * Time.deltaTime);
            }
        }
        else
        {
            var lookPos=target.transform.position-transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 3);
            transform.Translate(Vector3.forward * correr * Time.deltaTime);

        }
        
    }
}
