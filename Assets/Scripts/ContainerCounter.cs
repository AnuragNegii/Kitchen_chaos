using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounter : BaseCounter {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public event EventHandler OnPlayerGrabObject;

     public override void Interact(Player player){
        if(!player.HasKitchenObject()){
            //Player is not carrying anything
        KitchenObject.spawnKitchenObject(kitchenObjectSO, player);

        OnPlayerGrabObject?.Invoke(this, EventArgs.Empty);
        }
    }
}


