using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    public Camera currentCamera;

    [SerializeField] CinemachineShake cinemachineShake;
    public void Init()
    {

    }

    public void ShakeCamera(float intensity = 4, float duration = .125f)
    {
        cinemachineShake.ShakeCamera(intensity, duration);
    }
}
