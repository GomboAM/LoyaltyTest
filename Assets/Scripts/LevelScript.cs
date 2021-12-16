using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour
{
    [SerializeField] private Animator m_CharacterAnimator;

    private Animator m_LevelAnimator;
    public Animator GetCharacterAnimator => m_CharacterAnimator;

    private void OnEnable()
    {
        GameController.Instance.ActionWin += Win;
        GameController.Instance.ActionLose += Lose;
    }

    private void OnDisable()
    {
        GameController.Instance.ActionWin -= Win;
        GameController.Instance.ActionLose -= Lose;
    }

    private void Win()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Lose()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
