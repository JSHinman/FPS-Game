using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JoinHost : MonoBehaviour
{

    [SerializeField] private Text ipAddress;
    

    public void grabIP() {
        PlayerPrefs.SetString("ip", ipAddress.text);
    }


}
