using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChooseController2 : MonoBehaviour
{
    public ChooseLabelController2 label;
    public GameController gameController;
    private RectTransform rectTransform;
    private Animator animator;
    private float labelHeight = -1;
    private float totalLabelSize;
    private float widthBetween;

    void Start()
    {
        animator = GetComponent<Animator>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetupChoose(ChooseScene scene)
    {
        DestroyLabels();
        animator.SetTrigger("Show2");

        for(int index = 0; index < scene.labels.Count; index++)
        {
            totalLabelSize += scene.labels[index].text.Length;
        }

        widthBetween = rectTransform.rect.width - totalLabelSize - 50;
        widthBetween = widthBetween / scene.labels.Count;


        for(int index = 0; index < scene.labels.Count; index++)
        {
            ChooseLabelController2 newLabel = Instantiate(label.gameObject, transform).GetComponent<ChooseLabelController2>();

            if(labelHeight == -1)
            {
                labelHeight = newLabel.GetHeight();
            }

            // 1, 1 to put em next to each other??? ---- scene.labels[index].text.textBounds
            newLabel.Setup(scene.labels[index], this, CalculateLabelPosition(1, 1), index * widthBetween);

        }

        Vector2 size = rectTransform.sizeDelta;
        size.y = 2 * labelHeight / 4 * 3;
        rectTransform.sizeDelta = size;
        
    }

    public void PerformChoose(StoryScene scene, string selectedAnswer)
    {
        gameController.PlayScene(scene);
        animator.SetTrigger("Hide2");

        // For testing purposes for the answers in a list
        //gameController.ClearLog();
        if(!gameController.answerList.Contains(selectedAnswer))
        {
            gameController.answerList.Add(selectedAnswer);
        }
        foreach (var answer in gameController.answerList)
        {
            //Debug.Log(answer.ToString());
        }
    }

    private float CalculateLabelPosition(int labelIndex, int labelCount)
    {
        return labelHeight / 2;
    }

    private void DestroyLabels()
    {
        foreach(Transform childTransform in transform)
        {
            Destroy(childTransform.gameObject);
        }
    }
}
