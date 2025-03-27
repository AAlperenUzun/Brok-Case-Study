using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private MovementContext movementContext;
    private Transform cameraTarget;
    private CharacterData characterData;

    [SerializeField] private Transform characterModel;
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private float mouseSensitivity = 1f;

    private bool isRunning;

    private PlayerInputActions inputActions;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private float xRotation = 0f;

    public void Construct(MovementContext movementContext)
    {
        this.movementContext = movementContext;
    }

    private void Awake()
    {
        inputActions = new PlayerInputActions();

        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        inputActions.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Look.canceled += ctx => lookInput = Vector2.zero;

        inputActions.Player.Sprint.performed += ctx => isRunning = true;
        inputActions.Player.Sprint.canceled += ctx => isRunning = false;

        inputActions.Player.Jump.performed += ctx =>
        {
            if (movementContext != null)
            {
                movementContext.Jump();
            }
        };
    }

    private void OnEnable()
    {
        inputActions.Enable();
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }

    private void OnDisable()
    {
        inputActions.Disable();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Start()
    {
        SetupCamera();
    }

    private void SetupCamera()
    {
        cameraTarget = GameObject.FindGameObjectWithTag("CameraTarget").transform;
        cameraTarget.SetParent(cameraPosition, false);
    }

    public void Initialize(CharacterData characterData)
    {
        this.characterData = characterData;
        if (movementContext != null)
        {
            movementContext.SetCharacterData(characterData);
        }
    }

    private void Update()
    {
        HandleInput();
    }

    private void LateUpdate()
    {
        HandleMouseLook();
    }

    private void HandleInput()
    {
        Vector3 transformForward = transform.forward;
        Vector3 transformRight = transform.right;

        transformForward.y = 0;
        transformRight.y = 0;
        transformForward = transformForward.normalized;
        transformRight = transformRight.normalized;

        Vector3 moveDirection = (transformForward * moveInput.y + transformRight * moveInput.x);

        if (movementContext != null)
        {
            movementContext.Move(moveDirection, isRunning);
        }
    }

    private void HandleMouseLook()
    {
        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraPosition.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    public void UpdateCharacterStats(CharacterData updatedData)
    {
        characterData = updatedData;
        if (movementContext != null)
        {
            movementContext.SetCharacterData(characterData);
        }
    }
}
