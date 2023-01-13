using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//used "https://www.youtube.com/watch?v=1tuQqm3gQBI&ab_channel=DaniKrossing" for character controller and added my own features to it.


public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb2D;

    private float moveSpeed, jumpForce, moveHorizontal, moveVertical;
    private bool isJumping;
    private bool isRunning;

    public LayerMask spikeLayer;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();

        moveSpeed = 2f;
        jumpForce = 40f;
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        Vector2 position = transform.position;
        Vector2 direction =  Vector2.down;
        
    }
    void FixedUpdate()
    {
        if ((moveHorizontal > 0.1f || moveHorizontal < -.1f))
        {
            rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
        }
        if (moveVertical > 0.1f && isJumping != true)
        {
            rb2D.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);

        }
        if (moveVertical < -.1f)
        {
            this.transform.localScale = new Vector3(1, 1, 0);
        }
        else
        {
            this.transform.localScale = new Vector3(1, 2, 0);
        }
        if (isRunning == true)
        {
            moveSpeed = 1f;
        }
        else
        {
            moveSpeed = 2f;
        }

        RayCastSpikeDetection();
        

    } 
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Platform")
        {
            isJumping = false;
            isRunning = false;
        }
        
    }
    private void OnTriggerExit2D()
    {
        isJumping = true;
        isRunning = true;
    }
    public void Die()
    {
        Debug.Log("character has died");
        
    }
    private void RayCastSpikeDetection()
    {
        Debug.DrawRay(transform.position, -Vector3.up * 10, Color.white, 0);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 10, LayerMask.GetMask("spikes"));
        if(hit.collider != null)
        {
            if(hit.collider.tag == "spikes")
            {
                Debug.Log("died");
            }
        }
    }
}