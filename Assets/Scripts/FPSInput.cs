using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]

public class FPSInput : MonoBehaviour
{

    private CharacterController _charController;

    public float speed = 6.0f;
    public float sprintMultiplyer = 1.5f;
    public float gravity = -9.8f;
    public float jumpHeight = 3.0f;

    private Vector3 _velocity;

    // Use this for initialization
    void Start()
    {
        _charController = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        // For movement and speed //

        bool isSprinting = Input.GetKey(KeyCode.LeftShift);

        //transform.Translate (speed, 0, 0);
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;

        if (isSprinting)
        {
            deltaX *= sprintMultiplyer;
            deltaZ *= sprintMultiplyer;
        }


        // For jumping //

        bool isJumping = Input.GetKey(KeyCode.Space);

        // Check if the player is on the ground
        if (_charController.isGrounded)
        {
            // If so, check if they are pressing space
            if (isJumping)
            {
                _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            } else
            {
                _velocity.y = -2f;
            }
        } else
        {
            _velocity.y += gravity * Time.deltaTime;
        }


        Vector3 movement = new Vector3(deltaX, _velocity.y, deltaZ);

        // Was stopping the sprinting from working
        //movement = Vector3.ClampMagnitude(movement, speed);

        // Was affecting the jump function
        //movement.y = gravity;

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _charController.Move(movement);

        //transform.Translate (deltaX*Time.deltaTime, 0, deltaZ*Time.deltaTime);
    }
}

