using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 30;
    private PlayerController playerControllerScript;
    private float leftBound = -15;

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (playerControllerScript.gameOver == false)
        {
            // If the player is sprinting faster, the speed is doubled
            if (playerControllerScript.doubleSpeed)
            {
                transform.Translate(Vector3.left * Time.deltaTime * (speed * 2));
            }

            // If the player is not sprinting, the speed is normal
            else
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
        }

        // Destroys a game object if it reaches the boundary
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
