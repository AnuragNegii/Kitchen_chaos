using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7.0f;
    [SerializeField] private GameInput gameInput;
    private bool isWalking = false;
    private void Update() {

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
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
                if(canMove){
                    //can move only on the x

                    moveDir = moveDirX;
                }else{
                    ///cannnot move only on the x

                    //Attemp only Z movement
                    Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                    canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
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

    public bool IsWalking(){
        return isWalking;
    }
}
