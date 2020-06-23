using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [System.Serializable]
    public class Player
    {
        public int playerId;
        public string playerName;
        public int playerScore;
        //public Camera playerCamera;
        public Transform playerBoardPosition;
        public Transform playerCameraPosition;
        public GameObject playerObject;
    }

    public GameObject playerPrefab;
    public Player[] allPlayers = new Player[2];
    public int totalNumberOfTurn = 20;
    Queue<Player> playerQueue = new Queue<Player>();
    public Player currentPlayer;
    bool isShootingComplete = false;

    private void Start()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        StartCoroutine(GameInit());
        yield return null;

        StartCoroutine(GameLoop());
        yield return null;

        StartCoroutine(GameEnd());
        yield return null;
    }

    IEnumerator GameInit()
    {
        //player init

        foreach (var player in allPlayers)
        {
            GameObject instance = Instantiate(playerPrefab, player.playerBoardPosition);

            instance.name = player.playerName;
            player.playerObject = instance;
            instance.GetComponentInChildren<Board>().GenerateBoard();

            playerQueue.Enqueue(player);
        }
        yield return null;
    }

    IEnumerator GameLoop()
    {
        while (totalNumberOfTurn > 0)
        {
            currentPlayer = playerQueue.Dequeue();
            ClickToMove clickToMove = currentPlayer.playerObject.GetComponentInChildren<ClickToMove>();

            clickToMove.isTurn = true;
            SetCameraPosition(currentPlayer.playerCameraPosition);

            while (clickToMove.isTurn == true || !isShootingComplete)
            {
                yield return null;
            }

            playerQueue.Enqueue(currentPlayer);
            isShootingComplete = false;
        }
    }

    IEnumerator GameEnd()
    {
        Debug.Log("Game Ended");
        yield return null;
    }

    void SetCameraPosition(Transform cameraPosition)
    {
        Camera.main.transform.localPosition = cameraPosition.localPosition;
        Camera.main.transform.localRotation = cameraPosition.localRotation;
    }

    public void ToggleTankShooting()
    {
        if (currentPlayer != null)
            currentPlayer.playerObject.GetComponentInChildren<ClickToMove>().ToggleTankShooting();

    }

    public void ShootingComplete()
    {
        isShootingComplete = true;
        totalNumberOfTurn--;
    }

}
