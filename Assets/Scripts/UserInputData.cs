using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UserInputData : MonoBehaviour
{
    [SerializeField] private InputField _spawnTimeIntervalInput;
    [SerializeField] private InputField _speedInput;
    [SerializeField] private InputField _distanceInput;

    private int _spawnTimeInterval = 1;
    private int _speed = 1;
    private int _distance = 1;

    public int SpawnTimeInterval
    {
        get => _spawnTimeInterval;
        private set
        {
            _spawnTimeInterval = value;
            SpawnTimeIntervalEntered?.Invoke(_spawnTimeInterval);
        }
    }

    public int Speed
    {
        get => _speed;
        private set
        {
            _speed = Mathf.Clamp(value, 0, int.MaxValue);
            SpeedUpdated?.Invoke(_speed);
        }
    }

    public int Distance
    {
        get => _distance;
        private set
        {
            _distance = Mathf.Clamp(value, 0, int.MaxValue);
            DistanceUpdated?.Invoke(_distance);
        }
    }

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

    private void OnSpawnTimeIntervalEndEdit(string input) => SpawnTimeInterval = GetIntegerFrom(input);

    private void OnSpeedEndEdit(string input) => Speed = GetIntegerFrom(input);

    private void OnDistanceEndEdit(string input) => Distance = GetIntegerFrom(input);

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