using UnityEngine;

public class CuttingCounter : BaseCounter {

    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;
    
    public override void Interact(Player player){
        if(!HasKitchenObject()){
            //There is no kitchen object
            if(player.HasKitchenObject()){
                //player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
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
            }
        }
    }

    public override void InteractAlternate(Player player){
        if(HasKitchenObject()){
            // there is a kitchen object here
            GetKitchenObject().DestroySelf();

            KitchenObject.spawnKitchenObject(cutKitchenObjectSO, this);

        }
    }
}