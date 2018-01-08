using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public LevelBuilder m_LevelBuilder;
    public GameObject m_NextButton;
    public Text stepText;
    public Text timeText;
    private bool m_ReadyForInput;
    private PlayerController m_Player;
    private float t;

    void Start ()
    {
        m_NextButton.SetActive(false);
        ResetScene();
        
    }

    void Update ()
    {
        //Contador de tiempo
        if (!IsLevelComplete())
        {
            t = Time.time - StaticData.currentTime;
            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f2");
            timeText.text = minutes + ":" + seconds;
        }
        else
        {
            m_ReadyForInput = false;
        }


        //Contador de pasos
        stepText.text = "Steps: " + StaticData.steps;

        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveInput.Normalize();
        
        if (moveInput.sqrMagnitude > 0.5)//Boton presionado
        {
            if (m_ReadyForInput)
            {
                if (!IsLevelComplete())
                {
                    StaticData.steps++;
                }
                m_ReadyForInput = false;
                m_Player.Move(moveInput);
                m_NextButton.SetActive(IsLevelComplete());
                
            }
        }
        else
        {
            m_ReadyForInput = true;
        }
        
    }

    //Reseteamos los datos estaticos
    public void DataReset ()
    {
        StaticData.steps = 0;
        StaticData.currentTime = 0.0f;
    }

    //Comprobamos si hemos terminado el nivel
    bool IsLevelComplete ()
    {
        BoxController[] boxes = FindObjectsOfType<BoxController>();
        foreach (var box in boxes)
        {
            if (!box.m_OnCross) return false;
        }
        //Tiempo final
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");
        timeText.text = minutes + ":" + seconds;

        m_Player = null;

        return true;
    }

    //Cambiar al proximo nivel
    public void NextLevel ()
    {
        m_NextButton.SetActive(false);
        m_LevelBuilder.NextLevel();
        StartCoroutine(ResetSceneASync());
        DataReset();
    }

    //Resetear pantalla
    public void ResetScene ()
    {
        DataReset();
        StartCoroutine(ResetSceneASync());
        StaticData.currentTime = Time.time;
    }

    //Volvemos al menu inicial
    public void ReturnMenu ()
    {
        SceneManager.LoadScene("TitleScene");
    }

    //Corrutina para descargar el viejo nivel y cargar el nuevo
    IEnumerator ResetSceneASync ()
    {
        if (SceneManager.sceneCount > 1)
        {
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync("LevelScene");
            while(!asyncUnload.isDone)
            {
                yield return null;
                Debug.Log("Unloading...");
            }
            Debug.Log("Unload done");
            Resources.UnloadUnusedAssets();
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("LevelScene", LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
            Debug.Log("Loading...");
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("LevelScene"));
        m_LevelBuilder.Build();
        m_Player = FindObjectOfType<PlayerController>();
        Debug.Log("Level Loaded");
        StaticData.currentTime = Time.time;
    }
	
}
