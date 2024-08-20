using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public Canvas canvas;
    public GameObject panelRight, enemigo;
    // Start is called before the first frame update
    private void Start()
    {
        panelRight= canvas.transform.GetChild(1).transform.GetChild(2).gameObject;
        enemigo = GameObject.FindGameObjectWithTag("Enemigo");
    }
    public void ModoExplorar()
    {
        canvas.transform.GetChild(1).gameObject.SetActive(false);
    }
    public void ModoBatalla()
    {
        canvas.transform.GetChild(1).gameObject.SetActive(true);
        

    }
    public void Cargar()
    {
        enemigo = GameObject.FindGameObjectWithTag("Enemigo");
        enemigo.GetComponent<EnemyFigther>().statusPanel = panelRight.GetComponent<StatusPanel>();
    }
}
