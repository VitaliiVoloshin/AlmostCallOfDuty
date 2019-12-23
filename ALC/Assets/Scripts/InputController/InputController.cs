using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController
{
    [SerializeField] private KeyCode m_grenadeThrowButton;
    [SerializeField] private KeyCode m_shootButton;
    [SerializeField] private string m_verticalAxis;
    [SerializeField] private string m_horizontalAxis;

    public KeyCode grenadeThrow => m_grenadeThrowButton;

}
