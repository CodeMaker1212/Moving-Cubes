using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UserInputData : MonoBehaviour
{
    [SerializeField] private InputField _spawnTimeInterval;
    [SerializeField] private InputField _speed;
    [SerializeField] private InputField _distance;

    public event UnityAction<int> SpawnTimeIntervalEntered;
    public event UnityAction<int> SpeedEntered;
    public event UnityAction<int> DistanceEntered;

    private void Awake()
    {
        _spawnTimeInterval.contentType = InputField.ContentType.IntegerNumber;
        _speed.contentType = InputField.ContentType.IntegerNumber;
        _distance.contentType = InputField.ContentType.IntegerNumber;

        _spawnTimeInterval.onEndEdit.AddListener(OnSpawnTimeIntervalEndEdit);
        _speed.onEndEdit.AddListener(OnSpeedEndEdit);
        _distance.onEndEdit.AddListener(OnDistanceEndEdit);
    }

    private void OnSpawnTimeIntervalEndEdit(string input) => SpawnTimeIntervalEntered?.Invoke(GetIntegerFrom(input));

    private void OnSpeedEndEdit(string input) => SpeedEntered?.Invoke(GetIntegerFrom(input));

    private void OnDistanceEndEdit(string input) => DistanceEntered?.Invoke(GetIntegerFrom(input));

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