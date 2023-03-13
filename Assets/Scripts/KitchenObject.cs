using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    public IKitchenObjectParent KitchenObjectParent { 
        get { return kitchenObjectParent; } 
        set {  
            if(kitchenObjectParent != null) {
                //Vaciar el anterior padre (ya no tiene el objeto)
                kitchenObjectParent.ClearKitchenObject();
            }
            //Poner el objeto en el nuevo padre
            kitchenObjectParent = value;

            if(kitchenObjectParent.HasKitchenObject()) {
                Debug.LogError("Counter already has a KitchenObject!");
            }

            kitchenObjectParent.KitchenObject = this;

            //Actulizar visualmente
            transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
            transform.localPosition = Vector3.zero;
        } 
    }

    public KitchenObjectSO GetKitchenObjectSO() { return kitchenObjectSO; }

}
