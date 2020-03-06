using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//This notifies the camera where the camera should move in action
public class UserCameraManager
{
    static UserCameraManager userCamera = new UserCameraManager();
    public event Action<EMovementID> CameraCharacterNotifyOnMovement;

    public static UserCameraManager UserHandling { get { return userCamera; } }

    public static void SendCharacterCameraMovementID(EMovementID movementID)
    {
        if (userCamera.CameraCharacterNotifyOnMovement != null)
        {
            userCamera.CameraCharacterNotifyOnMovement(movementID);
        }
    }

}
