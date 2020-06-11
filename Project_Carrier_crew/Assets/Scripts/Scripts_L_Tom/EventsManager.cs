using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public static EventsManager current;
    public event Action onPlayer1Interaction;
    public event Action onPlayer2Interaction;

    private void Awake()
    {
        current = this;
    }

    public void Player1Interaction()
    {
        onPlayer1Interaction?.Invoke();
    }
    
    public void Player2Interaction()
    {
        onPlayer2Interaction?.Invoke();
    }
}
