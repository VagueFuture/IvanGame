using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class ShootWeapon : MonoBehaviour
{
    public GameObject bullet;
    public List<Transform> barrels = new List<Transform>();
    [Range(0, 200)]
    public float shootSpeed;
    [Range(0, 20)]
    public float shootspeedDelay;
    public bool riful,shootgun;
    public int ammoCount;
    public float dmg = 0;
    public PhotonView photonView;
    public bool network = true;

    public void Shoot()
    {
        if(!photonView.IsMine)
            return;
        if(Ui_Settings.instance != null)
            Ui_Settings.instance.SetAmmo(ammoCount);
        if(ammoCount>0){
            photonView.RPC("Fire",RpcTarget.AllViaServer);
            //Fire();
            }
    }

    public void StopShoot(){
    }

    public void Fire(){
        int xVelocity = 1;
        if(transform.lossyScale.x < 0)
            xVelocity = -1;
        else
            xVelocity = 1;

        foreach(Transform barrel in barrels){
            GameObject flyBullet;
            //if(!network)
                flyBullet = Instantiate(bullet,barrel.position, Quaternion.identity) as GameObject;
           // else
            //    flyBullet = PhotonNetwork.Instantiate(bullet.name,barrel.position,Quaternion.identity);  
            flyBullet.GetComponent<Bullet>().damage = dmg;
            flyBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity*shootSpeed, barrel.localRotation.z*20);
            }
        ammoCount--;
    }
    
}
