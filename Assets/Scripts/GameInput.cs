using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    PlayerInputActions playerInputActions;

    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable(); 
    }

    public Vector2 GetMovementVectorNormalizedOld() {
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        float verticalAxis = Input.GetAxisRaw("Vertical");

        Vector2 inputVector = new Vector2(horizontalAxis, verticalAxis);
        inputVector = inputVector.normalized;

        return inputVector;
    }

    public Vector2 GetMovementVectorNormalized() {      

        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        //inputVector = inputVector.normalized;

        //Debug.Log(inputVector);

        return inputVector;
    }
}
