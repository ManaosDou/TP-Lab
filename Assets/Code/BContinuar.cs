using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BContinuar : MonoBehaviour
{
    public BPedir scriptPedir;
    public BPlantarse scriptPlantarse;
    public Baraja baraja;
    //JUgador
    public Transform panelJugador;
    public Button botonPedir;
    public Button botonPlantarse;
    public TMP_Text txtValorJugador;
    //Final Ronda
    public TMP_Text txtResultado;
    public Button botonContinuar;
    //Enemigo
    public Enemigo enemigo;
    public Transform panelEnemigo;
    public TMP_Text txtValorEnemigo;
    //Fichas
    public Transform panelJugadorFichas; 
    public Transform panelEnemigoFichas; 
    public GameObject prefabFichaJugador;
    public GameObject prefabFichaEnemigo;
    public Button botonFichas;
    public BFicha scriptFichas;
    private int rondas = 0;

    public void Continuar()
    {
        if (scriptPlantarse.vidaEnemigo > 0 && scriptPlantarse.vidaJugador > 0)
        {
            rondas++;

            if (rondas % 2 == 0)
            {
                CrearFicha(prefabFichaJugador, panelJugadorFichas);
                CrearFicha(prefabFichaEnemigo, panelEnemigoFichas);
            }

        // Reiniciar jugador
        foreach (Transform child in panelJugador)
        {
            Destroy(child.gameObject);
        }
        scriptPedir.cartasJugador.Clear();  
        scriptPedir.valorTotalSinAs = 0;  
        scriptPedir.valorTotalConAs = 0;  
        scriptPedir.cantidadAs = 0; 

        // Reiniciar enemigo
        foreach (Transform child in panelEnemigo)
        {
            Destroy(child.gameObject);
        }
        enemigo.cartasEnemigo.Clear();
        enemigo.valorTotalSinAs = 0;
        enemigo.valorTotalConAs = 0;
        enemigo.cantidadAs = 0;

        //Reiniciar turnos
        scriptPlantarse.turnoN = 0;
        scriptPlantarse.IniciarRonda();

        //Reiniciar baraja
        baraja.ArmarBaraja();

        // Actualizar la UI
        txtValorJugador.text = "Valor del Jugador: " + scriptPedir.valorTotalConAs.ToString();
        txtValorEnemigo.text = "Valor del Enemigo: " + enemigo.valorTotalConAs.ToString();
        txtResultado.gameObject.SetActive(false);
        botonContinuar.gameObject.SetActive(false);
        }
        if (scriptPlantarse.vidaEnemigo <= 0 && scriptPlantarse.vidaJugador > 0)
        {
            SceneManager.LoadScene("Win");
        }
        if (scriptPlantarse.vidaJugador <= 0 && scriptPlantarse.vidaEnemigo > 0)
        {
            SceneManager.LoadScene("Lose");
        }

        scriptPedir.Iniciar();
        enemigo.Iniciar();
    }

    //Ficha
    private void CrearFicha(GameObject prefabFicha, Transform panelFichas)
    {
        Instantiate(prefabFicha, panelFichas);
        prefabFicha.transform.localScale = Vector3.one;
        scriptFichas.DesctivarTxt();
    }
}
