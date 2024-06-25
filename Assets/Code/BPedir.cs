using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BPedir : MonoBehaviour
{
    public Baraja baraja;
    public Transform panelJugador;
    public Button botonPedir;
    public Button botonPlantarse;
    public Button botonContinuar;
    public TMP_Text txtValorJugador;
    public TMP_Text txtResultado;
    public int valorTotalSinAs = 0;
    public int valorTotalConAs = 0;
    public int cantidadAs = 0;
    public List<GameObject> cartasJugador = new List<GameObject>();
    void Start()
    {
        Iniciar();
    }
    public void Iniciar()
    {
        StartCoroutine(Inicio());
    }
    private IEnumerator Inicio()
    {
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            Pedir();
            txtValorJugador.text = "Valor del Enemigo: " + valorTotalConAs.ToString();
            break;
        }
    }

    //Pedir
    public void Pedir()
    {
        GameObject cartaRobada = baraja.TomarCartaAleatoria();
        cartasJugador.Add(cartaRobada);

        // Instanciar la carta en el panel del jugador
        GameObject cartaInstanciada = Instantiate(cartaRobada, panelJugador);
        cartaInstanciada.transform.localScale = Vector3.one; // Mantener el tamaño de la carta

        // Calcular el valor de la carta robada
        int valorCarta = ObtenerValorCarta(cartaRobada);

        if (valorCarta == 1)
        {
            cantidadAs++;
        }

        // Recalcular los valores totales
        CalcularValoresTotales();

        // Actualizar el texto del valor total del jugador
        txtValorJugador.text = "Valor del Jugador: " + valorTotalConAs.ToString();

        // Desactivar el botón si el valor total es mayor a 21
        if (valorTotalConAs > 21)
        {
            botonPedir.interactable = false;
        }
    }

    public void CalcularValoresTotales()
    {
        valorTotalSinAs = 0; // Resetear valores para evitar acumulaciones incorrectas
        int valorConAsVariable = 0;
        int cantidadAsLocales = 0;
        foreach (GameObject carta in cartasJugador)
        {
            int valorCarta = ObtenerValorCarta(carta);
            if (valorCarta == 1)
            {
                cantidadAsLocales++;
            }
            else
            {
                valorTotalSinAs += valorCarta;
                valorConAsVariable += valorCarta;
            }
        }
        // Procesar los As
        if (cantidadAsLocales > 0)
        {
            if (valorTotalSinAs + 11 + (cantidadAsLocales - 1) <= 21)
            {
                // Si el valor total con un As valiendo 11 no supera 21
                valorConAsVariable = valorTotalSinAs + 11 + (cantidadAsLocales - 1);
            }
            else
            {
                // Si supera 21, todos los As valen 1
                valorConAsVariable = valorTotalSinAs + cantidadAsLocales;
            }
        }
        valorTotalConAs = valorConAsVariable;
        // Ajustar el valor del As si valorTotalConAs supera 21
        if (valorTotalConAs > 21 && cantidadAsLocales > 0)
        {
            valorConAsVariable = valorTotalSinAs + cantidadAsLocales;
            if (valorConAsVariable <= 21)
            {
            valorTotalConAs = valorConAsVariable;
            }
        }
    }

    public int ObtenerValorCarta(GameObject carta)
    {
        if (carta == baraja.prefabAs) return 1; 
        if (carta == baraja.prefab2) return 2;
        if (carta == baraja.prefab3) return 3;
        if (carta == baraja.prefab4) return 4;
        if (carta == baraja.prefab5) return 5;
        if (carta == baraja.prefab6) return 6;
        if (carta == baraja.prefab7) return 7;
        if (carta == baraja.prefab8) return 8;
        if (carta == baraja.prefab9) return 9;
        if (carta == baraja.prefab10) return 10;
        return 0;
    }

    // Dar el valor al plantarse
    public int ObtenerValorTotalConAs()
    {
        return valorTotalConAs;
    }
}
