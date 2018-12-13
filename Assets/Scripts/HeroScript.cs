using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroScript : MonoBehaviour {
    private int health = 30;
    public TextMeshProUGUI healthText;
    public GameObject canvas;
  
	// Use this for initialization
	void Start () {
        healthText.text = health.ToString();
	}

    // Update is called once per frame
    void GotDamaged(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            if(gameObject.tag == "targetablep2")
            {
                GameObject newText = new GameObject();
                var newTextComp = newText.AddComponent<Text>();
                //newText.AddComponent<CanvasRenderer>();

                //Text newText = transform.gameObject.AddComponent<Text>();
                newTextComp.text = "P1 Wins";
                newTextComp.color = new Color(0,255,0);
                newTextComp.alignment = TextAnchor.MiddleCenter;
                newTextComp.fontSize = 10;

                newText.transform.SetParent(canvas.transform);

            }
            if (gameObject.tag == "targetable")
            {
                GameObject newText = new GameObject();
                var newTextComp = newText.AddComponent<Text>();
                //newText.AddComponent<CanvasRenderer>();

                //Text newText = transform.gameObject.AddComponent<Text>();
                newTextComp.text = "P2 Wins";
                newTextComp.color = new Color(0, 255, 0);
                newTextComp.alignment = TextAnchor.MiddleCenter;
                newTextComp.fontSize = 10;

                newText.transform.SetParent(canvas.transform);

            }
            Destroy(gameObject, 0.5f);
            
        }
        healthText.text = health.ToString();
    }
}
