using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This class handles the active gameplay, taking care of
        - player deaths/game-overs
        - keeping track of score
        - win conditions.
    ideally this class should be global.    
*/
public class gameplayManager : MonoBehaviour
{
    public List<GameObject> ships = new List<GameObject>();

    // Variable to keep track of score
    public static int Score;

    // The spawn location
    public Vector3 PlayerSpawnLocation;
    enum States {
        inGame,
        gameOver,
        levelComplete,
        pause
    };

    States currentState;

    // Constructor
    public gameplayManager() {
        currentState = States.inGame;
        //GetShipList();
    }

    // Start is called before the first frame update
    void Start()
    {
        GetShipList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetShipList() {
        //use GameObject.getgameobjectswithtag to get array of ships in play.
        //convert this ship to a nice list.
        GameObject[] shipArray = GameObject.FindGameObjectsWithTag("boat");
        Debug.Log(shipArray.Length);

        // For length of array, copy each array element to the list
        for(int i = 0; i < shipArray.Length; ++i) {
            ships.Add(shipArray[i]);
            Debug.Log(shipArray[i] + "copied to" + ships[i]);
        }
    }

    void OnGameOver() {
        currentState = States.gameOver;
    }

    void OnPause() {
        currentState = States.pause;
    }

    void OnLevelComplete() {
        currentState = States.levelComplete;
    }
}
