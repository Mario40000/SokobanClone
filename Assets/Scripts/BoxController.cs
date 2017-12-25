using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    //True si una caja ha sido movida a la salida
    public bool m_OnCross;

    //Impedimos el movimiento diagonal
    public bool Move(Vector2 direction)
    {
        //Si el paso esta bloqueado no avanza
        if (BoxBlocked(transform.position , direction))
        {
            return false;
        }
        //Si esta libre avanza
        else
        {
            transform.Translate(direction);
            TestForOnCross();
            return true;
        }

    }
    //Cajas bloqueadas por otras cajas y muros
    bool BoxBlocked (Vector3 position, Vector2 direction)
    {
        Vector2 newPos = new Vector2(position.x, position.y) + direction;
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (var wall in walls)
        {
            if(wall.transform.position.x == newPos.x && wall.transform.position.y == newPos.y)
            {
                return true;
            }
        }
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        foreach (var box in boxes)
        {
            if (box.transform.position.x == newPos.x && box.transform.position.y == newPos.y)
            {
                return true;
            }
        }
        return false;
    }

    void TestForOnCross ()
    {
        GameObject[] crosses = GameObject.FindGameObjectsWithTag("Cross");
        foreach (var cross in crosses)
        {
            if (transform.position.x == cross.transform.position.x && transform.position.y == cross.transform.position.y)
            {
                //Sobre una cruz
                GetComponent<SpriteRenderer>().color = Color.green;
                m_OnCross = true;
                return;
            }
        }
        GetComponent<SpriteRenderer>().color = Color.white;
        m_OnCross = false;
    }
	
}
