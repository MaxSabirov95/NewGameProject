using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 inputVector;
    [SerializeField]
    float currentSpeed;
    [SerializeField]
    float jumpForce;
    [SerializeField]
    float fovSpeed;
    [SerializeField]
    float currentGravMod;
    [SerializeField]
    float baseGravMod;
    [SerializeField]
    float maxGravMod;
    [SerializeField]
    float minGravMod;
    [SerializeField]
    float addedGravSpeed;
     [SerializeField]
    float jumpTime;
     [SerializeField]
    LayerMask layerMask;


    [SerializeField]
    Transform groundChecker;

    float _jumpTimer;

    Rigidbody2D rb;
    [SerializeField]
    Camera cam;

    bool doJump;
    [SerializeField]
    bool isJumpHeld;
    [SerializeField]
    bool isGrounded;
    [SerializeField]
    bool isJumping;
    bool doGrounded;
    private bool canJump;

    private Vector3 lastPosition;
    private Vector3 lastVelocity;
    private Vector3 lastAcceleration;

    private void Awake()
    {
        Vector3 position = transform.position;
        Vector3 velocity = Vector3.zero;
        Vector3 acceleration = Vector3.zero;
    }

    void Start()
    {
        doGrounded = true;
        canJump = false;
        isJumping = false;

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //inputVector.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump") && canJump)
        {
            Jump();
        }

        isJumpHeld = isJumping && Input.GetButton("Jump");
        //{
        //    //inputVector.y = jumpForce;
        //}
        //decide currentSpeed

        //inputVector *= currentSpeed;
    }
    void Jump()
    {
        canJump = false;
        isJumping = true;
        rb.AddRelativeForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        doJump = false;
        _jumpTimer = 0;
    }

    private void FixedUpdate()
    {
        inputVector.x = Time.fixedDeltaTime;

        inputVector *= currentSpeed;
        
        
        rb.AddRelativeForce(inputVector, ForceMode2D.Impulse);

        if(isJumpHeld)
        {
            rb.AddRelativeForce(Vector2.up * jumpForce/2f);

        }

        // cam.fieldOfView = 60+ rb.velocity.magnitude*fovSpeed*Time.fixedDeltaTime;
        //if (doGrounded)
        IsGrounded();

        AddedGravity();

        //Vector3 position = transform.position;
        //Vector3 velocity = (position - lastPosition) / Time.deltaTime;
        //Vector3 acceleration = (velocity - lastVelocity) / Time.deltaTime;
        //if (Mathf.Abs(acceleration.magnitude - lastAcceleration.magnitude) < 0.01f)
        //{
        //    // Still
        //}
        //else if (acceleration.magnitude > lastAcceleration.magnitude)
        //{
        //    cam.fieldOfView = 110;
        //}
        //else
        //{
        //    // Decelerating
        //}
        //lastAcceleration = acceleration;
        //lastVelocity = velocity;
        //lastPosition = position;
    }
    public bool IsGrounded()
    {
        bool toReturn = Physics2D.BoxCast(groundChecker.position, Vector2.one/2f, 0, Vector2.down, 1f, layerMask);
        isJumping = !toReturn;
        isGrounded = canJump = toReturn;
        return toReturn;
    }
    IEnumerator GravityAddLoop()
    {
        yield return new WaitForSeconds(jumpTime);
        doGrounded = true;
        while (!isGrounded)
        {
            AddedGravity();
            currentGravMod += Time.fixedDeltaTime * addedGravSpeed;
            yield return new WaitForFixedUpdate();
        }
        isJumping = false;

        currentGravMod = 1;
        canJump = true;
    }

    void AddedGravity()
    {
        rb.AddRelativeForce(Physics2D.gravity * currentGravMod);
    }
}
