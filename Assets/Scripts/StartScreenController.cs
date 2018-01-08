using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenController : MonoBehaviour
{

    //Metodo para el boton de empezar a jugar
	public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    //Metodo para el boton de entrar al editor
    public void StartEditor()
    {
        SceneManager.LoadScene("EditorScene");
    }

    //Metodo para salir del juego
    public void ExitGame()
    {
        Application.Quit();
    }
}
