using System;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour {
    

    [SerializeField] private TextMeshProUGUI countdownText;

    private void Start(){
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;

        Hide();
    }

    private void KitchenGameManager_OnStateChanged(object sender, EventArgs e){
        if(KitchenGameManager.Instance.IsCountDownToStartActive()){
            Show();
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

    private void Update() {
        countdownText.text = MathF.Ceiling(KitchenGameManager.Instance.GetCountdownToStartTimer()).ToString();
    }

}