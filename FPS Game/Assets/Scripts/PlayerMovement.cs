using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb; //Player Rigidbody


    //Horizontal and Vertical Movement
    float currentSpeedH = 0f; //Horizontal Speed
    float currentSpeedV = 0f; //Vertical Speed
    public float acceleration = 5f; //Horizontal Acceleration & Vertical
    public float speed = 5f; //Movement Speed

    bool movingH = false; 
    bool movingV = false;

    //Camera
    float verticalRotation = 0f;
    float horizontalRotation = 0f;
    public float sensitivity;
    public GameObject playerCam;


    //Jumping
    bool grounded; 
    public float jumpHeight;
    public float gravityAcceleration;
    float downwardSpeed = 0;
    float jumpSpeed = 0;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        //Camera Movement
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity * 100f;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * -sensitivity * 100f;

        verticalRotation += mouseY;
        horizontalRotation += mouseX;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);


        transform.rotation = Quaternion.Euler(0f, horizontalRotation, 0f);
        playerCam.transform.rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);

        
        //Horizontal Movement 
        float HorizontalMovement = Input.GetAxisRaw("Horizontal");
        float VerticalMovement = Input.GetAxisRaw("Vertical");

        if (HorizontalMovement != 0) {
            movingH = true;
            if (movingV) {
                currentSpeedH = HorizontalMovement;
            } else {
                if (HorizontalMovement > 0) {
                    if (currentSpeedH < 1f) {
                        currentSpeedH += Time.deltaTime * acceleration;
                    }
                } else {
                    if (currentSpeedH > -1f) {
                        currentSpeedH -= Time.deltaTime * acceleration;
                    }
                }
        }
        } else {
            currentSpeedH = 0f;
            movingH = false;
        }

        //Vertical Movement
        if (VerticalMovement != 0) {
            movingV = true;

            if (movingH) {
                currentSpeedV = VerticalMovement;
            } else {
                if (VerticalMovement > 0) {
                    if (currentSpeedV < 1f) {
                        currentSpeedV += Time.deltaTime * acceleration;
                    }
                } else {
                    if (currentSpeedV > -1f) {
                        currentSpeedV -= Time.deltaTime * acceleration;
                    }
                }
            }
        } else {
            currentSpeedV = 0f;
            movingV = false;
        }

        //Making character move
        if (movingV && movingH) {
            float speedTempV = Mathf.Sqrt((Mathf.Abs(currentSpeedV) * Mathf.Abs(currentSpeedV))/2); //Idfk wat this does tbh
            float speedTempH = Mathf.Sqrt((Mathf.Abs(currentSpeedH) * Mathf.Abs(currentSpeedH))/2); //It just worked

            if (HorizontalMovement > 0) {
                if (VerticalMovement > 0) {
                    rb.velocity = (transform.forward * speedTempV * 10f + transform.right * speedTempH * 10) * speed;
                } else {
                    rb.velocity = (transform.forward * -speedTempV * 10f + transform.right * speedTempH * 10f) * speed;
                }
            } else {
                if (VerticalMovement > 0) {
                    rb.velocity = (transform.forward * speedTempV * 10f + transform.right * speedTempH * -10f) * speed;
                } else {
                    rb.velocity = (transform.forward * speedTempV * -10f + transform.right * speedTempH * -10f) * speed;
                }
            }
        } else {
            rb.velocity = (transform.forward * currentSpeedV * 10f + transform.right * currentSpeedH * 10f) * speed;
            
        }



        //Gravity
        if (!grounded) {
            downwardSpeed -= gravityAcceleration * Time.deltaTime;
            float tempDown = downwardSpeed;
            downwardSpeed += jumpSpeed;
            jumpSpeed += tempDown;

            rb.velocity = new Vector3(rb.velocity.x, downwardSpeed, rb.velocity.z);
        } else {
            downwardSpeed = 0;
        }



        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) && grounded) {

            float jumpForce = Mathf.Sqrt(19.6f * jumpHeight);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpSpeed = rb.velocity.y;
        }

    }


    void OnTriggerEnter(Collider col) {
        if (col.gameObject.layer == 6) {
            grounded = true;
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.gameObject.layer == 6) {
            grounded = false;
        }
    }
}

