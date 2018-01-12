using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileColorController : MonoBehaviour
{
    Dropdown dropDown;
    Image ima;

    // Use this for initialization
    //Usamos un listener para saber cuando se cambia de opcion el dropDown
    void Start()
    {
        dropDown = GetComponent<Dropdown>();
        ima = GetComponent<Image>();
        dropDown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(dropDown);
        });

    }
    //Cambiamos el color del dropDown segun la opcion elegida
    void DropdownValueChanged (Dropdown change)
    {
        if(dropDown.value == 0)
        {
            ima.color = Color.gray;
                
        }
        else if(dropDown.value == 1)
        {
            ima.color = Color.blue;
        }
        else if(dropDown.value == 2)
        {
            ima.color = Color.red;
        }
        else if(dropDown.value == 3)
        {
            ima.color = Color.green;
        }
        else if(dropDown.value == 4)
        {
            ima.color = Color.black;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
