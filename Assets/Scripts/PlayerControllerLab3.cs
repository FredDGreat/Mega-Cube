using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerLab3 : MonoBehaviour
{
    private Rigidbody playerRg;
    public float jumpForce = 600f;
    private float upBound = 5.86f;
    private float gravityModifier = 1f;
    private float downBound = -6.66f;
    private float horizontalInput;
    private float verticalInput;
    private float speed = 10f;
    private bool isOnGround = true;
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerRg = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ConstraintPlayerPosition();
    }
    /**
     * Moves player based on arroy keys
     */
    private void MovePlayer()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.Space) && isOnGround)
        {
            isOnGround = false;
            playerRg.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
    }
    //prevent the player from leaving the environment
    private void ConstraintPlayerPosition()
    {
        float curZ = transform.position.z;
        if (curZ >= upBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, upBound);
        }
        if (curZ <= downBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, downBound);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerRg.freezeRotation = true;
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            //playerRg.AddForce(Vector3.up * 5, ForceMode.Impulse);
        }
    }
}
