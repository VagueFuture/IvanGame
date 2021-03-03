using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonObservInfo : MonoBehaviour, IPunObservable
{
    PhotonView photonView;
    Helth_settings helth_Settings;
    Weapons weapons;
    CharacterController characterController;
    Character character;
    HandHits handHits;
    Vector3 newPosition;
    float distance;
    private void Awake() {
        characterController = GetComponent<CharacterController>();
        helth_Settings = GetComponent<Helth_settings>();
        weapons = GetComponent<Weapons>();
        photonView = GetComponent<PhotonView>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
     {
         if(stream.IsWriting)
         {
            stream.SendNext(characterController.shoot);
            stream.SendNext(helth_Settings.helth);
            stream.SendNext(weapons.nowActive);
            //Send moving
            stream.SendNext(this.gameObject.transform.position.x);
            stream.SendNext(this.gameObject.transform.position.y);
            stream.SendNext(this.gameObject.transform.localScale.x);
         }
         else
         {
            characterController.shoot = (bool) stream.ReceiveNext();
            helth_Settings.helth = (float) stream.ReceiveNext();
            weapons.picupWeapon((int) stream.ReceiveNext(),0);
            
            //Get moving
            
            float x = (float) stream.ReceiveNext();
            float y = (float) stream.ReceiveNext();
            float scaleX = (float) stream.ReceiveNext();
            newPosition = new Vector3(x,y,0);
            distance = Vector3.Distance(transform.position,newPosition);
            this.gameObject.transform.localScale = new Vector2(scaleX,transform.localScale.y);
         }
     }

     private void FixedUpdate() {
         if (!this.photonView.IsMine)
            {
                transform.position = Vector3.MoveTowards(transform.position, newPosition, distance * (1.5f/ PhotonNetwork.SerializationRate));
            }
     }
}
