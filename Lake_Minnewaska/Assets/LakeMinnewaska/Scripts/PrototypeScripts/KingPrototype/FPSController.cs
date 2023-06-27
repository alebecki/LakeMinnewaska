using UnityEngine;

public class FPSController : MonoBehaviour
{
    public bool canJump = true;

    [SerializeField] private bool enableSprint;
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpPower = 2.0f;
    [SerializeField] private float fallSpeed = 1.2f;
    [SerializeField] private float mouseSensitivity = 1.5f;
    [SerializeField] private float maxUpAngle = 80;
    [SerializeField] private float maxDownAngle = -80;
    [SerializeField] private float jumpCacheTime = 0.1f;
    
    [SerializeField] private Transform cameraTransform;

    private const float GRAVITY = -9.81f;
    private CharacterController _controller;
    private float _velocityY;
    private float camRotX;
    private float _jumpCacheTimer, _jumpPrevTimer;
    private bool _isGroundedLastFrame;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        _jumpCacheTimer = 0;
        _jumpPrevTimer = jumpCacheTime;
    }

    private void Update()
    {
        // jump cache & prev
        _jumpCacheTimer += Time.deltaTime;
        _jumpPrevTimer += Time.deltaTime;
        if (_controller.isGrounded)
        {
            _jumpCacheTimer = 0;
            if (_jumpPrevTimer < jumpCacheTime)
            {
                Jump();
            }
        }
        
        UpdateMove();
        UpdateViewRot();
    }

    private void Jump()
    {
        if (!canJump) return;
        _velocityY += Mathf.Sqrt(jumpPower * -GRAVITY);
    }

    private void UpdateMove()
    {
        if (!_controller.enabled)
            return;
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        if (move.sqrMagnitude > 1)
            move.Normalize();
        move *= Input.GetKey(KeyCode.LeftShift) && enableSprint ? moveSpeed * 1.5f : moveSpeed;
        
        if (_controller.isGrounded && _velocityY < 0)
        {
            _velocityY = 0f;
        }
        if (Input.GetButtonDown("Jump"))
        {
            if (_jumpCacheTimer < jumpCacheTime)
            {
               Jump();
            }
            else
            {
                _jumpPrevTimer = 0;
            }
        }
        _velocityY += GRAVITY * Time.deltaTime * fallSpeed;
        
        move.y = _velocityY;
        _controller.Move( Time.deltaTime * move);
    }

    private void UpdateViewRot()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        camRotX -= mouseY;
        camRotX = Mathf.Clamp(camRotX, maxDownAngle, maxUpAngle);

        cameraTransform.localRotation = Quaternion.Euler(camRotX, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }
}