using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum ChoiceOption { Accept, Decline, TakeRisk, TakeCaution, React, Ignore, Cheat, Lie, Forgive, Confess, Restart };

[System.Serializable]
public struct EventCard
{
    public bool isDone;
    public string eventTitle;
    public string eventStory;
    public EventData dataRef;
    public ChoiceOption[] eventChoiceOptions;

    //Constructor (not necessary, but helpful)
    public EventCard(bool newIsDone, string newTitle, string newStory, ChoiceOption[] newOptions, EventData newDataRef = null)
    {
        this.isDone = newIsDone;
        this.eventTitle = newTitle;
        this.eventStory = newStory;
        this.eventChoiceOptions = newOptions;
        this.dataRef = newDataRef;
    }
}

public class EventDeck : MonoBehaviour
{
    public static EventDeck Instance;
    
    public List<EventData> includeData = new List<EventData>();

    public List<EventCard> deckOfEvents;
    public List<EventCard> burnedEvents;
    public List<EventCard> reserveOfEvents;
    private int currentDeckIndex;

    public EventCard GetNextEvent()
    {
        ChoiceOption[] optionsError = { ChoiceOption.Restart };
        if (deckOfEvents.Count == 0) return new EventCard(false, "Events dried up", "Event Deck is empty, make other Choices to populate FollowUp events to Event pool", optionsError);
        currentDeckIndex += 1;
        if (currentDeckIndex >= deckOfEvents.Count)
        {
            currentDeckIndex = 0;
        }
        Debug.Log($"currentDeckIndex = {currentDeckIndex} deckOfEvents.Count = {deckOfEvents.Count}");
        return deckOfEvents[currentDeckIndex];
    }

    private void Awake()
    {
        // Singleton
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;                        
        
        ResetEventDeck();

        // Singleton
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
    public void ResetEventDeck()
    {
        currentDeckIndex = -1;
        deckOfEvents = new List<EventCard>();
        burnedEvents = new List<EventCard>();
        reserveOfEvents = new List<EventCard>();
                    // Populate EventData from ScriptableObjects assets
        foreach (EventData ed in Resources.FindObjectsOfTypeAll(typeof(EventData)) as EventData[])
        {
            if (ed.startsInDeck)
            {
                deckOfEvents.Add(new EventCard(false, ed.title, ed.story, ed.choiceOptions, ed));
                Debug.Log("deckOfEvents.Add for " + ed.name);
            }
            else
            {
                Debug.Log("reserveOfEvents.Add for " + ed.name);
                reserveOfEvents.Add(new EventCard(false, ed.title, ed.story, ed.choiceOptions, ed));
            }            
        }
    }
}
