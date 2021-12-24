using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartUI : MonoBehaviour
{
    [SerializeField] private Image m_ProgressLoseBar, m_ProgressWinBar;
    [SerializeField] private Button m_StartButton;

    private CanvasGroup m_CG;

    private void Awake()
    {
        m_CG = GetComponent<CanvasGroup>();
        m_StartButton.onClick.AddListener(StartButtonClick);

        float progressKarma = 0.125f * DataController.Instance.GetProgressKarma;
        m_ProgressLoseBar.fillAmount = progressKarma < 0 ? Mathf.Abs(progressKarma) : 0f;
        m_ProgressWinBar.fillAmount = progressKarma > 0 ? progressKarma : 0f;
    }

    private void StartButtonClick()
    {
        m_CG.blocksRaycasts = false;

        m_CG.DOFade(0f, 0.1f)
            .OnComplete(() => 
            {
                Player.Instance.StartGame();
            });
    }
}
