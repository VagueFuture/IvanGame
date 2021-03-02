using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui_Settings : MonoBehaviour
{
    public static Ui_Settings instance;
    public Text ammo_Text;
    public Image helthBar;
    public Text LDInfo;

    private void Awake() {
        if(instance = null)
            instance = this;
    }
    public void SetHp(float hp){
        helthBar.fillAmount = (hp/100);
    }

    public void SetAmmo(int ammo){
        ammo_Text.text = "Ammo: "+ ammo;
    }

    public void LogKD(string info){
        LDInfo.text = info;
    }
}
