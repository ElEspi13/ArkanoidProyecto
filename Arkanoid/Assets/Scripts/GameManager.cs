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
    private int bolaMaxima = 0;
    public BolaManager BolaManager;

    [Header("Lives")]
    private int vidas=3;
    public TextMeshProUGUI VidasText;

    [Header("Timer")]
    private float timeElapsed = 0f;
    public bool TimerActive = true;
    public TextMeshProUGUI TimerText;

    [Header("Score")]
    private int score = 0;
    public TextMeshProUGUI ScoreText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        UpdateVidasText();
        UpdateScoreText();
    }

    private void UpdateVidasText()
    {
        VidasText.text = "Vidas: " + vidas;
    }

    private void UpdateScoreText()
    {
        ScoreText.text = "Puntuación " + score;
    }

    private void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        if (!TimerActive) return;

        timeElapsed += Time.deltaTime;
        TimerText.text = FormatTime(timeElapsed);
    }
    private string FormatTime(float t)
    {
        int minutes = Mathf.FloorToInt(t / 60f);
        int seconds = Mathf.FloorToInt(t % 60f);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }


internal void ActivarPowerUp(PowerUp.TipoPowerUp tipo, float duracion)
    {
        switch (tipo)
        {
            case PowerUp.TipoPowerUp.BolaExtra:
                if (bolaMaxima<3)
                {
                    GameObject bola=Instantiate(BolaExtraPrefab, Pala.transform.position, Quaternion.identity);
                    
                    BolaManager.AgregarBola(bola.GetComponent<Bola>());
                    bolaMaxima++;
                }
                else {
                    Debug.Log("Ya tienes el maximo de bolas");
                }

                break;
            
        }
    }

    internal void PerderVida()
    {
        vidas--;
        UpdateVidasText();
    }

    internal void AumentarPuntuacion(int v)
    {
        score += v;
        UpdateScoreText();
    }
}
