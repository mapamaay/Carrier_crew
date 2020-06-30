using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_p2 : MonoBehaviour
{
    private Collider Door_collider;
    private GameObject Player;
    private Collider Player_collider;

    private void Start()
    {
        Door_collider = GetComponent<Collider>();
        Player = GameObject.FindGameObjectWithTag("Player2");
        Player_collider = Player.GetComponent<Collider>();

        Debug.Log("Variables!");
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Impact");

        if (col.gameObject.tag == "Player2")
        {
            Physics.IgnoreCollision(Player_collider, Door_collider);
        }

    }
}

