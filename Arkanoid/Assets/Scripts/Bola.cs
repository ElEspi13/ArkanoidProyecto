using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class Bola : MonoBehaviour
{
    private float velocidad = 8f;
    public bool Jugando = false;
    private Rigidbody2D rb;
    private Transform pala;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pala = GameObject.FindWithTag("Pala").transform;
        rb.isKinematic = true; 
    }

    void Update()
    {

        if (!Jugando)
        {
            Vector3 palaPos = pala.position;
            transform.position = new Vector3(palaPos.x, palaPos.y + 0.5f, 0f);
        }
    }

    public void PosicionarSobrePala(Vector3 palaPos)
    {
        rb.isKinematic = true;
        transform.position = new Vector3(palaPos.x, palaPos.y + 0.5f, 0f);
        Jugando = false;
    }

    /// <summary>
    /// Lanzamiento de bola hacia una posición objetivo
    /// </summary>
    /// <param name="targetPos"></param>
    public void Lanzar(Vector3 targetPos)
    {
        rb.isKinematic = false;

        Vector2 direccion = (targetPos - transform.position).normalized;

        float minY = 0.3f;
        if (direccion.y < minY)
        {
            direccion.y = minY;
            direccion = direccion.normalized;
        }

        rb.velocity = direccion * velocidad;
        Jugando = true;
    }

    /// <summary>
    /// Control de la colisión con la pala para ajustar la dirección
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Pala"))
        {
            Vector2 direccion = new Vector2(CalcularDireccionX(collision.transform), 1f).normalized;
            rb.velocity = direccion * velocidad;
        }


        Vector2 vel = rb.velocity;

        

        vel.x += Random.Range(-0.05f, 0.05f);
        vel.y += -0.05f;
        rb.velocity = vel.normalized * velocidad;
    }

    /// <summary>
    /// Calcula la dirección X en función de la posición de la bola y la pala
    /// </summary>
    /// <param name="palaTransform"></param>
    /// <returns></returns>
    private float CalcularDireccionX(Transform palaTransform)
    {
        float diferenciaX = transform.position.x - palaTransform.position.x;
        float mitadAnchoPala = palaTransform.localScale.x / 2;
        return diferenciaX / mitadAnchoPala;
    }

    /// <summary>
    /// Evento de colisión con el área de muerte que devuelve la bola al BolaManager
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter2D(Collider2D collision)
    {

        Jugando = false;
        GameManager.Instance.BolaManager.AgregarBola(this);
    }

}
