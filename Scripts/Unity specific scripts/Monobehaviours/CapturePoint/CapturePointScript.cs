using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class CapturePointScript : MonoBehaviour, ICaptureData
{

    ICapturable capturePoint;

    public ICapturable Capturable { get { return this.capturePoint; } }

    public bool IsEnabled { get; private set; }

    void Start()
    {
        capturePoint = new CapturePointOnlyOnce(2.0f, 1.0f, 10.0f, this.gameObject);
        capturePoint.Capturing += CapturePoint_Capturing;
        capturePoint.Captured += CapturePoint_Captured;
    }

    private void CapturePoint_Captured(int obj)
    {
        Debug.Log("Captured bro");
    }

    private void CapturePoint_Capturing(int obj)
    {
        Debug.Log("I started to capture: " + obj);
    }

    private void OnTriggerEnter(Collider other)
    {
        capturePoint.IncreaseCapture();
    }

    private void OnTriggerStay(Collider other)
    {
        capturePoint.Capture(Time.deltaTime);
    }

    private void OnTriggerExit(Collider other)
    {
        capturePoint.DecreaseCaptureNumber();
    }

    public void EnableComponent(bool isEnabled)
    {
        this.enabled = isEnabled;
        this.IsEnabled = isEnabled;
    }
}
