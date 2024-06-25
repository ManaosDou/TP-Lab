using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BPlantarse : MonoBehaviour
{
    public BPedir scriptPedir;
    public Enemigo enemigo;
    public Button botonPedir;
    public Button botonPlantarse;
    public Button botonContinuar;
    public TMP_Text txtResultado;
    public TMP_Text txtTurno;
    public TMP_Text txtVidaEnemigo;
    public TMP_Text txtVidaJugador;
    public bool turnoJugador;
    public int turnoN = 0;
    public int vidaJugador = 5;
    public int vidaEnemigo = 5;

    void Start()
    {
        txtVidaEnemigo.text = "Vida enemigo: " + vidaEnemigo.ToString();
        txtVidaJugador.text = "Vida jugador: " + vidaJugador.ToString();
        IniciarRonda();
    }

    public void Plantarse()
    {
        turnoN++;
        if (turnoN == 1)
        {
            StartCoroutine(PrimerTurno());
        }
        else if (turnoN == 2)
        {
            StartCoroutine(SegundoTurno());
        }
    }

    public void PlantarseEnemigo()
    {
        turnoN++;
        enemigo.txtValorEnemigo.text = "El Enemigo se planta con: " + enemigo.valorTotalConAs.ToString();
        if (turnoN == 1)
        {
            StartCoroutine(PrimerTurno());
        }
        if (turnoN == 2)
        {
            StartCoroutine(SegundoTurno());
        }
    }
    private IEnumerator PrimerTurno()
    {
        if (turnoJugador == true)
        {
            txtTurno.text = "Turno: Enemigo";
            turnoJugador = false;
            enemigo.ComenzarTurno();
            botonPedir.interactable = false;
            botonPlantarse.interactable = false;
        }
        else
        {
            txtTurno.text = "Turno: Jugador";
            turnoJugador = true;
            botonPedir.interactable = true;
            botonPlantarse.interactable = true;
        }
        // Agregar un tiempo de espera corto antes de continuar
        yield return new WaitForSeconds(0.1f);
    }

    private IEnumerator SegundoTurno()
    {
        txtTurno.text = "Ronda finalizada";
        Resultado();
        botonPedir.interactable = false;
        botonPlantarse.interactable = false;
        txtResultado.gameObject.SetActive(true);
        botonContinuar.gameObject.SetActive(true);

        // Agregar un tiempo de espera corto antes de continuar
        yield return new WaitForSeconds(0.1f);
    }
    private void Resultado()
    {
        int valorJugador = scriptPedir.ObtenerValorTotalConAs();
        int valorEnemigo = enemigo.ObtenerValorTotalConAs();

        // Empate si ambos se pasan de 21
        if (valorJugador > 21 && valorEnemigo > 21)
        {
            txtResultado.text = "Empate";
        }
        // Empate si los valores son iguales y ninguno se pasa de 21
        else if (valorJugador == valorEnemigo)
        {
            txtResultado.text = "Empate";
        }
        // Jugador gana si el enemigo se pasa de 21 y el jugador no
        else if (valorEnemigo > 21 && valorJugador <= 21)
        {
            txtResultado.text = "Ganaste";
            vidaEnemigo--;
        }
        // Enemigo gana si el jugador se pasa de 21 y el enemigo no
        else if (valorJugador > 21 && valorEnemigo <= 21)
        {
            txtResultado.text = "Perdiste";
            vidaJugador--;
        }
        // Jugador gana si su valor es mayor que el del enemigo y no se pasa de 21
        else if (valorJugador > valorEnemigo && valorJugador <= 21)
        {
            txtResultado.text = "Ganaste";
            vidaEnemigo--;
        }
        // Enemigo gana si su valor es mayor que el del jugador y no se pasa de 21
        else if (valorEnemigo > valorJugador && valorEnemigo <= 21)
        {
            txtResultado.text = "Perdiste";
            vidaJugador--;
        }
        txtVidaEnemigo.text = "Vida enemigo: " + vidaEnemigo.ToString();
        txtVidaJugador.text = "Vida jugador: " + vidaJugador.ToString();
    }
    public void IniciarRonda()
    {
        turnoJugador = Random.Range(0, 2) == 0; // 0 para jugador, 1 para enemigo
        if (turnoJugador == true)
        {
            txtTurno.text = "Turno: Jugador";
            botonPedir.interactable = true;
            botonPlantarse.interactable = true;
        }
        else
        {
            txtTurno.text = "Turno: Enemigo";
            botonPedir.interactable = false;
            botonPlantarse.interactable = false;
            enemigo.ComenzarTurno();
        }
    }
}
