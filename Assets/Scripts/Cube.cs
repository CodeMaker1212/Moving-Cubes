using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Mover))]
public class Cube : MonoBehaviour
{
    private IObjectPool<Cube> _pool;
    private Vector3 _targetPosition;
    private Vector3 _startPosition;

    private void OnEnable()
    {      
        _startPosition = transform.position;
        _targetPosition = new Vector3(_startPosition.x, _startPosition.y, 10);
    }

    private void OnDisable() => transform.position = _startPosition;

    private void Update()
    {
        if(transform.position == _targetPosition) 
            Disappear();
    }

    public void SetPool(IObjectPool<Cube> pool) => _pool = pool;

    private void Disappear() => _pool.Release(this); 
}