using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemigo : MonoBehaviour
{
    //Botones
    public BPlantarse scriptPlantarse;
    public BPedir scriptPedir;
    public Baraja baraja;
    //Enemigo
    public Transform panelEnemigo;
    public TMP_Text txtValorEnemigo;
    public List<GameObject> cartasEnemigo = new List<GameObject>();
    public int valorTotalSinAs = 0;
    public int valorTotalConAs = 0;
    public int cantidadAs = 0;
    //Ficha
    public Transform panelEnemigoFichas;
    public GameObject prefabFichaEnemigo;
    //Random
    public System.Random random = new System.Random();

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
        Pedir();
        txtValorEnemigo.text = "Valor del Enemigo: " + valorTotalConAs.ToString();
    }

    // Método para comenzar el turno del enemigo
    public void ComenzarTurno()
    {
        StartCoroutine(JugarTurno());
    }

    // Corrutina para manejar las acciones del enemigo con espera
    private IEnumerator JugarTurno()
    {
        yield return new WaitForSeconds(1); // Agregar un tiempo de espera inicial

        bool turnoTerminado = false;

        while (!turnoTerminado)
        {
            // Verificar si el enemigo necesita usar una ficha
            if (valorTotalConAs > 21 && panelEnemigoFichas.childCount > 0)
            {
                UsarFicha();
                yield return new WaitForSeconds(1); // Agregar un tiempo de espera después de usar una ficha
            }
            else
            {
                // Lógica para pedir carta o plantarse, teniendo en cuenta el valor del jugador si es el segundo turno
                if (valorTotalConAs >= 21)
                {
                    Plantarse();
                    turnoTerminado = true; // Terminar el turno si el enemigo se planta
                }
                else if (scriptPlantarse.turnoN == 1)
                {
                    int valorJugador = scriptPedir.ObtenerValorTotalConAs();
                    if (valorJugador > 21)
                    {
                        Plantarse();
                        turnoTerminado = true;
                    }
                    else if (valorTotalConAs > valorJugador && valorTotalConAs <= 21)
                    {
                        Plantarse();
                        turnoTerminado = true;
                    }
                    else if (valorTotalConAs == valorJugador && random.Next(0, 100) < 90)
                    {
                        Plantarse();
                        turnoTerminado = true;
                    }
                    else
                    {
                        Pedir();
                        yield return new WaitForSeconds(1); // Agregar un tiempo de espera después de pedir una carta
                    }
                }
                else
                {
                    // Lógica normal para pedir carta o plantarse
                    if (valorTotalConAs == 20 && random.Next(0, 100) < 98)
                    {
                        Plantarse();
                        turnoTerminado = true; // Terminar el turno si el enemigo se planta
                    }
                    else if (valorTotalConAs == 19 && random.Next(0, 100) < 95)
                    {
                        Plantarse();
                        turnoTerminado = true; // Terminar el turno si el enemigo se planta
                    }
                    else if (valorTotalConAs == 18 && random.Next(0, 100) < 90)
                    {
                        Plantarse();
                        turnoTerminado = true; // Terminar el turno si el enemigo se planta
                    }
                    else if (valorTotalConAs == 17 && random.Next(0, 100) < 85)
                    {
                        Plantarse();
                        turnoTerminado = true; // Terminar el turno si el enemigo se planta
                    }
                    else if (valorTotalConAs == 16 && random.Next(0, 100) < 25)
                    {
                        Plantarse();
                        turnoTerminado = true; // Terminar el turno si el enemigo se planta
                    }
                    else
                    {
                        Pedir();
                        yield return new WaitForSeconds(1); // Agregar un tiempo de espera después de pedir una carta
                    }
                }
            }
        }
    }

    // Método para pedir una carta
    private void Pedir()
    {
        GameObject cartaRobada = baraja.TomarCartaAleatoria();
        cartasEnemigo.Add(cartaRobada);

        // Instanciar la carta en el panel del enemigo
        GameObject cartaInstanciada = Instantiate(cartaRobada, panelEnemigo);
        cartaInstanciada.transform.localScale = Vector3.one; // Mantener el tamaño de la carta

        // Calcular el valor de la carta robada
        int valorCarta = ObtenerValorCarta(cartaRobada);

        if (valorCarta == 1)
        {
            cantidadAs++;
        }

        // Recalcular los valores totales
        CalcularValoresTotales();

        // Actualizar el texto del valor total del enemigo
        txtValorEnemigo.text = "Valor del Enemigo: " + valorTotalConAs.ToString();
    }

    private void CalcularValoresTotales()
    {
        valorTotalSinAs = 0; // Resetear valores para evitar acumulaciones incorrectas
        int valorConAsVariable = 0;
        int cantidadAsLocales = 0;

        foreach (GameObject carta in cartasEnemigo)
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
    private void Plantarse()
    {
        scriptPlantarse.PlantarseEnemigo();
    }
    public int ObtenerValorTotalConAs()
    {
        return valorTotalConAs;
    }
    private void UsarFicha()
    {
        if (panelEnemigo.childCount > 0)
        {
            // Eliminar la última carta del enemigo
            Transform ultimaCarta = panelEnemigo.GetChild(panelEnemigo.childCount - 1);
            int valorCarta = ObtenerValorCarta(ultimaCarta.gameObject);
            Destroy(ultimaCarta.gameObject);
            cartasEnemigo.RemoveAt(cartasEnemigo.Count - 1);

            // Restar el valor de la carta eliminada
            valorTotalConAs -= valorCarta;
            if (valorCarta == 1)
            {
                cantidadAs--;
            }

            // Recalcular los valores totales para ajustar por el As
            CalcularValoresTotales();

            // Actualizar el texto del valor total del enemigo
            txtValorEnemigo.text = "Valor del Enemigo: " + valorTotalConAs.ToString();

            // Eliminar una ficha del panel de fichas del enemigo
            Transform ultimaFicha = panelEnemigoFichas.GetChild(panelEnemigoFichas.childCount - 1);
            Destroy(ultimaFicha.gameObject);
        }
    }
}
