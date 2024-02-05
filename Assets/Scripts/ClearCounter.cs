using UnityEngine;

public class ClearCounter : MonoBehaviour {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

    public void Interact(){
        Transform kitchenObjecTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
        kitchenObjecTransform.localPosition = Vector3.zero;

        Debug.Log(kitchenObjecTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().name);
    }
}