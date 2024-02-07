using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private IKitchenObjectParent secondClearCounter;
    public bool testing;

    private KitchenObject kitchenObject;

    private void Update() {
        if(testing && Input.GetKeyDown(KeyCode.T)){
            if (kitchenObject != null){
                kitchenObject.SetKitchenObjectParent(secondClearCounter);
            }
        }
    }

    public void Interact(){
        if(kitchenObject == null){
        Transform kitchenObjecTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
        ///kitchenObjecTransform.GetComponent<KitchenObject>().SetClearCounter(this);
        kitchenObjecTransform.localPosition = Vector3.zero;
        }else{
            Debug.Log(kitchenObject.GetKitchenObjectParent());
        }
    }

    public Transform GetKitchenObjectFollowTransform(){
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