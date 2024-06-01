using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraShakeHandler : MonoBehaviour
{
    public static CameraShakeHandler Instance { get; private set; }
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
    }

    public void LaunchCameraShake(float intensity, float seconds)
    {
        StartCoroutine(CameraShake(intensity, seconds));
    }

    IEnumerator CameraShake(float intensity, float seconds)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

        yield return new WaitForSeconds(seconds);

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
    }
}
