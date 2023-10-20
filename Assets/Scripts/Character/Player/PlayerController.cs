using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private Vector2 curMovementInput;
    public float jumpForce;
    public LayerMask groundLayerMask;

    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask windMask;
    [SerializeField] private LayerMask thunderMask;
    [SerializeField] private LayerMask flowerJumpMask;

    [Header("Look")]
    [SerializeField] private Transform cameraPivot;

    public float minXLook;
    public float maxXLook;

    private float camCurXRot;
    public float lookSensitivity;

    private Vector2 mouseDelta;

    private static readonly int IsWalk = Animator.StringToHash("IsWalk");
    // private static readonly int IsHit = Animator.StringToHash("IsHit");
    private static readonly int Jump = Animator.StringToHash("Jump");

    [HideInInspector]
    private Rigidbody _rigidbody;


    public static PlayerController instance;
    private void Awake()
    {
        instance = this;
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        LookAround();
    }

    private void LookAround()
    {
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);

        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraPivot.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
    }

    private void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
            animator.SetBool(IsWalk, true);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
            animator.SetBool(IsWalk, false);
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (IsGrounded())
            {
                _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
                animator.SetTrigger(Jump);
            }

        }
    }
    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnEscape(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && (!UIManager.IsOpen(eUIType.Option) || UIManager.IsHide(eUIType.Option)))
        {
            Cursor.lockState = CursorLockMode.Confined;
            var ui = UIManager.ShowUI<UIOption>();
            ui.Initialize(() => { Cursor.lockState = CursorLockMode.Locked; });
        }
    }
    private bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (Vector3.up * 0.01f) , Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f)+ (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + (transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (transform.right * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.right * 0.2f), Vector3.down);
    }

    private void OnTriggerStay(Collider other)
    {
        if (windMask.value == (windMask.value | (1 << other.gameObject.layer)))
        {
            if (transform.position.z < 20)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(1f, 0, 0), 0.07f);
            }

            else
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(-1f, 0, 0), 0.07f);
            }
        }
        else if (flowerJumpMask == (flowerJumpMask.value | (1 << other.gameObject.layer)))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, 1f, 0), 0.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (thunderMask.value == (thunderMask.value | (1 << other.gameObject.layer)))
        {
            transform.position = other.GetComponent<Teleport>().TeleportPosition;
        }
    }
}