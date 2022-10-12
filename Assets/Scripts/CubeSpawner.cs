using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private float _timeIntervalInSeconds;
    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(Create, OnTakeFromPool, OnReturnFromPool);
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private Cube Create()
    {
        var cube = Instantiate(_cubePrefab);
        cube.SetPool(_pool);
        return cube;
    }

    private void OnReturnFromPool(Cube cube) => cube.gameObject.SetActive(false);

    private void OnTakeFromPool(Cube cube) => cube.gameObject.SetActive(true);

    private IEnumerator Spawn()
    {
        while (true)
        {
            GetCube();
            yield return new WaitForSeconds(_timeIntervalInSeconds);
        }       
    }

    private void GetCube() => _pool.Get();
}