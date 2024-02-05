using Cinemachine;
using NaughtyAttributes;
using UnityEngine;

[ExecuteInEditMode]
public class CinemachineBillboard : MonoBehaviour
{
    private Transform _cameraTransform;

    private void OnEnable()
    {
        _cameraTransform = Camera.main.transform;
        CinemachineCore.CameraUpdatedEvent.AddListener(OnCameraUpdated);
    }

    private void OnDisable()
    {
        CinemachineCore.CameraUpdatedEvent.RemoveListener(OnCameraUpdated);
    }

    private void OnCameraUpdated(CinemachineBrain brain)
    {
        transform.forward = _cameraTransform.forward;
    }
}