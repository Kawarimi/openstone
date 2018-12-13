using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManaBarScript : MonoBehaviour {
    public int mana;
    public int maxMana;
    public TextMeshProUGUI manabarText;
    
	// Use this for initialization
	void Start () {
        mana = 1;
        maxMana = 1;
        SetManaBar();
	}

    // Update is called once per frame
    void NextTurn()
    {
        if(maxMana <= 10) {
            maxMana++;
        }
        mana = maxMana;

        SetManaBar();
    }
    void UseMana(int manaused){
        mana -= manaused;
        SetManaBar();
    }
    void SetManaBar() {
        manabarText.text = mana + "/" + maxMana;
    }
}
