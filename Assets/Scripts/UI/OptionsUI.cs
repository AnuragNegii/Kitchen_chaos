using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance{get; private set;}
    [SerializeField] private Button soundEffectButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;


    private void Awake() {
        Instance = this;
        soundEffectButton.onClick.AddListener(() =>{
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() =>{
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        closeButton.onClick.AddListener(()=>{
            Hide();
        });
    }


    private void Start(){
        KitchenGameManager.Instance.OnGameUnpause += KitchenGameManager_OnGameUnpause;
        UpdateVisual();
        Hide();
    }

    private void KitchenGameManager_OnGameUnpause(object sender, EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual(){
        soundEffectsText.text = "Sound Effects: "+ Mathf.Round(SoundManager.Instance.GetVolume() * 10);
        musicText.text = "Music: "+ Mathf.Round(MusicManager.Instance.GetVolume() * 10);
    }

    public void Show(){
        gameObject.SetActive(true);
    }

    private void Hide(){
        gameObject.SetActive(false);
    }
}
