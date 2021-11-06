using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    private Dictionary<string, UnityEvent> eventDictionary;

    private static EventManager eventManager;
    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                // first time
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;
                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManager script on a GameObject in a scene");
                }
                else
                {
                    // initialize dictionary the first time
                    eventManager.Init();
                }
            }
            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, UnityEvent>();
        }
    }

    public static void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent e = null;
        if (instance.eventDictionary.TryGetValue(eventName, out e))
        {
            e.AddListener(listener);
        }
        else
        {
            e = new UnityEvent();
            e.AddListener(listener);
            instance.eventDictionary.Add(eventName, e);
        }

    }
    public static void StopListening(string eventName, UnityAction listener)
    {
        if (eventManager == null) return;
        UnityEvent e = null;
        if (instance.eventDictionary.TryGetValue(eventName, out e))
        {
            e.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName)
    {
        UnityEvent e = null;
        if (instance.eventDictionary.TryGetValue(eventName, out e))
        {
            e.Invoke();
        }
    }
}
