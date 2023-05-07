using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;


public class OnHostWorld : MonoBehaviour
{

    [SerializeField] private TestRelay script;
    // Start is called before the first frame update
    void Awake()
    {
       script.CreateRelay();
    }

    
}
