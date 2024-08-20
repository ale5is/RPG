using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<GameObject> Bag = new List<GameObject>();
    public GameObject inv;
    public bool Activar;
    public GameObject Arma;
    public ItemContador item;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            other.gameObject.SetActive(false);
            item.ItemGuardado();
            
            Arma.gameObject.SetActive(true);
            for(int i=0; i<Bag.Count; i++)
            {
                if (Bag[i].GetComponent<Image>().enabled == false)
                {
                    Bag[i].GetComponent<Image>().enabled = true;
                    Bag[i].GetComponent<Image>().color = Color.blue;

                    break;
                }
            }
        }
    }

    void Update()
    {
        if (Activar)
        {
            inv.SetActive(true);
        }
        else
        {
            inv.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            Activar = !Activar;
        }
    }
}
