using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botones : MonoBehaviour
{

    //Eventos de los botones
    #region
    public void OnClick_BotonJugar()
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene("Juego");
    }

    public void OnClick_BotonSalir()
    {
        Application.Quit();
    }
    public void OnClick_BotonMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuInicio");
    }
    #endregion
}
