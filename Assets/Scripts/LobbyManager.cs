using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private bool connectedToMaster;
    public Text logtext,nickname;
    void Start()
    {
        
    }

    public void ConnectToMaster(){
        string nickPlayer = nickname.text;
        
        if(nickPlayer == "")
            nickPlayer = "Player" + Random.Range(1000,9999);
        
        PhotonNetwork.NickName = nickPlayer;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
    }
    
    public override void OnConnectedToMaster(){
        connectedToMaster = true;
       Log("Connected to master");
    }
  
    public void CreteRoom(){
        if(connectedToMaster)
            PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions {MaxPlayers = 4});
        else
            Log("Wait connected to master");
    }

    public void joinRoom(){
        if(connectedToMaster)
            PhotonNetwork.JoinRandomRoom();
        else
            Log("Wait connected to master");
    }

    public override void OnJoinedRoom(){
        Log("Joined the room");

        PhotonNetwork.LoadLevel("Lvl1");
    }

    void Update()
    {
        
    }

    public void Log(string txt){
    logtext.text += "\n";
    logtext.text += txt;
    }
}
