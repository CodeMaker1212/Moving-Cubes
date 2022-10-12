using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField][Range(0, 5f)] private float _timeIntervalInSeconds = 1f;
    private ObjectPool<Cube> _pool;

    public float TimeIntervalInSeconds
    {
        get => _timeIntervalInSeconds;
        private set => _timeIntervalInSeconds = Mathf.Clamp(value, 0, float.MaxValue);
    }

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(Create, OnTakeFromPool, OnReturnFromPool);
    }

    private void Start() => StartCoroutine(Spawn());

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

    private IEnumerator Spawn()
    {
        while (true)
        {
            GetCube();
            yield return new WaitForSeconds(TimeIntervalInSeconds);
        }       
    }

    private void GetCube() => _pool.Get();
}