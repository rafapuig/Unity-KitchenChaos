using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CuttingCounter : BaseCounter {

      [SerializeField] private CuttingRecipeSO[] cuttingRecipes;

    public override void Interact(Player player) {

        if (!HasKitchenObject()) {
            //There is no KitchenObject here
            if (player.HasKitchenObject()) {
                //And player is carrying something
                if (HasRecipeWithInput(player.KitchenObject.GetKitchenObjectSO())) {
                    //Player carrying something that can be cut
                    player.KitchenObject.KitchenObjectParent = this;
                }
            } else {
                //Player not carrying anything
            }
        } else {
            //There is a KitchenObject here
            if (player.HasKitchenObject()) {
                //Player is carrying something
            } else {
                //Player is not carrying anything
                KitchenObject.KitchenObjectParent = player;
            }
        }
    }

    public override void InteractAlternate(Player player, EventArgs e) {
        if(HasKitchenObject()  && HasRecipeWithInput(KitchenObject.GetKitchenObjectSO())) {
            //There is a KitchenObject here AND it can be cut
            KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(KitchenObject.GetKitchenObjectSO());
            KitchenObject.DestroySelf();

            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipes) {
            if (cuttingRecipeSO.input == inputKitchenObjectSO) {
                return cuttingRecipeSO.output;
            }
        }
        return null;
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipes) {
            if (cuttingRecipeSO.input == inputKitchenObjectSO) {
                return true;
            }
        }
        return false;
    }

}
