using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ControllerVelocity : MonoBehaviour
{
    public InputActionProperty velocityProperty;
    public Vector3 Velocity {get; private set;} = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        Velocity = velocityProperty.action.ReadValue<Vector3>();
    }
}
