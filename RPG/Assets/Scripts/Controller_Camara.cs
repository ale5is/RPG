using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Camara : MonoBehaviour
{
    public Transform seguir;
    public float distancia;
    public static Controller_Camara Instance;
    private void Awake()
    {
        if (Controller_Camara.Instance == null)
        {
            Controller_Camara.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        transform.position=seguir.position+new Vector3(0,0,-distancia);
    }
}
