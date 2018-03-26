using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayArea : MonoBehaviour {

    Scene currentScene;

    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //SceneManager.LoadScene(currentScene.name);
            other.gameObject.SetActive(false);
        }
    }
}
