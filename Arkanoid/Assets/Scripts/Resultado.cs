using UnityEngine;

[System.Serializable]
public class Resultado
{
    [SerializeField] private int puntuacion;
    [SerializeField] private string tiempo;

    public int Puntuacion => puntuacion;
    public string Tiempo => tiempo;

    public Resultado(string puntuacionStr, string tiempo)
    {
        int.TryParse(puntuacionStr, out puntuacion);
        this.tiempo = tiempo;
    }
}
