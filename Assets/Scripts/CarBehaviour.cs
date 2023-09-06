using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehaviour : MonoBehaviour
{
    public WheelCollider wheelColliderBL;
    public WheelCollider wheelColliderBR;
    public WheelCollider wheelColliderFL;
    public WheelCollider wheelColliderFR;
    public Rigidbody _rigidBody;
    public float maxTorque = 500;
    public float maxSteerAngle = 45;
    public float sidewaysStiffness = 1.5f;
    public float forewardStiffness = 1.5f;
    private Rigidbody _rigidbody;
    private float _currentSpeedKMH;
    public float maxSpeedKMH = 150;
    public float maxSpeedBackwardKMH = 30;


    void Start() {
        SetWheelFrictionStiffness(forewardStiffness, sidewaysStiffness);
        _rigidbody = GetComponent<Rigidbody>();
     


    }

    void Update()
    {
        _currentSpeedKMH = _rigidBody.velocity.magnitude * 3.6f;

    }

    void FixedUpdate()
    {
        SetMotorTorque(maxTorque * Input.GetAxis("Vertical"));
        SetSteerAngle(maxSteerAngle * Input.GetAxis("Horizontal"));
        // Determine if the car is driving forwards or backwards
        bool velocityIsForeward = Vector3.Angle(transform.forward,
         _rigidbody.velocity) < 50f;
        // get the current speed from the velocity vector
        _currentSpeedKMH = _rigidBody.velocity.magnitude * 3.6f;
        // Determine if the cursor key input means braking
        bool doBraking = _currentSpeedKMH > 0.5f &&
        (Input.GetAxis("Vertical") < 0 && velocityIsForeward ||
        Input.GetAxis("Vertical") > 0 && !velocityIsForeward);
        if (doBraking)
        {
            wheelColliderFL.brakeTorque = 5000;
            wheelColliderFR.brakeTorque = 5000;
            wheelColliderBL.brakeTorque = 5000;
            wheelColliderBR.brakeTorque = 5000;
            wheelColliderFL.motorTorque = 0;
            wheelColliderFR.motorTorque = 0;
        }
        else
        {
            wheelColliderFL.brakeTorque = 0;
            wheelColliderFR.brakeTorque = 0;
            wheelColliderBL.brakeTorque = 0;
            wheelColliderBR.brakeTorque = 0;
            wheelColliderFL.motorTorque = maxTorque * Input.GetAxis("Vertical");
            wheelColliderFR.motorTorque = wheelColliderFL.motorTorque;
        }

    }
    void SetSteerAngle(float angle)
    {
        wheelColliderFL.steerAngle = angle;
        wheelColliderFR.steerAngle = angle;
    }
    void SetMotorTorque(float amount)
    {
        //float _currentSpeedKMH = _rigidBody.velocity.magnitude * 3.6f;
        //wheelColliderFL.motorTorque = (_currentSpeedKMH <= maxSpeedKMH && _currentSpeedKMH >= -maxSpeedBackwardKMH) ? amount : 0f;
        wheelColliderFL.motorTorque = amount;
        //wheelColliderFR.motorTorque = (_currentSpeedKMH <= maxSpeedKMH && _currentSpeedKMH >= -maxSpeedBackwardKMH) ? amount : 0f;
        wheelColliderFR.motorTorque = amount;
    }

    void SetWheelFrictionStiffness(float newForwardStiffness, float newSidewaysStiffness)
    {
        WheelFrictionCurve fwWFC = wheelColliderFL.forwardFriction;
        WheelFrictionCurve swWFC = wheelColliderFL.sidewaysFriction;
        fwWFC.stiffness = newForwardStiffness;
        swWFC.stiffness = newSidewaysStiffness;
        wheelColliderFL.forwardFriction = fwWFC;
        wheelColliderFL.sidewaysFriction = swWFC;
        wheelColliderFR.forwardFriction = fwWFC;
        wheelColliderFR.sidewaysFriction = swWFC;
        wheelColliderBL.forwardFriction = fwWFC;
        wheelColliderBL.sidewaysFriction = swWFC;
        wheelColliderBR.forwardFriction = fwWFC;
        wheelColliderBR.sidewaysFriction = swWFC;
    }

   

}
