using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using UnityEngine;

public class GameTurn : MonoBehaviour {
    //true p1
    public static bool turn = true;
    public GameObject P1deck;
    public GameObject P2deck;
    public GameObject P1cards;
    public GameObject P2cards;
    public GameObject P1hero;
    public GameObject P2hero;
    public GameObject allcards;
    public GameObject P1mana;
    public GameObject P2mana;

    // Use this for initialization
    void Start () {
        System.Random rand = new System.Random();
        List<Deckjson> deckp1 = JsonConvert.DeserializeObject<List<Deckjson>>(File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\openstone\deck.json"));

        //init deck for p1
        for (int i = 0; i < deckp1.Count; i++)
        {
            for(int t = 0; t < allcards.transform.childCount; t++)
            {
                if(deckp1[i].name == allcards.transform.GetChild(t).GetComponent<CardGame>().name)
                {
                    Instantiate(allcards.transform.GetChild(t),P1deck.transform);
                }
            }
        }
        //init deck for p2 (Bot)
        for (int i = 0; i < deckp1.Count; i++)
        {
            for (int t = 0; t < allcards.transform.childCount; t++)
            {
                if (deckp1[i].name == allcards.transform.GetChild(t).GetComponent<CardGame>().name)
                {
                    Instantiate(allcards.transform.GetChild(t), P2deck.transform);
                }
            }
        }


        //init p1 hand
        foreach (var i in Enumerable.Range(0, 3))
        {
            try
            {
                Transform a = P1deck.transform.GetChild(rand.Next(0, P1deck.transform.childCount));
                a.SetParent(P1cards.transform);
                a.localPosition = new Vector3(a.localPosition.x,a.localPosition.y,-10);
                a.tag = "targetable";
            }
            catch{}

        }
        //init p2 hand
        foreach (var i in Enumerable.Range(0, 3))
        {
            try
            {
                Transform a = P2deck.transform.GetChild(rand.Next(0, P2deck.transform.childCount));
                a.SetParent(P2cards.transform);
                a.localPosition = new Vector3(a.localPosition.x, a.localPosition.y, -10);
                a.tag = "targetablep2";
            }
            catch{}
        }
    }
    void EndTurn()
    {
        turn = !turn;
        //do p1
        if (turn)
        {
            //update mana
            P1mana.SendMessage("NextTurn");
            //send card
            if (!(P1deck.transform.childCount == 0))
            {
                Transform a = P1deck.transform.GetChild(new System.Random().Next(0, P1deck.transform.childCount));
                a.SetParent(P1cards.transform);
                a.localPosition = new Vector3(a.localPosition.x, a.localPosition.y, -10);
                a.tag = "targetablep2";
            }
            //atirron damage
            else
            {
                P1hero.SendMessage("GotDamaged",1);
            }
        }
        //do p2
        if(!turn)
        {
            //update mana
            P2mana.SendMessage("NextTurn");
            //send card
            if (!(P2deck.transform.childCount == 0))
            {
                Transform a = P2deck.transform.GetChild(new System.Random().Next(0, P2deck.transform.childCount));
                a.SetParent(P2cards.transform);
                a.localPosition = new Vector3(a.localPosition.x, a.localPosition.y, -10);
                a.tag = "targetable";
            }
            //atirron damage
            else
            {
                P2hero.SendMessage("GotDamaged", 1);
            }
        }
    }
}
