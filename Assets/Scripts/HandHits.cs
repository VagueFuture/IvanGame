using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandHits : MonoBehaviour
{
    private Animator myanim;
    public BoxCollider2D hitCollider;
    public int handDamage;
    public List<Helth_settings> enemysHelth = new List<Helth_settings>();
    bool attack;
    private void Awake() {
        myanim = GetComponent<Animator>();
    }

    public void HitHand(){
        foreach(Helth_settings h in enemysHelth){
            h.helth -= handDamage;
        }
        attack = true;
        myanim.SetBool("HandHit",true);
    }

    public void EndHit(){
        myanim.SetBool("HandHit",false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Helth_settings helth =  other.GetComponent<Helth_settings>();
        HandHits hand = other.GetComponent<HandHits>(); 
        if(hand != this)
            if(helth!=null)
                enemysHelth.Add(helth);
    }

    private void OnTriggerExit2D(Collider2D other) {
        Helth_settings helth =  other.GetComponent<Helth_settings>();
        HandHits hand = other.GetComponent<HandHits>(); 
        if(hand != this)
            if(helth!=null)
                enemysHelth.Remove(helth);
    }
}
