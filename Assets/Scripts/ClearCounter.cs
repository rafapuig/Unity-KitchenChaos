using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;   
    
    public override void Interact(Player player) {
        if (KitchenObject == null) {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);

            kitchenObjectTransform.GetComponent<KitchenObject>().KitchenObjectParent = this;
        
        } else {
            //Give it to the player
            KitchenObject.KitchenObjectParent = player;
        }
    }

}