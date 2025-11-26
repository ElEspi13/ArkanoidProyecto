using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum TipoPowerUp { BolaExtra, VidaUp }
    public TipoPowerUp Tipo;
    public float Duracion = 5f;

    public float velocidadCaida = 2f;

    /// <summary>
    /// Movimiento del power up hacia abajo
    /// </summary>
    void Update()
    {
        transform.Translate(Vector2.down * velocidadCaida * Time.deltaTime);
    }

    /// <summary>
    /// Metodo que controla si toca la pala o la zona de destrucción
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pala"))
        {
            GameManager.Instance.ActivarPowerUp(Tipo, Duracion);



            Destroy(gameObject); 
        }
        else if (collision.CompareTag("ZonaDestruccion"))
        {
            Destroy(gameObject);
        }
    }
}
