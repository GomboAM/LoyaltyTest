using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataController : Singleton<DataController>
{
    [SerializeField] private List<ChapterData> m_Levels;
    [SerializeField] private List<ChapterType> m_ChapterSequence;

    private ProgressModel m_Progress;
    private ChapterModel m_ChapterProgress;
    private StageData m_CurrentStage;
    private int m_RewardKarma = 0, m_CurrentKarma = 0;
    public ProgressModel GetProgress => m_Progress;

    public Transform GetLevelPrefab => m_CurrentStage.GetLevelPrefab;
    public QuestionData[] GetQuestions => m_CurrentStage.GetQuestions;
    public int GetProgressKarma => m_CurrentKarma;
    public int GetRewardKarma => m_RewardKarma;
    public int GetCurrentLevel => m_Progress.CurrentLevel;

    protected override void Awake()
    {
        base.Awake();
        LoadData();
    }

    private void OnEnable()
    {
        GameController.Instance.Action_UpdateProgress += UpdateProgress;
    }

    private void OnDisable()
    {
        GameController.Instance.Action_UpdateProgress -= UpdateProgress;
    }

    private void UpdateProgress(float _karma)
    {
        m_RewardKarma = m_CurrentStage.GetLevelKarma(_karma);
        m_ChapterProgress.Karma += m_RewardKarma;
        m_ChapterProgress.Karma = Mathf.Clamp(m_ChapterProgress.Karma, -8, 8);
        m_Progress.CurrentLevel++;
        m_CurrentKarma = m_ChapterProgress.Karma;

        if (m_CurrentStage.IsChapterEnd(m_ChapterProgress.Karma))
        {
            m_ChapterProgress.Karma = 0;
            m_ChapterProgress.StageName = "Meeting";
            m_ChapterProgress.ChapterIndex += 1;
            int chaptersCount = m_Levels.Where(a => a.GetChapterType == m_ChapterProgress.ChapterType).Count();
            m_ChapterProgress.ChapterIndex = m_ChapterProgress.ChapterIndex - (int)(m_ChapterProgress.ChapterIndex / chaptersCount) * chaptersCount;
        }
        else
        {
            m_ChapterProgress.StageName = m_CurrentStage.GetNextStageName(m_ChapterProgress.Karma);
        }

        SaveData();
    }

    private void LoadData()
    {
        string data = PlayerPrefs.GetString(Constants.GameData, string.Empty);

        if (string.IsNullOrEmpty(data))
        {
            m_Progress = new ProgressModel();
            m_Progress.CurrentLevel = 0;

            for (int i = 0; i < Enum.GetNames(typeof(ChapterType)).Length; i++)
            {
                m_Progress.ChaptersList.Add(new ChapterModel()
                {
                    ChapterIndex = 0,
                    StageName = "Meeting",
                    ChapterType = (ChapterType)i,
                    Karma = 0
                });
            }

            SaveData();
        }
        else
        {
            m_Progress = JsonUtility.FromJson<ProgressModel>(data);
        }

        SelectCurrentStage();
    }

    private void SaveData()
    {
        PlayerPrefs.SetString(Constants.GameData, JsonUtility.ToJson(m_Progress));
    }

    private void SelectCurrentStage()
    {
        int convertedIndex = m_Progress.CurrentLevel - (int)(m_Progress.CurrentLevel / m_ChapterSequence.Count) * m_ChapterSequence.Count;
        m_ChapterProgress = m_Progress.ChaptersList.FirstOrDefault(b => b.ChapterType == m_ChapterSequence[convertedIndex]);
        m_CurrentKarma = m_ChapterProgress.Karma;

        ChapterData chapter = m_Levels.FirstOrDefault(a => a.GetChapterType == m_ChapterProgress.ChapterType && a.GetChapterIndex == m_ChapterProgress.ChapterIndex);

        m_CurrentStage = chapter.GetCurrentStage(m_ChapterProgress.StageName);        
    }
}
