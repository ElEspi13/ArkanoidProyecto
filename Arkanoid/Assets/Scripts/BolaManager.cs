using System.Collections.Generic;
using UnityEngine;

public class BolaManager : MonoBehaviour
{
    public Move pala; 
    public LineRenderer lineRenderer;
    public List<Bola> bolasEnPala = new List<Bola>();

    void Update()
    {
        UpdatePointer();


        foreach (Bola b in bolasEnPala)
        {
            if (!b.Jugando)
            {
                b.PosicionarSobrePala(pala.transform.position);
            }


        }


        if (Input.GetKeyDown(KeyCode.Space) && bolasEnPala.Count > 0)
        {
            Bola b = bolasEnPala[0];
            b.Lanzar(lineRenderer.GetPosition(1));
            bolasEnPala.RemoveAt(0); 
        }
    }
    private void UpdatePointer()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector3 direction = (mousePos - pala.transform.position).normalized;

        float maxAngle = 60f;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 
        angle = Mathf.Clamp(angle, 90f - maxAngle, 90f + maxAngle); 
        float rad = angle * Mathf.Deg2Rad;
        direction = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f);


        Vector3 offset = new Vector3(0, 0.5f, 0f); 
        Vector3 startPos = pala.transform.position + offset;
        Vector3 endPos = startPos + direction * 3f;


        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }


    public void AgregarBola(Bola b)
    {
        if (!bolasEnPala.Contains(b))
        {
            bolasEnPala.Add(b);
        }
    }
}
