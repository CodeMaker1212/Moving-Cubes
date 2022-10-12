using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(ObjectMover))]
public class Cube : MonoBehaviour
{
    private ObjectMover _mover;
    private IObjectPool<Cube> _pool;
    private Vector3 _startPosition;

    private void Awake()
    {
        _mover = GetComponent<ObjectMover>();
    }

    private void Start()
    {
        _startPosition = transform.position;
        _mover.TargetDistanceCovered += Disappear;
    }

    private void OnDisable() => transform.position = _startPosition;

    public void SetPool(IObjectPool<Cube> pool) => _pool = pool;

    private void Disappear() => _pool.Release(this); 
}