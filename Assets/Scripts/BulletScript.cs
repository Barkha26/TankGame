using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public Node startNode;
    public Node targetNode;
    public Board targetBoard;
    public GameObject bulletExplosionEffectPrefab;


    public void Init(Node node)
    {
        startNode = node;

        foreach (var board in FindObjectsOfType<Board>())
        {
            bool isTarget = false;
            foreach (var n in board.nodes)
            {
                if (n == startNode)
                {
                    isTarget = true;
                    break;
                }
            }

            if (!isTarget)
            {
                targetBoard = board;
                //search for the target node
                foreach (var n in board.nodes)
                {
                    if (n.xIndex == startNode.xIndex && n.yIndex == startNode.yIndex)
                    {
                        targetNode = n;
                        break;
                    }
                }
            }
        }

        StartCoroutine(MoveBullet());
    }

   IEnumerator MoveBullet()
    {
        while (true)
        {
            Vector3 movementPosition = targetNode.transform.position - transform.position;
            movementPosition.y = 0;

            if (FindObjectOfType<PlayerManager>().currentPlayer.playerName != FindObjectOfType<PlayerManager>().allPlayers[0].playerName)
                movementPosition *= -1;

            transform.Translate(movementPosition * Time.deltaTime);

            MoveCameraWithBullet(transform);

            if (movementPosition.magnitude < .1f)
            {
                //explosion effect
                yield return StartCoroutine(TriggerExplosion());

                MoveCameraWithBullet(null);
                FindObjectOfType<PlayerManager>().ShootingComplete();

                Destroy(gameObject, .5f);
                break;
            }
            yield return null;
        }
    }

    IEnumerator TriggerExplosion()
    {
        GameObject explosionEffect = Instantiate(bulletExplosionEffectPrefab, transform);
        explosionEffect.GetComponent<ParticleSystem>().Play();

        GetComponent<MeshRenderer>().enabled = false;
        Destroy(explosionEffect, 3f);

        SearchForTankOnTargetNode();

        yield return new WaitForSeconds(3f);
    }

    private void MoveCameraWithBullet(Transform target)
    {
        Camera.main.transform.SetParent(target);
    }

    void SearchForTankOnTargetNode()
    {
        foreach (var tank in targetBoard.transform.parent.GetComponentInChildren<TankGenerator>().allTanks)
        {
            if(tank.currentNode == targetNode)
            {
                tank.TakeDamage();
                Debug.Log("damage");
            }
        }
    }    
}
