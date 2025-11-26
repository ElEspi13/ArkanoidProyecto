using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultadosUI : MonoBehaviour
{
    public TextMeshProUGUI puntuacionText;
    public TextMeshProUGUI tiempoText;

    /// <summary>
    /// Guarda la puntuación y el tiempo desde el GameManager y destruye su instancia.
    /// </summary>
    void Start()
    {
        if (GameManager.Instance != null)
        {
            puntuacionText.text =GameManager.Instance.ScoreText.text;
            tiempoText.text = "TIEMPO\n " + GameManager.Instance.TimerText.text;
            Destroy(GameManager.Instance.gameObject);
        }
    }
}
