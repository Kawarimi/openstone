using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManaBarScript : MonoBehaviour {
    public int mana = 1;
    public int maxMana = 1;
    public TextMeshProUGUI manabarText;
    
	// Use this for initialization
	void Start () {
        SetManaBar();
	}

    // Update is called once per frame
    void NextTurn()
    {
        if(!(maxMana <= 10))
        maxMana++;

        mana = maxMana;
        SetManaBar();
    }
    void UseMana(int manaused){
        mana -= manaused;
    }
    void SetManaBar() {
        manabarText.text = mana + "/" + maxMana;
    }
}
