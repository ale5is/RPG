using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPared : MonoBehaviour
{
    public GameObject Pared;
    // Start is called before the first frame updat
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("LLave"))
        {
            Pared.gameObject.SetActive(false);
        }
        
    }
    }
