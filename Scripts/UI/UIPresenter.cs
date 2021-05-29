using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIPresenter : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private Text _nameItem;
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private EndMenu _endMenu;

    private Iteraction _iteraction;

    public void Pause()
    {
        _timer.StopTimer();
        _pauseMenu.gameObject.SetActive(true);
    }
    public void UnPause()
    {
        _timer.StartTimer();
        _pauseMenu.gameObject.SetActive(false);
    }
    private void ChangeNameItem(string name)
    {
        _nameItem.text = name;
    }
    private void EndLevel(string message)
    {
        _endMenu.gameObject.SetActive(true);
        _endMenu.Message(message);
    }
    private void Start()
    {
        _iteraction = GameObject.FindObjectOfType<Iteraction>();
        _iteraction.ItemSelection += ChangeNameItem;
        GameManager.Instance.EndLevel += EndLevel;

        _timer.StartTimer();
    }
    private void OnDestroy()
    {
        _iteraction.ItemSelection -= ChangeNameItem;
        GameManager.Instance.EndLevel -= EndLevel;
    }
}
