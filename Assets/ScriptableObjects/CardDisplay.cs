using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityTimer;

public class CardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
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
    public int maxindeck;
    public int cardID;
    public bool cardOver;
    public bool cardExpanded;
    public bool cardShrinkable;
    private Timer expansionTimer;
    private Action letcardShrink;

    // Use this for initialization
    void Start()
    {
        letcardShrink = delegate{cardShrinkable = true;};
        nameText.text = name;
        descriptionText.text = description;

        artworkImage.sprite = artwork;

        manaText.text = manaCost.ToString();
        attackText.text = attack.ToString();
        healthText.text = health.ToString();
        //eventSystem = GetComponent<EventSystem>();
        
    }
    void Update()
    {
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

        if (Input.GetKey(KeyCode.Mouse1) && cardOver)
        {

            if (!cardExpanded)
            {
                ExpandCard();
            }

        }

    }
    void GetID(int cardid)
    {
        cardID = cardid;
    }
    void RecieveStats(List<Cardjson> cardstats)
    {
        Cardjson[] cardstatsarray = cardstats.ToArray();
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(File.ReadAllBytes(cardstatsarray[cardID].img));
        artwork = Sprite.Create(tex, new Rect(0, 0, 128, 128), new Vector2(0f, 0f));

        print(cardstatsarray[cardID].img);
        name = cardstatsarray[cardID].name;
        description = cardstatsarray[cardID].description;

        manaCost = cardstatsarray[cardID].manacost;
        attack = cardstatsarray[cardID].atk;
        health = cardstatsarray[cardID].hp;
        maxindeck = cardstatsarray[cardID].maxindeck;

        nameText.text = name;
        descriptionText.text = description;

        artworkImage.sprite = artwork;

        manaText.text = manaCost.ToString();
        attackText.text = attack.ToString();
        healthText.text = health.ToString();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (expansion.transform.childCount == 0)
        {
            cardOver = true;

        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject.Find("SCT").SendMessage("AddToDeck",name);
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
        expansionTimer = Timer.Register(0.2f, letcardShrink);
    }
}