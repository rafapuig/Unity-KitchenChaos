using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameInput : MonoBehaviour
{  
    PlayerInputActions playerInputActions;

    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += Interact_performed;
    }

    public event EventHandler InteractAction;
    protected virtual void OnInteractAction(EventArgs e) {
        InteractAction?.Invoke(this, e);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAction(EventArgs.Empty);
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
