using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehaviour : MonoBehaviour
{
    public WheelCollider wheelColliderBL;
    public WheelCollider wheelColliderBR;
    public WheelCollider wheelColliderFL;
    public WheelCollider wheelColliderFR;
    public static Rigidbody _rigidBody;
    public float maxTorque = 500;
    public float maxSteerAngle = 45;
    public float sidewaysStiffness = 1.5f;
    public float forewardStiffness = 1.5f;
    private float _currentSpeedKMH = _rigidBody.velocity.magnitude * 3.6f;
    public float maxSpeedKMH = 150;
    public float maxSpeedBackwardKMH = 30;


    void Start() {
        SetWheelFrictionStiffness(forewardStiffness, sidewaysStiffness);
    }

    void FixedUpdate()
    {
        SetMotorTorque(maxTorque * Input.GetAxis("Vertical"));
        SetSteerAngle(maxSteerAngle * Input.GetAxis("Horizontal"));

    }
    void SetSteerAngle(float angle)
    {
        wheelColliderFL.steerAngle = angle;
        wheelColliderFR.steerAngle = angle;
    }
    void SetMotorTorque(float amount)
    {
        //float _currentSpeedKMH = _rigidBody.velocity.magnitude * 3.6f;
        wheelColliderFL.motorTorque = (_currentSpeedKMH <= maxSpeedKMH && _currentSpeedKMH >= -maxSpeedBackwardKMH) ? amount : 0f;
        //wheelColliderFL.motorTorque = amount;
        wheelColliderFR.motorTorque = (_currentSpeedKMH <= maxSpeedKMH && _currentSpeedKMH >= -maxSpeedBackwardKMH) ? amount : 0f;
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
