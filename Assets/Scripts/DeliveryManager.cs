using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryManager : MonoBehaviour{


    public static DeliveryManager Instance{get; private set;}

    [SerializeField] private RecipieListSO recipieListSO;

    private List<RecipieSO> waitingRecipieSOList;
    private float spawnRecipieTimer;
    private float spawnRecipieTimerMax = 4f;
    private int waitingRecipiesMax = 4;

    private void Awake() {
        Instance = this;

        
        waitingRecipieSOList = new List<RecipieSO>();
    }
    private void Update() {
        spawnRecipieTimer -= Time.deltaTime;
        if(spawnRecipieTimer < 0){
            spawnRecipieTimer = spawnRecipieTimerMax;

            if(waitingRecipieSOList.Count < waitingRecipiesMax){
            RecipieSO waitingRecipieSO = recipieListSO.recipieSOList[Random.Range(0, recipieListSO.recipieSOList.Count)];
            waitingRecipieSOList.Add(waitingRecipieSO);
            Debug.Log(waitingRecipieSO.recipieName);
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
                    Debug.Log("Player delivered the correct recipe");
                    waitingRecipieSOList.RemoveAt(i);
                    return;
                }
            }
        }
        ///No matches found 
        ///Player did not delivered the correct recipe
        Debug.Log("Player did not delivered the correct recipe");
    }
}
