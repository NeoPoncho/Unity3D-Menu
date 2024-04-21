using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed;
    public float TurnSpeed;
    public float jumpForce;
    private Animator animationPlayer;
    public float gravityModifier;
    public bool isOnGround = true;
    private float velocity;
    public AudioClip jumpSound;
    public AudioSource runSound;
    private AudioSource playerAudio;
    public Transform cameraTransform;
    public bool isJumping;
    private float jumpMultiplier = 1.5f;
    public Transform spawnPoint;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        animationPlayer = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        RespawnPoint();

    }



    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            isOnGround = true;
            animationPlayer.SetInteger("jumpSpeed", 0);


        }

    }

    private void PlayerMovement()
    {
        //Player Movement
        float inputVertical = Input.GetAxis("Vertical");
        float inputHorizontal = Input.GetAxis("Horizontal");

        Vector3 movementInput = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * new Vector3(inputHorizontal, 0, inputVertical);
        Vector3 movementDirection = movementInput.normalized;


        //Rotation
        if (movementDirection != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, TurnSpeed * Time.deltaTime);
        }

        //Move Player
        transform.position = transform.position + movementInput * Time.deltaTime * speed;


        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {


            if (Input.GetKey(KeyCode.LeftShift) && inputVertical > 0 && isOnGround || inputVertical < 0 && Input.GetKey(KeyCode.LeftShift) && isOnGround)
            {
                animationPlayer.SetInteger("jumpSpeed", 1);
                isOnGround = false;
                isJumping = true;
                animationPlayer.SetBool("sprint", true);
                jumpMultiplier = 1.5f;
                playerRb.AddRelativeForce(Vector3.up * jumpForce * jumpMultiplier, ForceMode.Impulse);
                JumpingSound();


            }
            else
            {
                animationPlayer.SetInteger("jumpSpeed", 1);
                isOnGround = false;
                isJumping = true;
                animationPlayer.SetBool("sprint", false);
                jumpMultiplier = 1;
                playerRb.AddRelativeForce(Vector3.up * jumpForce * jumpMultiplier, ForceMode.Impulse);
                JumpingSound();

            }







        }

        //animate and change speed
        if (Input.GetKey(KeyCode.LeftShift))
        {
            velocity = speed * Time.deltaTime * inputVertical * 1.5f;
            animationPlayer.SetBool("sprint", true);
            runSound.pitch = 1.60f;

        }
        else
        {
            velocity = speed * Time.deltaTime * inputVertical;
            animationPlayer.SetBool("sprint", false);
            runSound.pitch = 1.40f;

        }

        if (movementDirection != Vector3.zero)
        {
            animationPlayer.SetInteger("speed", 2);
            runSound.enabled = true;


        }
        else if (inputVertical == 0 && isOnGround || inputHorizontal == 0 && isOnGround)
        {
            animationPlayer.SetInteger("speed", 0);
            runSound.enabled = false;
        }


        //Player Movement (something i tried to make but needs work)
        /*Vector3 movementinput = Quaternion.Euler(0,cameraTransform.eulerAngles.y,0) * new Vector3(inputHorizontal, 0, inputVertical);
        Vector3 movementDirection = movementinput.normalized;

        Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation,desiredRotation, TurnSpeed * Time.deltaTime);
        transform.position = transform.position + Camera.main.transform.forward * velocity;




        transform.Rotate( 0, TurnSpeed * Time.deltaTime * y, 0);*/

    }

    void RespawnPoint()
    {
        //Se precionar a letra R dá respawn
        if (Input.GetKey(KeyCode.R))
        {
            transform.position = spawnPoint.position;
            animationPlayer.SetInteger("speed", 0);
            animationPlayer.SetBool("sprint", false);
            animationPlayer.SetInteger("jumpSpeed", 0);
            runSound.enabled = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        spawnPoint.transform.position = other.transform.position;
    }

    public void JumpingSound()
    {
        if (isJumping)
        {
            playerAudio.clip = jumpSound;
            playerAudio.pitch = 1f;
            playerAudio.PlayOneShot(playerAudio.clip, 1.0f);
        }
    }

}
