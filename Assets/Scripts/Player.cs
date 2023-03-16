using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent {   

    public static Player Instance { get; private set; }

    private void Awake() {
        if (Instance != null) {
           Debug.LogError("There's more than one player");
        }
        Instance = this;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    private Vector3 lastInteractDirection;
    
    

    private CharacterController characterController;

    public class SelectedCounterChangedEventArgs : EventArgs {
        public BaseCounter selectedCounter;
    }

    public event EventHandler<SelectedCounterChangedEventArgs> SelectedCounterChanged;
    
    protected virtual void OnSelectedCounterChanged(SelectedCounterChangedEventArgs e) {
        SelectedCounterChanged?.Invoke(this, e);
    }

    // Start is called before the first frame update
    void Start() {
        characterController = GetComponent<CharacterController>();
        gameInput.InteractAction += GameInput_InteractAction;
    }

    private void GameInput_InteractAction(object sender, System.EventArgs e) {    
        selectedCounter?.Interact(this);        
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

    private BaseCounter selectedCounter;

    private BaseCounter SelectedCounter {
        get { return selectedCounter; }
        set {
            if (selectedCounter != value) {
                selectedCounter = value;
                OnSelectedCounterChanged(new SelectedCounterChangedEventArgs { selectedCounter = value });
            }
        }
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

        //Solo probamos si el rayo choca contra colliders cuyos objectos esten en la capa de los Counters
        if(Physics.Raycast(transform.position, lastInteractDirection, 
            out RaycastHit hitInfo, interactDistance, countersLayerMask)) {
            //Si hemos tocado un objecto con el componente ClearCounter
            if(hitInfo.transform.TryGetComponent(out BaseCounter baseCounter)) {
                //Object hited has ClearCounter
                //clearCounter.Interact();
                if(baseCounter != selectedCounter) {
                    SelectedCounter = baseCounter;             
                } //Si es el mismo no hacemos nada
            // Si no hemos tocado con el raycast un ClearCounter
            } else {
                SelectedCounter = null;
            }
        //Si no hemos tocado nada con el raycast
        } else {
            SelectedCounter = null;
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

    public Transform GetKitchenObjectFollowTransform() {
        return kitchenObjectHoldPoint;
    }

    public KitchenObject KitchenObject { get; set; }

    public void ClearKitchenObject() {
        KitchenObject = null;
    }

    public bool HasKitchenObject() {
        return KitchenObject != null;
    }
}