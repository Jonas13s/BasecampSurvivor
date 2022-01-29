using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;

public class MainManager : MonoBehaviour
{

    // Singleton
    public static MainManager Instance;

    // Choice Buttons
    public Button buttonA;
    public Button buttonB;
    public Button buttonC;
    public Button quitButton;

    public TextMeshProUGUI statusBar;
    public TextMeshProUGUI deltaBar;

    public EventCard currentEvent;

    private void Awake()
    {
        // Singleton
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
                
        buttonA.GetComponent<Button>().onClick.AddListener(ChoiceA);
        buttonB.GetComponent<Button>().onClick.AddListener(ChoiceB);
		buttonC.GetComponent<Button>().onClick.AddListener(ChoiceC);
        quitButton.GetComponent<Button>().onClick.AddListener(QuitApplication);

        // Singleton
        DontDestroyOnLoad(gameObject);        
    }

    private void QuitApplication()
    {
        Application.Quit();
    }
    private void Start()
    {
        if (EventDeck.Instance != null)
        {
            DisplayNextEvent();
        }
    }

    private void ChoiceA()
    {
        HandleChoice(0, currentEvent.eventChoiceOptions[0]);
    }

    private void ChoiceB()
    {
        HandleChoice(1, currentEvent.eventChoiceOptions[1]);
    }

    private void ChoiceC()
    {
        HandleChoice(2, currentEvent.eventChoiceOptions[2]);
    }

    private void HandleChoice(int choice, ChoiceOption option)
    {
        // First of all, lets just check if this is Unique choice "Restart", and early exit with a restart if so
        if (option == ChoiceOption.Restart)
        {
            Debug.Log("HandleChoice(" + choice + ") called, option was " + option.ToString() + ", redirecting to ResetEventDeck!");
            EventDeck.Instance.ResetEventDeck();
            Bars.Instance.ResetValues();
            DisplayNextEvent();
            return;
        }

        Debug.Log("HandleChoice(" + choice + ") called, option was " + option.ToString() + "!");

        // Remove event from deckOfEvents and move it to burnedEvents
        EventDeck.Instance.burnedEvents.Add(currentEvent);
        EventDeck.Instance.deckOfEvents.Remove(currentEvent);
        currentEvent.isDone = true;

        // Add followUpEvent to deckOfEvents if set
        if (currentEvent.dataRef != null)
        {
            BarDelta resultDelta = new BarDelta();
            EventData followUpEvent = null;
            switch (choice)
            {
                case 0:
                    followUpEvent = currentEvent.dataRef.followUpEventChoiceA;
                    resultDelta = currentEvent.dataRef.choiceDeltaA;
                    break;
                case 1:
                    followUpEvent = currentEvent.dataRef.followUpEventChoiceB;
                    resultDelta = currentEvent.dataRef.choiceDeltaB;
                    break;
                case 2:
                    followUpEvent = currentEvent.dataRef.followUpEventChoiceC;
                    resultDelta = currentEvent.dataRef.choiceDeltaC;
                    break;
            }
            if (followUpEvent != null)
            {
                EventDeck.Instance.deckOfEvents.Add(new EventCard(false, followUpEvent.title, followUpEvent.story, followUpEvent.choiceOptions, followUpEvent));
                Debug.Log("followUpEvent added to deckOfEvents!");
            }
            Bars.Instance.lastDelta = resultDelta;
            Bars.Instance.AddDelta();                        
        }
                
        DisplayNextEvent();
    }

    private void DisplayNextEvent()
    {
        if (EventDeck.Instance != null) DisplayEvent(EventDeck.Instance.GetNextEvent());
    }

    private string EnumToReadable(string ParamEnum)
    {
        if (string.IsNullOrWhiteSpace(ParamEnum))
           return "";
        StringBuilder ReadableText = new StringBuilder(ParamEnum.Length * 2);
        ReadableText.Append(ParamEnum[0]);
        for (int i = 1; i < ParamEnum.Length; i++)
        {
            if (char.IsUpper(ParamEnum[i]) && ParamEnum[i - 1] != ' ')
                ReadableText.Append(' ');
            ReadableText.Append(ParamEnum[i]);
        }
        return ReadableText.ToString();
    }

    private void AdjustButtonsToFormationOfSize(int formationSize)
    {
        Vector3 refPos = buttonA.transform.localPosition;
        switch (formationSize)
        {
            case 1:
                buttonA.transform.localPosition = new Vector3(0f, refPos.y, refPos.z);
                break;
            case 2:
                buttonA.transform.localPosition = new Vector3(-200f, refPos.y, refPos.z);
                buttonB.transform.localPosition = new Vector3(200f, refPos.y, refPos.z);
                break;
            case 3:
                buttonA.transform.localPosition = new Vector3(-400f, refPos.y, refPos.z);
                buttonB.transform.localPosition = new Vector3(0f, refPos.y, refPos.z);
                buttonC.transform.localPosition = new Vector3(400f, refPos.y, refPos.z);
                break;
        }
    }

    public void DisplayEvent(EventCard paramEvent)
    {
        currentEvent = paramEvent;
        GameObject TitleText = GameObject.Find("EventTitleText");
        if (TitleText != null) 
        {
            TextMeshProUGUI ChangingText = TitleText.GetComponent<TMPro.TextMeshProUGUI>();
            ChangingText.text = paramEvent.eventTitle;
        }
        
        GameObject StoryText = GameObject.Find("EventStoryText");
        if (StoryText != null)
        {
            TextMeshProUGUI ChangingText = StoryText.GetComponent<TMPro.TextMeshProUGUI>();
            ChangingText.text = paramEvent.eventStory;
        }

        if (buttonA != null && buttonB != null && paramEvent.eventChoiceOptions.Length > 0)
        {
            buttonA.GetComponentInChildren<Text>().text = EnumToReadable(paramEvent.eventChoiceOptions[0].ToString());
            buttonB.gameObject.SetActive(false);
            buttonC.gameObject.SetActive(false);
            if (paramEvent.eventChoiceOptions.Length > 1)
            {
                buttonB.GetComponentInChildren<Text>().text = EnumToReadable(paramEvent.eventChoiceOptions[1].ToString());
                buttonB.gameObject.SetActive(true);
            }    
            if (paramEvent.eventChoiceOptions.Length > 2)
            {
                buttonC.GetComponentInChildren<Text>().text = EnumToReadable(paramEvent.eventChoiceOptions[2].ToString());
                buttonC.gameObject.SetActive(true);
            }
            AdjustButtonsToFormationOfSize(paramEvent.eventChoiceOptions.Length);
        }

        statusBar.text = Bars.Instance.GetStatusString();
        deltaBar.text = Bars.Instance.GetDeltaString();
    }
}
