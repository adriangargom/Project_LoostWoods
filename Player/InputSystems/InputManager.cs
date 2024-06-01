using UnityEngine;

public class InputManager : MonoBehaviour
{
    [field: Header("Player Attributes")]
    [field: SerializeField] public Rigidbody PlayerRigidbody { get; private set; }
    [field: SerializeField] public CapsuleCollider PlayerCollider { get; private set; }
    [field: SerializeField] public Transform Player { get; private set; }
    [field: SerializeField] public Transform PlayerBody { get; private set; }
    [field: SerializeField] public Transform CameraTargetPoint { get; private set; }

    [field: Space]
    [field: Header("Movement Inputs")]
    [field: SerializeField] public Vector2 MovementInput { get; private set; }
    [field: SerializeField] public Vector2 RawMovementInput { get; private set; }
    [field: SerializeField] public bool IsRunning { get; private set; }
    [field: SerializeField] public bool IsRolling { get; private set; }

    [field: Space]
    [field: Header("Mouse Inputs")]
    [field: SerializeField] public Vector2 MouseInput { get; private set; }

    [field: Space]
    [field: Header("Attack Inputs")]
    [field: SerializeField] public bool IsAttacking { get; private set; }
    [field: SerializeField] public bool IsAiming { get; private set; }

    [field: Space]
    [field: Header("Interaction Inputs")]
    [field: SerializeField] public bool IsInteracting { get; private set; }



    void Awake()
    {
        PlayerCollider = GetComponent<CapsuleCollider>();
        PlayerRigidbody = GetComponent<Rigidbody>();
        Player = GetComponent<Transform>();
        PlayerBody = GameObject.Find("PlayerBody").GetComponent<Transform>();
        CameraTargetPoint = GameObject.Find("CameraTargetPoint").GetComponent<Transform>();
    }

    void Update()
    {
        GetMovementInput();
        GetRawMovementInput();
        GetMouseInput();
        GetRunningInput();
        GetRollingInput();
        GetAttackingInput();
        GetAimingInput();
        GetInteractionInput();
    }



    // Horizontal and vertical Keyboard movement axis inputs
    private void GetMovementInput()
    {
        MovementInput = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        );
    }

    // Horizontal and vertical Keyboard raw movement axis inputs
    private void GetRawMovementInput()
    {
        RawMovementInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );
    }

    // X and Y Mouse movement axis inputs
    private void GetMouseInput()
    {
        MouseInput = new Vector2(
            Input.GetAxis("Mouse X"),
            Input.GetAxis("Mouse Y")
        );
    }

    // "LeftShift" Keyboard input
    private void GetRunningInput()
    {
        IsRunning = Input.GetKey(KeyCode.LeftShift) && MovementInput.magnitude != 0;
    }

    // "Space" Keyboard Input
    private void GetRollingInput()
    {
        IsRolling = Input.GetKeyDown(KeyCode.Space);
    }

    // "Mouse0" Keyboard Input
    private void GetAttackingInput()
    {
        IsAttacking = Input.GetKeyDown(KeyCode.Mouse0);
    }

    // "Mouse1" Keyboard Input
    private void GetAimingInput()
    {
        IsAiming = Input.GetKey(KeyCode.Mouse1);
    }

    // "E" Keyboard Input
    private void GetInteractionInput()
    {
        IsInteracting = Input.GetKeyDown(KeyCode.E);
    }
}