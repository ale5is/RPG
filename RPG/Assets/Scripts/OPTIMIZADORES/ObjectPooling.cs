using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    [System.Serializable]
    public class pool
    {
        public string Tag;
        public GameObject Prefab;
        public int Size;
    }

    public static ObjectPooling instance;

    private void Awake()
    {
        instance = this;
    }

    public List<pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDirectionary;
    void Start()
    {
        poolDirectionary = new Dictionary<string, Queue<GameObject>>();
        foreach (var pool in pools)
        {
            Queue<GameObject> ObjectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.Size; i++)
            {
                GameObject Obj = Instantiate(pool.Prefab);
                 Obj.gameObject.SetActive(false);
                ObjectPool.Enqueue(Obj);
            }

            poolDirectionary.Add(pool.Tag, ObjectPool);
        }

    }

    public GameObject SpawnProuPool(string tag,Vector3 position,Quaternion rotation)
    {
        GameObject objectToSpawn = poolDirectionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDirectionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;

    }

}
