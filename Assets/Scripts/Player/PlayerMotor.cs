using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 1.5f;
    public float sprintSpeed = 8f;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        //movementSound = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        isGrounded = controller.isGrounded;
    }
    // receive the inputs for our InputManager.cs and apply them to our character controller.
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if(isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        //if (!movementSound)
        //{
        //    movementSound.PlayOneShot(movementSound.clip);
        //}
        //movementSound.PlayOneShot(movementSound.clip);
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }
    public void sprint()
    {
        speed = sprintSpeed;
    }
    public void normalSpeed()
    {
        speed = 5f;
    }
    IEnumerator movementWait()
    {
        yield return new WaitForSeconds(.5f);
    }
}
