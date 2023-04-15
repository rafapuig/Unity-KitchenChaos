using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class StoveCounter : BaseCounter, IHasProgress {

    public UnityEvent<State> OnStateChanged;   

    public enum State {
        Idle,
        Frying,
        Fried,
        Burned
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipes;
    [SerializeField] private BurningRecipeSO[] burningRecipes;

    private State CurrentState {
        get { return state; }
        set { 
            
            if (state != value) { 
                state = value;
                OnStateChanged?.Invoke(state);
            }
        }
    }

    private State state;
    private float fryingTimer;
    private FryingRecipeSO fryingRecipeSO;
    private float burningTimer;
    private BurningRecipeSO burningRecipeSO;

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    protected void InvokeOnProgressChanged(IHasProgress.OnProgressChangedEventArgs e) {
        OnProgressChanged?.Invoke(this, e);
    }

    private float FryingTimer {
        get { return fryingTimer; }
        set { 
            fryingTimer = value;

            InvokeOnProgressChanged(new IHasProgress.OnProgressChangedEventArgs {
                progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
            });
        }
    }

    private float BurningTimer {
        get { return burningTimer; }
        set {
            burningTimer = value;

            InvokeOnProgressChanged(new IHasProgress.OnProgressChangedEventArgs {
                progressNormalized = burningTimer / burningRecipeSO.burningTimerMax
            });
        }
    }

    private void Start() {
        state = State.Idle;        
    }

    private void Update() {
        if (HasKitchenObject()) {
            switch (state) {
                case State.Idle:
                    break;
                case State.Frying:
                    //fryingTimer += Time.deltaTime;

                    //InvokeOnProgressChanged(new IHasProgress.OnProgressChangedEventArgs {
                    //    progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    //});

                    FryingTimer += Time.deltaTime;

                    if (fryingTimer > fryingRecipeSO.fryingTimerMax) {
                        // Fried 
                        KitchenObject.DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);

                        Debug.Log("Object Fried!");

                        //state = State.Fried;
                        //OnStateChanged?.Invoke(state);
                        CurrentState = State.Fried;

                        //burningTimer = 0f;
                        burningRecipeSO = GetBurningRecipeSOWithInput(KitchenObject.GetKitchenObjectSO());
                        BurningTimer = 0f;
                        
                    }
                    Debug.Log(fryingTimer);
                    break;
                case State.Fried:
                    //burningTimer += Time.deltaTime;

                    //InvokeOnProgressChanged(new IHasProgress.OnProgressChangedEventArgs {
                    //    progressNormalized = burningTimer / burningRecipeSO.burningTimerMax
                    //});

                    BurningTimer += Time.deltaTime;

                    if (burningTimer > burningRecipeSO.burningTimerMax) {
                        // Fried  
                        KitchenObject.DestroySelf();
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

                        Debug.Log("Object Burned!");

                        //state = State.Burned;
                        //OnStateChanged?.Invoke(state);
                        CurrentState = State.Burned;

                        //InvokeOnProgressChanged(new IHasProgress.OnProgressChangedEventArgs {
                        //    progressNormalized = 0f
                        //});

                        BurningTimer = 0f;
                    }
                    Debug.Log(burningTimer);
                    break;
                case State.Burned:
                    break;
            }
            Debug.Log(state);
        } 
    }

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            //There is no KitchenObject here
            if (player.HasKitchenObject()) {
                //And player is carrying something
                if (HasRecipeWithInput(player.KitchenObject.GetKitchenObjectSO())) {
                    //Player carrying something that can be Fried
                    player.KitchenObject.KitchenObjectParent = this;

                    fryingRecipeSO = GetFryingRecipeSOWithInput(KitchenObject.GetKitchenObjectSO());

                    //fryingTimer = 0f;
                    //InvokeOnProgressChanged(new IHasProgress.OnProgressChangedEventArgs {
                    //    progressNormalized = 0f //fryingTimer / fryingRecipeSO.fryingTimerMax
                    //});
                    FryingTimer = 0f;

                    //state = State.Frying;
                    //OnStateChanged?.Invoke(state);
                    CurrentState = State.Frying;

                    
                }
            } else {
                //Player not carrying anything
            }
        } else {
            //There is a KitchenObject here
            if (player.HasKitchenObject()) {
                //Player is carrying something
                if (player.KitchenObject.TryGetPlate(out PlateKitchenObject plate)) {
                    //Player is holding a Plate                    
                    if (plate.TryAddIngredient(KitchenObject.GetKitchenObjectSO())) {
                        KitchenObject.DestroySelf();

                        CurrentState = State.Idle;

                        InvokeOnProgressChanged(new IHasProgress.OnProgressChangedEventArgs {
                            progressNormalized = 0f
                        });
                    }
                }
            } else {
                //Player is not carrying anything
                KitchenObject.KitchenObjectParent = player;

                //state = State.Idle;
                //OnStateChanged?.Invoke(state);
                CurrentState = State.Idle;

                InvokeOnProgressChanged(new IHasProgress.OnProgressChangedEventArgs {
                    progressNormalized = 0f
                });
            }
        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO input) {
        FryingRecipeSO fryingRecipe = GetFryingRecipeSOWithInput(input);
        return fryingRecipe?.output;
    }

    private bool HasRecipeWithInput(KitchenObjectSO input) {
        FryingRecipeSO fryingRecipe = GetFryingRecipeSOWithInput(input);
        return fryingRecipe != null;
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO input) {
        foreach (FryingRecipeSO fryingRecipe in fryingRecipes) {
            if (fryingRecipe.input == input) {
                return fryingRecipe;
            }
        }
        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO input) {
        foreach (BurningRecipeSO burningRecipe in burningRecipes) {
            if (burningRecipe.input == input) {
                return burningRecipe;
            }
        }
        return null;
    }

}
