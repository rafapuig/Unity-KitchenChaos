using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{  
    PlayerInputActions playerInputActions;

    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
    }

    
    public event EventHandler InteractAction;
    protected virtual void OnInteractAction(EventArgs e) {
        InteractAction?.Invoke(this, e);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAction(EventArgs.Empty);
    }

    public event EventHandler InteractAlternateAction;

    protected virtual void OnInteractAlternateAction(EventArgs e) {
        InteractAlternateAction?.Invoke(this, e);
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAlternateAction(EventArgs.Empty);
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
