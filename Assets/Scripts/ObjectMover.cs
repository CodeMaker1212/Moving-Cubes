using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ObjectMover : MonoBehaviour
{
    [SerializeField][Range(0f, 100f)] private float _speed = 5f;
    [SerializeField] private float _distance = 10f;
    private Vector3 _targetPosition;

    public event UnityAction TargetDistanceCovered;

    public float Speed
    {
        get => _speed * Time.deltaTime;
        private set => _speed = Mathf.Clamp(value, 0, float.MaxValue);
    }
     
    private void OnEnable() => _targetPosition = transform.position + Vector3.forward * _distance;

    private void Update()
    {
        if (enabled)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, Speed);
            if (transform.position.z == _targetPosition.z)
                TargetDistanceCovered?.Invoke();
        }          
    }
}