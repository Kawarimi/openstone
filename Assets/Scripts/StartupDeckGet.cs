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
    string deckjsonpath = StandaloneFileBrowser.OpenFilePanel("Select Deck Json","",".json",false)[0];
    List<Deckjson> selecteddeck = JsonConvert.DeserializeObject<List<Deckjson>>(File.ReadAllText(deckjsonpath));
    Instantiate(samplecard, GameObject.Find("DeckP" + playerID).transform).SendMessage("RecieveStats",);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
