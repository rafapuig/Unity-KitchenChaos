using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float moveSpeed = 7f;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        float verticalAxis = Input.GetAxisRaw("Vertical");

        Vector2 inputVector = new Vector2 (horizontalAxis, verticalAxis);
        inputVector = inputVector.normalized;

        Vector3 moveDirection = new Vector3 (inputVector.x, 0, inputVector.y);
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        float rotationSpeed = 10f;

        transform.forward = Vector3.Slerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);

        isWalking = moveDirection != Vector3.zero;
    }

    private bool isWalking;

    public bool IsWalking() {
        return isWalking;
    }
}