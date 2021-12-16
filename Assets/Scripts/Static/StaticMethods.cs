using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;
using UnityEngine.EventSystems;
using System;

public static class StaticMethods
{
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

    public static string GetEmotionTrigger(EmotionType _type)
    {
        string trigger = "";

        switch (_type)
        {
            case EmotionType.Laugh:
                trigger = "laughing";
                break;
            case EmotionType.Angry:
                trigger = "angry";
                break;                
            default:
                break;
        }

        return trigger;
    }
}
