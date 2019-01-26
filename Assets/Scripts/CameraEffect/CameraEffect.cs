using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffect : MonoBehaviour
{
    private Transform cameraTransform;
    private bool isShakingCamera = false;
    private float _currentShakeStrength = 0;
    private float _currentPeriod = 0;
    private float _currentTimeCount = 0;

    public void Init()
    {
        cameraTransform = Singleton.instance.mainCamera.transform;
    }

    public void ShakeCamera(float strenght, float period)
    {
        _currentShakeStrength = strenght;
        _currentPeriod = period;
        _currentTimeCount = 0;
        if (isShakingCamera)
        {
            return;
        }
        StartCoroutine(ShakeCameraEnumerator());
    }

    private IEnumerator ShakeCameraEnumerator()
    {
        isShakingCamera = true;
        Vector3 position = Vector3.zero;
        while (_currentTimeCount < _currentPeriod)
        {
            position.x = Random.Range(-_currentShakeStrength, _currentShakeStrength);
            position.y = Random.Range(-_currentShakeStrength, _currentShakeStrength);
            cameraTransform.localPosition = position;
            _currentTimeCount += Time.deltaTime;
            yield return null;
        }
        cameraTransform.localPosition = Vector3.zero;
        isShakingCamera = false;
    }
}
