using TMPro;
using UnityEngine;

public class HeroScript : MonoBehaviour {
    private int health = 30;
    public TextMeshProUGUI healthText;
  
	// Use this for initialization
	void Start () {
        healthText.text = health.ToString();
	}

    // Update is called once per frame
    void GotDamaged(int dmg)
    {
        if (health <= 0)
        {
            Destroy(gameObject, 0.5f);
        }
        health -= dmg;
        healthText.text = health.ToString();
    }
}
