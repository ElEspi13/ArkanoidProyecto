using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum TipoPowerUp { BolaExtra }
    public TipoPowerUp Tipo;
    public float Duracion = 5f;

    public float velocidadCaida = 2f;

    void Update()
    {
        transform.Translate(Vector2.down * velocidadCaida * Time.deltaTime);
    }

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
