using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CameraNetword : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (IsOwner) return;
        gameObject.SetActive(false);
    }
}
