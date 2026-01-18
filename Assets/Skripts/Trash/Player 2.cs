using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_2 : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float maxSpeed = 9f;
    [SerializeField] private float rotationSpeed = 300f;
    [SerializeField] private float deceleration = 9f; // hamowanie
    [SerializeField] private float brakeDeceleration = 28f;
    [SerializeField] private float acceleration = 16f; // przyspieszenie

    [Header("Offroad (grass) settings")]
    [SerializeField] private float offroadSpeedFactor = 0.5f; // -- % on offroad zone
    [SerializeField] private float offroadAccelerationFactor = 0.6f; // -- % acceleration on offroad zone

    private Rigidbody2D rb;

    private float moveInput; // -1 .. 0 .. +1, W/S
    private float turnInput; // -1 .. 0 .. +1, A/D

    private float currentSpeed = 0f;
    private const float speedEpsilon = 0.05f;

    private float grassCounter = 0;
    private bool onGrass => grassCounter > 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        moveInput = 0f;
        if (Input.GetKey(KeyCode.UpArrow)) moveInput = 1f;
        if (Input.GetKey(KeyCode.DownArrow)) moveInput = -1f;

        turnInput = 0f;
        if (Input.GetKey(KeyCode.RightArrow)) turnInput = -1f;
        if (Input.GetKey(KeyCode.LeftArrow)) turnInput = 1f;


    }

    private void FixedUpdate()
    {

        //float targetSpeed = moveInput * maxSpeed;

        float realMaxSpeed = onGrass ? maxSpeed * offroadSpeedFactor : maxSpeed;
        float realAcceleration = onGrass ? acceleration * offroadAccelerationFactor : acceleration;

        // Acceleration / Deceleration / brake Deceleration
        if (moveInput != 0)
        {

            bool opositeDiraction = Mathf.Sign(currentSpeed) != Mathf.Sign(moveInput) && currentSpeed != 0f;
            if (opositeDiraction)
            {
                currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, brakeDeceleration * Time.fixedDeltaTime);
            }
            else
            {
                currentSpeed = Mathf.MoveTowards(currentSpeed, moveInput * realMaxSpeed, realAcceleration * Time.fixedDeltaTime);
            }
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.fixedDeltaTime);
        }
        // Limit of speed
        currentSpeed = Mathf.Clamp(currentSpeed, -realMaxSpeed, realMaxSpeed);


        // Rotation
        bool canTurn = Mathf.Abs(currentSpeed) > speedEpsilon;

        if (canTurn)
        {
            // Turn Factor in %
            float turnFactor = Mathf.InverseLerp(0f, maxSpeed, Mathf.Abs(currentSpeed));

            // Rotation speed depending on Moving speed
            float effectiveRotationSpeed = rotationSpeed * turnFactor;

            // Inversion for turning behind
            float turnSign = (currentSpeed < 0f) ? -1f : 1f;

            float rotation = turnInput * effectiveRotationSpeed * turnSign * Time.fixedDeltaTime;
            rb.MoveRotation(rb.rotation + rotation);

        }
        // Moving forward
        Vector2 forward = (Vector2)transform.up;
        rb.MovePosition(rb.position + forward * currentSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Offroad"))
            grassCounter++;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Offroad"))
        {
            grassCounter--;
            if (grassCounter < 0) grassCounter = 0;
        }
    }

}
