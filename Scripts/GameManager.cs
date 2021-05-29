using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public event UnityAction<string> EndLevel;
    public static GameManager Instance;

    private ConfigurationLevel _configurationLevel;
    private GameStatus _gameStatus;
    private int _pastTense;

    public ConfigurationLevel ConfigurationLevel => _configurationLevel;
    public GameStatus GameStatus => _gameStatus;

    public void Pause()
    {
        Time.timeScale = 0f;
        _gameStatus = GameStatus.Pause;
        _configurationLevel.ChangeRemainingTime(_configurationLevel.RemainingTime - _pastTense);
        StopCoroutine(nameof(Loss));
    }
    public void UnPause()
    {
        Time.timeScale = 1f;
        _gameStatus = GameStatus.Game;
        StartCoroutine(nameof(Loss));
    }
    public void Victory()
    {
        _gameStatus = GameStatus.Pause;
        EndLevel?.Invoke("Victory");
    }
    public void ReloadLevel()
    {
        StopCoroutine(nameof(Loss));
        _configurationLevel.UpdateRequiredWieght();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Time.timeScale = 1f;
        _gameStatus = GameStatus.Game;
    }
    public void ExitTiMainMenu()
    {
        _gameStatus = GameStatus.Menu;
        SceneManager.LoadScene("MainMenu");
    }
    public void ExitGame()
    {
        if (_gameStatus != GameStatus.Menu) return;

        Application.Quit();
    }
    public void StartGame(LevelDifficulty difficulty)
    {
        _gameStatus = GameStatus.Game;
        switch (difficulty)
        {
            case LevelDifficulty.Easy:
                _configurationLevel = new ConfigurationLevel(2, 90, 30, 60);
                SceneManager.LoadScene("EasyLevel");
                break;
            case LevelDifficulty.Medium:
                _configurationLevel = new ConfigurationLevel(4, 120, 40, 100);
                SceneManager.LoadScene("MediumLevel");
                break;
            case LevelDifficulty.Hard:
                _configurationLevel = new ConfigurationLevel(6, 150, 50, 110);
                SceneManager.LoadScene("HardLevel");
                break;
        }

        StartCoroutine(nameof(Loss));
    }
    private IEnumerator Loss()
    {
        for (_pastTense = 0; _pastTense < _configurationLevel.RemainingTime; _pastTense++)
            yield return new WaitForSeconds(1f);

        _gameStatus = GameStatus.Pause;
        EndLevel?.Invoke("Loss");
    }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(gameObject);
        _gameStatus = GameStatus.Menu;
    }
}

public enum LevelDifficulty
{
    Easy,
    Medium,
    Hard,
}

public enum GameStatus
{
    Game,
    Pause,
    Menu,
}