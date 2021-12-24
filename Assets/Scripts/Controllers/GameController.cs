using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : Singleton<GameController>
{
    public event Action Action_GameEnd;
    public event Action<float> Action_UpdateProgress;

    private float m_LevelKarma;

    public float GetLevelKarma => m_LevelKarma;

    public void StageEnd(float _karmaValue)
    {
        m_LevelKarma = _karmaValue;
        Action_UpdateProgress?.Invoke(_karmaValue);
    }

    public void GameEnd()
    {
        Action_GameEnd?.Invoke();
    }
}
