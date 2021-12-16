using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Singleton<Player>
{
    [SerializeField] private Transform m_AnswerContainer;
    [SerializeField] private AnswerItem m_AnswerPrefab;
    [SerializeField] private Text m_QuestionText;
    [SerializeField] private Image m_PositiveProgress, m_NegativeProgress;

    private Animator m_TargetAnimator;
    private QuestionData[] m_Data;
    private int m_CurrentQuestion = 0;
    private float m_CollectedKarma = 0f;

    private List<GameObject> m_Answers = new List<GameObject>();

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
       

        for (int i = 0; i < m_Answers.Count; i++)
        {
            Destroy(m_Answers[i].gameObject);
        }

        m_Answers.Clear();

        if (m_CurrentQuestion < m_Data.Length)
        {
            QuestionData currentQuestion = m_Data[m_CurrentQuestion];
            for (int i = 0; i < currentQuestion.GetAnswers.Length; i++)
            {
                AnswerItem newAnswer = Instantiate(m_AnswerPrefab, m_AnswerContainer);
                newAnswer.SetAnswer(currentQuestion.GetAnswers[i]);
                m_Answers.Add(newAnswer.gameObject);
            }

            m_QuestionText.text = currentQuestion.GetText;
        }
        else
        {
            GameController.Instance.StageEnd(m_CollectedKarma);
        }
    }    

    public void SetAnswer(AnswerData _answer)
    {
        m_CurrentQuestion++;
        
        m_TargetAnimator.SetTrigger(StaticMethods.GetEmotionTrigger(_answer.GetEmotionType));
        m_CollectedKarma += _answer.GetKarmaValue * (_answer.GetType == KarmaType.Positive ? 1 : -1);
        m_CollectedKarma = Mathf.Clamp(m_CollectedKarma, -1f, 1f);

        if (m_CollectedKarma > 0)
        {
            m_PositiveProgress.fillAmount = m_CollectedKarma;
            m_NegativeProgress.fillAmount = 0;
        }
        else
        {
            m_NegativeProgress.fillAmount = Mathf.Abs(m_CollectedKarma);
            m_PositiveProgress.fillAmount = 0;
        }
        ShowQuestion();        
    }
}
