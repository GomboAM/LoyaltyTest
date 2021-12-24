using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[CreateAssetMenu(fileName = "New ChapterData", menuName = "Create Chapter Data", order = 54)]
public class ChapterData : ScriptableObject
{
    [SerializeField] private string m_StartStage;
    [SerializeField] private ChapterType m_ChapterType;
    [SerializeField] private int m_ChapterIndex;
    [SerializeField] private List<StageData> m_StagesList = new List<StageData>();

    public string GetStartStage => m_StartStage;
    public int GetChapterIndex => m_ChapterIndex;

    public ChapterType GetChapterType => m_ChapterType;

    public StageData GetCurrentStage(string _stageName)
    {
        return m_StagesList.FirstOrDefault(a => a.GetStageName == _stageName);
    }
}

[Serializable]
public class StageData
{
    [SerializeField] private string m_StageName;
    [SerializeField] private NetStageData[] m_NextStages;
    [SerializeField] private ConditionData m_EndCondition;
    [SerializeField] private int m_LoseKarma = -1, m_WinKarma = 1;
    [SerializeField] private Transform m_LevelPrefab;
    [SerializeField] private bool m_IsChapterEnd = false;
    [SerializeField] private List<CutSceneData> m_CutScenesList = new List<CutSceneData>();
    [SerializeField] private QuestionData[] m_Questions;

    public string GetStageName => m_StageName;
    public Transform GetLevelPrefab => m_LevelPrefab;
    public QuestionData[] GetQuestions => m_Questions;

    public bool IsChapterEnd(float _karma)
    {
        bool isEnd = m_IsChapterEnd;

        switch (m_EndCondition.GetEntryType)
        {
            case EntryType.Defualt:
                break;
            case EntryType.Greater:
                isEnd = _karma >= m_EndCondition.GetTriggerValue;
                break;
            case EntryType.Less:
                isEnd = _karma <= m_EndCondition.GetTriggerValue;
                break;
            case EntryType.Equals:
                isEnd = _karma == m_EndCondition.GetTriggerValue;
                break;
            default:
                break;
        }


        return isEnd;
    }

    public string GetNextStageName(float _karma)
    {
        string name = string.Empty;

        for (int i = 0; i < m_NextStages.Length; i++)
        {
            switch (m_NextStages[i].GetStartCondition.GetEntryType)
            {
                case EntryType.Defualt:
                    name = m_NextStages[i].GetNextStageName;
                    break;
                case EntryType.Greater:
                    name = _karma >= m_NextStages[i].GetStartCondition.GetTriggerValue ? m_NextStages[i].GetNextStageName : string.Empty;
                    break;
                case EntryType.Less:
                    name = _karma <= m_NextStages[i].GetStartCondition.GetTriggerValue ? m_NextStages[i].GetNextStageName : string.Empty;
                    break;
                case EntryType.Equals:
                    name = _karma == m_NextStages[i].GetStartCondition.GetTriggerValue ? m_NextStages[i].GetNextStageName : string.Empty;
                    break;
                default:
                    break;
            }

            if (!string.IsNullOrEmpty(name))
                break;
        }

        return name;
    }

    public int GetLevelKarma(float _karma)
    {
        return _karma < 0 ? m_LoseKarma : m_WinKarma;
    }
}

[Serializable]
public class CutSceneData
{
    [SerializeField] private Transform m_CutScenePrefab;
    //[SerializeField] private EntryType m_EntryType = EntryType.Defualt;
    //[SerializeField] private float m_TriggerValue = 0f;

    public Transform GetCutScenePrefab => m_CutScenePrefab;

    //public bool IsShow(float _karma)
    //{
    //    bool isActive = false;
    //    switch (m_EntryType)
    //    {
    //        case EntryType.Defualt:
    //            isActive = true;
    //            break;
    //        case EntryType.Greater:
    //            isActive = _karma > m_TriggerValue;
    //            break;
    //        case EntryType.Less:
    //            isActive = _karma < m_TriggerValue;
    //            break;
    //        case EntryType.Equals:
    //            isActive = _karma == m_TriggerValue;
    //            break;
    //        default:
    //            break;
    //    }
    //    return isActive;
    //}
}

[Serializable]
public class QuestionData
{
    [SerializeField] private string Text;
    [SerializeField] private AnswerData[] Answers;

    public string GetText => Text;
    public AnswerData[] GetAnswers => Answers;
}

[Serializable]
public class AnswerData
{
    [SerializeField] private string Text;
    [SerializeField] private KarmaType Type;
    [SerializeField] private EmotionType EmotionType;
    [Range(0.01f, 1f)]
    [SerializeField] private float KarmaValue = 0.01f;

    public string GetText => Text;
    public KarmaType GetType => Type;
    public float GetKarmaValue => KarmaValue;
    public EmotionType GetEmotionType => EmotionType;
}

[Serializable]
public class ConditionData
{
    [SerializeField] private EntryType m_EntryType;
    [SerializeField] private float m_TriggerValue;

    public EntryType GetEntryType => m_EntryType;
    public float GetTriggerValue => m_TriggerValue;
}

[Serializable]
public class NetStageData
{
    [SerializeField] private ConditionData m_StartCondition;
    [SerializeField] private string m_NextStageName;

    public ConditionData GetStartCondition => m_StartCondition;
    public string GetNextStageName => m_NextStageName;
}
