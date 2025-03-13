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

        Vector3 move = new Vector3(deltaX, 0, deltaZ);

        if (move.magnitude > 0.1f)
        {
            move = transform.TransformDirection(move);
            _charController.Move(move * Time.deltaTime);
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

        _charController.Move(new Vector3(0, _velocity.y, 0) * Time.deltaTime);
    }
}

