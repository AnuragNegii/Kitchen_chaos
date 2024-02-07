using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounter : BaseCounter {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public event EventHandler OnPlayerGrabObject;

     public override void Interact(Player player){
        Transform kitchenObjecTransform = Instantiate(kitchenObjectSO.prefab);
        kitchenObjecTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
        OnPlayerGrabObject.Invoke(this, EventArgs.Empty);
        }
}


