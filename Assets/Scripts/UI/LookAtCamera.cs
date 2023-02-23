using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform _cameraTransform;

    void Start()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        PerformRotation();
    }

    private void PerformRotation()
    {
        var cameraTransform = _cameraTransform.forward;
        transform.LookAt(cameraTransform + transform.position);
    }
}
