using UnityEngine;
using UnityEngine.UI; 

public class TopKontrol : MonoBehaviour
{
    public float moveSpeed = 50f;
    public float maxJumpForce = 15f;
    public float chargeRate = 15f;
    public float groundLevel = -5f;

    public Slider jumpForceSlider;
    public Image fillImage;
    int adjustedMoveSpeed;

    private Rigidbody rb;
    private Vector3 moveDirection = Vector3.zero;
    private float currentJumpForce = 0f;
    private bool isCharging = false;
    private bool isGrounded = true;
    private Vector3 startingPosition;
    public int ziplamaSayisi = 0;

    public float resolutionWidthReference = 1920f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startingPosition = transform.position;
        jumpForceSlider.value = 0f;
        fillImage.color = Color.green;
        Debug.Log("Screen Width: " + Screen.width +"screen height" + Screen.height);
    }

    void Update()
    {
        if (transform.position.y < groundLevel)
        {
            ResetPosition();
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        float currentScreenWidth = Screen.width;
        float scalingFactor = currentScreenWidth / resolutionWidthReference;
        float adjustedMoveSpeed = moveSpeed * scalingFactor;

        rb.MovePosition(transform.position + moveDirection * adjustedMoveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isCharging = true;
            currentJumpForce = 0f;
        }

        if (Input.GetKey(KeyCode.Space) && isCharging)
        {
            currentJumpForce += chargeRate * Time.deltaTime;
            currentJumpForce = Mathf.Clamp(currentJumpForce, 0, maxJumpForce);
            jumpForceSlider.value = currentJumpForce / maxJumpForce;
            float value = jumpForceSlider.value;
            fillImage.color = Color.Lerp(Color.red, Color.green, value);
        }

        if (Input.GetKeyUp(KeyCode.Space) && isCharging)
        {
            isCharging = false;
            Jump();
            jumpForceSlider.value = 0f; 
        }
    }

    void Jump()
    {
        Vector3 jumpDirection = moveDirection * currentJumpForce;
        jumpDirection.y = currentJumpForce;

        rb.AddForce(jumpDirection, ForceMode.Impulse);
        isGrounded = false;
        ziplamaSayisi++;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (ziplamaSayisi > 0 && collision.gameObject.CompareTag("Zemin"))
        {
            if (!isGrounded && collision.contacts.Length > 0)
            {
                ResetPosition();
                ziplamaSayisi = 0;
            }
        }
    }

    void ResetPosition()
    {
        transform.position = startingPosition;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        isGrounded = true;
        ziplamaSayisi = 0;
        jumpForceSlider.value = 0f;
    }
}
