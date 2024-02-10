using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class AntiForeshorten : MonoBehaviour
{
    [SerializeField] private float _angle = 45f;

    [SerializeField, HideInInspector]
    Camera _camera;

    void OnValidate()
    {
        TryGetComponent(out _camera);
    }

    // Technically this only needs to happen once at start-up,
    // or when the window is being resized, but it's cheap
    // enough to do every frame in the absence of a built-in
    // OnProjectionChanged event.
    void LateUpdate()
    {
        // Get the default projection matrix for this camera.
        _camera.ResetProjectionMatrix();
        var mat = _camera.projectionMatrix;

        // Scale the vertical axis by 1/sin(angle).
        mat[1, 1] *= 1 / Mathf.Sin(Mathf.Deg2Rad * _angle);
        //sin 45 = sqr 2 / 2
        // 2 / sqr 2
        // Use our modified matrix.
        _camera.projectionMatrix = mat;
    }

    [Button]
    private void ResetProjectionMatrix()
    {
        _camera.ResetProjectionMatrix();
    }
}
