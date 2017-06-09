using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Assets.Scripts;
public class Villager : NetworkBehaviour {
    [SyncVar]
    public int Health;

    [SyncVar]
    public string Role;

    [SyncVar]
    public int HouseNumber = 0;
    //0 is Village

    public VillagerManager villageManager;

    public Text ChatText;

	// Use this for initialization
	void Start () {
        Health = 2;
        ChatText = GameObject.Find("ChatText").GetComponent<Text>();
    }


    public string checkStatus() {
        if (Health <= 0)
        {
            Network.Destroy(gameObject);
            ChatText.text += "You have died! \n";
            return "Dead";
        }else if(Health == 1)
        {
            return "Injured";
        }else
        {
            return "Healthy";
        }
    }
    
    [Command]
    public void CmdSetAsWerewolf()
    {
        RpcSetRole("Werewolf");
        TargetDeclareRole(connectionToClient, "Werewolf");
    }
    
    [Command]
    public void CmdSetAsHuman()
    {
        RpcSetRole("Human");
        TargetDeclareRole(connectionToClient, "Human");
    }

    [ClientRpc]
    public void RpcSetRole(string role)
    {
        Role = role;
    }

    [TargetRpc]
    public void TargetDeclareRole(NetworkConnection target, string role)
    {
        ChatText.text += "You are a " + Role + "! \n";
    }

    [TargetRpc]
    public void TargetDeclareLocation(NetworkConnection target, string location)
    {
        ChatText.text += "You are in " + location + "! \n";
    }

    [Command]
    public void CmdSetLocation(string location)
    {
        TargetDeclareLocation(connectionToClient, location);
    }
    
}
