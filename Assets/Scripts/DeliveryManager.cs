using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryManager : MonoBehaviour{

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    public static DeliveryManager Instance{get; private set;}

    [SerializeField] private RecipieListSO recipieListSO;

    private List<RecipieSO> waitingRecipieSOList;
    private float spawnRecipieTimer;
    private float spawnRecipieTimerMax = 4f;
    private int waitingRecipiesMax = 4;
    private int successfulRecipesAmount;

    private void Awake() {
        Instance = this;

        
        waitingRecipieSOList = new List<RecipieSO>();
    }
    private void Update() {
        spawnRecipieTimer -= Time.deltaTime;
        if(spawnRecipieTimer < 0){
            spawnRecipieTimer = spawnRecipieTimerMax;

            if(KitchenGameManager.Instance.IsGamePlaying()  && waitingRecipieSOList.Count < waitingRecipiesMax){
            RecipieSO waitingRecipieSO = recipieListSO.recipieSOList[UnityEngine.Random.Range(0, recipieListSO.recipieSOList.Count)];
           
            waitingRecipieSOList.Add(waitingRecipieSO);
           
            OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DelieverRecipie(PlateKitchenObject plateKitchenObject){
        for (int i = 0; i < waitingRecipieSOList.Count; i++){
            RecipieSO waitingRecipieSO = waitingRecipieSOList[i];

            if (waitingRecipieSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count){
                ///Has the same number of ingridients
                bool plateContentsMatchesRecipie = true;
                foreach(KitchenObjectSO recipieKitcheenObjeecSO in waitingRecipieSO.kitchenObjectSOList){
                    //cycling through all ingridients in the recipe
                    bool ingridientsFound = false;
                    foreach(KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()){
                        //cycling through all ingridient in the plate
                        if(plateKitchenObjectSO == recipieKitcheenObjeecSO){
                            ///Ingridient matches
                            ingridientsFound = true;
                            break;
                        }
                    }
                    if(!ingridientsFound){
                        //This recipe ingredient was not found on the plate
                        plateContentsMatchesRecipie = false;
                    }
                }
                if(plateContentsMatchesRecipie){
                    //Player delievered the correct recipe

                    successfulRecipesAmount++;

                    waitingRecipieSOList.RemoveAt(i);

                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);

                    return;
                }
            }
        }
        ///No matches found 
        ///Player did not delivered the correct recipe
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        ///
    }

    public List<RecipieSO> GetWaitingRecipieSOList(){
        return waitingRecipieSOList;
    }

    public int GetSuccessfulRecipeAmount(){
        return successfulRecipesAmount;
    }
}
