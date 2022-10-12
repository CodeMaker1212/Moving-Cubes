using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ObjectMover : MonoBehaviour
{
    private UserInputData _inputData;
    private Vector3 _targetPosition;

    public event UnityAction TargetDistanceCovered;

    private void OnEnable()
    {
        _inputData = FindObjectOfType<UserInputData>();
        _targetPosition = transform.localPosition + Vector3.forward * _inputData.Distance;
           
        StartCoroutine(MoveForwardToTargetPoint());
    }

    private void OnDisable() => StopCoroutine(MoveForwardToTargetPoint());

    private IEnumerator MoveForwardToTargetPoint()
    {
        var waitForEndOfFrame = new WaitForEndOfFrame();
        while (transform.localPosition.z < _targetPosition.z)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _targetPosition, _inputData.Speed * Time.deltaTime);
            yield return waitForEndOfFrame;
        }
        TargetDistanceCovered?.Invoke();
    }
}