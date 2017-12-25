using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverEnemy : MonoBehaviour {

    public float speed;
    private GameController gameController;
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameControllerObject == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
        if ((gameController.score >= gameController.scoreDifficultyRaiser * gameController.difficultyRaised))
        {
            if (gameController.difficultyRaised <=2)
            {
                gameController.speedIncrement = gameController.speedIncrement + 2f;
                gameController.difficultyRaised += 1;
            }
        }
        speed += gameController.speedIncrement;
        GetComponent<Rigidbody>().velocity = transform.up * speed;
    }
}
