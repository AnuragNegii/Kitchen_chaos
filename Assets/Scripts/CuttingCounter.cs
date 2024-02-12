using UnityEngine;

public class CuttingCounter : BaseCounter {

    [SerializeField] private CuttingRecipieSO[] cuttingRecipieSOArray;
    
    public override void Interact(Player player){
        if(!HasKitchenObject()){
            //There is no kitchen object
            if(player.HasKitchenObject()){
                //player is carrying something
                if(HasRecipieWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //player carrying something that can be Cut
                    player.GetKitchenObject().SetKitchenObjectParent(this);
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
            }
        }
    }

    public override void InteractAlternate(Player player){
        if(HasKitchenObject() && HasRecipieWithInput(GetKitchenObject().GetKitchenObjectSO())){
            // there is a kitchen object here and the object can be cut
            KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            GetKitchenObject().DestroySelf();

            KitchenObject.spawnKitchenObject(outputKitchenObjectSO, this);

        }
    }

    private bool HasRecipieWithInput(KitchenObjectSO inputKitchenObjectSO){
        foreach (CuttingRecipieSO cuttingRecipieSO in cuttingRecipieSOArray){
            if(inputKitchenObjectSO == cuttingRecipieSO.input){
                return true;
            }
        }
        return false;
    }
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (CuttingRecipieSO cuttingRecipieSO in cuttingRecipieSOArray){
            if(inputKitchenObjectSO == cuttingRecipieSO.input){
                return cuttingRecipieSO.output;
            }
        }
        return null;
    }
}