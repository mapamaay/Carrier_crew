using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    public GameObject Poutre;
    public event Action OnInteraction;


    private void Awake()
    {
        if (this.CompareTag("Player 1"))
        {
            
        }
        else
        {
            
        }
    }

    private void OnInteract()
    {
        OnInteraction?.Invoke();
    }

    private void Testing_OnInteractionPressed(object sender, EventArgs e)
    {
        Debug.Log("Interacting!");
    }
    void Update()
    {
        
    }
}
