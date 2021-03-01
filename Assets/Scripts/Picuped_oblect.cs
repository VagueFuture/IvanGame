using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picuped_oblect : MonoBehaviour
{
    // Start is called before the first frame update
    public bool itsWeapon;
    public bool itsHeal;
    public int ammoCount;
    public int number;

    private Vector2 spawnPosition;

    private void Awake() {
        spawnPosition = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Triger item");
        if(itsWeapon){
            Weapons weapon = other.GetComponent<Weapons>();
            if(weapon != null){
                weapon.picupWeapon(number,ammoCount);
                //Object.Destroy(this.gameObject);//Create pool objects
                this.gameObject.transform.position = new Vector3(1000,0,0);
                StartCoroutine(Respawn());
            }
        }
        if(itsHeal){
            Weapons weapon = other.GetComponent<Weapons>();
            if(weapon != null){
                weapon.countMilk = ammoCount;
                this.gameObject.transform.position = new Vector3(1000,0,0);
                StartCoroutine(Respawn());
            }
        }
    }

    IEnumerator Respawn(){
        yield return new WaitForSeconds(5);
        this.gameObject.transform.position = spawnPosition;
    }
}
