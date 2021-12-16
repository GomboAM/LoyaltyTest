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
        GameController.Instance.Action_UpdateProgress += FinishAnimation;
    }

    private void OnDisable()
    {
        GameController.Instance.Action_UpdateProgress -= FinishAnimation;
    }

    private void FinishAnimation(float _karma)
    {
        Invoke("NextLevel", 1f);
    }

    private void NextLevel()
    {
        SceneManager.LoadScene(0);
    }
}
