using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

[Serializable]
public class EventDialogue
{
    public enum EventType { dialogue, decision, endgame }
    public EventType eventType;

    public string dialogue;

    public GameObject decisionPanel;
}

public class EventController : MonoBehaviour
{
    public List<EventDialogue> eventList;

    private bool startEvent;
    private bool isFinished;

    [SerializeField]
    private float scrollSpeed;

    [SerializeField]
    private int index;
    private int currentEvent;
    private bool showingDialogue;

    private int times;

    public int Index
    {
        get
        {
            return index;
        }
    }

    private MainController controller;

    [SerializeField]
    private TextMeshProUGUI textTM;

    Coroutine textCoroutine;

    private void Start()
    {
        controller = FindObjectOfType<MainController>();
    }

    public void NextDialogue()
    {
        times++;
        if (currentEvent >= eventList.Count - 1)
            return;

        if (!showingDialogue)
        {
            textCoroutine = StartCoroutine(Scrolltext(eventList[currentEvent].dialogue));
            isFinished = false;
            
        }
        else
        {
            if (!isFinished)
            {

                textTM.text = eventList[currentEvent].dialogue;
                isFinished = true;
                StopCoroutine(textCoroutine);
            }
            else
            {
                currentEvent++;
                if (eventList[currentEvent].eventType == EventDialogue.EventType.dialogue)
                {
                    showingDialogue = false;
                    NextDialogue();
                    
                }
                else if (eventList[currentEvent].eventType == EventDialogue.EventType.decision)
                {
                    eventList[currentEvent].decisionPanel.SetActive(true);
                }
                else
                {
                    controller.GameOver();
                }
            }
        }

    }

    IEnumerator Scrolltext(string text)
    {
        for (int i = 0; i <= text.Length; i++)
        {
            textTM.text = text.Substring(0, i);
            if (i == text.Length)
            {
                //if(canSkip) StartCoroutine(skipMessageCoroutine);
                isFinished = true;
            }
            yield return new WaitForSeconds(scrollSpeed);
            showingDialogue = true;
            //writingText = "";            
        }
    }
}
