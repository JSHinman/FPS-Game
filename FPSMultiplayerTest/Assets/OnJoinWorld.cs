using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode.Transports.UTP;
using Unity.Netcode;

public class OnJoinWorld : MonoBehaviour
{

    [SerializeField] private joinRelay script;
    // Start is called before the first frame update
    void Awake()
    {
       script.JoinRelay(PlayerPrefs.GetString("ip"));
    }

  
}
