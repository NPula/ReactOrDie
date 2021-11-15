using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManagerT<T> : Singleton<EventManagerT<T>>
{
    public Dictionary<string, Action<EventParam>> dictionary = new Dictionary<string, Action<EventParam>>();

    public void StartListening(string eventName, Action<EventParam> listener)
    {
        Action<EventParam> thisEvent;
        if (Instance.dictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent += listener;
            Instance.dictionary[eventName] = thisEvent;
        }
        else
        {
            thisEvent += listener;
            Instance.dictionary.Add(eventName, thisEvent);
        }
    }

    public void StopListening(string eventName, Action<EventParam> listener)
    {
        if (Instance != null)
        {
            return;
        }

        Action<EventParam> thisEvent;
        if (Instance.dictionary.TryGetValue(eventName, out thisEvent))
        {
            //Remove event from the existing one
            thisEvent -= listener;

            //Update the Dictionary
            Instance.dictionary[eventName] = thisEvent;
        }
    }

    public static void TriggerEvent(string eventName, EventParam param)
    {
        Action<EventParam> thisEvent = null;
        if (Instance.dictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(param);
            // OR USE instance.eventDictionary[eventName]();
        }
    }

    public struct EventParam
    {
        public T param1;
        public T param2;
        public T param3;
        public T param4;
    } 
}
