using UnityEngine;

public class TicketRotator : MonoBehaviour
{
    [SerializeField] float _rotationSpeed = 5f;

    /// <summary>
    /// Rotates the object on its y-axis.
    /// </summary>
    void Update()
    {
        transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0);
    }
}
