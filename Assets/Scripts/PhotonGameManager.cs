using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PhotonGameManager : MonoBehaviourPunCallbacks
{
    public List<GameObject> playersPrefubs = new List<GameObject>();
    public List<Transform> spawnPlayses = new List<Transform>(); 
    public List<Sprite> portrets = new List<Sprite>();
    public List<Sprite> portretsHealth = new List<Sprite>();
    public Image portredPlace;
    public Image headPortredPlace;
    private int choosenPortret;

    void Start()
    {
       //StartCoroutine(wait());
    }

    IEnumerator wait(){
        yield return null;
        Spawn();
    }

    public void chooseFighter(int number){
        portredPlace.GetComponent<Image>().sprite = portrets[number];
        headPortredPlace.sprite = portretsHealth[number];
        choosenPortret = number;
    }

    public void Spawn(){
        int spawn = Random.Range(0,3);
        Vector3 pos = spawnPlayses[spawn].position;
        PhotonNetwork.Instantiate(playersPrefubs[choosenPortret].name,pos,Quaternion.identity); 
    }

    public void Leave(){
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom(){
        SceneManager.LoadScene(0);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer){
        Debug.LogFormat("Player {0} entered room", newPlayer.NickName);
    } 

    public override void OnPlayerLeftRoom(Player otherPlayer){
        Debug.LogFormat("Player {0} left room", otherPlayer.NickName);
    }
}
