using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using UnityEngine;

public class GameTurn : MonoBehaviour {

    private int turn = 0;
    public GameObject P1deck;
    public GameObject P2deck;
    public GameObject P1cards;
    public GameObject P2cards;
    public GameObject allcards;
    // Use this for initialization
    void Start () {
        System.Random rand = new System.Random();
        List<Deckjson> deckp1 = JsonConvert.DeserializeObject<List<Deckjson>>(File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\openstone\deck.json"));

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



        foreach (var i in Enumerable.Range(0, 3))
        {
            try
            {
                P1deck.transform.GetChild(rand.Next(0, P1deck.transform.childCount)).transform.SetParent(P1cards.transform);
            }
            catch{}

        }
        foreach (var i in Enumerable.Range(0, 3))
        {
            try
            {
                Transform p2c = P2deck.transform.GetChild(rand.Next(0, P2deck.transform.childCount));
                p2c.SetParent(P2cards.transform);
               // p2c.rotation.eulerAngles.Set(0,0,360);
            }
            catch{}
        }
    }
}
