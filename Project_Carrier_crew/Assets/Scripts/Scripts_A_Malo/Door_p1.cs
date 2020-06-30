using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_p1 : MonoBehaviour
{
    private Collider Door_collider;
    private GameObject Player;
    private Collider Player_collider;

    private void Start()
    {
        Door_collider = GetComponent<Collider>();
        Player = GameObject.FindGameObjectWithTag("Player1");
        Player_collider = Player.GetComponent<Collider>();

        Debug.Log("Variables!");
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Impact");

        if (col.gameObject.tag == "Player1")
        {
            Physics.IgnoreCollision(Player_collider, Door_collider);
        }

    }
}

