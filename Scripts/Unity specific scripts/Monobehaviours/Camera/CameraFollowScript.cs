using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour, ICameraData
{

    [SerializeField]
    LayerMask collidewith;
    [SerializeField]
    Transform target;

    //Ranged cam pos
    [Header("Ranged camera Positions")]
    [SerializeField]
    Transform rangedIdle;
    [SerializeField]
    Transform rangedRun;
    [SerializeField]
    Transform rangedAim;

    //Melee cam pos
    [Header("Melee camera Positions")]
    [SerializeField]
    Transform meleedIdle;
    [SerializeField]
    Transform meleeRun;
    [SerializeField]
    Transform meleeAim;

    [Header("Object to rotate")]
    [SerializeField]
    Transform objectToRotateTransform;

    ICameraPlaces meleeCamplaces;
    ICameraPlaces rangedCamplaces;

    ISpStateView meleeView;
    ISpStateView rangedView;
    IRotateObject objectRotator;

    ISpCameraFollow cameraFollow;

    public ISpCameraFollow SpCamera { get { return this.cameraFollow; } }

    public bool IsEnabled { get { return this.enabled; } }

    // Use this for initialization
    void Awake()
    {
        meleeCamplaces = new SpCamPositions(meleedIdle.localPosition, meleeAim.localPosition, meleeRun.localPosition);
        rangedCamplaces = new SpCamPositions(rangedIdle.localPosition, rangedAim.localPosition, rangedRun.localPosition);

        ISpView meleeBasicView = new SpOneView(meleedIdle, transform, 5.0f, -40.0f, 30.0f, 0.1f, 5f);
        ISpView meleeRunView = new SpOneView(meleeRun, transform, 5.0f, -40.0f, 30.0f, 0.1f, 5f);
        ISpView meleeAimView = new SpOneView(meleeAim, transform, 5.0f, -40.0f, 30.0f, 0.1f, 5f);

        ISpView rangedBasicView = new SpOneView(rangedIdle, transform, 5.0f, -40.0f, 30.0f, 0.1f, 5f);
        ISpView rangedRunView = new SpOneView(rangedRun, transform, 5.0f, -40.0f, 30.0f, 0.1f, 5f);
        ISpView rangedAimView = new SpOneView(rangedAim, transform, 5.0f, -40.0f, 30.0f, 0.1f, 5f);


        meleeView = new SpStateView(meleeBasicView, meleeAimView, meleeRunView);
        rangedView = new SpStateView(rangedBasicView, rangedAimView, rangedRunView);

        objectRotator = new SpacemarineRotator(objectToRotateTransform, 5f);

        cameraFollow = new SpCameraFollow(target, transform, collidewith, ref meleeView, ref rangedView, ref objectRotator);
    }

    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Mouse Y"), Input.GetAxisRaw("Mouse X"));
        cameraFollow.UpdateLocalPosition(input.x, input.y, Time.deltaTime);
    }

    private void LateUpdate()
    {
        cameraFollow.LateUpdate(Time.deltaTime);
    }

    public void EnableComponent(bool isEnabled)
    {
        this.enabled = isEnabled;
    }
}
