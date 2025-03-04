using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DarNumero : MonoBehaviour
{
    public ElegirEscena[] objetos; // Lista de objetos con el script ElegirEscena

    private void Start()
    {
        // Asignar números únicos cuando inicie la escena actual
        //
        //AsignarNumerosUnicos();
    }

    private void OnEnable()
    {
        // Suscribirse al evento que se llama cuando se carga una nueva escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Desuscribirse del evento cuando ya no sea necesario
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Método que se llama cuando se carga una nueva escena
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Volver a asignar los números a las puertas en la nueva escena
        AsignarNumerosUnicos();
    }

    // Método para asignar números únicos a las puertas
    public void AsignarNumerosUnicos()
    {
        // Buscar automáticamente todos los objetos con el script ElegirEscena en la nueva escena
        objetos = FindObjectsOfType<ElegirEscena>(); // Encuentra todos los objetos de la escena con el script ElegirEscena

        if (objetos.Length == 0)
        {
            Debug.LogError("No se encontraron objetos con el script ElegirEscena en la escena.");
            return;
        }

        // Crear una lista de números únicos disponibles (asegúrate de que el tamaño de la lista sea adecuado)
        List<int> numerosDisponibles = new List<int> { 1, 2, 3, 4 }; // Por ejemplo, 4 números únicos

        // Mezclar la lista de números aleatoriamente (Fisher-Yates Shuffle)
        ShuffleList(numerosDisponibles);

        // Asignar un número único a cada objeto, pero solo hasta el número de objetos disponibles
        for (int i = 0; i < objetos.Length && i < numerosDisponibles.Count; i++)
        {
            objetos[i].SetNumero(numerosDisponibles[i]); // Asignar el número único al objeto
        }
    }

    // Método para mezclar la lista (Fisher-Yates Shuffle)
    void ShuffleList(List<int> lista)
    {
        // Mezclar la lista de números aleatoriamente
        for (int i = lista.Count - 1; i > 0; i--)
        {
            // Obtener un índice aleatorio
            int randomIndex = UnityEngine.Random.Range(0, i + 1);

            // Intercambiar el valor en la posición actual con el valor en el índice aleatorio
            (lista[i], lista[randomIndex]) = (lista[randomIndex], lista[i]);
        }
    }
}
