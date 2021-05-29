public class ConfigurationLevel
{
    private int _countScale;
    private int _allLevelTime;
    private int _remainingTime;
    private int _minWidth, _maxWidth;
    private float _requiredAllWeight;
    private float _weightForOneScale;

    public int RemainingTime => _remainingTime;
    public int AllLevelTime => _allLevelTime;
    public float RequiredAllWeight => _requiredAllWeight;
    public float WeightForOneScale => _weightForOneScale;

    public ConfigurationLevel(int countScale, int levelTime, int minWidth, int maxWidth)
    {
        _countScale = countScale;
        _allLevelTime = levelTime;
        _remainingTime = levelTime;
        _minWidth = minWidth;
        _maxWidth = maxWidth;

        UpdateRequiredWieght();
    }

    public void ChangeRemainingTime(int time)
    {
        _remainingTime = time;
    }
    public void UpdateRequiredWieght()
    {
        _remainingTime = _allLevelTime;
        _requiredAllWeight = UnityEngine.Random.Range(_minWidth, _maxWidth);
        _weightForOneScale = RequiredAllWeight / _countScale;
    }
}