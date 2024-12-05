using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    public Transform girl;
    public Transform boy;

    private float fixedX;
    private float fixedZ;
    private float initialY;

    void Start()
    {
        fixedX = transform.position.x;
        initialY = transform.position.y;
        fixedZ = transform.position.z;
    }

    void Update()
    {
        if (girl != null && boy != null)
        {
            // Determine which character is higher
            float highestY = Mathf.Max(girl.position.y, boy.position.y);

            // Clamp the camera's Y position to the highest character's Y position
            float clampedY = Mathf.Max(highestY, initialY);
            transform.position = new Vector3(fixedX, clampedY, fixedZ);
        }
    }
}
