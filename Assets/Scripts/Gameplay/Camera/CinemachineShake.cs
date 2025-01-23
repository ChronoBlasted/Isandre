using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

public class CinemachineShake : MonoBehaviour
{
    [SerializeField] CinemachineCamera cinemachineCamera;

    Tweener _shakeTween;
    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;

    private void Awake()
    {
        cinemachineBasicMultiChannelPerlin = cinemachineCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float intensity, float duration)
    {
        if (_shakeTween.IsActive()) _shakeTween.Kill();

        cinemachineBasicMultiChannelPerlin.AmplitudeGain = intensity;

        _shakeTween = DOVirtual.Float(cinemachineBasicMultiChannelPerlin.AmplitudeGain, 0, duration, x => cinemachineBasicMultiChannelPerlin.AmplitudeGain = x);

    }
}