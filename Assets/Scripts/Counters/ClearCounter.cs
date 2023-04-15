using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player) {

        if (!HasKitchenObject()) {
            //There is no KitchenObject here
            if (player.HasKitchenObject()) {
                //And player is carrying something
                player.KitchenObject.KitchenObjectParent = this;
            } else {
                //Player not carrying anything
            }
        } else {
            //There is a KitchenObject here
            if (player.HasKitchenObject()) {
                //Player is carrying something
                if(player.KitchenObject.TryGetPlate(out PlateKitchenObject plate)) {
                    //Player is holding a Plate                    
                    if (plate.TryAddIngredient(KitchenObject.GetKitchenObjectSO())){
                        KitchenObject.DestroySelf();
                    }                    
                } else {
                    //Player is not carrying a Plate but something else
                    if(KitchenObject.TryGetPlate(out plate)) {
                        //Counter is holding a plate
                        if(plate.TryAddIngredient(player.KitchenObject.GetKitchenObjectSO())) {
                            player.KitchenObject.DestroySelf();
                        }
                    }
                }
            } else {
                //Player is not carrying anything
                KitchenObject.KitchenObjectParent = player;
            }
        }
    } 
        //if (KitchenObject == null) {
        //    Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);

        //    kitchenObjectTransform.GetComponent<KitchenObject>().KitchenObjectParent = this;
        
        //} else {
        //    //Give it to the player
        //    KitchenObject.KitchenObjectParent = player;
        //}
    

}