using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BeamTrigger : MonoBehaviour
{
    public delegate void PlayerInBeamTrigger(GameObject _triggerObj);   //event
    public static event PlayerInBeamTrigger playerInBeamTrigger;        //event

    //VARIABLES
    public enum direction
    {
        North,
        South,
    };

    [SerializeField]
    private direction _pointDirection;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player 1" || other.tag == "Player 2")
        {
            if (_pointDirection == direction.North)
            {
                if (BeamManager.PlayerNorth == null) //Si l'autre joueur n'est pas au Nord
                {
                    if (other.tag == "Player 1")
                    {
                        BeamManager.Player1IsIn = gameObject;
                    }

                    else if (other.tag == "Player 2")
                    {
                        BeamManager.Player2IsIn = gameObject;
                    }

                    BeamManager.PlayerNorth = other.gameObject;
                }
            }
            else if (_pointDirection == direction.South)
            {
                if (BeamManager.PlayerSouth == null) //Si l'autre joueur n'est pas au Sud
                {
                    if (other.tag == "Player 1")
                    {
                        BeamManager.Player1IsIn = gameObject;
                    }

                    else if (other.tag == "Player 2")
                    {
                        BeamManager.Player2IsIn = gameObject;
                    }

                    BeamManager.PlayerSouth = other.gameObject;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (BeamManager.PlayerNorth != null && other.tag == BeamManager.PlayerNorth.tag)
        {
            if (_pointDirection == direction.North)
            {
                BeamManager.PlayerNorth = null;

            }
        }
        else if (BeamManager.PlayerSouth != null && other.tag == BeamManager.PlayerSouth.tag)
        {
            if (_pointDirection == direction.South)

            {
                BeamManager.PlayerSouth = null;
            }
        }
    }
}