using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Definimos los elementos del nivel
[System.Serializable]
public class LevelElement
{
    public string m_Character;
    public GameObject m_Prefab;
}

public class LevelBuilder : MonoBehaviour
{
    public int m_CurrentLevel;
    public List<LevelElement> m_LevelElements;
    private Level m_Level;

    //Metodo para recoger los prefabs
    GameObject GetPrefab(char c)
    {
        LevelElement levelElement = m_LevelElements.Find(le => le.m_Character == c.ToString());
        if (levelElement != null)
        {
            //Debug.Log("OK");
            return levelElement.m_Prefab;
        }
        else
        {
            //Debug.Log("NULL");
            return null;
            
        }
            
    }
    //Metodo para llamar al proximo nivel
    public void NextLevel ()
    {
        m_CurrentLevel++;
        if(m_CurrentLevel >= GetComponent<LevelsManager>().m_Levels.Count)
        {
            //Si los acabamos todos volvemos al primero
            m_CurrentLevel = 0;
        }
    }

    //Metodo para construir el nivel
    public void Build ()
    {
        m_Level = GetComponent<LevelsManager>().m_Levels[m_CurrentLevel];
        //Reseteamos las coordenadas para que nuestro nivel se cree a 0,0
        int startx = -m_Level.Width / 2;
        int x = startx;
        int y = -m_Level.Height / 2;
        foreach (var row in m_Level.m_Rows)
        {
            foreach (var ch in row)
            {
                Debug.Log(ch);
                GameObject prefab = GetPrefab(ch);
                if (prefab)
                {
                    //Instanciamos el prefab que toque en las coordenadas x,y
                    Debug.Log(prefab.name);
                    Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
                }
                x++;
            }
            y++;
            x = startx;
        }
    }
	
}
