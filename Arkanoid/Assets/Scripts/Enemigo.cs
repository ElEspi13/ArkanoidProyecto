using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    private float speed=0.3f;
    private int vida ;
    private bool isSpecial;
    public GameObject PowerUpPrefab;

    public void Init(float speed, int vida)
    {
        this.speed = speed;
        this.vida = vida;
        isSpecial = CompareTag("EnemigoEspecial");
    }
    
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        RecibirDano(1);
    }
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ZonaDestruccion"))
        {
            Destroy(gameObject);
            GameManager.Instance.PerderVida();

        }
        
    }

}
