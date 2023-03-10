using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private VariableJoystick variableJoystick;
    private Rigidbody rb;
    private Vector3 direction;
    [field:SerializeField] public static Vector3 VelocityCarrier { get; private set; }
    private bool SpeedLimiter => rb.velocity.magnitude < _maxSpeed;
    private bool _touching;

    [SerializeField] private GameObject body;
    private void Awake() => rb = GetComponent<Rigidbody>();

    private void FixedUpdate()
    {
        _touching = variableJoystick.touched;
        CheckDirection();
        MoveCharacter(direction);
        VelocityCarrier = rb.velocity;
    }
    private void CheckDirection() => direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
    private void MoveCharacter(Vector3 direction)
    {
        if (SpeedLimiter)
            rb.AddForce(speed * Time.fixedDeltaTime * direction, ForceMode.VelocityChange);

        if(_touching)
            body.transform.eulerAngles = new Vector3(0, Mathf.Atan2(-variableJoystick.Vertical, variableJoystick.Horizontal) * 180 / Mathf.PI, 0);
    }
}