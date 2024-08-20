using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualizarEscenaBatalla : MonoBehaviour
{
    public GameObject controlador, jugador;
    public CombatManager combatManager;
    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        
        controlador = GameObject.FindGameObjectWithTag("Save");
        jugador = GameObject.FindGameObjectWithTag("Player");
        jugador.GetComponent<Fighter>().enabled = true;

        //combatManager.fighters[0] = jugador.GetComponent<Fighter>();
        jugador.GetComponent<PlayerFigther>().combatManager.PlayerTeam[0] = combatManager.PlayerTeam[0];
        
    }

    // Update is called once per frame
    void Update()
    {
        var chequeo = controlador.GetComponent<Controlador>();
        if (chequeo.activar == 0)
        {
            chequeo.jugador = jugador;
        }
    }
}
