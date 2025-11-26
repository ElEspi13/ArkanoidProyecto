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

    /// <summary>
    /// Controla el temporizador y genera una nueva oleada cuando el intervalo se cumple.
    /// Una oleada consiste en un patrón de enemigos instanciado en los puntos de spawn.
    /// </summary>
    void Update()
    {
        contador+= Time.deltaTime;
        if(contador >= intervaloDeSpawn)
        {
            SpawnOleada();
        }
    }

    /// <summary>
    /// Este metodo elige un patrón de spawn aleatorio y genera una oleada de enemigos en puntos especificos.
    /// Aumenta la dificultad cada cierto número de oleadas.
    /// </summary>
    private void SpawnOleada()
    {
        int pattern = UnityEngine.Random.Range(0, 4);

        if (contadorOleadas % 10 == 0)
        {
            vidaEnemigo++;
            GameManager.Instance.AumentarBolas();
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

    //Metodos de patrones de spawn
    #region
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
    #endregion

    /// <summary>
    /// Controla la creación de enemigos en patrones predefinidos..
    /// Los enemigos se generan desde los puntos configurados en la escena.
    /// </summary>
    /// <param name="point"></param>
    private void SpawnEnemigo(Transform point)
    {
        GameObject prefab;

        float rand = UnityEngine.Random.value; 

        if (rand < 0.95f)
        {

            prefab = EnemigoPrefab[0];
        }
        else
        {
            int indexRaro = UnityEngine.Random.Range(1, EnemigoPrefab.Length); 
            prefab = EnemigoPrefab[indexRaro];
        }

        GameObject enemigo = Instantiate(prefab, point.position, Quaternion.identity);
        enemigo.GetComponent<Enemigo>().Init(velocidadEnemigo, vidaEnemigo);
    }

}
