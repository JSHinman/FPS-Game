using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private Transform ak47;
    public float moveSpeed;
    public float sensitivity;
    public GameObject PlayerCam;
    Vector2 rotation = Vector2.zero;
    [SerializeField] private float gunOffsetHeight;
    Transform spawnedAK;
    // Start is called before the first frame update
    void Start()
    {   
        if (!IsOwner) return;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        spawnedAK = Instantiate(ak47);
        spawnedAK.GetComponent<NetworkObject>().Spawn(true);
    }

    // Update is called once per frame
    void Update()   
    {
        if (!IsOwner) return;
        
        spawnedAK.position = new Vector3(transform.position.x+ 0.2f, transform.position.y + gunOffsetHeight, transform.position.z+0.2f);
        spawnedAK.rotation = PlayerCam.transform.rotation * Quaternion.Euler(0,0,0);

        // Player movement
        Vector3 vel = new Vector3();
        if (Input.GetAxisRaw("Horizontal") != 0) {
            vel.x = moveSpeed * Input.GetAxisRaw("Horizontal");
        }  

        if (Input.GetAxisRaw("Vertical") != 0) {
            if(Input.GetAxisRaw("Horizontal") == 0) {
                vel.z = moveSpeed * Input.GetAxisRaw("Vertical");
            } else {
                vel = new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * 0.7f, 0, Input.GetAxisRaw("Vertical") * moveSpeed * 0.7f); 
            }
        }
        
        transform.position += vel.x * Time.deltaTime * PlayerCam.transform.right;
        transform.position += vel.z * Time.deltaTime * transform.forward;

        //Camera Movement   
        rotation.x += Input.GetAxis("Mouse X") * sensitivity; //Gets input from X axis of mouse
		rotation.y += Input.GetAxis("Mouse Y") * sensitivity; // Gets input from Y axis of mouse
        rotation.y = Mathf.Clamp(rotation.y, -65f, 65f); //Clamps vertical mouse movement

        var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
		var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);
        transform.localRotation = xQuat;
		PlayerCam.transform.localRotation = yQuat;

       
    }
}
