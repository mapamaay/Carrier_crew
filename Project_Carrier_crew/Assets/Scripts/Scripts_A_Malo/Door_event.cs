using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_event : MonoBehaviour
{
    public string EnterTag = "Player";

    public event System.Action<Collider> OnDoorEnter;
    public event System.Action<Collider> OnDoorExit;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == EnterTag)
        {
            OnDoorEnter(other);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == EnterTag)
        {
            OnDoorEnter(other);
        }
    }
}
