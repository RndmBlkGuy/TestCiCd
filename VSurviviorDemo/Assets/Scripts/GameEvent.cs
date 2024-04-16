using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New GameEvent", menuName = "Game Event/Game Event")]
public class GameEvent : ScriptableObject
{
    // A list of listeners that this event will notify if it is raised.
    private readonly List<GameEventListener> eventListeners = new List<GameEventListener>();

    public void Raise()
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
        {
            eventListeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(GameEventListener listener)
    {
        if (!eventListeners.Contains(listener))
        {
            eventListeners.Add(listener);
        }
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (eventListeners.Contains(listener))
        {
            eventListeners.Remove(listener);
        }
    }
}
