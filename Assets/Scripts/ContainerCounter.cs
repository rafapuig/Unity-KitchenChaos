using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter {

    public event EventHandler PlayerGrabbedObject;



    [SerializeField] private KitchenObjectSO kitchenObjectSO;
       
    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);

            kitchenObjectTransform.GetComponent<KitchenObject>().KitchenObjectParent = player;

            PlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        } 
    }
}
