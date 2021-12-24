using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : Singleton<LevelController>
{
    private LevelScript m_CurrentLevel;

    public LevelScript GetCurrentLevel => m_CurrentLevel;

    protected override void Awake()
    {
        base.Awake();
        CreateLevel();
    }

    private void CreateLevel()
    {
        m_CurrentLevel = Instantiate(DataController.Instance.GetLevelPrefab, transform).GetComponent<LevelScript>();
    }
}
