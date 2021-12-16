using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[CreateAssetMenu(fileName = "New ChapterData", menuName = "Create Chapter Data", order = 54)]
public class ChapterData : ScriptableObject
{
    [SerializeField] private ChapterType m_ChapterType;
    [SerializeField] private int m_ChapterIndex;
    [SerializeField] private List<StageData> m_StagesList = new List<StageData>();    

    public int GetChapterIndex => m_ChapterIndex;

    public ChapterType GetChapterType => m_ChapterType;

    public StageData GetCurrentStage(float _karma, int _StageIndex)
    {
        return m_StagesList.FirstOrDefault(a => a.IsActive(_karma, _StageIndex));
    }
}

[Serializable]
public class StageData
{
    [SerializeField] private string m_StageName;
    [SerializeField] private ConditionData m_StartCondition, m_EndCondition;
    [SerializeField] private int StageIndex = 0;
    [SerializeField] private Transform m_LevelPrefab;
    [SerializeField] private bool m_IsChapterEnd = false;
    [SerializeField] private List<CutSceneData> m_CutScenesList = new List<CutSceneData>();
    [SerializeField] private QuestionData[] m_Questions;

    public Transform GetLevelPrefab => m_LevelPrefab;
    public QuestionData[] GetQuestions => m_Questions;

    public bool IsActive(float _karma, int _StageIndex)
    {
        bool isActive = false;

        if (_StageIndex == StageIndex)
        {
            switch (m_StartCondition.GetEntryType)
            {
                case EntryType.Defualt:
                    isActive = true;
                    break;
                case EntryType.Greater:
                    isActive = _karma >= m_StartCondition.GetTriggerValue;
                    break;
                case EntryType.Less:
                    isActive = _karma <= m_StartCondition.GetTriggerValue;
                    break;
                case EntryType.Equals:
                    isActive = _karma == m_StartCondition.GetTriggerValue;
                    break;
                default:
                    break;
            }
        }

        return isActive;
    }

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
