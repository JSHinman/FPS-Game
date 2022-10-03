using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGun : MonoBehaviour
{
    public Transform bulletStartPos;
    public LayerMask layerMask;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(bulletStartPos.position, transform.TransformDirection(Vector3.back), out hit, Mathf.Infinity, layerMask))
            {
                if (hit.transform.name == "Cube") {
                    Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                    rb.AddForce(transform.TransformDirection(Vector3.back), ForceMode.Impulse);
                }
            }
            else
            {
               // Debug.DrawRay(bulletStartPos.position, transform.TransformDirection(Vector3.back) * 10000f, Color.white);
                //Debug.Log("Did not Hit");
            }
        }
    }
}
