using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowGameCode : MonoBehaviour
{
    [SerializeField] private Text text;
    void Update() {
        text.text = PlayerPrefs.GetString("joinCode");
    }
}
