using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatesCounter : BaseCounter {

    public UnityEvent OnPlateSpawned;
    public UnityEvent OnPlateRemoved;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int platesSpawnedAmount;
    private int platesSpawnedAmountMax = 4;

    private void Update() {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax) {
            spawnPlateTimer = 0;
            if(platesSpawnedAmount<platesSpawnedAmountMax) {
                platesSpawnedAmount++;

                OnPlateSpawned?.Invoke();
            }            
        }
    }

    public override void Interact(Player player) {
        if(!player.HasKitchenObject()) {
            //Player is empty handed
            if(platesSpawnedAmount > 0) {
                //There's at least one plate here
                platesSpawnedAmount--;

                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPlateRemoved?.Invoke();
            }
        }
    }
}
