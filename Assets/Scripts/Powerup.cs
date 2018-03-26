using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{


    [SerializeField]
    bool isHeavy = false;

    [SerializeField]
    bool isSpeedBoost = false;

    [SerializeField]
    bool isPowerShot = false;

    [SerializeField]
    float powerUpMass = 3f;

    [SerializeField]
    Rigidbody powerShot;

    [SerializeField]
    Rigidbody normalShot;

    [SerializeField]
    int spawnTime = 10;

    [SerializeField]
    int initialSpawnTime = 5;

    MeshRenderer meshRenderer;

    BoxCollider boxCollider;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        StartCoroutine(SpawnPowerUp());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject gameObject = other.gameObject;

            StartCoroutine(PowerUpTime(gameObject));
            StartCoroutine(SpawnPowerUp());
        }

        
    }

    IEnumerator PowerUpTime(GameObject gameObject)
    {
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
        if (isPowerShot)
        {     
            gameObject.GetComponent<Shoot>().shot = powerShot;
            yield return new WaitForSeconds(5);
            gameObject.GetComponent<Shoot>().shot = normalShot;
        }

        if (isSpeedBoost)
        {
            gameObject.GetComponent<CraftMovement>().speed = 20;
            yield return new WaitForSeconds(5);
            gameObject.GetComponent<CraftMovement>().speed = 12;
        }

        if (isHeavy)
        {
            gameObject.GetComponent<Rigidbody>().mass = powerUpMass;
            yield return new WaitForSeconds(5);
            gameObject.GetComponent<Rigidbody>().mass = 1;
        }

        
    }

    IEnumerator SpawnPowerUp()
    {
        yield return new WaitForSeconds(spawnTime);
        int x = Random.Range(1, 4);
        if (x == 1)
        {
            isPowerShot = true;
            isSpeedBoost = false;
            isHeavy = false;
            meshRenderer.material.color = Color.cyan;
        }
        else if (x == 2)
        {
            isPowerShot = false;
            isSpeedBoost = true;
            isHeavy = false;
            meshRenderer.material.color = Color.magenta;
        }
        else if (x == 3)
        {
            isPowerShot = false;
            isSpeedBoost = false;
            isHeavy = true;
            meshRenderer.material.color = Color.red;
        }
        meshRenderer.enabled = true;
        boxCollider.enabled = true;
        
    }

    public void Reset()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
        StopAllCoroutines();
        StartCoroutine(SpawnPowerUp());
    }

}
