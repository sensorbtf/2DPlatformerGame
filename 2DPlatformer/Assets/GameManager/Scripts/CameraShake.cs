using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;
    private CinemachineBasicMultiChannelPerlin vmCamEffect;
    private float currentTime;
    private void Awake()
    {
        vmCamEffect = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        Instance = this;
    }

    private void Update()
    {
        if (!(currentTime > 0)) 
            return;
        
        currentTime -= Time.deltaTime;
        
        if (!(currentTime <= 0f)) 
            return;
        
        vmCamEffect.m_AmplitudeGain = 0;
    }

    public void Shake(float duration, float magnitude)
    {
        currentTime = duration;
        vmCamEffect.m_AmplitudeGain = magnitude;
    }
}
