using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SFB;
using UnityEngine;

public class StartupDeckGet : MonoBehaviour {

    public GameObject samplecard;
    private int playerID = 1; //When multiplayer use join player id
    // Use this for initialization
	void Start () {
        string deckjsonpath = StandaloneFileBrowser.OpenFilePanel("Select Deck Json","","json",false)[0];
        string cardjsonpath = StandaloneFileBrowser.OpenFilePanel("Select Card Pack", "", "json", false)[0];
        List<Deckjson> selecteddeck = JsonConvert.DeserializeObject<List<Deckjson>>(File.ReadAllText(deckjsonpath));
        List<Cardjson> selectedpack = JsonConvert.DeserializeObject<List<Cardjson>>(File.ReadAllText(cardjsonpath));
        object[] DirandIndex = new object[1];
        DirandIndex[0] = selectedpack; 
        for(int i = 0; selecteddeck.Count > i; i++)
        {
            DirandIndex[1] = selectedpack[i].name.IndexOf(selecteddeck[i].name);
            Instantiate(samplecard, GameObject.Find("DeckP" + playerID).transform).SendMessage("RecieveStatsII", DirandIndex);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
