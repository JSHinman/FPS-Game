using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    bool decellerateAfterJump = false; 
    bool VerticalDirection; // True is forwards
    public Transform point; 
    bool verticalDecel = false;
    bool VerticalMovementTemp;

    //Camera
    float verticalRotation = 0f;
    float horizontalRotation = 0f;
    public float sensitivity;
    public GameObject playerCam;


    //Jumping
    bool grounded; 
    bool startJump = false;
    public float jumpForce;

    public Text velocityText; // Show current velocity
    public float decelerationTime;
 
    bool isJumping = false;
    bool justBunnyHopped = false;
    float jumpDirection;
    bool startedJumping = false;
    Quaternion currentDir;
    
    


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
         
        point.position = transform.position;

        if (startedJumping && grounded) {
            startedJumping = false;
        }

        //Camera Movement
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity * 100f;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * -sensitivity * 100f;

        verticalRotation += mouseY;
        horizontalRotation += mouseX;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);


        transform.rotation = Quaternion.Euler(0f, horizontalRotation, 0f);
        playerCam.transform.rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);

        
        


        if (decellerateAfterJump) {
            if (!isJumping && grounded && !justBunnyHopped) {
                if (currentSpeedV > 0 && VerticalDirection == true) {
                    currentSpeedV -= decelerationTime * Time.deltaTime;
                } else if (currentSpeedV < 0 && VerticalDirection == false) {
                    currentSpeedV += decelerationTime * Time.deltaTime;
                } else {
                    currentSpeedV = 0;
                    decellerateAfterJump = false;
                }
            }
            


        }
        //Horizontal Movement 
        float HorizontalMovement = Input.GetAxisRaw("Horizontal");
        float VerticalMovement = Input.GetAxisRaw("Vertical");

        
        if (HorizontalMovement != 0) {
            movingH = true;

            if (currentSpeedV == 1f || currentSpeedV == -1f) {
                currentSpeedH = 1f * HorizontalMovement;
            } else {
                if (HorizontalMovement > 0) {
                    if (currentSpeedH < 1f) {
                        currentSpeedH += Time.deltaTime * acceleration;
                    } else if (currentSpeedH > 1f) {
                        currentSpeedH = 1f;
                    }
                } else {
                    if (currentSpeedH > -1f) {
                        currentSpeedH -= Time.deltaTime * acceleration; 
                    } else if (currentSpeedH < -1f) {
                        currentSpeedH = -1f;

                    }
                }
            }
        } else {
            currentSpeedH = 0f;
            movingH = false;
        }

        //Vertical Movement
        if (grounded) {
            if (VerticalMovement != 0) {
                movingV = true;
                
                if (currentSpeedH == 1f || currentSpeedH == -1f) {
                    currentSpeedV = VerticalMovement;
                } else if (grounded) {
                    if (VerticalMovement > 0) {
                        VerticalDirection = true;
                        if (currentSpeedV < 1f) {
                            currentSpeedV += Time.deltaTime * acceleration;
                        } else if (currentSpeedV > 1f) {
                            currentSpeedV = 1f;
                        }
                    } else {
                        VerticalDirection = false;
                        if (currentSpeedV > -1f) {
                            currentSpeedV -= Time.deltaTime * acceleration;
                        } else if (currentSpeedV < -1f) {
                            currentSpeedV = -1f;
                        }
                    }
                }
            } else{
                currentSpeedV = 0f;
                movingV = false;
            }
        } else {
                if (!startedJumping) {
                    currentDir = rb.rotation;
                    Vector3 currentVel = rb.velocity;
                    startedJumping = true;
                    point.rotation = transform.rotation;
                }   

                if (VerticalMovement > 0f && currentSpeedV < 0f) {
                    verticalDecel = true;
                    VerticalMovementTemp = true;
                } else if (VerticalMovement < 0f && currentSpeedV > 0f) {
                    verticalDecel = true;
                    VerticalMovementTemp = false;
                }

        }
        
        
        if (verticalDecel) {
            if (VerticalMovementTemp) {
                if (currentSpeedV < 0f) {
                    currentSpeedV += Time.deltaTime * acceleration *1.3f;
                } else if (currentSpeedV > 0f) {
                    currentSpeedV = 0f;
                    verticalDecel = false;
                }
            } else {
                if (currentSpeedV > 0f) {
                    currentSpeedV -= Time.deltaTime * acceleration * 1.3f;
                } else if (currentSpeedV < 0f) {
                    currentSpeedV = 0f;
                    verticalDecel = false;

                }
            }
        }

        //Making character move
        
        Vector3 vel = rb.velocity;

        if (movingV && movingH) {
            
            float speedTempV = Mathf.Sqrt((Mathf.Abs(currentSpeedV) * Mathf.Abs(currentSpeedV))/2); //Idfk wat this does tbh
            float speedTempH = Mathf.Sqrt((Mathf.Abs(currentSpeedH) * Mathf.Abs(currentSpeedH))/2); //It just worked


            if (currentSpeedH > 0) {
                if (currentSpeedV > 0) {
                    rb.velocity = (transform.forward * speedTempV * 10f + transform.right * speedTempH * 10) * speed;
                } else {
                    rb.velocity = (transform.forward * -speedTempV * 10f + transform.right * speedTempH * 10f) * speed;
                }
            } else {
                if (currentSpeedV > 0) {
                    rb.velocity = (transform.forward * speedTempV * 10f + transform.right * speedTempH * -10f) * speed;
                } else {
                    rb.velocity = (transform.forward * speedTempV * -10f + transform.right * speedTempH * -10f) * speed;
                }
            }
        } else if (grounded) {
            rb.velocity = (transform.forward * currentSpeedV * 10f + transform.right * currentSpeedH * 10f) * speed;
            
        } else {
            rb.velocity = (point.transform.forward * currentSpeedV * 10f + transform.right * currentSpeedH * 10f) * speed;
        }




        Vector3 vel2 = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (startJump) {
            vel.y += jumpForce;
            rb.velocity = new Vector3(vel2.x, vel.y, vel2.z);
            startJump = false;
        } else {
            rb.velocity = new Vector3(vel2.x, vel.y, vel2.z);
        }

        





        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) && grounded) {
            startJump = true;
            
        }



        velocityText.text = (Mathf.Sqrt(Mathf.Abs(rb.velocity.x * rb.velocity.x) + Mathf.Abs(rb.velocity.z * rb.velocity.z)).ToString());

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

