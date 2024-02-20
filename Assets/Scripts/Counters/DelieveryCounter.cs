using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelieveryCounter : BaseCounter
{

    public static DelieveryCounter Instance{get; private set;}

    public void Awake(){
        Instance = this;
    }

    public override void Interact(Player player){
       if(player.HasKitchenObject()){
        if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)){
            //Only accepts the plate

            DeliveryManager.Instance.DelieverRecipie(plateKitchenObject);
            player.GetKitchenObject().DestroySelf();
        }
       }
    }
}
