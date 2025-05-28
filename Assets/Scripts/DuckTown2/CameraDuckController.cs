using UnityEngine;

public class CameraDuckController : MonoBehaviour
{
    [SerializeField] private Transform followObject;

    [SerializeField] private float distance = 5.0f;
    [SerializeField] private float cameraSpeed = 2.0f;
    [SerializeField] private float minVerticalAngle = -45;
    [SerializeField] private float maxVerticalAngle = 45;

    [SerializeField] private Vector2 framingOffset;

    [SerializeField] private bool invertX;
    [SerializeField] private bool invertY;

    private float rotationY;
    private float rotationX;

    private float invertXVal;
    private float invertYval;


    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * 2.0f; // 缩放速度可以调节
        distance = Mathf.Clamp(distance, 2.0f, 10.0f);

        invertXVal = invertX ? -1 : 1;
        invertYval = invertY ? -1 : 1;

        rotationY += Input.GetAxis("Mouse X") * cameraSpeed * invertYval;
        //Debug.Log("Duck: " + rotationY);
        rotationX += Input.GetAxis("Mouse Y") * cameraSpeed * invertXVal;
        //Debug.Log("Duck: " + rotationX);

        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);

        Quaternion rotateAngle = Quaternion.Euler(rotationX, rotationY, 0);

        Vector3 focusPosition = followObject.position + new Vector3(framingOffset.x, framingOffset.y, 0);

        transform.position = focusPosition - rotateAngle * new Vector3(0, 0, distance);
        transform.rotation = rotateAngle;

    }

    //这是一个getter的简单写法。
    //planar旋转也是常用句式
    public Quaternion PlanarRotation => Quaternion.Euler(0, rotationY, 0);
}
