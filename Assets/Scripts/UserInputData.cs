using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UserInputData : MonoBehaviour
{
    [SerializeField] private InputField _spawnTimeIntervalInput;
    [SerializeField] private InputField _speedInput;
    [SerializeField] private InputField _distanceInput;
    [SerializeField] private Text _spawnTimeInterval;
    [SerializeField] private Text _speed;
    [SerializeField] private Text _distance;

    public int SpawnTimeInterval => Convert.ToInt32(_spawnTimeInterval.text);
    public int Speed => Convert.ToInt32(_speed.text);
    public int Distance => Convert.ToInt32(_distance.text);

    public event UnityAction<int> SpawnTimeIntervalEntered;
    public event UnityAction<int> SpeedUpdated;
    public event UnityAction<int> DistanceUpdated;

    private void Awake()
    {
        _spawnTimeIntervalInput.contentType = InputField.ContentType.IntegerNumber;
        _speedInput.contentType = InputField.ContentType.IntegerNumber;
        _distanceInput.contentType = InputField.ContentType.IntegerNumber;

        _spawnTimeIntervalInput.onEndEdit.AddListener(OnSpawnTimeIntervalEndEdit);
        _speedInput.onEndEdit.AddListener(OnSpeedEndEdit);
        _distanceInput.onEndEdit.AddListener(OnDistanceEndEdit);
    }

    private void OnSpawnTimeIntervalEndEdit(string input) => SpawnTimeIntervalEntered?.Invoke(GetIntegerFrom(input));

    private void OnSpeedEndEdit(string input) => SpeedUpdated?.Invoke(GetIntegerFrom(input));

    private void OnDistanceEndEdit(string input) => DistanceUpdated?.Invoke(GetIntegerFrom(input));

    private int GetIntegerFrom(string value)
    {
        try
        {
            return Convert.ToInt32(value);
        }
        catch
        {
            return default;
        }
    }
}