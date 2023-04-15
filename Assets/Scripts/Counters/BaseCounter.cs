using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {

    public virtual void Interact(Player player) {
        Debug.Log("BaseCounter.Interact()");
    }

    public virtual void InteractAlternate(Player player, EventArgs e) {
        Debug.Log("BaseCounter.InteractAlternate()");
    }

    [SerializeField] private Transform counterTopPoint;

    public Transform GetKitchenObjectFollowTransform() { return counterTopPoint; }

    public KitchenObject KitchenObject { get; set; }

    public void ClearKitchenObject() { KitchenObject = null; }

    public bool HasKitchenObject() { return KitchenObject != null; }

    
}