using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeItUp : MonoBehaviour
{
    private Transform cameraTransform;

    public float shakeDuration = 0.0f;
    public float shakeMagnitude = 0.7f;
    public float dampingSpeed = 1.0f;

    Vector3 initialPosition;

    void Awake()
    {
        if (cameraTransform == null)
        {
            cameraTransform = GetComponent<Transform>();
        }
    }

    private void OnEnable()
    {
        initialPosition = cameraTransform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            cameraTransform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            cameraTransform.localPosition = initialPosition;
        }
    }

    public void TriggerShake()
    {
        shakeDuration = 2.0f;
    }
}
