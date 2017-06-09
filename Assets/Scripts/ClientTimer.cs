using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using modules;

public class ClientTimer : NetworkBehaviour {


    public Text timerText;
    public Text stateText;
    public Text nextStateText;


    [SyncVar]
    public double value;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
    
	void Update () {
        timerText.text = value.ToString();

    }

    public string ChangeStates() {
        if (stateText.text == "Daytime:")
            {
                stateText.text = "Nighttime:";
                nextStateText.text = "Sunrise:";
                return "Nighttime";
            }else
            {
                stateText.text = "Daytime:";
                nextStateText.text = "Sunset:";
                return "Daytime";
            }
        }
}
