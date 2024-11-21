
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
            float averageY = (girl.position.y + boy.position.y) / 2;
            float clampedY = Mathf.Max(averageY, initialY);
            transform.position = new Vector3(fixedX, clampedY, fixedZ);
        }
    }
}
