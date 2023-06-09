using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Reflection;

public class GameController : MonoBehaviour
{
    public GameScene currentScene;
    public BottomBarController bottomBar;
    public BottomBarController2 bottomBar2;
    public BottomBarController3 bottomBar3;
    public SpriteSwitcher backgroundController;
    public ChooseController chooseController;
    public ChooseController2 chooseController2;
    public AudioController audioController;
    public GameObject BottomBar;
    public GameObject BottomBar2;
    public GameObject BottomBar3;
    public GameObject Choose; 
    public GameObject Choose2;

    public DataHolder data;

    public string menuScene;

    public List<string> answerList = new List<string>();

    private State state = State.IDLE;

    private List<StoryScene> history = new List<StoryScene>();

    private enum State
    {
        IDLE, ANIMATE, CHOOSE
    }

    void Start()
    {
        if (SaveManager.IsGameSaved())
        {
            SaveData data = SaveManager.LoadGame();
            data.prevScenes.ForEach(scene =>
            {
                history.Add(this.data.scenes[scene] as StoryScene);
            });
            currentScene = history[history.Count - 1];
            history.RemoveAt(history.Count - 1);

            if(BottomBar2.activeInHierarchy == true)
            {
                bottomBar2.SetSentenceIndex(data.sentence - 1);
            }else if(BottomBar3.activeInHierarchy == true)
            {
                bottomBar3.SetSentenceIndex(data.sentence - 1);
            }else{
                bottomBar.SetSentenceIndex(data.sentence - 1);
            }

        }
        if (currentScene is StoryScene)
        {
            StoryScene storyScene = currentScene as StoryScene;
            history.Add(storyScene);

            if(BottomBar2.activeInHierarchy == true)
            {
                bottomBar2.PlayScene(storyScene, bottomBar2.GetSentenceIndex());
                backgroundController.SetImage(storyScene.background);
                PlayAudio(storyScene.sentences[bottomBar2.GetSentenceIndex()]);
            }else if(BottomBar3.activeInHierarchy == true){
                bottomBar3.PlayScene(storyScene, bottomBar3.GetSentenceIndex());
                backgroundController.SetImage(storyScene.background);
                PlayAudio(storyScene.sentences[bottomBar3.GetSentenceIndex()]);
            }else{
                bottomBar.PlayScene(storyScene, bottomBar.GetSentenceIndex());
                backgroundController.SetImage(storyScene.background);
                PlayAudio(storyScene.sentences[bottomBar.GetSentenceIndex()]);
            }
        }
    }

