using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Transform ParedDerecha;
    private Transform ParedIzquierda;
    private float speed = 10f;         
    private float minX, maxX;      

    void Awake()
    {
        ParedDerecha = GameObject.FindWithTag("Derecha").transform;
        ParedIzquierda = GameObject.FindWithTag("Izquierda").transform;
        UpdateLimits();
        
    }
    /// <summary>
    /// Actualiza la posición de la pala según la entrada del usuario y limita su movimiento dentro de los bordes definidos.
    /// </summary>
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 position = transform.position;
        position.x += horizontal * speed * Time.deltaTime;
        position.x = Mathf.Clamp(position.x, minX, maxX);
        transform.position = position;


 
      
    }

    /// <summary>
    /// Actualiza los límites de movimiento de la pala según la posición de las paredes
    /// </summary>
    private void UpdateLimits()
    {
        float MitadAnchoPala= transform.localScale.x/2;
        float MitadAnchoParedDerecha= ParedDerecha.transform.localScale.x/2;
        float MitadAnchoParedIzquierda = ParedIzquierda.transform.localScale.x / 2;

        minX = ParedIzquierda.position.x + MitadAnchoParedIzquierda + MitadAnchoPala;
        maxX = ParedDerecha.position.x - MitadAnchoParedDerecha - MitadAnchoPala;
    }
}
