using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;


    public void Interact(Player player){
        if(kitchenObject == null){
        Transform kitchenObjecTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
        kitchenObjecTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        kitchenObjecTransform.localPosition = Vector3.zero;
        }else{
           //Give Object to the player
            kitchenObject.SetKitchenObjectParent(player);
        }
    }

    public Transform GetKitchenTransform(){
        return counterTopPoint;
    }


    public void SetKitchenObject(KitchenObject kitchenObject){
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject(){
        return kitchenObject;
    }

    public void ClearKitchenObject(){
        if (kitchenObject != null){
            kitchenObject = null;
        }
    }

    public bool HasKitchenObject(){
        return kitchenObject != null;
    }
}