using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : Singleton<GameController>
{
    public event Action ActionLose, ActionWin;
    public event Action<float> Action_UpdateProgress;

    public void StageEnd(float _karmaValue)
    {
        Action_UpdateProgress?.Invoke(_karmaValue);
    }

    public void GameEnd(float _karmaValue)
    {        
        if (_karmaValue > 0)
            ActionWin?.Invoke();
        else
            ActionLose?.Invoke();
    }
}
