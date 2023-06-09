using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public string gameScene;

    public TextMeshProUGUI musicValue;
    public AudioMixer musicMixer;
    public TextMeshProUGUI soundsValue;
    public AudioMixer soundsMixer;
    public Button loadButton;

    private Animator animator;
    private int _window = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
        loadButton.interactable = SaveManager.IsGameSaved();
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && _window == 1)
        {
            HideOptions();
        }
    }

    public void NewGame()
    {
        SaveManager.ClearSavedGame();
        Load();
    }

    public void Load()
    {
        SceneManager.LoadScene(gameScene, LoadSceneMode.Single);
    }

    public void ShowOptions()
    {
        animator.SetTrigger("ShowOptions");
        _window = 1;
    }

    public void HideOptions()
    {
        animator.SetTrigger("HideOptions");
        _window = 0;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowCredits() 
    {
        animator.SetTrigger("ShowCredits");
        _window = 1;
    }

    public void HideCredits(){
        animator.SetTrigger("HideCredits");
        _window = 0;
    }

    public void OnMusicChanged(float value)
    {
        musicValue.SetText(value + "%");
        musicMixer.SetFloat("volume", + -50 + value/2);
    }

    public void OnSoundsChanged(float value)
    {
        soundsValue.SetText(value + "%");
        soundsMixer.SetFloat("volume", + -50 + value/2);
    }
}
