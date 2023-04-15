using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject {

    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;

    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake() {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO) {
        if(!validKitchenObjectSOList.Contains(kitchenObjectSO)) return false; //Not a valid ingredient
        if(kitchenObjectSOList.Contains(kitchenObjectSO)) return false;
        kitchenObjectSOList.Add(kitchenObjectSO);
        return true;
    }
   
}
