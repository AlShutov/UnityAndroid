using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public GameObject startButton;

	// Use this for initialization
	void Start () {
        startButton.SetActive(true);	
	}
	public void Started()
    {
        SceneManager.LoadScene("Main");
    }
	
}
