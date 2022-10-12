using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Windows;

public enum InputParameter {SpawnRate, Speed, Distance}
public class UserInputHandler : MonoBehaviour
{
    [SerializeField] private InputField _spawnRateField;
    [SerializeField] private InputField _speedField;
    [SerializeField] private InputField _distanceField;

    public event UnityAction<int> SpawnRateEntered;
    public event UnityAction<int> SpeedEntered;
    public event UnityAction<int> DistanceEntered;

    private void Awake()
    {
        _spawnRateField.contentType = InputField.ContentType.IntegerNumber;
        _speedField.contentType = InputField.ContentType.IntegerNumber;
        _distanceField.contentType = InputField.ContentType.IntegerNumber;

        _spawnRateField.onEndEdit.AddListener(OnSpawnRateEndEdit);
        _speedField.onEndEdit.AddListener(OnSpeedEndEdit);
        _distanceField.onEndEdit.AddListener(OnDistanceEndEdit);
    }

    private void OnSpawnRateEndEdit(string input) => SpawnRateEntered?.Invoke(GetIntegerFrom(input));

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