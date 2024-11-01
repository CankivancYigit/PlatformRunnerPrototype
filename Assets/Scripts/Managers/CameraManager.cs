using System;
using UnityEngine;
using Cinemachine;
using UnityEngine.Serialization;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera runnerCamera;
    [FormerlySerializedAs("camera2")] [SerializeField] private CinemachineVirtualCamera wallPaintCamera;

    private CinemachineVirtualCamera _activeCamera;

    private void Start()
    {
        SetActiveCamera(runnerCamera);
    }

    private void OnEnable()
    {
        EventBus<PlayerReachedFinishEvent>.AddListener(OnPlayerReachFinish);
    }

    private void OnDisable()
    {
        EventBus<PlayerReachedFinishEvent>.RemoveListener(OnPlayerReachFinish);
    }

    private void OnPlayerReachFinish(object sender, PlayerReachedFinishEvent @event)
    {
        SetActiveCamera(wallPaintCamera);
    }

    private void SetActiveCamera(CinemachineVirtualCamera newCamera)
    {
        if (_activeCamera != null)
        {
            _activeCamera.Priority = 0;
        }
        
        _activeCamera = newCamera;
        _activeCamera.Priority = 10;
    }
}

