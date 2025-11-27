using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public static class ResultadosManager
{
    private static string fileName = "resultado.json";

    private static string FilePath =>
        Path.Combine(Application.persistentDataPath, fileName);

    /// <summary>
    /// Carga todos los resultados del archivo. 
    /// Si no existe devuelve una lista vacía.
    /// </summary>
    public static List<Resultado> CargarResultados()
    {
        if (!File.Exists(FilePath))
            return new List<Resultado>();

        try
        {
            string json = File.ReadAllText(FilePath);
            return JsonUtility.FromJson<Wrapper>(json).lista;
        }
        catch (Exception e)
        {
            Debug.LogError("Error al cargar JSON: " + e);
            return new List<Resultado>();
        }
    }

    /// <summary>
    /// Guarda una lista de resultados en el archivo.
    /// </summary>
    public static void GuardarResultados(List<Resultado> lista)
    {
        Wrapper wrapper = new Wrapper { lista = lista };
        string json = JsonUtility.ToJson(wrapper, true);

        try
        {
            File.WriteAllText(FilePath, json);
            Debug.Log("Resultados guardados en: " + FilePath);
        }
        catch (Exception e)
        {
            Debug.LogError("Error al escribir JSON: " + e);
        }
    }

    /// <summary>
    /// Agrega un resultado, ordena y guarda solo top 10.
    /// </summary>
    public static void AgregarResultado(Resultado nuevo)
    {
        List<Resultado> lista = CargarResultados();

        lista.Add(nuevo);

        // Ordenar por puntuación descendente
        lista.Sort((a, b) => b.Puntuacion.CompareTo(a.Puntuacion));

        // Limitar a 10
        if (lista.Count > 10)
            lista = lista.GetRange(0, 10);

        GuardarResultados(lista);
    }

    /// <summary>
    /// Necesario porque JSONUtility no soporta listas sueltas.
    /// </summary>
    [Serializable]
    private class Wrapper
    {
        public List<Resultado> lista;
    }
}
