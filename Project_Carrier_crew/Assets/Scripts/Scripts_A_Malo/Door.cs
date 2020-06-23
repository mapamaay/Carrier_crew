using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public Door_event DoorEvent;

    Collider Door_collider;

    // Start is called before the first frame update
    void Start()
    {
        DoorEvent.OnDoorEnter += DoorEvent_OnDoorEnter;
        DoorEvent.OnDoorExit += DoorEvent_OnDoorExit;
        Door_collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

    }   


    private void DoorEvent_OnDoorEnter(Collider obj)
    {
        Door_collider.enabled = false;
    }



    private void DoorEvent_OnDoorExit(Collider obj)
    {
        Door_collider.enabled = true;
    }


}
