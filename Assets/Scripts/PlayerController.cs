using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int WalkSpeed = 5;
    [SerializeField] private int RunSpeed = 10;
    [SerializeField] private int JumpPower = 200;
    [SerializeField] private bool Grounded;
    [SerializeField] private Rigidbody rb;

    [SerializeField] public float mouseSensitivity = 200f;

    [SerializeField] public Transform Camera;

    private bool isGrounded;
    private float xRotation = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        CameraController();
        MoveController();
    }

    void MoveController()
    {
        Vector3 velocity = new Vector3();

        if (Input.GetKey(KeyCode.W))
        {
            velocity += transform.forward * WalkSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            velocity -= transform.forward * WalkSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            velocity -= transform.right * WalkSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            velocity += transform.right * WalkSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            velocity *= RunSpeed;
        }
        else
        {
            velocity *= WalkSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space) && Grounded)
        {
            rb.AddForce(transform.up * JumpPower);
        }

        velocity.y = rb.velocity.y;

        rb.velocity = velocity;
    }

    void CameraController()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        Camera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Grounded = true;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Grounded = false;
        }
    }
}