using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EditorController : MonoBehaviour
{

    public GameObject exitButton;
    public GameObject saveButton;
    public GameObject saveText;

    public GameObject[] tiles;

    void Start ()
    {
        saveText.SetActive(false);
    }

	//Metodo pata volver al menu inicial
    public void BackToTitle ()
    {
        SceneManager.LoadScene("TitleScene");
    }

    //Metodo para guardar el nivel
    public void SaveLevel ()
    {
        TextWriter();
    }

    //Recoenctamos los botones ya hacemos aparecer y desaparecer el mensaje de salvado
    IEnumerator SaveComplete ()
    {
        saveText.SetActive(true);
        yield return new WaitForSeconds(2);
        saveText.SetActive(false);
        exitButton.SetActive(true);
        saveButton.SetActive(true);
    }

    //Logica de guardado del nivel
    public void TextWriter ()
    {
        exitButton.SetActive(false);
        saveButton.SetActive(false);

        int contador = 0;
        string level = "";

        string path = "Assets/Resources/Levels.txt";
        if (File.Exists(path))
        {
            StreamWriter writer = File.AppendText(path);
            //Ponemos el separador del nivel
            writer.WriteLine(";");
            

            //Leemos los dropText
            for (int i = 0;i < tiles.Length;i++)
            {
                GameObject child = tiles[i];

                if (child.GetComponent<Dropdown>().value == 0)
                {
                    level += " ";
                }
                else if (child.GetComponent<Dropdown>().value == 1)
                {
                    level += "@";
                }
                else if (child.GetComponent<Dropdown>().value == 2)
                {
                    level += "#";
                }
                else if (child.GetComponent<Dropdown>().value == 3)
                {
                    level += ".";
                }
                else if (child.GetComponent<Dropdown>().value == 4)
                {
                    level += "$";
                }

                contador++;

                if (contador == 12)
                {
                    writer.WriteLine(level);
                    contador = 0;
                    level = "";
                }

            }
            writer.Close();
            StartCoroutine(SaveComplete());
        }
        

    }


}
