using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    private float velocidad=0.3f;
    private int vida ;
    private bool isSpecial;
    public GameObject PowerUpPrefab;

    /// <summary>
    /// Inicializa las estadisticas del enemigo
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="vida"></param>
    public void Init(float speed, int vida)
    {
        this.velocidad = speed;
        this.vida = vida;
        isSpecial = CompareTag("EnemigoEspecial");
    }

    /// <summary>
    /// Movimiento del enemigo hacia abajo
    /// </summary>
    void Update()
    {
        transform.Translate(Vector2.down * velocidad * Time.deltaTime);
    }

    /// <summary>
    /// Evento de colisión con la bola
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        RecibirDano(1);
    }

    /// <summary>
    /// Reduce la vida del enemigo y lo destruye si llega a 0
    /// </summary>
    /// <param name="dano"></param>
    private void RecibirDano(int dano)
    {
        vida -= dano;
        if (vida <= 0)
        {
            Destroy(gameObject);
            if (isSpecial)
            {
                Instantiate(PowerUpPrefab,this.transform.position,Quaternion.identity);
            }
            GameManager.Instance.AumentarPuntuacion(isSpecial ? 20 : 10);
        }
    }

    /// <summary>
    /// Controla la colisión con la zona de destrucción
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ZonaDestruccion"))
        {
            Destroy(gameObject);
            GameManager.Instance.PerderVida();

        }
        
    }

}
