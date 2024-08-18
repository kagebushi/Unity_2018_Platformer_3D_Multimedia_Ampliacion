using System.Collections;
using System.Collections.Generic;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControler : MonoBehaviour
{

    public static PlayerControler Instance;

    [Header("Movement")]
    public float moveSpeed;
    public float jumpForce;
    public float gravityScale = 3f;
    public Vector3 moveDirection;
    public float rotateSpeed = 5f;
    public int aditionalJump = 1;
    public int jumpCount;
    public float bounceForce = 8f;

    [Header("References")]
    public CharacterController charCtrl;
    public Camera playerCam;
    public GameObject playerModel;
    public Animator animator;

    [Header("KnockBack")]
    public bool isKnocking;
    public float knockbackLength = .5f;
    private float knockbackCounter;
    public Vector2 knockbackPower;
    public GameObject[] playerPieces;

    internal bool stopMoving;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        jumpCount = aditionalJump;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isKnocking && !stopMoving)
        {
            float yStore = moveDirection.y;
            //moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"),0f,Input.GetAxisRaw("Vertical"));

            moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
            moveDirection.Normalize();
            moveDirection = moveDirection * moveSpeed;
            moveDirection.y = yStore;

            if (charCtrl.isGrounded)
            {
                moveDirection.y = 0f; // Resetea la velocidad vertical si está en el suelo

                jumpCount = aditionalJump; // Reinicia el contador de saltos si está en el suelo

                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpForce;
                }
            }
            else // Si está en el aire
            {
                if (Input.GetButtonDown("Jump") && jumpCount > 0)
                {
                    moveDirection.y = jumpForce;
                    jumpCount--;
                }

                moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale; // Aplica la gravedad
            }

            // Pieza del codigo para mover el personaje
            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
            charCtrl.Move(moveDirection * Time.deltaTime);

            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                transform.rotation = Quaternion.Euler(0f, playerCam.transform.rotation.eulerAngles.y, 0f);
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);

            }

        }

        if (stopMoving) 
        {
            moveDirection = Vector3.zero;
            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
            charCtrl.Move(moveDirection);
        }

        if (isKnocking)
        {
            knockbackCounter -= Time.deltaTime;

            float yStore = moveDirection.y;
            moveDirection = (playerModel.transform.forward * knockbackPower.x);
            moveDirection.y = yStore;

            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

            charCtrl.Move(moveDirection * Time.deltaTime);

            if (knockbackCounter <= 0)
            {
                isKnocking = false;
            }
        }
            
        animator.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
        animator.SetBool("Grounded", charCtrl.isGrounded);
    }

    public void knockback()
    {
        isKnocking = true;

        knockbackCounter = knockbackLength;
        Debug.Log("KnockBack");

        moveDirection.y = knockbackPower.y;

        charCtrl.Move(moveDirection * Time.deltaTime);
    }

    public void Bounce()
    {
        moveDirection.y = bounceForce;
        charCtrl.Move(moveDirection * Time.deltaTime);
    }
}
