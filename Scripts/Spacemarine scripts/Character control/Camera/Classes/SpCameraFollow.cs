using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpCameraFollow : ISpCameraFollow
{
    float x;
    float y;

    bool isMeleeView;
    bool isZooming;

    Vector3 localStartPos;
    Vector3 currentLocalPos;
    Vector3 currentRotation;
    Vector3 targetRotation;
    Vector3 smoothVelocity;
    LayerMask toCollideWith;

    Transform targetToFollow;
    Transform cameraTransform;
    Transform pivotTransform;
    Transform cameraRoot;

    ISensitivity cameraSensitivity;
    ICameraOptions cameraSettings;
    IFollowObject cameraFollow;
    IRotateObject objectRotator;

    ISpStateView currentView;
    ISpStateView meleeView;
    ISpStateView rangedView;

    #region Getters and setters

    public LayerMask ToCollideWith { get { return this.toCollideWith; } }

    public Transform Root { get { return this.cameraRoot; } }

    public Transform TargetTransform { get { return this.targetToFollow; } }

    public Transform CameraTransform { get { return this.cameraTransform; } }

    public Transform PivotTransform { get { return this.pivotTransform; } }

    public ISensitivity SensitivityOptions { get { return this.cameraSensitivity; } }

    public ICameraOptions CameraSettings { get { return this.cameraSettings; } }

    public IFollowObject ObjectToFollow { get { return this.cameraFollow; } }

    public ISpStateView MeleeView { get { return this.meleeView; } }

    public ISpStateView RangedView { get { return this.rangedView; } }

    public IRotateObject ObjectRotator { get { return this.objectRotator; } }

    public bool IsMeleeViewEnabled
    {
        get { return this.isMeleeView; }
        set
        {
            isMeleeView = value;
            if (isMeleeView)
            {
                this.currentView = this.meleeView;
            }
            else
            {
                this.currentView = this.rangedView;
            }
        }
    }

    public bool IsZooming { get { return this.isZooming; } }
    #endregion

    public SpCameraFollow(Transform targetTransform, Transform cameraRoot, LayerMask collideWith,
        ref ISpStateView meleeView, ref ISpStateView rangedView, ref IRotateObject objectRotator)
    {
        this.cameraRoot = cameraRoot;
        this.pivotTransform = cameraRoot.GetChild(0);
        //cameras transform placed as the child of the pivot's position
        this.cameraTransform = pivotTransform.GetChild(0);
        this.targetToFollow = targetTransform;

        this.meleeView = meleeView;
        this.rangedView = rangedView;

        this.cameraSettings = rangedView.BasicView.CameraOption;
        this.cameraSensitivity = new SensitivityCamera(5f, 5f);
        this.cameraFollow = new CameraFollowObject(150f, targetTransform, cameraRoot);

        this.localStartPos = this.cameraTransform.localPosition;
        this.currentLocalPos = rangedView.BasicView.OnePosition.localPosition;
        this.toCollideWith = collideWith;
        this.objectRotator = objectRotator;
    }

    public void LateUpdate(float time)
    {
        cameraFollow.FollowObject(time);
        if (objectRotator.CanRotate)
        {
            objectRotator.RotateObject(this.pivotTransform);
        }
    }

    public void UpdateLocalPosition(float inputX, float inputY, float time)
    {
        UpdateRotation(inputX, inputY, time);
        UpdateCameraPosition(time);
    }

    public void SetPosition(EMovementID placeMarker)
    {
        if (isZooming)
        {
            return;
        }
        switch (placeMarker)
        {
            case EMovementID.Walking:
                this.cameraSettings = currentView.BasicView.CameraOption;
                this.currentLocalPos = currentView.BasicView.OnePosition.localPosition;
                break;
            case EMovementID.Running:
                this.cameraSettings = currentView.RunView.CameraOption;
                this.currentLocalPos = currentView.RunView.OnePosition.localPosition;
                break;
            default:
                break;
        }
    }

    public void SetZoom(bool isZooming)
    {
        this.isZooming = isZooming;
        if (isZooming)
        {
            this.objectRotator.CanRotate = true;
            this.currentLocalPos = currentView.AimView.OnePosition.localPosition;
        }
        else
        {
            if (isMeleeView)
            {
                this.objectRotator.CanRotate = false;
            }
            this.currentLocalPos = currentView.BasicView.OnePosition.localPosition;
        }
    }

    private void UpdateCameraPosition(float time)
    {
        RaycastHit hit;
        //Get the direction of the raycast
        Vector3 camPosition = cameraTransform.position;
        Vector3 pivotPosition = pivotTransform.position;
        Vector3 direction = camPosition - pivotPosition;
        float maxDistance = isZooming ? cameraSettings.MaxCheckDistance * 0.4f : cameraSettings.MaxCheckDistance;
        if (Physics.SphereCast(pivotPosition, cameraSettings.SphereRadius, direction, out hit, maxDistance, toCollideWith))
        {
            MoveFromObstacleAway(hit, pivotPosition, direction);
        }
        else
        {
            PositionCamera(currentLocalPos, time);
        }
    }

    private void PositionCamera(Vector3 desiredLocalPosition, float time)
    {
        Vector3 currentLocalPosition = cameraTransform.localPosition;
        Vector3 newLocalPosition = Vector3.Lerp(currentLocalPosition, desiredLocalPosition, time * cameraSettings.CameraLerpSpeed);
        cameraTransform.localPosition = newLocalPosition;
    }

    private void MoveFromObstacleAway(RaycastHit hit, Vector3 pivotPosition, Vector3 direction)
    {
        float hitDistance = hit.distance;
        Vector3 sphereCenter = pivotPosition + (direction.normalized * hitDistance);
        this.cameraTransform.position = sphereCenter;
    }

    private void UpdateRotation(float inputX, float inputY, float time)
    {
        x += inputX * cameraSensitivity.SensitivityX * -1;
        y += inputY * cameraSensitivity.SensitivityY;

        x = Mathf.Clamp(x, cameraSettings.MinRotationLimitX, cameraSettings.MaxRotationLimitX);

        targetRotation = new Vector3(x, y);

        currentRotation = Vector3.SmoothDamp(currentRotation, targetRotation, ref smoothVelocity, time * cameraSettings.RotationSmoothTime);

        pivotTransform.eulerAngles = currentRotation;
    }

}
