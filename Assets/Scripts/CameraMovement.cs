using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float mousSensitivity = 200f;

    private Transform playerBody;
    private float mouseX;
    private float mouseY;
    private float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GameObject.FindWithTag("Player").transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * mousSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mousSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
