using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter {

    public event EventHandler PlayerGrabbedObject;


    [SerializeField] private KitchenObjectSO kitchenObjectSO;
       
    public override void Interact(Player player) {

        if(!player.HasKitchenObject()) {       
            //Player is not carrying anything
       
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

            PlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        } 
    }
}
