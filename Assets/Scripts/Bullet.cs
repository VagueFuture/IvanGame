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

    /*private void OnTriggerEnter2D(Collider2D other) {
        Hit(other);
    }
*/
    private void FixedUpdate() {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 0.2f);
        if (hit){
            Hit(hit.collider);
        }

    }

    public void Hit(Collider2D other){

        Helth_settings helth = other.GetComponent<Helth_settings>();
        if(helth!=null){
            helth.helth -= damage;
            helth.lasthitMePlayer = owner;
        } 
        Object.Destroy(this.gameObject);

    }

}