    void Update()
    {

        if (state == State.IDLE) {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                if(BottomBar2.activeInHierarchy == true)
                {
                    if (bottomBar2.IsCompleted())
                    {
                        bottomBar2.StopTyping();
                        if (bottomBar2.IsLastSentence())
                        {
                            if((currentScene as StoryScene).nextScene != null){
                                PlayScene((currentScene as StoryScene).nextScene);
                            }else{
                                SceneManager.LoadScene(menuScene);
                            }
                        }
                        else
                        {
                            bottomBar2.PlayNextSentence();
                            PlayAudio((currentScene as StoryScene)
                                .sentences[bottomBar2.GetSentenceIndex()]);
                        }
                    }
                    else
                    {
                        bottomBar2.SpeedUp();
                    }
                }else if(BottomBar3.activeInHierarchy == true)
                {
                    if (bottomBar3.IsCompleted())
                    {
                        bottomBar3.StopTyping();
                        if (bottomBar3.IsLastSentence())
                        {
                            if((currentScene as StoryScene).nextScene != null){
                                PlayScene((currentScene as StoryScene).nextScene);
                            }else{
                                SceneManager.LoadScene(menuScene);
                            }
                        }
                        else
                        {
                            bottomBar3.PlayNextSentence();
                            PlayAudio((currentScene as StoryScene)
                                .sentences[bottomBar3.GetSentenceIndex()]);
                        }
                    }
                    else
                    {
                        bottomBar3.SpeedUp();
                    }
                }else{
                    if (bottomBar.IsCompleted())
                    {
                        bottomBar.StopTyping();
                        if (bottomBar.IsLastSentence())
                        {
                            if((currentScene as StoryScene).nextScene != null){
                                PlayScene((currentScene as StoryScene).nextScene);
                            }else{
                                SceneManager.LoadScene(menuScene);
                            }
                        }
                        else
                        {
                            bottomBar.PlayNextSentence();
                            PlayAudio((currentScene as StoryScene)
                                .sentences[bottomBar.GetSentenceIndex()]);
                        }
                    }
                    else
                    {
                        bottomBar.SpeedUp();
                    }
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                if((currentScene as StoryScene).sentences.Count > 0){
                    if(BottomBar2.activeInHierarchy == true ){
                        if (bottomBar2.IsFirstSentence())
                        {
                            if(history.Count > 1)
                            {
                                bottomBar2.StopTyping();
                                bottomBar2.HideSprites();
                                history.RemoveAt(history.Count - 1);
                                StoryScene scene = history[history.Count - 1];
                                history.RemoveAt(history.Count - 1);
                                PlayScene(scene, scene.sentences.Count - 2, false);
                            }
                        }
                        else
                        {
                            bottomBar2.GoBack();
                        }
                    }else if(BottomBar3.activeInHierarchy == true ){
                        if (bottomBar3.IsFirstSentence())
                        {
                            if(history.Count > 1)
                            {
                                bottomBar3.StopTyping();
                                bottomBar3.HideSprites();
                                history.RemoveAt(history.Count - 1);
                                StoryScene scene = history[history.Count - 1];
                                history.RemoveAt(history.Count - 1);
                                PlayScene(scene, scene.sentences.Count - 2, false);
                            }
                        }
                        else
                        {
                            bottomBar3.GoBack();
                        }
                    }else{
                        if (bottomBar.IsFirstSentence())
                        {
                            if(history.Count > 1)
                            {
                                bottomBar.StopTyping();
                                bottomBar.HideSprites();
                                history.RemoveAt(history.Count - 1);
                                StoryScene scene = history[history.Count - 1];
                                history.RemoveAt(history.Count - 1);
                                PlayScene(scene, scene.sentences.Count - 2, false);
                            }
                        }
                        else
                        {
                            bottomBar.GoBack();
                        }
                    }
                }else{
                    if(BottomBar2.activeInHierarchy == true ){
                        
                        if(history.Count > 1)
                        {
                            bottomBar2.HideSprites();
                            history.RemoveAt(history.Count - 1);
                            StoryScene scene = history[history.Count - 1];
                            history.RemoveAt(history.Count - 1);
                            PlayScene(scene, scene.sentences.Count - 2, false);
                        }
                        bottomBar2.GoBack();
                    }else if(BottomBar3.activeInHierarchy == true ){
                        
                        if(history.Count > 1)
                        {
                            bottomBar3.HideSprites();
                            history.RemoveAt(history.Count - 1);
                            StoryScene scene = history[history.Count - 1];
                            history.RemoveAt(history.Count - 1);
                            PlayScene(scene, scene.sentences.Count - 2, false);
                        }
                        bottomBar3.GoBack();
                    }else{
                        if(history.Count > 1)
                        {
                            bottomBar.HideSprites();
                            history.RemoveAt(history.Count - 1);
                            StoryScene scene = history[history.Count - 1];
                            history.RemoveAt(history.Count - 1);
                            PlayScene(scene, scene.sentences.Count - 2, false);
                        }
                        bottomBar.GoBack();
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                List<int> historyIndicies = new List<int>();
                history.ForEach(scene =>
                {
                    historyIndicies.Add(this.data.scenes.IndexOf(scene));
                });
                if(BottomBar2.activeInHierarchy == true){
                    SaveData data = new SaveData
                    {
                        sentence = bottomBar2.GetSentenceIndex(),
                        prevScenes = historyIndicies
                    };
                    SaveManager.SaveGame(data);
                }else if(BottomBar3.activeInHierarchy == true){
                    SaveData data = new SaveData
                    {
                        sentence = bottomBar3.GetSentenceIndex(),
                        prevScenes = historyIndicies
                    };
                    SaveManager.SaveGame(data);
                }else{
                    SaveData data2 = new SaveData
                    {
                    sentence = bottomBar.GetSentenceIndex(),
                    prevScenes = historyIndicies
                    };
                    SaveManager.SaveGame(data2);
                }
                SceneManager.LoadScene(menuScene);
            }
        }
    }

    public void PlayScene(GameScene scene, int sentenceIndex = -1, bool isAnimated = true)
    {
        StartCoroutine(SwitchScene(scene, sentenceIndex, isAnimated));
    }

    private IEnumerator SwitchScene(GameScene scene, int sentenceIndex = -1, bool isAnimated = true)
    {
        state = State.ANIMATE;
        currentScene = scene;
        if(isAnimated)
        {
            if(scene is ChooseScene)
            {

            }else{
                bottomBar.Hide();
            }
            yield return new WaitForSeconds(0.5f);
        }
        if (scene is StoryScene)
        {
            StoryScene storyScene = scene as StoryScene;
            history.Add(storyScene);
            PlayAudio(storyScene.sentences[sentenceIndex + 1]);
            if (isAnimated)
            {
                backgroundController.SwitchImage(storyScene.background);
                yield return new WaitForSeconds(1f);

                if(storyScene.background.ToString().Contains("Chat") || storyScene.background.ToString().Contains("chat"))
                {
                    BottomBar2.SetActive(true);
                    BottomBar.SetActive(false);
                    BottomBar3.SetActive(false);
                    bottomBar2.ClearText();
                    bottomBar.ClearText();
                    bottomBar3.ClearText();
                }else if(storyScene.background.ToString().Contains("Photo") || storyScene.background.ToString().Contains("photo")){
                    BottomBar2.SetActive(false);
                    BottomBar.SetActive(false);
                    BottomBar3.SetActive(true);
                    bottomBar2.ClearText();
                    bottomBar.ClearText();
                    bottomBar3.ClearText();
                }
                else{
                    BottomBar2.SetActive(false);
                    BottomBar.SetActive(true);
                    BottomBar3.SetActive(false);
                    bottomBar2.ClearText();
                    bottomBar.ClearText();
                    bottomBar3.ClearText();
                }

    	        if(storyScene.background.ToString().Contains("Bulletstorm") || storyScene.background.ToString().Contains("bulletstorm"))
                {
                    Choose2.SetActive(false); 
                }else{
                    Choose.SetActive(true);
                }

                bottomBar.Show();
                yield return new WaitForSeconds(0.5f);
                //ClearLog();
                //Debug.Log(storyScene.background.ToString());
            }
            else
            {
                backgroundController.SetImage(storyScene.background);
                if(storyScene.background.ToString().Contains("Chat") || storyScene.background.ToString().Contains("chat"))
                {
                    BottomBar2.SetActive(true);
                    BottomBar.SetActive(false);
                    BottomBar3.SetActive(false);
                    bottomBar.ClearText();
                }else if(storyScene.background.ToString().Contains("Photo") || storyScene.background.ToString().Contains("photo")){
                    BottomBar2.SetActive(false);
                    BottomBar.SetActive(false);
                    BottomBar3.SetActive(true);
                    bottomBar2.ClearText();
                }
                else{
                    BottomBar2.SetActive(false);
                    BottomBar.SetActive(true);
                    BottomBar3.SetActive(false);
                    bottomBar2.ClearText();
                }
                bottomBar.Show();
                // ClearLog();
                // Debug.Log(storyScene.background.ToString());
            }

            if(BottomBar2.activeInHierarchy == true){
                bottomBar2.PlayScene(storyScene, sentenceIndex, isAnimated);
            }else if(BottomBar3.activeInHierarchy == true){
                bottomBar3.PlayScene(storyScene, sentenceIndex, isAnimated);
            }else{
                bottomBar.PlayScene(storyScene, sentenceIndex, isAnimated);
            }
            state = State.IDLE;
        }
        else if (scene is ChooseScene)
        {
            state = State.CHOOSE;

            if(Choose2.activeInHierarchy == true){
                chooseController2.SetupChoose(scene as ChooseScene);
            }else{
                chooseController.SetupChoose(scene as ChooseScene);
            }
        }

    }

    private void PlayAudio(StoryScene.Sentence sentence)
    {
        audioController.PlayAudio(sentence.music, sentence.sound);
    }

    // public void ClearLog()
    // {
    //     var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
    //     var type = assembly.GetType("UnityEditor.LogEntries");
    //     var method = type.GetMethod("Clear");
    //     method.Invoke(new object(), null);
    // }
}