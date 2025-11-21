using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemigo : MonoBehaviour
{
    public GameObject[] EnemigoPrefab;
    public Transform[] puntosDeSpawn;
    public float intervaloDeSpawn = 3.0f;
    private float contador;

    private int contadorOleadas = 0;
    private float velocidadEnemigo = 0.3f;
    private int vidaEnemigo = 0;
   
    void Update()
    {
        contador+= Time.deltaTime;
        if(contador >= intervaloDeSpawn)
        {
            SpawnOleada();
        }
    }

    private void SpawnOleada()
    {
        int pattern = UnityEngine.Random.Range(0, 4);

        if (contadorOleadas % 20 == 0)
        {
            vidaEnemigo++; 
            Debug.Log("Vida de los enemigos aumentada a: " + vidaEnemigo);
        }
        switch (pattern)
        {
            case 0: // Patrón lineal
                SpawnRow();
                break;
            case 1: // Patrón alterno
                SpawnAlternating();
                break;
            case 2: // Patrón con huecos aleatorios
                SpawnRandomHoles();
                break;
            case 3:
                SpawnAlternatingInverse();
                break;
        }
        contadorOleadas++;
    }
    private void SpawnRow()
    {
        foreach (var punto in puntosDeSpawn)
        {
            SpawnEnemigo(punto);
        }
        contador = 0;
    }
    private void SpawnAlternating()
    {
        for (int i = 0; i < puntosDeSpawn.Length; i++)
        {
            if (i % 2 == 0)
            {
                SpawnEnemigo(puntosDeSpawn[i]);
            }
        }
        contador = 0;
    }
    private void SpawnAlternatingInverse()
    {
        for (int i = 0; i < puntosDeSpawn.Length; i++)
        {
            if (i % 3 == 0)
            {
               SpawnEnemigo(puntosDeSpawn[i]);
            }
        }
        contador = 0;
    }
    private void SpawnRandomHoles()
    {
        foreach (Transform punto in puntosDeSpawn)
            if (UnityEngine.Random.value > 0.4f)
                SpawnEnemigo(punto);
        contador = 0;
    }

    private void SpawnEnemigo(Transform point)
    {
        GameObject prefab = (UnityEngine.Random.value < 0.05f) ? EnemigoPrefab[1] : EnemigoPrefab[0];
        GameObject enemigo = Instantiate(prefab, point.position, Quaternion.identity);
        enemigo.GetComponent<Enemigo>().Init(velocidadEnemigo, vidaEnemigo);
    }
}
