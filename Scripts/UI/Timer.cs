using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Image _field;
    [SerializeField] private Text _text;

    public void StartTimer()
    {
        StartCoroutine(nameof(PlayTimer));
    }
    public void StopTimer()
    {
        StopCoroutine(nameof(PlayTimer));
    }
    private IEnumerator PlayTimer()
    {
        int time = GameManager.Instance.ConfigurationLevel.RemainingTime;

        for (int i = 0; i <= time; i++)
        {
            _text.text = TimeToString(time - i);
            _field.fillAmount = FillAmount(i);

            yield return new WaitForSeconds(1f);
        }
    }

    private string TimeToString(int time)
    {
        int minuts = (int)(time / 60);
        int seconds = (time % 60);

        return minuts.ToString() + ":" + seconds.ToString();
    }

    private float FillAmount(int seconds)
    {
        return 1f - ((seconds * 100f) / GameManager.Instance.ConfigurationLevel.AllLevelTime) / 100f;
    }
}
