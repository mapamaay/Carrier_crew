using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public Door_event DoorEvent;


    // Start is called before the first frame update
    void Start()
    {
        DoorEvent.OnDoorEnter += DoorEvent_OnDoorEnter;
        DoorEvent.OnDoorExit += DoorEvent_OnDoorExit;
    }

    

    private void DoorEvent_OnDoorEnter(Collider obj)
    {
        throw new System.NotImplementedException();
    }



    private void DoorEvent_OnDoorExit(Collider obj)
    {
        throw new System.NotImplementedException();
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
