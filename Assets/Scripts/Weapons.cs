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
    private CharacterController characterController;
    public int nowActive = 10;
    private void Awake() {
        myanim = GetComponent<Animator>();
        character = GetComponent<Character>();
        characterController = GetComponent<CharacterController>();
        disurm();
    }
    public void picupWeapon(int number,int ammoCount){
        disurm();
        nowActive = number;
        if(number == 10){
            if(myanim!= null)
                myanim.SetBool("PicupWeapon",false);
            character.shootweapons = null;
        }else
        {
            weapons[number].SetActive(true);
            if(myanim!= null){
                myanim.SetBool("PicupWeapon",true);
                myanim.SetFloat("WeaponAnims",number);
            }
            character.shootweapons = weapons[number].GetComponent<ShootWeapon>();
            character.shootweapons.ammoCount = ammoCount;
            character.shootweapons.photonView = characterController.photonView;
        }
    }

    public void disurm(){
        foreach(GameObject g in weapons)
            g.SetActive(false);
        if(myanim != null)
            myanim.SetBool("PicupWeapon",false);
        milk.SetActive(false);
        nowActive = 10;
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
        if(nowActive != 10)
            return weapons[nowActive].GetComponent<ShootWeapon>().ammoCount;
        else
            return 0;
    }

    public void DrinkMilk(){
        if(countMilk>0)
        {
            countMilk--;
            character.myanim.SetBool("Heal",true);
        }
    }
}
