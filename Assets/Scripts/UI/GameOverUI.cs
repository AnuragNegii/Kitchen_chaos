using System;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour{

    [SerializeField] private TextMeshProUGUI recipeDeliveredText;


    private void Start(){
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;

        Hide();
    }

    private void KitchenGameManager_OnStateChanged(object sender, EventArgs e){
        if(KitchenGameManager.Instance.IsGameOver()){
            Show();
            recipeDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipeAmount().ToString();
        }else{
            Hide();
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

}
