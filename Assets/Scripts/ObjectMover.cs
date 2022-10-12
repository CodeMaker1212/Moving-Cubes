using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ObjectMover : MonoBehaviour
{
    [SerializeField][Range(0f, 100f)] private float _speed = 5f;
    [SerializeField][Range(0f, 10f)] private float _distance = 10f;
    private Vector3 _targetPosition;

    public event UnityAction TargetDistanceCovered;

    public float Speed
    {
        get => _speed * Time.deltaTime;
        private set => _speed = Mathf.Clamp(value, 0, float.MaxValue);
    }

    public float Distance
    {
        get => _distance;
        private set => _distance = Mathf.Clamp(value, 0, float.MaxValue);
    }
     
    private void OnEnable()
    {
        _targetPosition = transform.position + Vector3.forward * Distance;
        StartCoroutine(MoveForwardToTargetPoint());
    }

    private void OnDisable() => StopCoroutine(MoveForwardToTargetPoint());

    private IEnumerator MoveForwardToTargetPoint()
    {
        var waitForEndOfFrame = new WaitForEndOfFrame();
        while (transform.localPosition.z < _targetPosition.z)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _targetPosition, Speed);
            yield return waitForEndOfFrame;
        }
        TargetDistanceCovered?.Invoke();
    }
}