using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class Instanciar : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Transform spawnPoint;
    ObjectPooling objectPooler;
    void Start()
    {
        objectPooler = ObjectPooling.instance;
    }

    private void FixedUpdate()
    {
        objectPooler.SpawnProuPool("Objeto",transform.position,Quaternion.identity);
    }
}
