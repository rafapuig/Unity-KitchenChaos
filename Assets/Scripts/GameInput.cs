using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public Vector2 GetMovementVectorNormalized() {
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        float verticalAxis = Input.GetAxisRaw("Vertical");

        Vector2 inputVector = new Vector2(horizontalAxis, verticalAxis);
        inputVector = inputVector.normalized;

        return inputVector;
    }
}
