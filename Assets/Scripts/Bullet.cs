using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour
{
    public float liveTime;
    public float damage = 10;
    public float timer = 0;
    public string owner;
   private void Awake() {
       StartCoroutine(HideBulet());
   }

   IEnumerator HideBulet(){
        yield return new WaitForSeconds(liveTime);
        Object.Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Helth_settings helth = other.GetComponent<Helth_settings>();
        if(helth!=null){
            helth.helth -= damage;
            Object.Destroy(this.gameObject);
        } 
        
    }

}
