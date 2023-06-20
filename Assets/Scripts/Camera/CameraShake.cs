using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Sirenix.OdinInspector;

public class CameraShake : MonoBehaviour
{
    //Singelton
    public static CameraShake Instance { get; private set; }

    [SerializeField] AnimationCurve defualtCurve;

    CinemachineVirtualCamera _cinemachineVirtualCamera;
    CinemachineBasicMultiChannelPerlin _channelPerlin;
    AnimationCurve _curve;
    float _shakeTimer;
    float _shakeTimerTotal;
    float _startingIntensity;
    Vector2 _offset;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        _channelPerlin =
           _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

    }

    [Button]
    public void Shake(float intensity, float time, AnimationCurve curve = null, Vector2 offset = default)
    {
        _curve = curve == null ? defualtCurve : curve;

        _offset = offset;

        _channelPerlin.m_AmplitudeGain = intensity;
        _startingIntensity = intensity;
        _shakeTimer = time;
        _shakeTimerTotal = time;

    }

    private void Update()
    {
        if (_shakeTimer <= 0f)
        {
            //make sure we dont have any camera shake
            if (_channelPerlin.m_AmplitudeGain != 0)
            {
                _channelPerlin.m_AmplitudeGain = 0f;
            }

            return;
        }

        _shakeTimer -= Time.deltaTime;

        float curveTimeValue = 1 - (_shakeTimer / _shakeTimerTotal);

        float curveEvul = _curve.Evaluate(curveTimeValue);

        //shake over time
        float currIntensity = curveEvul * _startingIntensity;
        _channelPerlin.m_AmplitudeGain = currIntensity;

        //offset over time
        float x = curveEvul * _offset.x;
        float y = curveEvul * _offset.y;
        _channelPerlin.m_PivotOffset = new Vector3(x, y, 0);

    }
}
