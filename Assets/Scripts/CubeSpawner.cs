using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : MonoBehaviour
{
    private const int _minTimeInterval = 1;
    private const int _maxRate = int.MaxValue;

    [SerializeField] private Cube _cubePrefab;   
    [SerializeField] private UserInputHandler _userInputHandler;
    private int _timeInterval = _minTimeInterval;
    private ObjectPool<Cube> _pool;
    private Coroutine _spawn;

    public int Rate
    {
        get => _timeInterval;
        private set
        {
            _timeInterval = Mathf.Clamp(value, 0, _maxRate);

            if(value >= _minTimeInterval && _spawn == null)
               _spawn = StartCoroutine(Spawn());
        } 
    }

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(Create, OnTakeFromPool, OnReturnFromPool);
        _userInputHandler.SpawnTimeIntervalEntered += OnUserEnteredTimeInterval;
    }

    private void Start() => _spawn = StartCoroutine(Spawn());

    private Cube Create()
    {
        var cube = Instantiate(_cubePrefab);
        cube.SetPool(_pool);      
        return cube;
    }

    private void OnReturnFromPool(Cube cube)
    {
        cube.gameObject.SetActive(false);
        cube.transform.position = _cubePrefab.transform.position;
    }

    private void OnTakeFromPool(Cube cube) => cube.gameObject.SetActive(true);

    private void OnUserEnteredTimeInterval(int interval) => Rate = interval;

    private IEnumerator Spawn()
    {
        while (Rate > 0)
        {
            GetCube();
            yield return new WaitForSeconds(Rate);
        }   
        _spawn = null;
    }

    private void GetCube() => _pool.Get();   
}