using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour {   

    public static Player Instance { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Instance = this;
        } else {
            Debug.LogError("There's more than one player");
        }
    }



    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;

    private Vector3 lastInteractDirection;
    private ClearCounter selectedCounter;

    private CharacterController characterController;

    public event EventHandler<SelectedCounterChangedEventArgs> SelectedCounterChanged;
    public class SelectedCounterChangedEventArgs : EventArgs {
        public ClearCounter selectedCounter;
    }

    protected virtual void OnSelectedCounterChanged(SelectedCounterChangedEventArgs e) {
        SelectedCounterChanged?.Invoke(this, e);
    }

    // Start is called before the first frame update
    void Start() {
        characterController = GetComponent<CharacterController>();
        gameInput.InteractAction += GameInput_InteractAction;
    }

    private void GameInput_InteractAction(object sender, System.EventArgs e) {    
        selectedCounter?.Interact();        
    }

    // Update is called once per frame
    void Update() {
        HandleMovement();
        HandleInteractions();
    }

    private bool isWalking;

    public bool IsWalking() {
        return isWalking;
    }

    private void HandleInteractions() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);

        //Si esta parado considerar para la interaccion la ultima direccion en que se movia
        if (moveDirection != Vector3.zero) {
            lastInteractDirection = moveDirection;
        }

        //Comprobar si estamos delante de un Counter (a la interactDistance de el)
        float interactDistance = 2f;

        //Solo probamos si el rayo choca contra objectos que esten en la capa de los Counters
        if(Physics.Raycast(transform.position, lastInteractDirection, 
            out RaycastHit hitInfo, interactDistance, countersLayerMask)) {
            //Si hemos tocado un objecto con el componente ClearCounter
            if(hitInfo.transform.TryGetComponent(out ClearCounter clearCounter)) {
                //Object hited has ClearCounter
                //clearCounter.Interact();
                if(clearCounter != selectedCounter) {
                    SelectedCounter = clearCounter;             
                }
            } else {
                selectedCounter = null;
            }
        } else {
            selectedCounter = null;
        }

    }

    private void HandleMovement() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;

        //float playerRadius = 0.7f;
        //float playerHeight = 2f;
        ////bool canMove = !Physics.Raycast(transform.position, moveDirection, playerSize);
        //bool canMove = !Physics.CapsuleCast(
        //    transform.position, transform.position + Vector3.up * playerHeight,
        //    playerRadius, moveDirection, moveDistance);

        //if (canMove) {
        //    //transform.position += moveDirection * moveSpeed * Time.deltaTime;
        //    transform.position += moveDirection * moveDistance;
        //}

        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        float rotationSpeed = 10f;

        transform.forward = Vector3.Slerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);

        isWalking = moveDirection != Vector3.zero;
    }

    private ClearCounter SelectedCounter { 
        get { return selectedCounter; }
        set {
            if (selectedCounter != value) {
                selectedCounter = value;
                OnSelectedCounterChanged(new SelectedCounterChangedEventArgs {selectedCounter = value });
            }
        }
    }

}