using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using UnityEngine.UI;
public class MenuScript : MonoBehaviour
{
    public Text iptext;
    // Start is called before the first frame update
    void Start()
    {
        IPHostEntry host;
        string localIP = "0.0.0.0";
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                 }
            }
        iptext.text = localIP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
