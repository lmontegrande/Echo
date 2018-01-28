using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 5f;
    public float strafeSpeed = 4f;
    public float turnSpeed = 5f;
    public float sprintModifier = 1.5f;

    private Rigidbody _rigidbody;
    private Vector2 mouseInputAxis;
    private Vector2 inputAxis;
    private bool isSprinting;

    public void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        HandleInput();
        HandleMovement();
    }

    private void HandleInput()
    {
        mouseInputAxis = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        inputAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        isSprinting = Input.GetButton("Sprint");
    }

    private void HandleMovement()
    {
        _rigidbody.velocity = (transform.forward * inputAxis.y * speed * (isSprinting ? sprintModifier : 1)) + (transform.right * inputAxis.x * strafeSpeed);
        transform.Rotate(Vector3.up * mouseInputAxis.x * turnSpeed);
    }
}
