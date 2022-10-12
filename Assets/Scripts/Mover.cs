using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;
    private Vector3 _targetPosition;

    private void OnEnable()
    {
        _targetPosition = new Vector3(transform.position.x, transform.position.y, _distance);
    }

    private void Update()
    {
        if (enabled)      
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
    }
}