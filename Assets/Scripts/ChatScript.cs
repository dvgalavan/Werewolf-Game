using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ChatScript : NetworkBehaviour {

    Text ChatText;
    InputField ChatInput;

	// Use this for initialization
	void Start () {
        ChatText = GameObject.Find("ChatText").GetComponent<Text>();
        ChatInput = GameObject.Find("ChatInput").GetComponent<InputField>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer)
            return;
        
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (ChatInput.text != "")
            {
                string Message = ChatInput.text;
                ChatInput.text = "";

                CmdSubmit(Message);
            }
        }
	}

    [Command]
    void CmdSubmit(string message)
    {
        RpcRecieve(message);
    }

    [ClientRpc]
    public void RpcRecieve(string message)
    {
        ChatText.text += ">>" + message + "\n";
    }

}
