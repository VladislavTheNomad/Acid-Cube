using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    // External
    private Rigidbody rb;
    private Collider playerCollider;

    // Own
    private Vector2 movementInput;
    private Vector2 groundDirection;

    // Settings

    [SerializeField] float speed = 5f;
    [SerializeField] float jumpGAP = 0.5f;
    [SerializeField] float jumpStrenght = 10f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float raycastGroundCheck = 0.55f;

    private bool isJumpAviable = true;
    //private float boostSpeed = 1f;
    private float maxSpeedBoosting = 2f;
    private bool isSpeedBoostingActive = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        if (movementInput.x != 0 && !isSpeedBoostingActive)
        {
            isSpeedBoostingActive = true;
            //StartCoroutine(SpeedBoostIncreasing());
        }
    }

    //IEnumerator SpeedBoostIncreasing()
    //{
    //    StopCoroutine(SpeedBoostDecreasing());
    //    while (movementInput.x != 0 && boostSpeed < maxSpeedBoosting)
    //    {
    //        boostSpeed += Time.deltaTime;
    //        yield return null;
    //    }
    //    isSpeedBoostingActive = false;
    //}

    //IEnumerator SpeedBoostDecreasing()
    //{
    //    while (movementInput.x == 0 && boostSpeed > 1f)
    //    {
    //        boostSpeed -= Time.deltaTime;
    //        yield return null;
    //    }
    //}

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isJumpAviable)
        {
            isJumpAviable = false;
            StartCoroutine(GapForJump());
            rb.AddForce(-groundDirection * jumpStrenght, ForceMode.Impulse);
            if (groundDirection.x != 0 && groundDirection.y == 0)
            {
                rb.AddForce(Vector3.up * jumpStrenght, ForceMode.Impulse);
            }
        }
    }

    IEnumerator GapForJump()
    {
        yield return new WaitForSeconds(jumpGAP);
        isJumpAviable = true;
    }

    void FixedUpdate()
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
        else
        {
            //StartCoroutine(SpeedBoostDecreasing());
        }
    }

    private void DetermineIsGround()
    {
        Vector3 startPoint = playerCollider.bounds.center;
        groundDirection = Vector3.zero;

        if (Physics.Raycast(startPoint, Vector3.right, out var rightHit, raycastGroundCheck, groundLayer))
        {
            Debug.DrawRay(startPoint, Vector3.right * raycastGroundCheck, Color.cyan, Time.deltaTime);
            groundDirection.x++;
        }

        if (Physics.Raycast(startPoint, Vector3.left, out var leftHit, raycastGroundCheck, groundLayer))
        {
            Debug.DrawRay(startPoint, Vector3.left * raycastGroundCheck, Color.cyan, Time.deltaTime);
            groundDirection.x--;
        }

        if (Physics.Raycast(startPoint, Vector3.down, out var groundHit, raycastGroundCheck, groundLayer))
        {
            Debug.DrawRay(startPoint, Vector3.down * raycastGroundCheck, Color.cyan, Time.deltaTime);
            groundDirection.y--;
        }
    }
}