using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Cube : MonoBehaviour
{
    private IObjectPool<Cube> _pool;

    public void SetPool(IObjectPool<Cube> pool) => _pool = pool;

    private void Deactivate() => _pool.Release(this);
}