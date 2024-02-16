using System;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress {

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public class OnStateChangedEventArgs: EventArgs{
        public State state;
    }

    public enum State{
        Idle,
        Frying,
        Fried,
        Burned
    }

    [SerializeField] private FryingRecipieSO[] fryingRecipieSOArray;
    [SerializeField] private BurningRecipieSO[] burningRecipieSOArray;
    
    private State state;
    private float fryingTimer;
    private float burningTimer;
    private FryingRecipieSO fryingRecipieSO;
    private BurningRecipieSO burningRecipieSO;


    private void Start(){
        state = State.Idle;
    }

    private void Update() {
        if(HasKitchenObject()){
            switch (state){
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                        progressNormalized = fryingTimer / fryingRecipieSO.fryingTimerMax
                    });
                    if(fryingTimer > fryingRecipieSO.fryingTimerMax){
                        //Fried
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(fryingRecipieSO.output, this);

                        state = State.Fried;
                        burningTimer = 0f;
                        burningRecipieSO = GetBurningRecipieSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                            state = state
                        });
                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                        progressNormalized = burningTimer / burningRecipieSO.burningTimerMax
                    });

                    if(burningTimer > burningRecipieSO.burningTimerMax){
                        //Burned
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(burningRecipieSO.output, this);

                        state = State.Burned;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                            progressNormalized = 0f
                        });
                
                    }
                    break;
                case State.Burned:
                    break;
            }
        }
    }

    public override void Interact(Player player){
        if(!HasKitchenObject()){
            //There is no kitchen object
            if(player.HasKitchenObject()){
                //player is carrying something
                if(HasRecipieWithInput(player.GetKitchenObject().GetKitchenObjectSO())){
                    //player carrying something that can be fried
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    fryingRecipieSO = GetFryingRecipieSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    state = State.Frying;
                    fryingTimer = 0f;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                        state = state
                    });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                        progressNormalized = fryingTimer / fryingRecipieSO.fryingTimerMax
                    });
                }
            }else{
                //Player not carrying anything
            }
        }else{
            //There is a kitchenObject here
            if(player.HasKitchenObject()){
                // Player is carrying something
            }else{
                //Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);

                state = State.Idle;

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                    state = state
                });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                    progressNormalized = 0f
                });

            }
        }
    }

    private bool HasRecipieWithInput(KitchenObjectSO inputKitchenObjectSO){
        FryingRecipieSO fryingRecipieSO = GetFryingRecipieSOWithInput(inputKitchenObjectSO);
        return fryingRecipieSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        FryingRecipieSO fryingRecipieSO = GetFryingRecipieSOWithInput(inputKitchenObjectSO);
        
        if(fryingRecipieSO != null){
            return fryingRecipieSO.output;
        }else{
            return null;
        }
    }

    private FryingRecipieSO GetFryingRecipieSOWithInput(KitchenObjectSO inputKitchenObjectSO){
            foreach (FryingRecipieSO fryingRecipieSO in fryingRecipieSOArray){
                if(inputKitchenObjectSO == fryingRecipieSO.input){
                    return fryingRecipieSO;
                }
            }
        return null;
    }

    private BurningRecipieSO GetBurningRecipieSOWithInput(KitchenObjectSO inputKitchenObjectSO){
            foreach (BurningRecipieSO burningRecipieSO in burningRecipieSOArray){
                if(inputKitchenObjectSO == burningRecipieSO.input){
                    return burningRecipieSO;
                }
            }
        return null;
    }
}