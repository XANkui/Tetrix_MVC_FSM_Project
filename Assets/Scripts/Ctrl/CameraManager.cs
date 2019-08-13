using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour {

    private Camera mainCamera;

    void Awake() {
        mainCamera = Camera.main;
    }

    // 放大
    public void ZoomIn() {
        mainCamera.DOOrthoSize(14.0f, 0.5f);
    }

    // 缩小
    public void ZoomOut()
    {
        mainCamera.DOOrthoSize(18.0f,0.5f);
    }
}
