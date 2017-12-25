using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level //Un unico nivel
{
    public List<string> m_Rows = new List<string>();

    public int Height
    {
        //Contamos las columnas 
        get
        {
            return m_Rows.Count;
        }
    }
    public int Width
    {
        //El ancho es la longitud de la fila mas larga
        get
        {
            int maxLength = 0;
            foreach (var r in m_Rows)
            {
                if (r.Length > maxLength) maxLength = r.Length;
            }
            return maxLength;
        }
    }
}

public class LevelsManager : MonoBehaviour
{
    public string filename;
    public List<Level> m_Levels;

    void Awake ()
    {
        //Controlamos que exista el texto desde donde cargar los niveles
        TextAsset textAsset = (TextAsset)Resources.Load(filename);
        if (!textAsset)
        {
            Debug.Log("Levels: " + filename + ".txt does no exist!");
            return;
        }
        else
        {
            Debug.Log("Levels imported");
        }

        string completeText = textAsset.text;
        string[] lines;
        lines = completeText.Split(new string[] { "\n" }, System.StringSplitOptions.None);
        m_Levels.Add(new Level());
        for (long i = 0;i < lines.LongLength; i++)
        {
            string line = lines[i];
            if (line.StartsWith(";"))
            {
                Debug.Log("New Level added");
                m_Levels.Add(new Level());
                continue;
            }
            //Siempre que añadimos un nivel se pone al final de la lista
            m_Levels[m_Levels.Count - 1].m_Rows.Add(line);
        }
    }
	
}
