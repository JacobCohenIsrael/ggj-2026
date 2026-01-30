using UnityEngine;
using UnityEngine.InputSystem;

namespace Overcrowded
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerInput))]
    public class FPSController : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("Transform that rotates up/down (usually the camera pivot).")]
        public Transform cameraPivot;
        [Tooltip("Optional: assign Camera for convenience. Not required.")]
        public Camera playerCamera;

        [Header("Movement")]
        public float walkSpeed = 5f;
        public float sprintSpeed = 8.5f;
        public float crouchSpeed = 3f;
        public float acceleration = 12f;

        [Header("Gravity")]
        public float gravity = -20f;
        public float groundedStickForce = -2f;

        [Header("Look")]
        public float mouseSensitivity = 0.08f;   // mouse delta scale
        public float stickSensitivity = 120f;    // degrees/sec for gamepad
        public float maxPitch = 89f;

        [Header("Crouch")]
        public float standingHeight = 1.8f;
        public float crouchingHeight = 1.1f;
        public float crouchTransitionSpeed = 12f;

        private CharacterController _cc;
        private PlayerInput _playerInput;

        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction _sprintAction;
        private InputAction _crouchAction;

        private Vector3 _velocity;      // includes vertical y
        private Vector3 _horizontalVel; // planar velocity
        private float _pitch;

        private bool _isSprinting;
        private bool _isCrouching;

        private void Awake()
        {
            _cc = GetComponent<CharacterController>();
            _playerInput = GetComponent<PlayerInput>();

            // Actions by name (must match your Input Actions)
            _moveAction = _playerInput.actions["Move"];
            _lookAction = _playerInput.actions["Look"];
            _sprintAction = _playerInput.actions["Sprint"];
            _crouchAction = _playerInput.actions["Crouch"];

            // Set initial sizes
            _cc.height = standingHeight;
            _cc.center = new Vector3(0, standingHeight * 0.5f, 0);

            LockCursor(true);
        }

        private void OnEnable()
        {
            _crouchAction.performed += OnCrouchToggle;
        }

        private void OnDisable()
        {
            _crouchAction.performed -= OnCrouchToggle;
        }

        private void Update()
        {
            ReadButtons();
            HandleLook();
            HandleMovement();
            HandleGravityAndGrounding();
            HandleCrouchHeight();
        }

        private void ReadButtons()
        {
            _isSprinting = _sprintAction.IsPressed() && !_isCrouching;
        }

        private void HandleMovement()
        {
            Vector2 move = _moveAction.ReadValue<Vector2>();
            Vector3 inputDir = new Vector3(move.x, 0f, move.y);
            inputDir = Vector3.ClampMagnitude(inputDir, 1f);

            float targetSpeed = _isCrouching ? crouchSpeed : (_isSprinting ? sprintSpeed : walkSpeed);

            // Transform input to world (relative to player yaw)
            Vector3 desired = transform.TransformDirection(inputDir) * targetSpeed;

            // Smooth acceleration
            _horizontalVel = Vector3.MoveTowards(_horizontalVel, desired, acceleration * Time.deltaTime);

            // Apply horizontal move (vertical via _velocity.y)
            Vector3 motion = _horizontalVel;
            motion.y = _velocity.y;

            _cc.Move(motion * Time.deltaTime);
        }

        private void HandleLook()
        {
            Vector2 look = _lookAction.ReadValue<Vector2>();

            // Simple heuristic: if a gamepad exists and mouse isn't moving, treat as stick.
            bool mouseMoving = Mouse.current != null && Mouse.current.delta.ReadValue().sqrMagnitude > 0.0001f;
            bool useStick = Gamepad.current != null && !mouseMoving;

            float yaw;
            float pitch;

            if (useStick)
            {
                yaw = look.x * stickSensitivity * Time.deltaTime;
                pitch = look.y * stickSensitivity * Time.deltaTime;
            }
            else
            {
                // Mouse delta is "per frame"
                yaw = look.x * mouseSensitivity;
                pitch = look.y * mouseSensitivity;
            }

            transform.Rotate(0f, yaw, 0f);

            _pitch -= pitch;
            _pitch = Mathf.Clamp(_pitch, -maxPitch, maxPitch);

            if (cameraPivot != null)
                cameraPivot.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
        }

        private void HandleGravityAndGrounding()
        {
            bool grounded = _cc.isGrounded;

            if (grounded && _velocity.y < 0f)
            {
                // Helps keep controller glued to slopes/ground
                _velocity.y = groundedStickForce;
            }

            _velocity.y += gravity * Time.deltaTime;
        }

        private void OnCrouchToggle(InputAction.CallbackContext ctx)
        {
            _isCrouching = !_isCrouching;

            // If you uncrouch but there's something overhead, stay crouched.
            if (!_isCrouching) // trying to stand
            {
                float radius = _cc.radius;

                // Start from roughly the current capsule top and check upward for clearance.
                // (This is a pragmatic check; feel free to tune for your setup.)
                Vector3 origin = transform.position + Vector3.up * (_cc.height * 0.5f);
                float checkDistance = (standingHeight - _cc.height) + 0.05f;

                if (Physics.SphereCast(
                        origin,
                        radius,
                        Vector3.up,
                        out RaycastHit hit,   // <-- FIX: explicit RaycastHit, no '_' collision
                        checkDistance,
                        ~0,
                        QueryTriggerInteraction.Ignore))
                {
                    _isCrouching = true;
                }
            }
        }

        private void HandleCrouchHeight()
        {
            float targetHeight = _isCrouching ? crouchingHeight : standingHeight;
            float newHeight = Mathf.Lerp(_cc.height, targetHeight, crouchTransitionSpeed * Time.deltaTime);

            // Keep bottom on the ground by adjusting center with height
            _cc.height = newHeight;
            _cc.center = new Vector3(0, _cc.height * 0.5f, 0);
        }

        private void LockCursor(bool locked)
        {
            Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !locked;
        }
    }
}
