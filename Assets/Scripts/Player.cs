using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player : Singleton<Player>
{
    [SerializeField] private GamePlayUI m_UI;

    private Animator m_TargetAnimator;
    private QuestionData[] m_Data;
    private int m_CurrentQuestion = 0;
    private float m_CollectedKarma = 0f;

    protected override void Awake()
    {
        base.Awake();
        LevelScript level = LevelController.Instance.GetCurrentLevel;
        m_Data = DataController.Instance.GetQuestions;
        m_TargetAnimator = level.GetCharacterAnimator;
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
            GameController.Instance.StageEnd(m_CollectedKarma);
        }
    }    

    public void SetAnswer(AnswerData _answer)
    {
        m_CurrentQuestion++;
        
        m_TargetAnimator.SetTrigger(StaticMethods.GetEmotionTrigger(_answer.GetEmotionType));
        m_CollectedKarma += _answer.GetKarmaValue * (_answer.GetType == KarmaType.Positive ? 1 : -1);
        m_CollectedKarma = Mathf.Clamp(m_CollectedKarma, -1f, 1f);

        m_UI.UpdateProgressBars(m_CollectedKarma);

        ShowQuestion();        
    }
}
