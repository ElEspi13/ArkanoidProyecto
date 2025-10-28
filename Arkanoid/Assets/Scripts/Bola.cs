using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class Bola : MonoBehaviour
{
    private float speed = 8f;
    private bool jugando = false;
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

        if (!jugando)
        {
            Vector3 palaPos = pala.position;
            transform.position = new Vector3(palaPos.x, palaPos.y + 0.5f, 0f);


            if (Input.GetKeyDown(KeyCode.Space))
            {
                jugando = true;
                rb.isKinematic = false;
                LanzarBola();
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Pala"))
        {
            Vector2 direccion = new Vector2(CalcularDireccionX(collision.transform), 1f).normalized;
            rb.velocity = direccion * speed;
        }
    }
    private float CalcularDireccionX(Transform palaTransform)
    {
        float diferenciaX = transform.position.x - palaTransform.position.x;
        float mitadAnchoPala = palaTransform.localScale.x / 2;
        return diferenciaX / mitadAnchoPala;
    }
    private void LanzarBola()
    {
        Vector2 direccion = new Vector2(Random.Range(-0.5f, 0.5f), 1f).normalized;
        rb.velocity = direccion * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        jugando = false;
    }

}
