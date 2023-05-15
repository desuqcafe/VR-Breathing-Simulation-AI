using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    public float amplitude = 0.5f;  // The maximum height the sphere will reach.
    public float speed = 1.0f;      // The speed at which the sphere moves up and down.

    private float startTime;
    private Vector3 startPosition;
    private bool isMoving;

    private void Start()
    {
        startPosition = transform.position;
        isMoving = false;
    }

    public void StartSphereMovement()
    {
        if (!isMoving)
        {
            startTime = Time.time;
            isMoving = true;
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            float elapsedTime = Time.time - startTime;
            float newY = startPosition.y + Mathf.Sin(elapsedTime * speed) * amplitude;

            // Apply the new position to the sphere using interpolation
            float t = elapsedTime / 2.0f; // Interpolation parameter (normalized time)
            transform.position = Vector3.Lerp(startPosition, new Vector3(startPosition.x, newY, startPosition.z), t);

            // Check if 2 seconds have passed and stop the movement
            if (elapsedTime >= 2.0f)
            {
                isMoving = false;
                // Reset the sphere position to its original position
                transform.position = startPosition;
            }
        }
    }
}
