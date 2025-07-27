using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AcidCube
{
    public class PlayerController : MonoBehaviour
    {
        // External
        private Rigidbody rb;
        private Collider playerCollider;
        public Renderer playerRenderer;

        // Own
        private Vector2 movementInput;
        private Vector2 groundDirection;
        private bool isJumpAvailable = true;

        // Settings

        [SerializeField] float speed = 5f;
        [SerializeField] float jumpDelay = 0.5f;
        [SerializeField] float jumpStrenght = 10f;
        [SerializeField] LayerMask groundLayer;
        [SerializeField] float raycastGroundCheck = 0.55f;
  

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            playerCollider = GetComponent<Collider>();
            playerRenderer = GetComponent<Renderer>();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            movementInput = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if(!context.performed || !isJumpAvailable) return;

            isJumpAvailable = false;
            StartCoroutine(WaitForJump());
            
            if(groundDirection.y != 0)
            {
                rb.AddForce(new Vector3(0, 1f, 0f) * jumpStrenght, ForceMode.Impulse);
            }

            else if (groundDirection.x != 0 && groundDirection.y == 0)
            {
                rb.AddForce(new Vector3(-groundDirection.x, 1f, 0f) * jumpStrenght, ForceMode.Impulse);
            }
        }

        private IEnumerator WaitForJump()
        {
            yield return new WaitForSeconds(jumpDelay);
            isJumpAvailable = true;
        }

        private void FixedUpdate()
        {
            DetermineIsGround();

            if (movementInput.x != 0)
            {
                rb.AddForce(Vector2.right * movementInput * speed /* *  boostSpeed*/, ForceMode.Force);
            }
            else if (movementInput.y != 0)
            {
                rb.AddForce(Vector2.left * movementInput * speed /* * boostSpeed*/, ForceMode.Force);
            }
        }

        private void DetermineIsGround()
        {
            Vector3 startPoint = playerCollider.bounds.center + new Vector3(0f, 0f, -0.4f);
            groundDirection = Vector3.zero;

            if (CastToDirection(startPoint, Vector3.right))
            {
                groundDirection.x++;
            }

            if (CastToDirection(startPoint, Vector3.left))
            {
                groundDirection.x--;
            }

            if (CastToDirection(startPoint, Vector3.down))
                groundDirection.y--;
        }

        private bool CastToDirection(Vector3 startPoint, Vector3 direction)
        {
            if (!Physics.Raycast(startPoint, direction, raycastGroundCheck, groundLayer))
            {
                return false;
            }
            return true;

            // NOTE: for debug
            // Debug.DrawRay(startPoint, direction * raycastGroundCheck, Color.cyan, Time.deltaTime);

        }
    }
}

