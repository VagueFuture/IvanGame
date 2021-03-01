using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public List<GameObject> weapons = new List<GameObject>();
    public GameObject milk;
    public int countMilk;
    private bool milkFlag;
    private Animator myanim;
    private Character character;
    public int nowActive;
    private void Awake() {
        myanim = GetComponent<Animator>();
        character = GetComponent<Character>();
        disurm();
    }
    public void picupWeapon(int number,int ammoCount){
        disurm();
        nowActive = number;
        weapons[number].SetActive(true);
        if(myanim!= null)
        myanim.SetBool("PicupWeapon",true);
        character.shootweapons = weapons[number].GetComponent<ShootWeapon>();
        character.shootweapons.ammoCount = ammoCount;
    }

    public void disurm(){
        foreach(GameObject g in weapons)
            g.SetActive(false);
        if(myanim != null)
            myanim.SetBool("PicupWeapon",false);
        milk.SetActive(false);
        nowActive = 0;
    }

    public void ShowMilk(){
        milkFlag = !milkFlag;
        milk.SetActive(milkFlag);
        myanim.SetBool("Heal",false);
    }

    public ShootWeapon GetNowShhotWeapon(){
        return weapons[nowActive].GetComponent<ShootWeapon>();
    }

    public int GetAmmoCount(){
        return weapons[nowActive].GetComponent<ShootWeapon>().ammoCount;
    }

    public void DrinkMilk(){
        if(countMilk>0)
        {
            countMilk--;
            character.myanim.SetBool("Heal",true);
        }
    }
}
