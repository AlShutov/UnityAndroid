using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    public GameObject playerExplosion;
    public GameObject explosion;
    public int scoreValue;
    private GameController gameController;
    private PlayerController playerController;
    private int chance;

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
        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        if (playerControllerObject != null)
        {
            playerController = playerControllerObject.GetComponent<PlayerController>();
        }
        if (playerControllerObject == null)
        {
            Debug.Log("Dafuck");
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }
        if (other.tag == "Enemy")
        {
            return;
        }
        if (other.tag == "EnemyBolt")
        {
            return;
        }
        if (other.tag == "LaserUp")
        {
            return;
        }
        if ((other.tag == "Laser") && (gameObject.tag == "LaserUp"))
        {
            return;
        }

        if (other.tag == "Player")
        {
            if (gameObject.tag == "LaserUp")
            {
                playerController.powerUpped++;
                Destroy(gameObject);
                Instantiate(gameController.powerUpSFX);
                return;
            }
            if (gameController.livesCount > 0)
            {
                gameController.livesCount = gameController.livesCount - 1;
                Destroy(gameObject);
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                return;
            }
            if (gameController.livesCount == 0)
            {
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                gameController.GameOver();
            }

        }
        chance = Random.Range(1, gameController.maxChance);
        if ((chance == 1) && (playerController.powerUpped < 2)&&(gameObject.tag!="EnemyBolt"))
        {
            Instantiate(gameController.powerUp, transform.position, Quaternion.Euler(90, 0, 0));
        }
        if (gameObject.tag != "EnemyBolt") { 
        Instantiate(explosion, transform.position, transform.rotation);
        }
        gameController.AddScore(scoreValue);    
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
