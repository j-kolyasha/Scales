using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private UIPresenter _uiPresenter;

    public void OnPause()
    {
        if (GameManager.Instance.GameStatus == GameStatus.Game)
        {
            GameManager.Instance.Pause();
            _uiPresenter.Pause();
        }
    }
    public void OnUnPause()
    {
        if (GameManager.Instance.GameStatus == GameStatus.Pause)
        {
            GameManager.Instance.UnPause();
            _uiPresenter.UnPause();
        }
    }
    public void ReloadLevel()
    {
        if (GameManager.Instance.GameStatus == GameStatus.Pause)
        {
            GameManager.Instance.ReloadLevel();
        }
    }
    public void ExitMainMenu()
    {
        if (GameManager.Instance.GameStatus == GameStatus.Pause)
        {
            GameManager.Instance.ExitTiMainMenu();
        }
    }
}
