using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private float movementSpeed = 5f;
    [SerializeField]
    private float jumpPower = 10f;
    [SerializeField]
    private float secondJumpPower = 10f;

    [SerializeField]
    private float radius = 0.3f;

    public Transform groundCheckPosition;   // checks if the player is standing on the ground.
    public LayerMask layerGround;
    

    private Rigidbody myBody;
    private bool isGrounded;
    private bool playerJumped = false;
    private bool canDoubleJump = false;

    private PlayerAnimation playerAnim;

    public GameObject smokePosition;

    [SerializeField]
    private bool gameStarted;    // check in the inspector;

    private BGScroller bgScroller;

    // private PlayerHealthDamageShoot playerShoot;

    private Button jumpButton;
    #endregion

    private void Awake()
    {
        myBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        PlayerMove();
        PlayerGrounded();
        PlayerJump();
    }
    
    void PlayerMove()
    {
        myBody.velocity = new Vector3(movementSpeed, myBody.velocity.y, 0f);
    }

    void PlayerGrounded()
    {
        isGrounded = Physics.OverlapSphere(groundCheckPosition.position, radius, layerGround).Length > 0;
    }

    void PlayerJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isGrounded && canDoubleJump)
        {
            canDoubleJump = false;
            myBody.AddForce(new Vector3(0, secondJumpPower, 0));
        }
        else if(Input.GetKeyUp(KeyCode.Space) && isGrounded)
        {
            playerAnim.DidJump();
            myBody.AddForce(new Vector3(0, jumpPower, 0));
            playerJumped = true;
            canDoubleJump = true;
        }
    }

    public void Jump()
    {
        if(isGrounded)
        {       
            playerAnim.DidJump();
            myBody.AddForce(new Vector3(0, jumpPower, 0));
            playerJumped = true;
            canDoubleJump = true;

        }

        if(!isGrounded && canDoubleJump)
        {
            canDoubleJump = false;
            myBody.AddForce(new Vector3(secondJumpPower, 0));
        }

    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2f);

        gameStarted = true;
        bgScroller.canScroll = true;
        //playerShoot.canShoot = true;
        //GameplayController.instance.canCountScore = true;
        smokePosition.SetActive(true);
        playerAnim.PlayerRun();
    }

    private void OnCollisionEnter(Collision target)
    {
        if(target.gameObject.tag == Tags.PLATFORM_TAG)
        {
            if(playerJumped)
            {
                playerJumped = false;
                playerAnim.DidLand();
            }
        }
    }





} // Player Class
