using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BeamManager : MonoSingleton<BeamManager>
{
    public static GameObject PlayerNorth = null;
    public static GameObject PlayerSouth = null;
    public static GameObject Player1IsIn;
    public static GameObject Player2IsIn;
    public GameObject BeamRef;

    [SerializeField]
    private GameObject _playerNorth;
    [SerializeField]
    private GameObject _playerSouth;
    [SerializeField]
    private GameObject _Player1IsIn;
    [SerializeField]
    private GameObject _Player2IsIn;
    [SerializeField]
    private int _numberOfPlayerRaising = 0;

    private int frameAnimation = 0;
    private int totalFrameAnimation = 30;
    public static bool BeamIsBeingRaised = false;

    //EVENTS
    // Event : Reinitialisation
    public delegate void OnReinitialization(int frameValue);
    public static event OnReinitialization onReinitialization;

    public void OnEnable()
    {
        PlayerController1.onRaiseBeam += OnCheckNumberOfPlayerRaising;
    }

    public void OnCheckNumberOfPlayerRaising(int _playerRaising)
    {
        _numberOfPlayerRaising +=_playerRaising;
    }

    public void OnDisable()
    {
        PlayerController1.onRaiseBeam -= OnCheckNumberOfPlayerRaising;
    }

    public void Reinitialization(int frameValue)
    {
        if (onReinitialization != null)
        {
            onReinitialization(frameAnimation);
        }
    }

    private void FixedUpdate()
    {
        _playerNorth = PlayerNorth;
        _playerSouth = PlayerSouth;
        _Player1IsIn = Player1IsIn;
        _Player2IsIn = Player2IsIn;

        if (_numberOfPlayerRaising == 2)
        {
            BeamIsBeingRaised = true;
        }
        if (BeamIsBeingRaised)
        {
            frameAnimation += 1;

            if (frameAnimation < totalFrameAnimation)
            {
                BeamRef.transform.localPosition = new Vector3(BeamRef.transform.localPosition.x, BeamRef.transform.localPosition.y + 0.13f, BeamRef.transform.localPosition.z);
            }
            if (frameAnimation == totalFrameAnimation)
            {
                frameAnimation = 0;
                BeamIsBeingRaised = false;
                BeamRef.transform.parent.transform.position = BeamRef.transform.position;
                BeamRef.transform.localPosition = new Vector3(0, 0, 0);
                
            } // REINITIALISATION PLAYERS
            Reinitialization(frameAnimation);
        }
    }
}
