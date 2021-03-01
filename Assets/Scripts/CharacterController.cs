using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterController : MonoBehaviour
{
    // Start is called before the first frame update
    private Character character;

    public PhotonView photonView;
    private Helth_settings helth_settings;
    private Ui_Settings ui_Settings;
    private HandHits handHits;
    public Weapons weapons;
    public bool shoot, handattacs;
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        character = GetComponent<Character>();
        helth_settings = GetComponent<Helth_settings>();
        helth_settings.photonView = photonView;
        weapons = GetComponent<Weapons>();
        handHits = GetComponent<HandHits>();
        ui_Settings = GameObject.Find("Ui_settings").GetComponent<Ui_Settings>();
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
        private void Update() {
        if(!photonView.IsMine) return;

        ui_Settings.SetHp(helth_settings.helth);
        ui_Settings.SetAmmo(weapons.GetAmmoCount());

        if(!helth_settings.dead){
            float direction = Input.GetAxis("Horizontal");
        
            if(direction>0){
                character.OnDoAction(1);
            //Debug.Log("Press D");
            }
            if(direction<0){
                character.OnDoAction(2);     
            //Debug.Log("Press A");
            }
            if(direction == 0){
                character.OnDoAction(0);
            }

            if(Input.GetAxis("Jump") == 1){
                character.OnDoAction(3);
            }

            if(Input.GetAxis("Run") > 0){
                character.buffSpeed =+ 2*Input.GetAxis("Run");
            }else{
                character.buffSpeed = 0;
            }

            if(Input.GetAxis("Fire2") == 1){
                weapons.DrinkMilk();
            }
            if(character.shootweapons != null)
                if(Input.GetAxis("Fire1") == 1){
                    shoot = true;
                }else{
                    shoot = false;
                }
            
            if(Input.GetAxis("HandAttacs") == 1){
                    if(!handattacs){
                        handHits.HitHand();
                        handattacs = true;
                    }
                }else{
                    handattacs = false;
                    handHits.EndHit();
                }
        }
    }

    IEnumerator Shoot(){
        while(true){
            if(shoot){
                character.shootweapons.Shoot();
                yield return new WaitForSeconds(character.shootweapons.shootspeedDelay);
            }else{
                yield return null;
            }
        }
    }

    [PunRPC]
    public void Fire(){
        character.shootweapons.Fire();
    }

    [PunRPC]
    public void Dead(){
        helth_settings.Dead();
    }
}
