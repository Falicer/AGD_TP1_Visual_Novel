using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public StoryScene currentScene;
    public BottomBarController bottomBar;
    public BackgroundController BackgroundController;

    private State state = State.IDLE;

    private enum State
    {
        IDLE, ANIMATE
    }

    // Start is called before the first frame update
    void Start()
    {
        bottomBar.PlayScene(currentScene);
        BackgroundController.SetImage(currentScene.background);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if(state == State.IDLE && bottomBar.IsCompleted())
            { 
                if(bottomBar.IsLastSentence())
                {
                    PlayScene(currentScene.nextScene);   
                }
                else
                {
                    bottomBar.PlayNextSentence();
                }
            }
        }
    }
    
    private void PlayScene(StoryScene scene)
    {
            StartCoroutine(SwitchScene(scene));
    }

    private IEnumerator SwitchScene(StoryScene scene)
    {
        state = State.ANIMATE;
        currentScene = scene;
        bottomBar.Hide();
        yield return new WaitForSeconds(1f);
        BackgroundController.SwitchImage(scene.background);
        yield return new WaitForSeconds(1f);
        bottomBar.ClearText();
        bottomBar.Show();
        yield return new WaitForSeconds(1f);
        bottomBar.PlayScene(scene);
        state = State.IDLE;
    }
}
