using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    
    PhotonView PV;

    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float turnSpeed = 200;
    [SerializeField] private float jumpForce = 7;

    [SerializeField] private Animator animator = null;
    public Rigidbody rigidBody = null;

    private float currentV = 0;
    private float currentH = 0;

    private readonly float interpolation = 10;
    private readonly float walkScale = 0.33f;

    private bool wasGrounded;
    private Vector3 currentDirection = Vector3.zero;

    private float jumpTimeStamp = 0;
    private float minJumpInterval = 0.25f;
    private bool jumpInput = false;

    private bool isGrounded;

    private List<Collider> collisions = new List<Collider>();


    private void Awake()
    {
        PV = GetComponent<PhotonView>();

        if (!animator) { gameObject.GetComponent<Animator>(); }
        if (!rigidBody) { gameObject.GetComponent<Animator>(); }

        rigidBody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!collisions.Contains(collision.collider))
                {
                    collisions.Add(collision.collider);
                }
                isGrounded = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }

        if (validSurfaceNormal)
        {
            isGrounded = true;
            if (!collisions.Contains(collision.collider))
            {
                collisions.Add(collision.collider);
            }
        }
        else
        {
            if (collisions.Contains(collision.collider))
            {
                collisions.Remove(collision.collider);
            }
            if (collisions.Count == 0) { isGrounded = false; }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collisions.Contains(collision.collider))
        {
            collisions.Remove(collision.collider);
        }
        if (collisions.Count == 0) { isGrounded = false; }
    }

    void Update()
    {
        if (!jumpInput && Input.GetKey(KeyCode.Space))
        {
            jumpInput = true;
        }
    }

    private void FixedUpdate()
    {
        if(!PV.IsMine) { return; }

        animator.SetBool("Grounded", isGrounded);
        Move();

        wasGrounded = isGrounded;
        jumpInput = false;

    }

    private void Move()
    {
        float v = Input.GetAxis("Vertical");      // 1f������ �� �޾ƿ��� �ﰢ���� W, A, S, D�� ����� �����ϱ� ���� ���
        float h = Input.GetAxis("Horizontal");    // 1f������ �� �޾ƿ��� �ε巯�� ���� ����� ���� ���

        Transform camera = Camera.main.transform;    // ī�޶� ������Ʈ �޾ƿ�

        if (Input.GetKey(KeyCode.LeftShift))     // ���� ShiftŰ�� ������ �ȱ�(�⺻������ �޸���)
        {
            v *= walkScale;
            h *= walkScale;
        }
        
        //ī�޶��� ���� ���� �����Ӱ� ���� ���������� ���� ���� ã�Ƽ� ��������
        currentV = Mathf.Lerp(currentV, v, Time.deltaTime * interpolation);
        currentH = Mathf.Lerp(currentH, h, Time.deltaTime * interpolation);

        Vector3 direction = camera.forward * currentV + camera.right * currentH;  // ī�޶��� ���Ͱ� �޾ƿͼ� ĳ������ �������� �޾ƿ�

        float directionLength = direction.magnitude;

        direction.y = 0;
        direction = direction.normalized * directionLength;

        if (direction != Vector3.zero)
        {
            currentDirection = Vector3.Slerp(currentDirection, direction, Time.deltaTime * interpolation);

            transform.rotation = Quaternion.LookRotation(currentDirection);
            transform.position += currentDirection * moveSpeed * Time.deltaTime;

            animator.SetFloat("MoveSpeed", direction.magnitude);  // moveSpeed�� ���� �ִϸ��̼� ���
        }

        JumpingAndLanding();
        Dance();
    }

    private void JumpingAndLanding()
    {
        bool jumpCooldownOver = (Time.time - jumpTimeStamp) >= minJumpInterval;  // ������ �ִ� 1������ ����

        if (jumpCooldownOver && isGrounded && jumpInput)
        {
            jumpTimeStamp = Time.time;
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);  // y������ �߷��� �༭ ����
        }

        // �ε巯�� ���� ����� ���� 2������ ��� ����
        if (!wasGrounded && isGrounded)
        {
            animator.SetTrigger("Land");
        }

        if (!isGrounded && wasGrounded)
        {
            animator.SetTrigger("Jump");
        }
    }

    private void Dance()   // Ư�� Ű �Է����� �� �ִϸ��̼� ȣ�� ����
    {
        if (isGrounded && Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Alpha2))
        {
            animator.SetTrigger("Dance");
        }

        if (isGrounded && Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.V))
        {
            animator.SetTrigger("Dance2");
        }
    }
}
