using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject BolaExtraPrefab;
    public GameObject Pala;
    private int contadoBolas = 1;
    public BolaManager BolaManager;

    /// <summary>
    /// Vidas del jugador
    /// </summary>
    [Header("Lives")]
    private int vidas=3;
    public TextMeshProUGUI VidasText;

    /// <summary>
    /// Contador del tiempo transcurrido
    /// </summary>
    [Header("Timer")]
    private float tiempo = 0f;
    public bool TimerActive = true;
    public TextMeshProUGUI TimerText;

    /// <summary>
    /// Puntos del jugador
    /// </summary>
    [Header("Score")]
    private int score = 0;
    public TextMeshProUGUI ScoreText;

    public TextMeshProUGUI TutorialText;

    /// <summary>
    /// Inicializa el singleton del GameManager
    /// </summary>
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    /// <summary>
    /// Inializa los textos al iniciar el juego
    /// </summary>
    void Start()
    {
        UpdateVidasText();
        UpdateScoreText();
        StartCoroutine(EsconderLuegoDe10s());
    }

    IEnumerator EsconderLuegoDe10s()
    {
        yield return new WaitForSeconds(9f);
        TutorialText.gameObject.SetActive(false);
    }

    /// <summary>
    /// Actualiza el texto de las vidas
    /// </summary>
    private void UpdateVidasText()
    {
        VidasText.text = "VIDAS\n" + vidas;
    }

    /// <summary>
    /// Actualiza el texto de la puntuación
    /// </summary>
    private void UpdateScoreText()
    {
        ScoreText.text = "PUNTUACION\n" + score;
    }

    /// <summary>
    /// Actualiza el temporizador cada frame
    /// </summary>
    private void Update()
    {
        UpdateTimer();
    }

    /// <summary>
    /// Actualiza el temporizador del juego
    /// </summary>
    private void UpdateTimer()
    {
        if (!TimerActive) return;

        tiempo += Time.deltaTime;
        TimerText.text = FormatTime(tiempo);
    }
    /// <summary>
    /// Formatea el tiempo en minutos y segundos
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    private string FormatTime(float t)
    {
        int minutes = Mathf.FloorToInt(t / 60f);
        int seconds = Mathf.FloorToInt(t % 60f);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    /// <summary>
    /// Activa el power up correspondiente
    /// </summary>
    /// <param name="tipo"></param>
    /// <param name="duracion"></param>
    internal void ActivarPowerUp(PowerUp.TipoPowerUp tipo, float duracion)
    {
        switch (tipo)
        {
            case PowerUp.TipoPowerUp.BolaExtra:
                if (contadoBolas<3)
                {
                    GameObject bola=Instantiate(BolaExtraPrefab, Pala.transform.position, Quaternion.identity);
                    
                    BolaManager.AgregarBola(bola.GetComponent<Bola>());
                    contadoBolas++;
                }
                else {
                    Debug.Log("Ya tienes el maximo de bolas");
                }

                break;
            case PowerUp.TipoPowerUp.VidaUp:
                if (vidas<5)
                {
                    vidas++;
                    UpdateVidasText();
                }
                else
                {
                    Debug.Log("Ya tienes el maximo de vidas");
                }
                break;

        }
    }

    /// <summary>
    /// Metodo para perder vida y cargar la escena de derrota si no quedan vidas
    /// </summary>
    internal void PerderVida()
    {
        vidas--;
        UpdateVidasText();
        if (vidas == 0)
        {
            TimerActive = false;
            UnityEngine.SceneManagement.SceneManager.LoadScene("MenuDerrota");
        }
    }

    /// <summary>
    /// Aumenta la puntuación del jugador
    /// </summary>
    /// <param name="puntos"></param>
    internal void AumentarPuntuacion(int puntos)
    {
        score += puntos;
        UpdateScoreText();
    }

    /// <summary>
    /// Decrementa el contador de bolas contadas para poder generar una nueva bola extra
    /// </summary>
    internal void AumentarBolas()
    {
        contadoBolas--;
    }
}
