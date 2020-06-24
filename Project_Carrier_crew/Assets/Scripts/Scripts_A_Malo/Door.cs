using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Collider Door_collider;

    private void Start()
    {
        Door_collider = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            Door_collider.enabled = !Door_collider.enabled;
            Debug.Log("Collider.enabled=" + Door_collider.enabled);
        }
            
    }

    void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            Door_collider.enabled = Door_collider.enabled;
            Debug.Log("Collider.enabled=" + Door_collider.enabled);
        }

    }
}

