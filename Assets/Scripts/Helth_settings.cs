using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Helth_settings : MonoBehaviour
{
    public float helth = 100;
    public bool dead = false;
    public float respawnDelay;
    public PhotonView photonView;
    private float nowHp;

    public string lasthitMePlayer = "";


    private void Update() {
        if(nowHp != helth)
        { 
            nowHp = helth;
            if(helth<=0)
                photonView.RPC("Dead",RpcTarget.AllViaServer);
                //Dead();
        }
        
    }

    public void Dead(){
        dead = true;
        GetComponent<Animator>().SetBool("Dead",true);
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn(){
        yield return new WaitForSeconds(respawnDelay);
        Iamalive();
    }

    public void Iamalive(){
        dead = false;
        helth = 100;
        transform.position = new Vector3(0,0,0);
        GetComponent<Animator>().SetBool("Dead",false);
    }

    public void heling(){
        if(helth < 100){
            helth += 10;
        }
    }
}
