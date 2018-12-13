using System;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardGame : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text nameText;
    public Text descriptionText;

    public Image artworkImage;

    public Text manaText;
    public Text attackText;
    public Text healthText;
  
    public new string name;
    public string description;
    public GameObject expansion;

    public Sprite artwork;

    public int manaCost;
    public int attack;
    public int health;
    private bool cardOver;
    private bool cardExpanded;
    private bool cardShrinkable;
    private bool targeting = false;
    private Action letcardShrink;

    // Use this for initialization
    void Start()
    {
        letcardShrink = delegate { cardShrinkable = true; };
        nameText.text = name;
        descriptionText.text = description;

        artworkImage.sprite = artwork;

        manaText.text = manaCost.ToString();
        attackText.text = attack.ToString();
        healthText.text = health.ToString();

    }
    void Update()
    {
        if(targeting && Input.GetMouseButtonDown(0))
        {
            //p1
            if(GameTurn.turn)
            {
                RaycastHit hitInfo = new RaycastHit();
                bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
                if (hit)
                {
                    Debug.Log("Hit " + hitInfo.transform.gameObject.name);
                    if (hitInfo.transform.gameObject.tag == "targetablep2" && GameTurn.turn)
                    {
                        hitInfo.transform.gameObject.SendMessage("GotDamaged",attack);
                        if(hitInfo.transform.gameObject.GetComponent<CardGame>() && GameObject.FindGameObjectWithTag("manabar1").GetComponent<ManaBarScript>().mana < manaCost)
                        {
                            GotDamaged(hitInfo.transform.gameObject.GetComponent<CardGame>().attack);
                            GameTurn.turn = !GameTurn.turn;
                            //direct mana burn, no placement
                            GameObject.FindGameObjectWithTag("manabar1").SendMessage("UseMana", manaCost);
                            
                        }
                        targeting = false;
                        Debug.Log("It's working!");
                    }
                    else
                    {
                        Debug.Log("nopz");
                    }
                }
            }
            //p2
            if(!GameTurn.turn)
            {
                RaycastHit hitInfo = new RaycastHit();
                bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
                if (hit)
                {
                    Debug.Log("Hit " + hitInfo.transform.gameObject.name);
                    if (hitInfo.transform.gameObject.tag == "targetable" && !GameTurn.turn)
                    {
                        hitInfo.transform.gameObject.SendMessage("GotDamaged", attack);
                        if (hitInfo.transform.gameObject.GetComponent<CardGame>() && GameObject.FindGameObjectWithTag("manabar2").GetComponent<ManaBarScript>().mana < manaCost)
                        {
                            GotDamaged(hitInfo.transform.gameObject.GetComponent<CardGame>().attack);
                            GameTurn.turn = !GameTurn.turn;
                            //direct mana burn, no placement
                            GameObject.FindGameObjectWithTag("manabar2").SendMessage("UseMana", manaCost);
                        }
                        Debug.Log("It's working!");
                        targeting = false;
                    }
                    else
                    {
                        Debug.Log("nopz");
                    }
                }
            }
        }



        if (Input.GetKey(KeyCode.Mouse1) && cardExpanded && cardShrinkable)
        {
            try
            {
                Destroy(expansion.transform.GetChild(0).gameObject);
                cardShrinkable = false;
                cardExpanded = false;
            }
            catch
            {
                print("No maximized card");
            }
        }

        if (Input.GetKey(KeyCode.Mouse1) && cardOver && !targeting)
        {

            if (!cardExpanded)
            {
                ExpandCard();
            }

        }
        if (Input.GetKey(KeyCode.Mouse0) && cardOver)
        {
            Targeting();
        }
        if(Input.GetKey(KeyCode.Mouse1) && targeting)
        {
            if (gameObject.tag == "targetablep2")
            {
                foreach (GameObject target in GameObject.FindGameObjectsWithTag("targetable"))
                {
                    if (target.GetComponent<Image>())
                    {
                        target.GetComponent<Image>().color = new Color(255, 255, 255);
                    }
                    if (target.GetComponent<CanvasRenderer>())
                    {
                        target.GetComponent<CanvasRenderer>().SetColor(new Color(255, 255, 255));
                    }
                }
            }
            if (gameObject.tag == "targetable")
            {
                foreach (GameObject target in GameObject.FindGameObjectsWithTag("targetablep2"))
                {
                    if (target.GetComponent<Image>())
                    {
                        target.GetComponent<Image>().color = new Color(255, 255, 255);
                    }
                    if (target.GetComponent<CanvasRenderer>())
                    {
                        target.GetComponent<CanvasRenderer>().SetColor(new Color(255, 255, 255));
                    }
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (expansion.transform.childCount == 0)
        {
            cardOver = true;
        }
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        cardOver = false;
    }
    void ExpandCard()
    {
        cardExpanded = true;
        GameObject expandedcard = Instantiate(gameObject, expansion.transform);
        RectTransform rt = (RectTransform)expandedcard.transform;
        expandedcard.transform.localScale = Vector3.one;
        expandedcard.transform.position = new Vector3((Screen.width / 2) - (rt.rect.width / 2), (Screen.height / 2) - (rt.rect.height / 2), 0);
        Timer.Register(0.2f, letcardShrink);
    }
    void GotDamaged(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Destroy(gameObject, 0.5f);   
        }
        healthText.text = health.ToString();
        healthText.color = new Color(200,0,0);
    }
    void Targeting()
    {
        targeting = true;
        if(gameObject.tag =="targetablep2")
        {
            foreach (GameObject target in GameObject.FindGameObjectsWithTag("targetable"))
            {
                if (target.GetComponent<Image>())
                {
                    target.GetComponent<Image>().color = new Color(0, 255, 0);
                }
                if (target.GetComponent<CanvasRenderer>())
                {
                    target.GetComponent<CanvasRenderer>().SetColor(new Color(0, 255, 0));
                }
            }
        }
        if (gameObject.tag == "targetable")
        {
            foreach (GameObject target in GameObject.FindGameObjectsWithTag("targetablep2"))
            {
                if (target.GetComponent<Image>())
                {
                    target.GetComponent<Image>().color = new Color(0, 255, 0);
                }
                if (target.GetComponent<CanvasRenderer>())
                {
                    target.GetComponent<CanvasRenderer>().SetColor(new Color(0, 255, 0));
                }
            }
        }
    }
}