using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Linq;
using System;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private Transform m_AnswerContainer, m_QuestionContainer;
    [SerializeField] private AnswerItem m_AnswerPrefab;
    [SerializeField] private Text m_QuestionText;
    [SerializeField] private CanvasGroup m_QuestionsCG;
    [SerializeField] private Image m_LoseProgressBar, m_WinProgressBar;

    private CanvasGroup m_CG;
    private List<RectTransform> m_Answers = new List<RectTransform>();
    private QuestionData m_Data;

    private void Awake()
    {
        m_CG = GetComponent<CanvasGroup>();

        Sequence seq = DOTween.Sequence()
            .Append(m_CG.DOFade(1f, 0.5f));
    }

    public void ShowQuestions(QuestionData _questions)
    {
        m_Data = _questions;
        HideQuestions();

        //m_QuestionsCG.blocksRaycasts = false;

        //m_SwitchSequence?.Kill();

        //m_SwitchSequence = DOTween.Sequence()
        //    .Append(m_QuestionsCG.DOFade(0f, 0.25f)
        //    .OnComplete(() => 
        //    {
        //        CreateQuestions(_questions);
        //    }))
        //    .Append(m_QuestionsCG.DOFade(1f, 0.25f))
        //    .OnComplete(()=> 
        //    {
        //        m_QuestionsCG.blocksRaycasts = true;
        //    });    
    }

    private void CreateQuestions()
    {

        for (int i = 0; i < m_Answers.Count; i++)
        {
            Destroy(m_Answers[i].gameObject);
        }

        m_Answers.Clear();

        AnswerData[] answers = m_Data.GetAnswers.OrderBy(a=> Guid.NewGuid()).ToArray();

        for (int i = 0; i < answers.Length; i++)
        {
            AnswerItem newAnswer = Instantiate(m_AnswerPrefab, m_AnswerContainer);
            newAnswer.SetAnswer(answers[i]);

            m_Answers.Add(newAnswer.GetComponent<RectTransform>());
        }

        m_QuestionText.text = m_Data.GetText;


        ShowQuestions();
    }

    private void ShowQuestions()
    {
        Sequence showSequence = DOTween.Sequence();

        showSequence
            .Append(m_QuestionContainer.DOScaleY(1f, 0.25f).SetEase(Ease.OutFlash))
            .Append(m_Answers[0].DOScaleY(1f, 0.25f).SetEase(Ease.OutFlash))
            .Append(m_Answers[1].DOScaleY(1f, 0.25f).SetEase(Ease.OutFlash));
                

        showSequence.
            OnComplete(() => { m_QuestionsCG.blocksRaycasts = true; });
    }

    private void HideQuestions()
    {
        m_QuestionsCG.blocksRaycasts = false;
        Sequence hideSequence = DOTween.Sequence()
            .AppendInterval(0.5f);

        if (m_Answers.Count > 0)
        {
            hideSequence
                .Append(m_Answers[1].DOScaleY(0f, 0.125f).SetEase(Ease.InFlash))
                .Append(m_Answers[0].DOScaleY(0f, 0.125f).SetEase(Ease.InFlash));
        }


        hideSequence
            .Append(m_QuestionContainer.DOScaleY(0f, 0.125f).SetEase(Ease.InFlash));

        hideSequence
            .AppendInterval(1f);

        hideSequence.OnComplete(() =>
        {
            CreateQuestions();
        });
    }

    public void UpdateProgressBars(float _collectedKarma)
    {
        m_LoseProgressBar.DOFillAmount(_collectedKarma < 0 ? Mathf.Abs(_collectedKarma) : 0f, 0.5f);
        m_WinProgressBar.DOFillAmount(_collectedKarma > 0 ? _collectedKarma : 0f, 0.5f);
    }

    public void HideUI()
    {
        m_CG.blocksRaycasts = false;
        Sequence seq = DOTween.Sequence()
            .Append(m_CG.DOFade(0f, 0.25f));            
    }
}
