using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TLinteraction : MonoBehaviour
{
    public bool player1InRange;
    public bool player2InRange;
    bool interacted;
    bool lookingPoint;
    bool moovedToPoint;
    GameObject Player1Object;
    GameObject Player2Object;

    private void Start()
    {
       
    }

    private void OnTriggerEnter(Collider CollidingCollider)
    {
        if(CollidingCollider.gameObject.CompareTag("Player 1"))
        {
            player1InRange = true;
            Debug.Log("Player 1 in range");
            Player1Object = CollidingCollider.gameObject;
        }

        if (CollidingCollider.gameObject.CompareTag("Player 2"))
        {
            player2InRange = true;
            Debug.Log("Player 2 in range");
            Player2Object = CollidingCollider.gameObject;
        }
    }

    private void OnTriggerExit(Collider CollidingCollider)
    {
        if(CollidingCollider.gameObject.CompareTag("Player 1"))
        {
            player1InRange = false;
            Debug.Log("Player 1 out of range");
        }

        if (CollidingCollider.gameObject.CompareTag("Player 2"))
        {
            player2InRange = false;
            Debug.Log("Player 2 out of range");
        }
    }

    void Update()
    {
        
        if(interacted == true && lookingPoint == false)
        {
            Quaternion lookdirection = (Quaternion.LookRotation(Vector3.RotateTowards(Player1Object.transform.forward, this.transform.position - Player1Object.transform.position, Time.deltaTime * 5, 0)));
            Player1Object.transform.rotation.Set(0,lookdirection.y,0,lookdirection.w);

           if(Vector3.Angle(Player1Object.transform.forward, this.transform.position - Player1Object.transform.position) <= 1.5)
            {
                lookingPoint = true;
            }
        }

        if (lookingPoint == true && moovedToPoint == false)
        {

        }
    }
}
