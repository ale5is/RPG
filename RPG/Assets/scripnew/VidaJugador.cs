using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaJugador : MonoBehaviour
{
    public int salud = 100;  // Salud inicial del jugador

    // Método para recibir daño
    public void RecibirDanio(int dano)
    {
        salud -= dano;
        Debug.Log("Jugador recibió daño, salud actual: " + salud);

        if (salud <= 0)
        {
            Morir();
        }
    }

    private void Morir()
    {
        Debug.Log("¡El jugador ha muerto!");
        // Aquí puedes agregar lógica para que el jugador muera (desaparecer, reiniciar la escena, etc.)
    }
}
