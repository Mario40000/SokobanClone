using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public LevelBuilder m_LevelBuilder;
    public GameObject m_NextButton;
    private bool m_ReadyForInput;
    private PlayerController m_Player; 

    void Start ()
    {
        m_LevelBuilder.Build();
        m_Player = FindObjectOfType<PlayerController>();
    }

    void Update ()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveInput.Normalize();
        if (moveInput.sqrMagnitude > 0.5)//Boton presionado
        {
            if (m_ReadyForInput)
            {
                m_ReadyForInput = false;
                m_Player.Move(moveInput);
                //m_NextButton.SetActive(IsLevelComplete());
            }
        }
        else
        {
            m_ReadyForInput = true;
        }
    }
	
}
