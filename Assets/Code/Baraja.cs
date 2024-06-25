using System.Collections.Generic;
using UnityEngine;

public class Baraja : MonoBehaviour
{
    public GameObject prefabAs;
    public GameObject prefab2;
    public GameObject prefab3;
    public GameObject prefab4;
    public GameObject prefab5;
    public GameObject prefab6;
    public GameObject prefab7;
    public GameObject prefab8;
    public GameObject prefab9;
    public GameObject prefab10;
    private List<GameObject> cartas;
    void Start()
    {
        ArmarBaraja();
    }
    private void AgregarCartas(GameObject carta, int cantidad)
    {
        for (int i = 0; i < cantidad; i++)
        {
            cartas.Add(carta);
        }
    }
    public GameObject TomarCartaAleatoria()
    {
        int cartaAleatoria = Random.Range(0, cartas.Count);
        GameObject cartaSeleccionada = cartas[cartaAleatoria];
        cartas.RemoveAt(cartaAleatoria);
        return cartaSeleccionada;
        
    }
    public void ArmarBaraja()
    {
        if (cartas == null)
        {
            cartas = new List<GameObject>();
        }
        else
        {
            cartas.Clear();
        }

        AgregarCartas(prefabAs, 4);
        AgregarCartas(prefab2, 4);
        AgregarCartas(prefab3, 4);
        AgregarCartas(prefab4, 4);
        AgregarCartas(prefab5, 4);
        AgregarCartas(prefab6, 4);
        AgregarCartas(prefab7, 4);
        AgregarCartas(prefab8, 4);
        AgregarCartas(prefab9, 4);
        AgregarCartas(prefab10, 4);
    }
}