using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;

public class BFicha : MonoBehaviour
{
    public BContinuar scriptContinuar;
    public Button botonFicha;
    public Transform panelJugadorFichas;
    public Transform panelJugador;
    public BPedir scriptPedir;
    public BPlantarse scriptPlantarse;
    public Button botonPedir;
    public TMP_Text txtValorJugador;
    public TMP_Text txtSinFichas;

    void FixedUpdate()
    {
        if (!scriptPlantarse.turnoJugador || scriptPlantarse.turnoN >= 2)
        {
            botonFicha.interactable = false;
        }
        else
        {
            botonFicha.interactable = true;
        }
    }

    public void UsarFicha()
    {
        if (panelJugador.childCount > 0 && panelJugadorFichas.childCount > 0)
        {
            DesctivarTxt();
            // Eliminar la última carta del jugador
            Transform ultimaCarta = panelJugador.GetChild(panelJugador.childCount - 1);
            int valorCarta = scriptPedir.ObtenerValorCarta(ultimaCarta.gameObject);
            Destroy(ultimaCarta.gameObject);
            scriptPedir.cartasJugador.RemoveAt(scriptPedir.cartasJugador.Count - 1);

            // Restar el valor de la carta eliminada
            scriptPedir.valorTotalConAs -= valorCarta;
            if (valorCarta == 1)
            {
                scriptPedir.cantidadAs--;
            }

            // Recalcular los valores totales para ajustar por el As
            scriptPedir.CalcularValoresTotales();

            // Actualizar el texto del valor total del jugador
            txtValorJugador.text = "Valor del Jugador: " + scriptPedir.valorTotalConAs.ToString();

            // Eliminar una ficha del panel de fichas del jugador
            Transform ultimaFicha = panelJugadorFichas.GetChild(panelJugadorFichas.childCount - 1);
            Destroy(ultimaFicha.gameObject);
            botonPedir.interactable = true;
        }
        else
        {
            ActivarTxt();
        }
    }
    public void ActivarTxt()
    {
        txtSinFichas.gameObject.SetActive(true);
    }
    public void DesctivarTxt()
    {
        txtSinFichas.gameObject.SetActive(false);
    }
}