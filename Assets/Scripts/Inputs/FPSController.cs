using Reflex.Attributes;
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
        public Transform CameraPivot;

        [Header("Movement")]
        public float WalkSpeed = 5f;
        public float Acceleration = 12f;

        [Header("Gravity")]
        public float Gravity = -20f;
        public float GroundedStickForce = -2f;

        [Header("Look")]
        public float MouseSensitivity = 0.08f;   // mouse delta scale
        public float StickSensitivity = 120f;    // degrees/sec for gamepad
        public float MaxPitch = 89f;
        public float Height = 1.8f;

        [Header("Slide")]
        [Tooltip("Layer(s) considered as slide surfaces (e.g., ice)")]
        public LayerMask SlideLayerMask;
        [Tooltip("Deceleration when sliding (should be low, e.g., 1)")]
        public float SlideDeceleration = 1f;

        [Tooltip("How much control the player has while sliding (0 = none, 1 = full)")]
        [Range(0,1)]
        public float SlideControl = 0.2f;
        [Tooltip("Max speed while sliding (optional, set high to ignore)")]
        public float SlideMaxSpeed = 8f;

        [Inject] private LevelMenuView _levelMenuView;

        private CharacterController _cc;
        private PlayerInput _playerInput;

        private InputAction _moveAction;
        private InputAction _lookAction;

        private float _velocity;      // includes vertical y
        private Vector3 _horizontalVel; // planar velocity
        private float _pitch;

        private bool _isSliding;

        private void Awake()
        {
            _cc = GetComponent<CharacterController>();
            _playerInput = GetComponent<PlayerInput>();

            // Actions by name (must match your Input Actions)
            _moveAction = _playerInput.actions["Move"];
            _lookAction = _playerInput.actions["Look"];

            // Set initial sizes
            _cc.height = Height;
            _cc.center = new(0, Height * 0.5f, 0);

            LockCursor(true);
        }

        private void Update()
        {
            if (_levelMenuView.SettingsShown)
                return;

            CheckSlideGround();
            HandleLook();
            HandleMovement();
            HandleGravityAndGrounding();
        }

        public void AddVelocity(Vector3 vel)
        {
            _velocity += vel.y;
            _horizontalVel += vel;
        }

        private void HandleMovement()
        {
            var move = _moveAction.ReadValue<Vector2>();
            var inputDir = new Vector3(move.x, 0f, move.y);
            inputDir = Vector3.ClampMagnitude(inputDir, 1f);

            if (_isSliding)
            {
                // Reduced control: blend input with current velocity, low acceleration
                var targetSpeed = SlideMaxSpeed;
                var desired = transform.TransformDirection(inputDir) * targetSpeed;
                // Only a fraction of input is applied
                var blended = Vector3.Lerp(_horizontalVel, desired, SlideControl);
                // Slow down slowly, but allow some input
                _horizontalVel = Vector3.MoveTowards(_horizontalVel, blended, SlideDeceleration * Time.deltaTime);
            }
            else
            {
                var targetSpeed = WalkSpeed;
                var desired = transform.TransformDirection(inputDir) * targetSpeed;
                _horizontalVel = Vector3.MoveTowards(_horizontalVel, desired, Acceleration * Time.deltaTime);
            }

            // Apply horizontal move (vertical via _velocity.y)
            var motion = _horizontalVel;
            motion.y = _velocity;

            _cc.Move(motion * Time.deltaTime);
        }

        private void HandleLook()
        {
            var look = _lookAction.ReadValue<Vector2>();

            // Simple heuristic: if a gamepad exists and mouse isn't moving, treat as stick.
            var mouseMoving = Mouse.current != null && Mouse.current.delta.ReadValue().sqrMagnitude > 0.0001f;
            var useStick = Gamepad.current != null && !mouseMoving;

            float yaw;
            float pitch;

            if (useStick)
            {
                yaw = look.x * StickSensitivity * Time.deltaTime;
                pitch = look.y * StickSensitivity * Time.deltaTime;
            }
            else
            {
                // Mouse delta is "per frame"
                yaw = look.x * MouseSensitivity;
                pitch = look.y * MouseSensitivity;
            }

            transform.Rotate(0f, yaw, 0f);

            _pitch -= pitch;
            _pitch = Mathf.Clamp(_pitch, -MaxPitch, MaxPitch);

            if (CameraPivot != null)
                CameraPivot.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
        }

        private void HandleGravityAndGrounding()
        {
            var grounded = _cc.isGrounded;

            if (grounded && _velocity < 0f)
            {
                // Helps keep controller glued to slopes/ground
                _velocity = GroundedStickForce;
            }

            _velocity += Gravity * Time.deltaTime;
        }

        private void LockCursor(bool locked)
        {
            Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !locked;
        }

        private void CheckSlideGround()
        {
            const float checkDist = 0.3f;

            _isSliding = false;

            if (!_cc.isGrounded)
                return;

            var origin = transform.position + Vector3.up * 0.1f;

            if (Physics.Raycast(origin, Vector3.down, out _, checkDist, SlideLayerMask, QueryTriggerInteraction.Ignore))
                _isSliding = true;
        }
    }
}
