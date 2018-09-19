using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public Text nick;



    private void Start()
    {
        nick.text = "Hello, " + Fabio.user.nick + "! :3";
    }
}
