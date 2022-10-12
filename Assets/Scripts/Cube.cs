using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(ObjectMover))]
public class Cube : MonoBehaviour
{
    private ObjectMover _mover;
    private IObjectPool<Cube> _pool;

    private void Awake()
    {
        _mover = GetComponent<ObjectMover>();
        _mover.TargetDistanceCovered += Disappear;
    }

    public void SetPool(IObjectPool<Cube> pool) => _pool = pool;

    private void Disappear() => _pool.Release(this); 
}