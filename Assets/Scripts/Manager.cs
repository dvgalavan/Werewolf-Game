using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Networking.Match;
using UnityEngine.SceneManagement;
public class Manager : MonoBehaviour {

    private NetworkManager NetManager;
    public bool Host;
    public Text InfoText;
    public Text InputText;
  

	// Use this for initialization
	void Start () {
        NetManager = gameObject.GetComponent<NetworkManager>();
		
	}
	

    public void SearchForMatch()
    {
        PlayerPrefs.SetString("Username", InputText.text);

        InfoText.text = "Searching";
        NetManager.StartMatchMaker();
        NetManager.matchMaker.ListMatches(0, 20, "", true, 0, 0, MatchReturn);
    }

    void MatchReturn(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {
        NetManager.matches = matches;

        if(NetManager.matches.Count == 0)
        {
            InfoText.text = "Creating Match";
            //gotta replace "match" with other names if more matches
            NetManager.matchMaker.CreateMatch("match", 2, true, "","","",0,0, MatchCreated);

        }else
        {
            foreach(var Match in NetManager.matches)
            {
                InfoText.text = "Joining Match";
                NetManager.matchName = Match.name;
                NetManager.matchSize = (uint)Match.currentSize;
                NetManager.matchMaker.JoinMatch(Match.networkId, "", "", "", 0,0,MatchJoined);
            }
        }
    } 

    public void MatchCreated(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        //9000 is the port
        NetworkServer.Listen(matchInfo, 9000);
        NetworkManager.singleton.StartHost(matchInfo);
        Host = true;
    }

    public void MatchJoined(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        NetworkManager.singleton.StartClient(matchInfo);
        Host = false;
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene("CreateServer");
    }

}
