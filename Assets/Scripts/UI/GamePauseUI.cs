using System;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour {
    
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;

    private void Awake() {
        resumeButton.onClick.AddListener(()  =>{
        KitchenGameManager.Instance.TogglePauseGame();
        });
        mainMenuButton.onClick.AddListener(() =>{
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        optionsButton.onClick.AddListener(()=>{
            Hide();
            OptionsUI.Instance.Show(Show);
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

        resumeButton.Select();
    }

    private void Hide(){
        gameObject.SetActive(false);
    }
}