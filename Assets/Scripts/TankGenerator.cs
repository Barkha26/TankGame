using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankGenerator : MonoBehaviour
{
    public int tankCount = 3;
    public int greenTankCount = 2;
    
    public GameObject tankPrefab;
    public List<Tank> allTanks = new List<Tank>();
    public float tankRotationSpeed = 10f;
    public int tankMovementSpeed = 1;
    public int greenTankHealth = 1;
    public int rustTankHealth = 2;

    public void GenerateTanks(Board board)
    {
        List<Node> tankNodes = new List<Node>();

        for (int i = 0; i < tankCount; i++)
        {
            while (true)
            {
                int xIndex = Random.Range(0, board.boardSize);
                int yIndex = Random.Range(0, board.boardSize);

                Node newNode = board.IsValidNode(new Vector2Int(xIndex, yIndex));

                if (newNode && !tankNodes.Contains(newNode))
                {
                    tankNodes.Add(newNode);

                    Quaternion rotation = transform.rotation;
                    int health = greenTankHealth;
                    
                    if (GetComponentInParent<PlayerManager>().allPlayers[1].playerName == transform.parent.name)
                        rotation = Quaternion.Euler(0, 180, 0);

                    GameObject instance = Instantiate(tankPrefab, newNode.transform.position, rotation);
                    instance.transform.SetParent(transform);
                    Tank currentTank = instance.GetComponent<Tank>();
                    Tank.TankType currentTankType = Tank.TankType.greenTank;

                    if (greenTankCount <= 0)
                    {
                        currentTankType = Tank.TankType.rustTank;
                        health = rustTankHealth;
                    }

                    currentTank.Init(newNode, currentTankType, health);
                    currentTank.tankRotationSpeed = tankRotationSpeed;
                    currentTank.tankMovementSpeed = tankMovementSpeed;
                    greenTankCount--;
                 
                    allTanks.Add(currentTank);

                    break;
                }
            }
        }
    }
}
