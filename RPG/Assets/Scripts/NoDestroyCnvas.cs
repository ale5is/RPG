using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDestroyCnvas : MonoBehaviour
{
    public static NoDestroyCnvas Instance;
    private void Awake()
    {
        if (NoDestroyCnvas.Instance == null)
        {
            NoDestroyCnvas.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
