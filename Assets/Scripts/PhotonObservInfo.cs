using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonObservInfo : MonoBehaviour, IPunObservable
{
    Helth_settings helth_Settings;
    Weapons weapons;
    CharacterController characterController;
    Character character;
    HandHits handHits;

    private void Awake() {
        characterController = GetComponent<CharacterController>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
     {
         if(stream.IsWriting)
         {
            stream.SendNext(characterController.shoot);
         }
         else
         {
             characterController.shoot = (bool) stream.ReceiveNext();
         }
     }
}
