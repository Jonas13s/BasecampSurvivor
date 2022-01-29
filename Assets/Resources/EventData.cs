using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "EventData/EventCard", order = 1)]
public class EventData : ScriptableObject
{
    public string title;
    public string story;
    public ChoiceOption[] choiceOptions;

    public bool startsInDeck = true;

    public EventData followUpEventChoiceA;
    public EventData followUpEventChoiceB;
    public EventData followUpEventChoiceC;
    public BarDelta choiceDeltaA;
    public BarDelta choiceDeltaB;
    public BarDelta choiceDeltaC;
}