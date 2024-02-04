using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7.0f;
    private bool isWalking = false;
    private void Update() {
        Vector2 inputVector = new Vector2(0, 0);

            if (Input.GetKey(KeyCode.W)){
                inputVector.y = +1;
            }
            if (Input.GetKey(KeyCode.A)){
                inputVector.x = -1;
            }
            if (Input.GetKey(KeyCode.S)){
                inputVector.y = -1;
            }
            if (Input.GetKey(KeyCode.D)){
                inputVector.x = +1;
            }

            inputVector = inputVector.normalized;

            Vector3 moveDir = new Vector3(inputVector.x, 0,inputVector.y);
            transform.position += moveDir * Time.deltaTime * moveSpeed;

            isWalking = moveDir != Vector3.zero;
            float rotateSpeed = 10.0f;
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking(){
        return isWalking;
    }
}