using UnityEngine;
using Cinemachine;
using System.Collections;

public class ScreenShakeController : MonoBehaviour
{
    public static ScreenShakeController Instance;
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeTimer;

    private void Awake()
    {
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();   
    }

    public void ShakeCamera(float intensity, float time)
    {
        if (gameObject != null)
        {
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
            cinemachineBasicMultiChannelPerlin.m_FrequencyGain = intensity;
            shakeTimer = time;
        }
    }

    public void Update()
    {
        if (gameObject != null)
        {
            if (shakeTimer > 0)
            {
                shakeTimer -= Time.deltaTime;
                if (shakeTimer <= 0f)
                {
                    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
                    cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 0;
                }
            }
        }
          
    }

}
