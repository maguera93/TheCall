using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MainController : MonoBehaviour
{
    [SerializeField]
    private EventController[] eventControllers;
    UnityAction action;
    [SerializeField]
    private Button button;
    private int prevEvent;

    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private int maxResistance;
    public int currentResistance;

    [SerializeField]
    private Image resistanceBar;

    private EventController currentEvent;


    private void Start()
    {
        StartEvent(0);
        
    }

    public void NextEvent()
    {
        currentEvent.NextDialogue();
    }

    public void StartEvent(int index)
    {
        for (int i = 0; i < eventControllers.Length; i++)
        {
            if (eventControllers[i].Index == index)
            {
                currentEvent = eventControllers[i];
                break;
            }

        }

        NextEvent();
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void AddResistance(int points)
    {
        currentResistance += points;

        resistanceBar.fillAmount = (float)currentResistance / (float)maxResistance;
        Debug.Log(resistanceBar.fillAmount);
    }
}
