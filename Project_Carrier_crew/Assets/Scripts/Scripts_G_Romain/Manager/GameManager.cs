using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameManager : MonoSingleton<GameManager>
{
    public static GameObject Player1;
    public static GameObject Player2;

    public GameObject _player1;
    public GameObject _player2;

    private void Update()
    {
        _player1 = Player1;
        _player2 = Player2;
    }

}
