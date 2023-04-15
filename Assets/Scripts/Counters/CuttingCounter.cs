using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows;

public class CuttingCounter : BaseCounter, IHasProgress {

    //public UnityEvent<float> OnProgressChanged;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public UnityEvent OnCut;

    protected void InvokeOnProgressChanged(IHasProgress.OnProgressChangedEventArgs eventArgs) {
        OnProgressChanged?.Invoke(this, eventArgs);
    }


    [SerializeField] private CuttingRecipeSO[] cuttingRecipes;

    private int cuttingProgress;

    

    public override void Interact(Player player) {

        if (!HasKitchenObject()) {
            //There is no KitchenObject here
            if (player.HasKitchenObject()) {
                //And player is carrying something
                if (HasRecipeWithInput(player.KitchenObject.GetKitchenObjectSO())) {
                    //Player carrying something that can be cut
                    player.KitchenObject.KitchenObjectParent = this;
                    cuttingProgress = 0;
                    CuttingRecipeSO cuttingRecipe = GetCuttingRecipeSOWithInput(KitchenObject.GetKitchenObjectSO());

                    InvokeOnProgressChanged(new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = (float)cuttingProgress / cuttingRecipe.cuttingProgressMax
                    });

                    //OnProgressChanged?.Invoke((float)cuttingProgress / cuttingRecipe.cuttingProgressMax);
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
        
        if (HasKitchenObject()) {
            KitchenObjectSO input = KitchenObject.GetKitchenObjectSO();

            if (HasRecipeWithInput(input)) {
                //There is a KitchenObject here AND it can be cut
                cuttingProgress++;

                OnCut?.Invoke();

                CuttingRecipeSO cuttingRecipe = GetCuttingRecipeSOWithInput(input);
                InvokeOnProgressChanged(new IHasProgress.OnProgressChangedEventArgs {
                    progressNormalized = (float)cuttingProgress / cuttingRecipe.cuttingProgressMax
                });
                //OnProgressChanged?.Invoke((float)cuttingProgress / cuttingRecipe.cuttingProgressMax);

                if (cuttingProgress >= cuttingRecipe.cuttingProgressMax) {

                    KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(KitchenObject.GetKitchenObjectSO());
                    KitchenObject.DestroySelf();

                    KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
                }
            }
        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO input) {
        CuttingRecipeSO cuttingRecipe = GetCuttingRecipeSOWithInput(input);
        return cuttingRecipe?.output;
    }

    private bool HasRecipeWithInput(KitchenObjectSO input) {
        CuttingRecipeSO cuttingRecipe = GetCuttingRecipeSOWithInput(input);
        return cuttingRecipe != null;
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO input) {
        foreach (CuttingRecipeSO cuttingRecipe in cuttingRecipes) {
            if (cuttingRecipe.input == input) {
                return cuttingRecipe;
            }
        }
        return null;
    }

}
