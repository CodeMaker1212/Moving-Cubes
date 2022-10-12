using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : MonoBehaviour
{
    private const int _minRate = 1;
    private const int _maxRate = int.MaxValue;

    [SerializeField] private Cube _cubePrefab;   
    [SerializeField] private UserInputHandler _userInputHandler;
    private int _rate = _minRate;
    private ObjectPool<Cube> _pool;
    private Coroutine _spawn;

    public int Rate
    {
        get => _rate;
        private set
        {
            _rate = Mathf.Clamp(value, 0, _maxRate);

            if(value > 0 && _spawn == null)
               _spawn = StartCoroutine(Spawn());
        } 
    }

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(Create, OnTakeFromPool, OnReturnFromPool);
        _userInputHandler.SpawnRateEntered += OnUserEnteredRate;
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

    private void OnUserEnteredRate(int interval) => Rate = interval;

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