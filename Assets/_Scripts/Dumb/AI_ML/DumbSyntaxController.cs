using UnityEngine;
using System;

public class DumbSyntaxController : MonoBehaviour
{
    [Header("Agent Settings")]
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _leapForce = 10f;
    [SerializeField] float _rotationSpeed = 5f;
    [SerializeField] float _delimeter = 6.5f; 

    private Rigidbody _rb;
    private Vector3 _originalPosition;

    public Rigidbody Rb { get => _rb; }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _originalPosition = transform.position;
    }

    /// <summary>
    /// Reset Syntax's position (random), velocity (0), and rotation (0).
    /// </summary>
    public void ResetSyntax()
    {
        // Reset the agent's position
        float randomX = UnityEngine.Random.Range(-_delimeter, _delimeter);
        float randomZ = UnityEngine.Random.Range(-_delimeter, _delimeter);
        transform.localPosition = new Vector3(randomX,0, randomZ);
        // Reset the agent's velocity
        _rb.linearVelocity = Vector3.zero;
        // Reset the agent's rotation
        transform.rotation = Quaternion.identity;
    }

    public void ResetSyntaxPosition()
    {
        transform.position = _originalPosition;
        _rb.linearVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    /// <summary>
    /// Move Syntax forward.
    /// </summary>
    /// <param name="move">Should be positive.</param>
    public void Move(float move)
    {
        _rb.linearVelocity = transform.forward * Mathf.Max(0f,move) * _moveSpeed;
    }

    /// <summary>
    /// Rotate Syntax clockwise or anti-clockwise.
    /// </summary>
    /// <param name="rotate">It should be 1 or -1</param>
    public void Rotate(float rotate)
    {
        transform.Rotate(0, rotate * _rotationSpeed, 0, Space.Self);
    }
}
