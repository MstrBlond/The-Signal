using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBob : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterController characterController;

    [Header("Position Bob")]
    [SerializeField] private float verticalAmplitude = 0.04f;
    [SerializeField] private float horizontalAmplitude = 0.02f;
    [SerializeField] private float bobFrequency = 8f;

    [Header("Rotation Bob")]
    [SerializeField] private float rotationXAmplitude = 2f;
    [SerializeField] private float rotationZAmplitude = 3f;

    [Header("Smoothing")]
    [SerializeField] private float positionSmooth = 10f;
    [SerializeField] private float rotationSmooth = 10f;

    [Header("Conditions")]
    [SerializeField] private float minMoveSpeed = 0.1f;

    private Vector3 initialLocalPosition;
    private Quaternion initialLocalRotation;
    private float bobTimer;

    private void Awake()
    {
        initialLocalPosition = transform.localPosition;
        initialLocalRotation = transform.localRotation;
    }

    private void Update()
    {
        if (characterController == null)
            return;

        Vector3 horizontalVelocity = characterController.velocity;
        horizontalVelocity.y = 0f;

        float speed = horizontalVelocity.magnitude;
        bool isMoving = speed > minMoveSpeed;
        bool isGrounded = characterController.isGrounded;

        Vector3 targetPosition = initialLocalPosition;
        Quaternion targetRotation = initialLocalRotation;

        if (isMoving && isGrounded)
        {
            bobTimer += Time.deltaTime * bobFrequency * speed;

            float bobX = Mathf.Cos(bobTimer * 0.5f) * horizontalAmplitude;
            float bobY = Mathf.Sin(bobTimer) * verticalAmplitude;

            targetPosition += new Vector3(bobX, bobY, 0f);

            float rotX = Mathf.Sin(bobTimer) * rotationXAmplitude;
            float rotZ = Mathf.Cos(bobTimer * 0.5f) * rotationZAmplitude;

            targetRotation *= Quaternion.Euler(rotX, 0f, rotZ);
        }
        else
        {
            bobTimer = Mathf.Lerp(bobTimer, 0f, Time.deltaTime * 5f);
        }

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            targetPosition,
            Time.deltaTime * positionSmooth
        );

        transform.localRotation = Quaternion.Slerp(
            transform.localRotation,
            targetRotation,
            Time.deltaTime * rotationSmooth
        );
    }
}
