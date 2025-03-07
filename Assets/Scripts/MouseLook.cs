using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{


    //enumerated structure to associate names with settings
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2

    }

    //Declare a public variable we can set things in Unity
    public RotationAxes axes = RotationAxes.MouseXAndY;

    public float sensitivityHor = 9.0f; //horizontal rotation speed
    public float sensitivityVer = 9.0f; //vertical rotation speed

    public float minimumVert = -45.0f; //don't let character spin too far down
    public float maximumVert = 45.0f;  //don't let character spin too far up

    private float _rotationX = 0; // Vertical Angle is private

    // Use this for initialization
    void Start()
    {

        //Rigidbody body = GetComponent<Rigidbody> ();
        //if (body != null)
        //	body.freezeRotation = true;


    }

    // Update is called once per frame
    void Update()
    {

        //horizontal rotation
        if (axes == RotationAxes.MouseX)
        {
            //transform.Rotate (0, sensitivityHor, 0);//run the rotation every time
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
        }
        //Vertical rotation
        else if (axes == RotationAxes.MouseY)
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVer; //increment the vertical angle based on the mouse
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert); //make sure we don't rotate too far

            float rotationY = transform.localEulerAngles.y;
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);


        }

        //horizontal and vertical at the same time
        else
        {

            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVer; //increment the vertical angle based on the mouse
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert); //make sure we don't rotate too far

            float delta = Input.GetAxis("Mouse X") * sensitivityHor;
            float rotationY = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);


        }

    }
}
