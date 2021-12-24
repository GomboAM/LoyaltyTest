using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerItem : MonoBehaviour
{
    [SerializeField] private Button m_SelectButton;
    [SerializeField] private Text m_AnswerText;

    private AnswerData m_Data;

    private void Awake()
    {
        m_SelectButton.onClick.AddListener(SelectButtonClick);
    }

    private void SelectButtonClick()
    {
        m_SelectButton.image.color = new Color32(0, 255, 101, 255);
        Player.Instance.SetAnswer(m_Data);
    }

    public void SetAnswer(AnswerData _data)
    {
        m_Data = _data;
        m_AnswerText.text = m_Data.GetText;
    }
}
