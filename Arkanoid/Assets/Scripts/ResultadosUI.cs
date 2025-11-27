using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultadosUI : MonoBehaviour
{
    public TextMeshProUGUI puntuacionText;
    public TextMeshProUGUI tiempoText;
    public TextMeshProUGUI leaderboard;

    /// <summary>
    /// MEtodo que se ejecuta al iniciar la escena de resultados cargando la puntuación y el tiempo desde el GameManager,
    /// </summary>
    void Start()
    {
        if (GameManager.Instance != null)
        {
            puntuacionText.text = GameManager.Instance.ScoreText.text;
            string raw = puntuacionText.text;
            string clean = raw.Replace("PUNTUACION\n", "");

            Resultado nuevoResultado = new Resultado(clean, GameManager.Instance.TimerText.text);

            tiempoText.text = "TIEMPO\n " + GameManager.Instance.TimerText.text;

            ResultadosManager.AgregarResultado(nuevoResultado);

            Destroy(GameManager.Instance.gameObject);
        }
        MostrarLeaderboard();
    }

    /// <summary>
    /// Saca los datos del leaderboard desde el ResultadosManager y los muestra en pantalla.
    /// </summary>
    private void MostrarLeaderboard()
    {
        List<Resultado> lista = ResultadosManager.CargarResultados();
        if (lista == null || lista.Count == 0)
        {
            leaderboard.text = "LEADERBOARD\n\nNo hay datos aún.";
            return;
        }

        string textoFinal = "LEADERBOARD\n\n";

        for (int i = 0; i < lista.Count; i++)
        {
            textoFinal += $"{i + 1}.  {lista[i].Puntuacion} pts   -   {lista[i].Tiempo}\n";
        }

        leaderboard.text = textoFinal;
    }
}
