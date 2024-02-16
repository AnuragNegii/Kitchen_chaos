using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CuttingCounter : BaseCounter, IHasProgress {

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;


    public event EventHandler OnCut;
    [SerializeField] private CuttingRecipieSO[] cuttingRecipieSOArray;

    private int cuttingProgress;
    
    public override void Interact(Player player){
        if(!HasKitchenObject()){
            //There is no kitchen object
            if(player.HasKitchenObject()){
                //player is carrying something
                if(HasRecipieWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //player carrying something that can be Cut
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;

                    CuttingRecipieSO cuttingRecipieSO = GetCuttingRecipieSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                        progressNormalized = (float) cuttingProgress / cuttingRecipieSO.cuttingProgressMax
                    });

                }
            }else{
                //Player not carrying anything
            }
        }else{
            //There is a kitchenObject here
            if(player.HasKitchenObject()){
                // Player is carrying something
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)){
                    //Player is holding a plate
                   if( plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())){
                        GetKitchenObject().DestroySelf();
                   }
                }
            }else{
                //Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player){
        if(HasKitchenObject() && HasRecipieWithInput(GetKitchenObject().GetKitchenObjectSO())){
            // there is a kitchen object here and the object can be cut
            cuttingProgress++;

            
            CuttingRecipieSO cuttingRecipieSO = GetCuttingRecipieSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                    progressNormalized = (float) cuttingProgress / cuttingRecipieSO.cuttingProgressMax
            });
            OnCut?.Invoke(this, EventArgs.Empty);
            
            if (cuttingProgress >= cuttingRecipieSO.cuttingProgressMax){
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);


            }
        }
    }

    private bool HasRecipieWithInput(KitchenObjectSO inputKitchenObjectSO){
        CuttingRecipieSO cuttingRecipieSO = GetCuttingRecipieSOWithInput(inputKitchenObjectSO);
        return cuttingRecipieSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        CuttingRecipieSO cuttingRecipieSO = GetCuttingRecipieSOWithInput(inputKitchenObjectSO);
        
        if(cuttingRecipieSO != null){
            return cuttingRecipieSO.output;
        }else{
            return null;
        }
    }

    private CuttingRecipieSO GetCuttingRecipieSOWithInput(KitchenObjectSO inputKitchenObjectSO){
            foreach (CuttingRecipieSO cuttingRecipieSO in cuttingRecipieSOArray){
                if(inputKitchenObjectSO == cuttingRecipieSO.input){
                    return cuttingRecipieSO;
                }
            }
        return null;
    }

}