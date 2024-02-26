using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance{get; private set;}
    [SerializeField] private Button soundEffectButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAltText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI Gamepad_interactText;
    [SerializeField] private TextMeshProUGUI Gamepad_interactAltText;
    [SerializeField] private TextMeshProUGUI Gamepad_pauseText;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAltButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button Gamepad_interactButton;
    [SerializeField] private Button Gamepad_interactAltButton;
    [SerializeField] private Button Gamepad_pauseButton;
    [SerializeField] private Transform pressToRebindKeyTransform;

    private Action OnCloseButtonAction;

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
            OnCloseButtonAction();
        });
        moveUpButton.onClick.AddListener(()=>{ RebindBinding(GameInput.Binding.Move_Up); });
        moveDownButton.onClick.AddListener(()=>{ RebindBinding(GameInput.Binding.Move_Down); });
        moveLeftButton.onClick.AddListener(()=>{ RebindBinding(GameInput.Binding.Move_Left); });
        moveRightButton.onClick.AddListener(()=>{ RebindBinding(GameInput.Binding.Move_Right); });
        interactButton.onClick.AddListener(()=>{ RebindBinding(GameInput.Binding.Interact); });
        interactAltButton.onClick.AddListener(()=>{ RebindBinding(GameInput.Binding.InteractAlternate); });
        pauseButton.onClick.AddListener(()=>{ RebindBinding(GameInput.Binding.Pause); });
        Gamepad_interactButton.onClick.AddListener(()=>{ RebindBinding(GameInput.Binding.Gamepad_Interact); });
        Gamepad_interactAltButton.onClick.AddListener(()=>{ RebindBinding(GameInput.Binding.Gamepad_InteractAlternate); });
        Gamepad_pauseButton.onClick.AddListener(()=>{ RebindBinding(GameInput.Binding.Gamepad_Pause); });
    }


    private void Start(){
        KitchenGameManager.Instance.OnGameUnpause += KitchenGameManager_OnGameUnpause;
        UpdateVisual();
        HidePressToRebindKey();
        Hide();
    }

    private void KitchenGameManager_OnGameUnpause(object sender, EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual(){
        soundEffectsText.text = "Sound Effects: "+ Mathf.Round(SoundManager.Instance.GetVolume() * 10);
        musicText.text = "Music: "+ Mathf.Round(MusicManager.Instance.GetVolume() * 10);

        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        Gamepad_interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        Gamepad_interactAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);
        Gamepad_pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);
    }

    public void Show(Action onCloseButtonAction){
        this.OnCloseButtonAction = onCloseButtonAction;
        gameObject.SetActive(true);

        soundEffectButton.Select();
    }

    private void Hide(){
        gameObject.SetActive(false);
    }

    private void ShowPressToRebindKey(){
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }

    private void HidePressToRebindKey(){
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding){
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(binding, () =>{
            HidePressToRebindKey();
            UpdateVisual();
            });
    }
}
