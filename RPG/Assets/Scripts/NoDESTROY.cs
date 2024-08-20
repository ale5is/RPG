using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDESTROY : MonoBehaviour
{
    public static NoDESTROY Instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if (NoDESTROY.Instance == null)
        {
            NoDESTROY.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
