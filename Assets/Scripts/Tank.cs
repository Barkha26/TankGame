using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public enum TankType
    {
        greenTank, //require 1 shot to kill
        rustTank //require 2 shot to kill
    }

    public Material greenTankMaterial;
    public Material rustTankMaterial;

    [HideInInspector]
    public float tankRotationSpeed = 10f;
    public Transform shootpoint;
    public TankType currentTankType = TankType.greenTank;
    public Node currentNode;
    public int tankMovementSpeed;
	public GameObject bulletPrefab;
    public GameObject tankExplosionEffect;
    public float shootSpeed = 10;
    int health = 0;

	public void Init(Node node, TankType tankType , int tankHealth)
    {
        currentNode = node;
        currentTankType = tankType;
        health = tankHealth;
        SetTankColor();
    }

    public IEnumerator MoveToNode(Node node)
    {
        Debug.Log("moving tank to node " + node.name);
        currentNode = node;
        Quaternion startRotation = transform.rotation;

        Vector3 direction = node.transform.position - transform.position;
        while (true)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), tankRotationSpeed * Time.deltaTime);

            if (Vector3.Cross(transform.forward, direction) == Vector3.zero)
                break;

            yield return null;
        }
        while (true)
        {
            transform.Translate(transform.forward * tankMovementSpeed * Time.deltaTime, Space.World);

            float distance = Vector3.Distance(transform.position, node.transform.position);

            if(distance <= 0.1f)
                break;

            yield return null;
        }
        while (true)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, startRotation, tankRotationSpeed * Time.deltaTime);

            if (transform.rotation == startRotation)
                break;

            yield return null;
        }
    }

    void SetTankColor()
    {
        switch (currentTankType)
        {
            case TankType.greenTank :
                ChangeColor(greenTankMaterial);
                break;
            default:
                ChangeColor(rustTankMaterial);
                break;
        }

        //local method to change color of the tank
        void ChangeColor(Material mat)
        {
            foreach (var item in GetComponentsInChildren<MeshRenderer>())
            {
                item.material = mat;
            }
        }
    }

	public void Shoot()
	{
        //Instantiate/Create Bullet
        Quaternion rotation = transform.rotation;

        if (GetComponentInParent<PlayerManager>().allPlayers[1].playerName == transform.parent.name)
            rotation = Quaternion.Euler(0, 180, 0);

        GameObject bullet = Instantiate(bulletPrefab, shootpoint.position, rotation);

        bullet.GetComponent<BulletScript>().Init(currentNode);

    }

    public void TakeDamage()
    {
        health--;

        //destroy tank with particle effect
        if (health == 0)
        {
            GameObject explosionEffect = Instantiate(tankExplosionEffect, transform);
            explosionEffect.GetComponent<ParticleSystem>().Play();

            Destroy(gameObject, .5f);
        }
      
    }
}
