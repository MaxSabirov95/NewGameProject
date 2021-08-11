using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    float maxSpeed; //max value of currentSpeed
    [SerializeField]
    float minSpeed; //min value of currentSpeed
    [SerializeField]
    float addedGravSpeed;
     [SerializeField]
    float jumpTime;
     [SerializeField]
    LayerMask layerMask;
    [SerializeField]
    LayerMask layerMask2;


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
    private bool isInterrupted;
    [SerializeField]
    private float speedRiseMod;

    public Queue<float> lastMagnitudes;
    [SerializeField]
    private float minDelta;
    public float minCamZ;
    public float maxCamZ;

    void Start()
    {
        lastMagnitudes = new Queue<float>(5);
        
        isInterrupted = false;
        doGrounded = true;
        canJump = false;
        isJumping = false;

        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(GainSpeed());
    }
    public void Interrupt()
    {
        isInterrupted = true;
    }

    void InterruptCheck()
    {
        lastMagnitudes.Enqueue(rb.velocity.magnitude);
        if (lastMagnitudes.Count >= 5)
            lastMagnitudes.Dequeue();
        if (lastMagnitudes.Count > 2)
        {
            var lowestValue = lastMagnitudes.Where(x => x == lastMagnitudes.Min()).First();
            var highestValue = lastMagnitudes.Where(x => x == lastMagnitudes.Max()).First();
            var highOrLowValue = lastMagnitudes.Where(x => x == lastMagnitudes.Max() || x== lastMagnitudes.Min()).First();

        //float max = lastMagnitude.Max();
        //float min = lastMagnitude.Min(x => x);

        Debug.Log(highOrLowValue);
        if(highOrLowValue == highestValue)
            {
                //Debug.Log("high, with delta of: " + (highestValue - lowestValue));
                if ((highestValue - lowestValue) > minDelta)
                {
                    Interrupt();
                    Debug.LogWarning("Interrupted");
                }
            }
        
        }
        //foreach (var mag in lastMagnitude)
        //{
            
        //}
    }
    IEnumerator GainSpeed()
    {
        float currentCooldown = 0;
        while (true)
        {
            if (isInterrupted && currentCooldown < 1)
            {
                currentSpeed = minSpeed;
                currentCooldown += Time.deltaTime;

                continue;
            }
            currentCooldown = 0; //zeros after first time not continuing (and every time when not interrupted);
            isInterrupted = false;

            currentSpeed += Time.fixedDeltaTime * speedRiseMod;
            currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);

            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator GainSpeedCooldown()
    {
        while (isInterrupted)
        {
            

            yield return new WaitForFixedUpdate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //inputVector.y = Input.GetAxisRaw("Vertical");
        InterruptCheck();
        Vector3 newCamPos = cam.transform.localPosition + (Vector3.forward * -.01f * rb.velocity.magnitude * Time.deltaTime) + (Vector3.forward * Time.deltaTime * .1f);
        newCamPos.z = Mathf.Clamp(newCamPos.z, minCamZ, maxCamZ); //min max cam z
        cam.transform.localPosition = newCamPos;

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
        GetComponent<Animator>().SetTrigger("Jump");
        canJump = false;
        isJumping = true;
        rb.AddRelativeForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        doJump = false;
        _jumpTimer = 0;

        RaycastHit2D hit = Physics2D.BoxCast(groundChecker.position, Vector2.one*transform.localScale.y/2, 0, Vector2.down, 1f, layerMask2);
        if(hit)
        {
            hit.collider.GetComponent<BW_Tile>().ColorMe();
        }
    }

    private void FixedUpdate()
    {


        inputVector.x = Time.fixedDeltaTime * currentSpeed;

        //inputVector *= currentSpeed;
        
        
        rb.AddRelativeForce(inputVector);

        if (isJumpHeld)
        {
            rb.AddRelativeForce(Vector2.up * jumpForce / 2f);

        }
        
            


        // cam.fieldOfView = 60+ rb.velocity.magnitude*fovSpeed*Time.fixedDeltaTime;
        //if (doGrounded)
        if (!IsGrounded())
        {
            AddedGravity();
        }
        else
        {
            GetComponent<Animator>().SetBool("Falling", false);
        }

    }
    public bool IsGrounded()
    {
        bool toReturn = Physics2D.BoxCast(groundChecker.position, Vector2.one * 8, 0, Vector2.down, 1f, layerMask);
        isJumping = !toReturn;
        GetComponent<Animator>().SetBool("Falling", !toReturn);
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

    public void CallCamToFOV(float fov, float time)
    {
        StartCoroutine(CamToFOV(fov, time));
    }

    IEnumerator CamToFOV(float fov, float time)
    {
        float start = cam.transform.localPosition.z;
        float t = 0f;
        float ogMinCamZ = minCamZ;
        if(minCamZ > fov)
        minCamZ = fov;

        while(t != time)
        {
            float newZ = Mathf.Lerp(start, fov,t/time);
            cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, cam.transform.localPosition.y, newZ);
            t += Time.deltaTime;
            yield return null;
        }
        minCamZ = ogMinCamZ;
    }
}
