using UnityEngine;

public class PlayerSounds : MonoBehaviour {
    
    private Player player;
    private float footstepTimer;
    private float footstepTimerMax;


    private void Awake(){
        player = GetComponent<Player>();
    }
    

    private void Update() {
        footstepTimer -= Time.deltaTime;
        if(footstepTimer < 0f){
            footstepTimer = footstepTimerMax;
            
            if(player.IsWalking()){
                float volume = 1.0f;
                SoundManager.Instance.PlayFootstepSound(player.transform.position, volume);
            }
        }
    }
}