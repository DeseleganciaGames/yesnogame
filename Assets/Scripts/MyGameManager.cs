using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyGameManager : MonoBehaviour {
    public bool gameOver = false;
    public static MyGameManager instance;
    public GameObject ko;
	// Use this for initialization
	void Start () {
        gameOver = false;
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GameOver()
    {
        gameOver = true;
        ko.SetActive(true);
        Invoke("Restart", 5);
    }
    void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
