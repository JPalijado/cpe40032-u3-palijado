using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;

    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver = false;
    public bool doubleJumpUsed = false;
    public float doubleJumpForce;
    public bool doubleSpeed = false;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    void Update()
    {
        if (gameOver == false)
        {
            // Make the player jump on the ground
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
                playerAnim.SetTrigger("Jump_trig");
                dirtParticle.Stop();
                playerAudio.PlayOneShot(jumpSound, 1.0f);
                doubleJumpUsed = false;

                // Allows the player to double jump
            }
            else if (Input.GetKeyDown(KeyCode.Space) && !isOnGround && !doubleJumpUsed)
            {
                doubleJumpUsed = true;
                playerRb.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
                playerAnim.Play("Running_Jump", 3, 0f);
                playerAudio.PlayOneShot(jumpSound, 1.0f);
            }

            // Allows the player to move faster on the groind
            if (Input.GetKey(KeyCode.LeftShift) && isOnGround)
            {
                doubleSpeed = true;
                playerAnim.SetFloat("Speed_Multiplier", 2.0f);
            }
            else if (doubleSpeed)
            {
                doubleSpeed = false;
                playerAnim.SetFloat("Speed_Multiplier", 1.0f);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Checks if the player is on the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        } 
        
        // Checks if the player hit an obstacle, and displays game over!
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            dirtParticle.Stop();
            Debug.Log("Game Over!");
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        } 
    }
}
