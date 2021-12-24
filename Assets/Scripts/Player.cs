using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using LionStudios.Suite.Analytics;

public class Player : Singleton<Player>
{
    [SerializeField] private GamePlayUI m_UI;
    [SerializeField] private ParticleSystem m_PositiveEffect, m_NegativeEffect;

    private Animator m_TargetAnimator;
    private QuestionData[] m_Data;
    private int m_CurrentQuestion = 0;
    private float m_CollectedKarma = 0f;

    protected override void Awake()
    {
        Application.targetFrameRate = 60;
        
        base.Awake();
        LevelScript level = LevelController.Instance.GetCurrentLevel;
        m_Data = DataController.Instance.GetQuestions;
        m_TargetAnimator = level.GetCharacterAnimator;
    }

    public void StartGame()
    {
        LionAnalytics.LevelStart(DataController.Instance.GetCurrentLevel, 0);
        ShowQuestion();
    }

    private void ShowQuestion()
    {
        if (m_CurrentQuestion < m_Data.Length)
        {
            QuestionData currentQuestion = m_Data[m_CurrentQuestion];
            m_UI.ShowQuestions(currentQuestion);
        }
        else
        {
            m_UI.HideUI();
            LionAnalytics.LevelComplete(DataController.Instance.GetCurrentLevel, 0);
            GameController.Instance.StageEnd(m_CollectedKarma);
        }
    }    

    public void SetAnswer(AnswerData _answer)
    {
        m_CurrentQuestion++;
        
        m_TargetAnimator.SetTrigger(StaticMethods.GetEmotionTrigger(_answer.GetEmotionType));
        m_CollectedKarma += _answer.GetKarmaValue * (_answer.GetType == KarmaType.Positive ? 1 : -1);
        m_CollectedKarma = Mathf.Clamp(m_CollectedKarma, -1f, 1f);

        if (_answer.GetType == KarmaType.Positive)
            m_PositiveEffect.Play();
        else
            m_NegativeEffect.Play();

        m_UI.UpdateProgressBars(m_CollectedKarma);

        ShowQuestion();        
    }
}
