using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturePointOnlyOnce : ICapturable
{
    public event Action<int> Captured;
    public event Action<int> Capturing;

    float captureInterval;
    float maxCaptureValue;
    float currentCaptureValue;
    GameObject attachedgameObject;

    bool isCaptured;
    bool isCapturing;
    float captureAmount;
    float elapsedTime;
    int currentCaptureMembers;
    int captureID;

    public float CaptureInterval { get { return this.captureInterval; } }

    public float MaxCaptureValue { get { return this.maxCaptureValue; } }

    public float CurrentCaptureValue { get { return this.currentCaptureValue; } }

    public float CaptureProportion { get { return this.currentCaptureValue / this.maxCaptureValue; } }

    public int CaptureID { get { return this.captureID; } }

    public CapturePointOnlyOnce(float captureInterval, float captureAmount, float maxCaptureValue, GameObject attachedObj)
    {
        this.maxCaptureValue = maxCaptureValue;
        this.captureAmount = captureAmount;
        this.captureInterval = captureInterval;
        this.currentCaptureMembers = 0;
        this.isCapturing = false;
        this.isCaptured = false;
        this.attachedgameObject = attachedObj;
    }

    public void Capture(float time)
    {
        if (isCaptured)
        {
            return;
        }

        if (!isCapturing)
        {
            elapsedTime = captureInterval;
            return;
        }

        if (elapsedTime >= captureInterval)
        {
            elapsedTime = 0;
            currentCaptureValue += captureAmount * currentCaptureMembers;
            if (currentCaptureValue > maxCaptureValue)
            {
                currentCaptureValue = maxCaptureValue;
                isCaptured = true;
                if (Captured != null)
                {
                    Captured(captureID);
                    attachedgameObject.GetComponent<Collider>().enabled = false;
                }
            }
        }
        else
        {
            elapsedTime += time;
        }
    }

    public void DecreaseCaptureNumber()
    {
        currentCaptureMembers--;
        if (currentCaptureMembers == 0)
        {
            isCapturing = false;
        }
    }

    public void IncreaseCapture()
    {
        if (!isCapturing)
        {
            isCapturing = true;
            if (Capturing != null)
            {
                Capturing(captureID);
            }
        }
        currentCaptureMembers++;
    }
}
