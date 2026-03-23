using UnityEngine; // core Unity types (MonoBehaviour, Vector3, etc.)
using UnityEngine.InputSystem; // new Input System types (InputActionAsset, InputAction)

/// <summary>
/// First-person player controller: handles ground movement, mouse look,
/// jumping, and sprinting via Unity's new Input System.
/// Requires a CharacterController on the same GameObject and a camera
/// holder Transform assigned in the Inspector.
/// </summary>
[RequireComponent(typeof(CharacterController))] // enforce CharacterController presence at authoring time
public class PlayerController : MonoBehaviour
{
    // --- Movement -----------------------------------------------------------

    [Header("Movement")] // group movement fields in the Inspector
    [SerializeField] private float _moveSpeed = 5f; // base walking speed in units/second
    [SerializeField] private float _sprintMultiplier = 1.8f; // multiplier applied to speed while sprinting
    [SerializeField] private float _jumpForce = 5f; // initial upward velocity on jump
    [SerializeField] private float _gravity = -19.62f; // downward acceleration (2× real gravity for snappier feel)

    // --- Look ---------------------------------------------------------------

    [Header("Look")] // group look fields in the Inspector
    [SerializeField] private float _mouseSensitivity = 0.15f; // degrees rotated per pixel of mouse delta
    [SerializeField] private float _maxPitchAngle = 85f; // maximum up/down camera angle to prevent flipping

    // --- References ---------------------------------------------------------

    [Header("References")] // group reference fields in the Inspector
    [SerializeField, Tooltip("Empty child Transform that the camera is parented to")]
    private Transform _cameraHolder; // pivot used to tilt the camera up/down independently of the player body

    [SerializeField, Tooltip("InputActionAsset containing the Player action map")]
    private InputActionAsset _inputAsset; // the shared input action definitions asset

    // --- Private state ------------------------------------------------------

    private CharacterController _controller; // cached reference — avoids GetComponent each frame
    private InputAction _moveAction; // cached Move action from the Player map
    private InputAction _lookAction; // cached Look action from the Player map
    private InputAction _jumpAction; // cached Jump action from the Player map
    private InputAction _sprintAction; // cached Sprint action from the Player map

    private Vector2 _moveInput; // raw WASD/stick input read each frame
    private float _verticalVelocity; // accumulated vertical velocity (gravity + jump)
    private float _cameraPitch; // current camera X-rotation in degrees, clamped to ±_maxPitchAngle
    private int _lookSkipFrames; // counts down from 3; look input is ignored until cursor lock settles

    // --- Lifecycle ----------------------------------------------------------

    private void Awake()
    {
        _controller = GetComponent<CharacterController>(); // cache CharacterController once at startup

        InputActionMap playerMap = _inputAsset.FindActionMap("Player", throwIfNotFound: true); // locate the Player action map by name
        _moveAction = playerMap.FindAction("Move", throwIfNotFound: true); // cache Move action
        _lookAction = playerMap.FindAction("Look", throwIfNotFound: true); // cache Look action
        _jumpAction = playerMap.FindAction("Jump", throwIfNotFound: true); // cache Jump action
        _sprintAction = playerMap.FindAction("Sprint", throwIfNotFound: true); // cache Sprint action
    }

    private void OnEnable()
    {
        _moveAction.Enable(); // activate Move action so it receives input
        _lookAction.Enable(); // activate Look action
        _jumpAction.Enable(); // activate Jump action
        _sprintAction.Enable(); // activate Sprint action
        _jumpAction.performed += OnJump; // subscribe to Jump so we only respond to the press edge
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // trap the cursor to the window center
        Cursor.visible = false; // hide the cursor while in gameplay
        _lookSkipFrames = 3; // discard the first 3 frames of look input while cursor lock settles
    }

    private void Update()
    {
        HandleLook(); // rotate player and camera from mouse delta first
        HandleMovement(); // then move so the direction reflects the new facing
    }

    private void OnDisable()
    {
        _jumpAction.performed -= OnJump; // unsubscribe to prevent memory leaks or stale callbacks
        _moveAction.Disable(); // deactivate actions when the component is off
        _lookAction.Disable();
        _jumpAction.Disable();
        _sprintAction.Disable();
    }

    // --- Input handlers -----------------------------------------------------

    /// <summary>Called once per Jump press (not every frame).</summary>
    private void OnJump(InputAction.CallbackContext context)
    {
        if (!_controller.isGrounded) return; // ignore jump input while airborne

        _verticalVelocity = _jumpForce; // launch upward at the configured force
    }

    // --- Per-frame helpers --------------------------------------------------

    /// <summary>Applies mouse look: rotates the player body on Y and the camera holder on X.</summary>
    private void HandleLook()
    {
        if (_lookSkipFrames > 0) { _lookSkipFrames--; return; } // skip frames while cursor lock settles to discard stale delta

        Vector2 lookDelta = _lookAction.ReadValue<Vector2>(); // raw mouse pixel delta this frame

        float yaw = lookDelta.x * _mouseSensitivity; // horizontal rotation amount for this frame
        transform.Rotate(Vector3.up, yaw, Space.World); // rotate the player body left/right in world space

        _cameraPitch -= lookDelta.y * _mouseSensitivity; // subtract so moving mouse up tilts camera up
        _cameraPitch = Mathf.Clamp(_cameraPitch, -_maxPitchAngle, _maxPitchAngle); // prevent gimbal flip
        _cameraHolder.localRotation = Quaternion.Euler(_cameraPitch, 0f, 0f); // apply clamped pitch to camera holder
    }

    /// <summary>Applies ground movement and gravity via CharacterController.</summary>
    private void HandleMovement()
    {
        _moveInput = _moveAction.ReadValue<Vector2>(); // WASD / left stick input as XY

        bool isSprinting = _sprintAction.IsPressed(); // true while the sprint button is held
        float speed = _moveSpeed * (isSprinting ? _sprintMultiplier : 1f); // scale speed by sprint state

        Vector3 move = transform.right * _moveInput.x + transform.forward * _moveInput.y; // world-space direction relative to player facing
        move *= speed; // apply speed before adding vertical component

        ApplyGravity(); // update _verticalVelocity each frame
        move.y = _verticalVelocity; // merge vertical velocity into the move vector

        _controller.Move(move * Time.deltaTime); // apply frame-rate-independent displacement
    }

    /// <summary>Accumulates gravity and resets vertical velocity when grounded.</summary>
    private void ApplyGravity()
    {
        if (_controller.isGrounded && _verticalVelocity < 0f)
        {
            _verticalVelocity = -2f; // small negative value keeps isGrounded reliable without visible snap
        }
        else
        {
            _verticalVelocity += _gravity * Time.deltaTime; // accumulate gravity over time
        }
    }
}