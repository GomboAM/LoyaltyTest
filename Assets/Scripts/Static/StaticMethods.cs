using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;
using UnityEngine.EventSystems;
using System;
using Random = UnityEngine.Random;

public static class StaticMethods
{
    private static string[] HappyEmotions = new string[] { "curious", "determined", "friendly", "happy", "proud", "laughing" };
    private static string[] SadEmotions = new string[] { "angry", "irritated", "sad", "sarcastic", "scared", "serious", "skeptical", "worried" };

    public static string GetEmotionTrigger(EmotionType _type)
    {
        return _type == EmotionType.Happy ? HappyEmotions[Random.Range(0, HappyEmotions.Length)] : SadEmotions[Random.Range(0, SadEmotions.Length)];
    }

    public static bool IsInternetConnected()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public static bool IsPointerOverUIElement(Vector3 _pointPosition)
    {
        var eventData = new PointerEventData(EventSystem.current);
        eventData.position = _pointPosition;
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
}
