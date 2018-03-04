using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public enum CameraType : int {
        FollowPlayer,
        SplitScreen,
        GlobalCam
    }

    public Camera globalCam;

    public CameraType cameraType;
    public List<GameObject> targets;

    public Vector3 globalCenter;

    public float globalOrthoSize = 50f;
    public float splitScreenOrthoSize = 10f;
    public float followOrthoSize = 5f;

    public float cycleZoomSpeed = 1f;
    public float followZoomSpeed = .1f;

    public int _targetIndex = 0;

    private Sequence _lastSequence;

    public void CycleType()
    {
        cameraType++;
        if(((int)cameraType) >= Enum.GetValues(typeof(CameraType)).Length)
        {
            cameraType = 0;
        }
    }

    public void CycleTarget()
    {
        _targetIndex++;
        if (_targetIndex >= targets.Count)
            _targetIndex = 0;
    }
	
	private void LateUpdate () {
        if (_lastSequence != null && _lastSequence.IsPlaying())
            _lastSequence.Kill();

        switch (cameraType)
        {
            case CameraType.FollowPlayer:
                var targetPos = targets[_targetIndex].transform.position;
                var camPos = globalCam.transform.position;
                var newCamPos = new Vector3(targetPos.x, 0, targetPos.z);
                if (globalCam.orthographicSize != followOrthoSize)
                {
                    _lastSequence = DOTween.Sequence()
                        .Append(globalCam.DOOrthoSize(followOrthoSize, cycleZoomSpeed))
                        .Join(globalCam.transform.DOMove(newCamPos, followZoomSpeed));
                }
                else
                {
                    _lastSequence = DOTween.Sequence()
                        .Append(globalCam.transform.DOMove(newCamPos, followZoomSpeed));
                }

                Camera.SetupCurrent(globalCam);
                break;
            case CameraType.SplitScreen:
                cameraType = CameraType.GlobalCam; // go to global until we implement it.
                break;
            default: // == CameraType.GlobalCam
                _lastSequence = DOTween.Sequence()
                    .Append(globalCam.DOOrthoSize(globalOrthoSize, cycleZoomSpeed))
                    .Join(globalCam.transform.DOMove(globalCenter, cycleZoomSpeed));
                Camera.SetupCurrent(globalCam);
                break;
        }

	}
}
