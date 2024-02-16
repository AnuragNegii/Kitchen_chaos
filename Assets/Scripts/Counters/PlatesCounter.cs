using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{

    public event EventHandler OnPlatesSpawned;
    public event EventHandler OnPlatesRemoved;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4.0f;
    private int platesSpawnedAmount;
    private int platesSpawnedAmountMax = 4;


    private void Update(){
        spawnPlateTimer += Time.deltaTime;

        if(spawnPlateTimer > spawnPlateTimerMax)
        {
            spawnPlateTimer = 0f;
            
            if(platesSpawnedAmount < platesSpawnedAmountMax){
                OnPlatesSpawned?.Invoke(this, EventArgs.Empty);
                platesSpawnedAmount++;
            }
        }
    }

    public override void Interact(Player player)
    {
       if(!player.HasKitchenObject()){
        //player is not holding anything
            if(platesSpawnedAmount > 0){
                //there is at least one plate in the counter
                platesSpawnedAmount--;
                
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPlatesRemoved?.Invoke(this, EventArgs.Empty);
            }
       }
    }
}
