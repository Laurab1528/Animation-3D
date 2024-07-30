using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //[SerializeField] private CharacterData characterData;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float movementVelocity = 100f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private bool canJump = true;
    [SerializeField] private CharacterVisualController playerVisualController;

    private Rigidbody _rigidBody = null;
    private float horizontalMovement = 0f;
    private float verticalMovement = 0f;
    private Vector3 playerInitialPosition = Vector3.zero;
    //private float movementVelocity = 0f;

    private void Awake()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody>();
        //movementVelocity = characterData.GetCharacterBaseVelocity;
        playerInitialPosition = transform.position;
    }

    private void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
        
        if (horizontalMovement != 0f || verticalMovement != 0)
        {
            playerVisualController.SetBoolAnimation("IsRunning", true);
        }
        else
        {
            playerVisualController.SetBoolAnimation("IsRunning", false);
        }
        
        if (canJump & Input.GetButtonDown("Jump"))
        {
            Jump();
            playerVisualController.SetTriggerAnimation("Jump");
        }
    }

    private void FixedUpdate()
    {
        //PHYSICS MOVEMENT
        _rigidBody.velocity = new Vector3(horizontalMovement * movementVelocity * Time.fixedDeltaTime, _rigidBody.velocity.y, verticalMovement * movementVelocity * Time.fixedDeltaTime);
        RotateCharacter(horizontalMovement, verticalMovement);
    }
    
    private void RotateCharacter(float horizontalInput, float verticalInput)
    {
        float targetAngle = Mathf.Atan2(horizontalInput, verticalInput) * Mathf.Rad2Deg;

        if (horizontalInput != 0 || verticalInput != 0)
        {
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
            _rigidBody.MoveRotation(Quaternion.Slerp(_rigidBody.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }

    private void Jump()
    {
        _rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            canJump = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            canJump = false;
        }
    }

    public void RespawnPlayer()
    {
        transform.position = playerInitialPosition;
    }
}
