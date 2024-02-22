using System;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour {
    
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake() {
        resumeButton.onClick.AddListener(()  =>{
        KitchenGameManager.Instance.TogglePauseGame();
        });
        mainMenuButton.onClick.AddListener(() =>{
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }


    private void Start(){
        KitchenGameManager.Instance.OnGamePause += KitchenGameManager_OnGamePause;
        KitchenGameManager.Instance.OnGameUnpause += KitchenGameManager_OnGameUnpause;

        Hide();
    }

    private void KitchenGameManager_OnGameUnpause(object sender, EventArgs e)
    {
        Hide();
    }

    private void KitchenGameManager_OnGamePause(object sender, EventArgs e)
    {
        Show();
    }

    private void Show(){
        gameObject.SetActive(true);
    }

    private void Hide(){
        gameObject.SetActive(false);
    }
}