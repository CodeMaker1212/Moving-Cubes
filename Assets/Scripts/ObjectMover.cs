using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ObjectMover : MonoBehaviour
{
    private float _speed;
    private float _distance;
    private UserInputData _inputData;
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
        _inputData = FindObjectOfType<UserInputData>();
        _inputData.SpeedUpdated += OnUserUpdatedSpeed;
        _inputData.DistanceUpdated += OnUserUpdatedDistance;
        Speed = _inputData.Speed;
      
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

    private void OnUserUpdatedSpeed(int value) => Speed = value;

    private void OnUserUpdatedDistance(int value) => Distance = value;
}