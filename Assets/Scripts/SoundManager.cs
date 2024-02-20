using System;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    

    public static SoundManager Instance{ get; private set;}
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;
    
    private void Awake() {
        Instance = this;
    }
    private void Start() {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipieSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlaced += BaseCounter_OnAnyObjectPlaced;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e){
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefsSO.trash[UnityEngine.Random.Range(0, audioClipRefsSO.trash.Length)], trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlaced(object sender, EventArgs e){
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSO.objectDrop, baseCounter.transform.position);
    }

    private void Player_OnPickedSomething(object sender, EventArgs e){
        PlaySound(audioClipRefsSO.objectPickup[UnityEngine.Random.Range(0, audioClipRefsSO.objectPickup.Length)], Player.Instance.transform.position);
    }
    

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e){
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e){
        DelieveryCounter delieveryCounter = DelieveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliveryFail, delieveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipieSuccess(object sender, EventArgs e){
        DelieveryCounter delieveryCounter = DelieveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliverySuccess, delieveryCounter.transform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1.0f){
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1.0f){
       PlaySound(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], position, volume);
    }

    public void PlayFootstepSound(Vector3 position, float volume = 1.0f){
        PlaySound(audioClipRefsSO.footsteps, position, volume);
    }
}