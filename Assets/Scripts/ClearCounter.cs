using UnityEngine;

public class ClearCounter : MonoBehaviour {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private ClearCounter secondClearCounter;
    public bool testing;

    private KitchenObject kitchenObject;

    private void Update() {
        if(testing && Input.GetKeyDown(KeyCode.T)){
            if (kitchenObject != null){
                kitchenObject.SetClearCounter(secondClearCounter);
            }
        }
    }



    public void Interact(){
        if(kitchenObject == null){
        Transform kitchenObjecTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
        kitchenObjecTransform.GetComponent<KitchenObject>().SetClearCounter(this);
        kitchenObjecTransform.localPosition = Vector3.zero;
        }else{
            Debug.Log(kitchenObject.GetClearCounter());
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