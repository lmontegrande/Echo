using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 5f;
    public float turnSpeed = .1f;

    private Rigidbody _rigidbody;
    private Vector2 mouseInputAxis;
    private Vector2 inputAxis;

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
    }

    private void HandleMovement()
    {
        _rigidbody.velocity = new Vector3(inputAxis.x, 0, inputAxis.y) * speed;
        transform.Rotate(Vector3.up * mouseInputAxis.x * turnSpeed);
    }
}
