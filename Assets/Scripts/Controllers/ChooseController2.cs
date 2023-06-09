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
            ChooseLabelController2 newLabel = Instantiate(label.gameObject, transform).GetComponent<ChooseLabelController2>();

            if(labelHeight == -1)
            {
                labelHeight = newLabel.GetHeight();
            }

            newLabel.Setup(scene.labels[index], this, CalculateLabelPosition(index, scene.labels.Count));

        }

        Vector2 size = rectTransform.sizeDelta;
        size.y = (scene.labels.Count + 2) * labelHeight;
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
        if(labelCount %2 == 0)
        {
            if(labelIndex < labelCount / 2)
            {
                return labelHeight * (labelCount / 2 - labelIndex - 1) + labelHeight / 2;
            }
            else
            {
                return -1 * (labelHeight * (labelIndex - labelCount / 2) + labelHeight / 2);
            }
        }
        else
        {
            if (labelIndex < labelCount / 2)
            {
                return labelHeight * (labelCount / 2 - labelIndex);
            }
            else if (labelIndex > labelCount / 2)
            {
                return -1 * (labelHeight * (labelIndex - labelCount / 2));
            }
            else
            {
                return 0;
            }
        }
    }

    private void DestroyLabels()
    {
        foreach(Transform childTransform in transform)
        {
            Destroy(childTransform.gameObject);
        }
    }
}
