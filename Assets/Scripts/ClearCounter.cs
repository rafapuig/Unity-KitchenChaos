using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

 
    private void Update() {
        
    }

    public void Interact(Player player) {
        if (KitchenObject == null) {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);

            kitchenObjectTransform.GetComponent<KitchenObject>().KitchenObjectParent = this;
        
        } else {
            //Give it to the player
            KitchenObject.KitchenObjectParent = player;
        }
    }

    public Transform GetKitchenObjectFollowTransform() { return counterTopPoint; }

    public KitchenObject KitchenObject { get; set; }

    public void ClearKitchenObject() { KitchenObject = null; }

    public bool HasKitchenObject() { return KitchenObject != null; }
}