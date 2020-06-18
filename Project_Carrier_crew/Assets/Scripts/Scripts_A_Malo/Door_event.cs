using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_event : MonoBehaviour
{
    public event System.Action<Collider> OnDoorEnter;
    public event System.Action<Collider> OnDoorExit;


    private void OnTriggerEnter(Collider other)
    {
        OnDoorEnter(other);
    }
    private void OnTriggerExit(Collider other)
    {
        OnDoorEnter(other);
    }
}
