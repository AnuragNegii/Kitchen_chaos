using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelieveryCounter : BaseCounter
{

    public override void Interact(Player player){
       if(player.HasKitchenObject()){
        if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)){
            //Only accepts the plate
            player.GetKitchenObject().DestroySelf();
        }
       }
    }
}
