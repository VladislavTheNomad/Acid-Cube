using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    // properties acceptable for all classes of State Pattern
    public Rigidbody rb { get; private set; }
    public Vector2 movementInput { get; private set; } // current input of moving
    public float speed { get { return _speed; } }
    public float jumpStrenght { get { return _jumpStrenght; } }
    public bool isGrounded { get; private set; } //flag to check if player is on the ground

    private bool isJumpAviable { get; set; } // check if Jump Timer is expired (sec gap between jumps)

    private Collider playerCollider;

    // customizable parameters
    [SerializeField] float _speed = 3f;
    [SerializeField] float _jumpStrenght = 10f;
    [SerializeField] float jumpTimer = 0.5f; // time for recovery of jump counts
    [SerializeField] int jumpCount = 1; // max jumps before recovering jumpTimer
    [SerializeField] LayerMask groundLayer; // Layer for all ground 
    [SerializeField] float raycastForGroundCheck = 0.55f; //how deep to bottom needs to check if there is a ground 
    [SerializeField] float raycastForWallCheck = 0.55f; //how deep to left needs to check if there is a wall 

    //State Pattern
    private IPlayerStates currentState;

    private void Awake()
    {
        isJumpAviable = true;
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>(); 
    }

    private void Start()
    {
        SetState(new IdleState()); //set up entry state for player - IDLE
    }

    public void SetState(IPlayerStates newState)
    {
        currentState?.OnExit(this); // if there is some state, cancels it
        currentState = newState; // set up new state
        currentState.OnEnter(this); // call Enter in this state
    }

    // --- Input System Callbacks ---

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>(); //save current input of moving
        currentState.HandleMovement(this, context);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && jumpCount > 0)
        {
            StartCoroutine(JumpCounter());
            currentState.HandleJump(this, context);
        }
    }

    // --- Unity LifeCycle Methods ---

    void FixedUpdate()
    {
        CheckIfGrounded();
        if (!isGrounded)
        {
            CheckIfOnTheWall();
        }
        currentState.PhysicsUpdate(this);

    }

    // --- Ground Detection ---

    private void CheckIfGrounded()
    {
        // Implement start point of raycasting
        Vector3 startPoint = playerCollider.bounds.center;

        // Throwing of RayCast
        RaycastHit hit;
        isGrounded = Physics.Raycast(startPoint, Vector3.down, out hit, raycastForGroundCheck, groundLayer); // start point, direction, information about hits, check distance, layers to check
    }

    private void CheckIfOnTheWall()
    {
        // Implement start point of raycasting
        Vector3 startPoint = playerCollider.bounds.center;

        // Throwing of RayCast
        RaycastHit hit;
        if (Physics.Raycast(startPoint, Vector3.right, out hit, raycastForWallCheck, groundLayer))
        {
            //isGrounded = true;
            SetState(new WallTouch("right"));
        }
        else if (Physics.Raycast(startPoint, Vector3.left, out hit, raycastForWallCheck, groundLayer))
        {
            SetState(new WallTouch("left"));
        }
    }

    IEnumerator JumpCounter()
    {
        jumpCount--;
        yield return new WaitForSeconds(jumpTimer);
        jumpCount++;
    }
}
