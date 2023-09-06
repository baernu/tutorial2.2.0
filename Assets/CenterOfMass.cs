using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfMass : MonoBehaviour
{
    public Transform centerOfMass;
    private Rigidbody _rigidbody;
    void Start()
    {
    
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = new Vector3(centerOfMass.localPosition.x,
        centerOfMass.localPosition.y,
        centerOfMass.localPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
