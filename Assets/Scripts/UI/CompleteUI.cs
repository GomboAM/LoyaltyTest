using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CompleteUI : MonoBehaviour
{    
    [SerializeField] private Button m_NextButton;
    [SerializeField] private Image m_LevelLoseKarmaBar, m_LevelWinKarmaBar, m_ProgressLoseBar, m_ProgressWinBar;
    [SerializeField] private CanvasGroup m_LevelProgressCG;
    [SerializeField] private RectTransform m_KarmaValue;

    private float m_PreviewKarma = 0f;

    private CanvasGroup m_CG;

    private void Awake()
    {
        m_CG = GetComponent<CanvasGroup>();

        m_NextButton.onClick.AddListener(NextButtonClick);
    }

    private void Start()
    {
        float progressKarma = 0.125f * DataController.Instance.GetProgressKarma;
        m_ProgressWinBar.fillAmount = progressKarma > 0 ? progressKarma : 0f;
    }

    private void OnEnable()
    {
        GameController.Instance.Action_GameEnd += EndGame;
    }

    private void OnDisable()
    {
        GameController.Instance.Action_GameEnd -= EndGame;
    }

    private void NextButtonClick()
    {
        DOTween.KillAll();
        SceneManager.LoadScene(0);
    }

    private void EndGame()
    {
        float levelKarma = GameController.Instance.GetLevelKarma;

        m_LevelLoseKarmaBar.fillAmount = levelKarma < 0 ? Mathf.Abs(levelKarma) : 0f;
        m_LevelWinKarmaBar.fillAmount = levelKarma > 0 ? levelKarma : 0f;

        int reward = DataController.Instance.GetRewardKarma;

        m_KarmaValue.GetComponent<Text>().text = (reward < 0 ? "" : "+") + reward.ToString();
        m_KarmaValue.GetComponent<Text>().color = reward < 0 ? Color.red : Color.green;

        Sequence seq = DOTween.Sequence()
            .Append(m_CG.DOFade(1f, 0.25f).OnComplete(()=> { m_CG.blocksRaycasts = true; }))
            .Append(m_KarmaValue.DOAnchorPosY(250f, 0.5f))
            .Join(m_KarmaValue.DOScale(4f, 0.5f))
            .Append(m_LevelProgressCG.DOFade(0f, 0.5f))
            .Append(m_KarmaValue.DOAnchorPosY(50f, 0.5f).SetEase(Ease.Linear))
            .Append(m_KarmaValue.DOAnchorPosY(-50f, 0.35f).SetEase(Ease.Linear))
            .Join(m_KarmaValue.DOScale(0f, 0.35f))
            .OnComplete(() =>
            {
                float progressKarma = 0.125f * DataController.Instance.GetProgressKarma;
                m_ProgressLoseBar.DOFillAmount(progressKarma < 0 ? Mathf.Abs(progressKarma) : 0f, 1f);
                m_ProgressWinBar.DOFillAmount(progressKarma > 0 ? progressKarma : 0f, 1f);
            });

        //float progressKarma = 0.125f * DataController.Instance.GetProgressKarma;

        //m_ProgressLoseBar.fillAmount = progressKarma < 0 ? Mathf.Abs(progressKarma) : 0f;
        //m_ProgressWinBar.fillAmount = progressKarma > 0 ? progressKarma : 0f;
    }
}
