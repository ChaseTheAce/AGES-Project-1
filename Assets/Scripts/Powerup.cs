using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    Text pwrUpTxt;

    MeshRenderer meshRenderer;

    BoxCollider boxCollider;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        pwrUpTxt.text = null;
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
            pwrUpTxt.text = "Power Shot!";
            yield return new WaitForSeconds(5);
            pwrUpTxt.text = null;
            gameObject.GetComponent<Shoot>().shot = normalShot;            
        }

        if (isSpeedBoost)
        {
            gameObject.GetComponent<PlayerMovement>().speed = 20;
            pwrUpTxt.text = "SpeedBoost!";
            yield return new WaitForSeconds(5);
            pwrUpTxt.text = null;
            gameObject.GetComponent<PlayerMovement>().speed = 12;
        }

        if (isHeavy)
        {
            gameObject.GetComponent<Rigidbody>().mass = powerUpMass;
            pwrUpTxt.text = "Stone Form!";
            yield return new WaitForSeconds(5);
            pwrUpTxt.text = null;
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

    IEnumerator ShowText()
    {
        yield return new WaitForSeconds(5);
        pwrUpTxt.text = null;

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
