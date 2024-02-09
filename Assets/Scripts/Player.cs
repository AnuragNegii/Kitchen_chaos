using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent {

    
    public static Player Instance { get; private set;}

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs{
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 7.0f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counters;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    private KitchenObject kitchenObject;

    private bool isWalking = false;
    private Vector3 lastInteractDir;

    private BaseCounter selectedCounter;

    private void Awake(){
        if (Instance != null){
            Debug.LogError("there is more than one player instance");
        }
        Instance = this;
    }

    private void Start() {
        gameInput.OnInteractAction += GameInput_OnInteraction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateInteraction;
    }

    private void GameInput_OnInteractAlternateInteraction(object sender, EventArgs e)
    {
       if (selectedCounter != null){
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteraction(object sender, EventArgs e)
    {
        if (selectedCounter != null){
            selectedCounter.Interact(this);
        }
    }

    private void Update() {
        HandleMovement();
        HandleInteractions();
    }


    public bool IsWalking(){
        return isWalking;
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        if (moveDir != Vector3.zero){
            lastInteractDir = moveDir;
        }
        float interactDistance = 2f;

        if(Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, counters)){
           if(raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)){
                // Has clear counter
                if (baseCounter != null){
                    SetSelectedCounter(baseCounter);
                }
           }else{
                     SetSelectedCounter(null);
                }
           }else{
             SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(BaseCounter selectedCounter){
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs{
            selectedCounter = selectedCounter
        });
    }
    
    private void HandleMovement(){
        
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

            Vector3 moveDir = new Vector3(inputVector.x, 0,inputVector.y);
            float moveDistance = moveSpeed * Time.deltaTime;
            float playerRadius = 0.7f; 
            float playerHeight = 2f;
            bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

            if (!canMove){
                //cannot move towards moveDir

                //Attemp only X movement
                Vector3 moveDirX = new Vector3(moveDir.x, 0 ,0).normalized;
                canMove = moveDir.x!= 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
                if(canMove){
                    //can move only on the x

                    moveDir = moveDirX;
                }else{
                    ///cannnot move only on the x

                    //Attemp only Z movement
                    Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                    canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                    if(canMove){
                        //can move only on the z

                        moveDir = moveDirZ;
                    }else{
                        //can not move in any direction
                    }
                }
            }

            if (canMove){
            transform.position += moveDir * moveDistance;
            }

            isWalking = moveDir != Vector3.zero;
            float rotateSpeed = 10.0f;
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    public Transform GetKitchenTransform(){
        return kitchenObjectHoldPoint;
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
