using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using SFB;
using System;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;

public class UIFunctions : MonoBehaviour
{
    // Use this for initialization
    string defpath;
    int amountindeck;

    void Start()
    {
        //defpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\openstone";
        //Directory.CreateDirectory(defpath);
        //if(File.Exists(defpath + @"\normalloc.txt"))
        //{
        //	defpath = File.ReadAllText(defpath + @"\normalloc.txt");
        //}
        //print(defpath);

    }
    private string _path;
    void GoToGithub()
    {
        Process.Start("https://github.com/Kawarimi/openstone");
    }
    void ChangeToMatch()
    {
        SceneManager.LoadScene("Game");
    }
    void ChangeToDecks()
    {
        SceneManager.LoadScene("DeckManager");
    }
    void ChangeToSettings()
    {
        SceneManager.LoadScene("Settings");
    }
    void ImportCardsDeckManager()
    {
        WriteResult(StandaloneFileBrowser.OpenFolderPanel("Select Pack Folder", "", false));
        string infojson = File.ReadAllText(_path + @"\packinfo.json");
        string images = @"\images\";
        string cardjson = File.ReadAllText(_path + @"\packs\cardpack.json");
        List<Cardjson> cards = JsonConvert.DeserializeObject<List<Cardjson>>(cardjson);
        PackInfo packinfo = JsonConvert.DeserializeObject<PackInfo>(infojson);
        var cardarray = cards.ToArray();
        TextMeshProUGUI infotext = GameObject.Find("Info").GetComponent<TextMeshProUGUI>();
        infotext.text = packinfo.name + "\n" + packinfo.desc + "\n v" + packinfo.version;
        GameObject excard = GameObject.Find("Card");

        //var packbundle = UnityWebRequestAssetBundle.GetAssetBundle("file:///" + _path);
        //UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle("file:///" + _path, 0);
        //yield return request.SendWebRequest();
        //AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
        int i = 0;
        foreach (Cardjson element in cardarray)
        {
            Instantiate(excard, GameObject.Find("CardExplorerContent").transform);
            //excard.name = ("Card " + i);
            GameObject.Find("CardExplorerContent").transform.GetChild(i).SendMessage("GetID", i);
            cardarray[i].img = _path + images + cardarray[i].img;
            //cardarray[i].artworkimg = bundle.LoadAsset<Sprite>(cardarray[i].img);
            print(cardarray[i].img);
            GameObject.Find("CardExplorerContent").transform.GetChild(i).SendMessage("RecieveStats", cards);
            print(cardarray[i].name);
            i++;
        }
        Destroy(GameObject.Find("Card"));
    }
    void DefineSaveLocation()
    {
        //WriteResult(StandaloneFileBrowser.OpenFolderPanel("Select Save Folder", "", false));

        //try{File.Delete(defpath + @"\normalloc.txt");}
        //catch(Exception ex){print(ex);}

        //using (StreamWriter sw = File.CreateText(defpath + @"\normalloc.txt")){sw.Write(_path);}
        //defpath = File.ReadAllText(defpath + @"\normalloc.txt");
        //print(defpath);
    }
    public void WriteResult(string[] paths)
    {
        if (paths.Length == 0)
        {
            return;
        }

        _path = "";
        foreach (var p in paths)
        {
            _path += p;
        }
        print(_path);
    }
    void SetDeckCardText(object[] deckReqs)
    {
        GetComponent<TextMeshProUGUI>().text = deckReqs[0] as string;
        gameObject.name = deckReqs[0] as string;
        amountindeck++;
        if (amountindeck >= (Int32)deckReqs[1])
        {
            Destroy(gameObject);
        }
    }
}
public class Cardjson
{

    [JsonProperty("atk")]
    public int atk { get; set; }
    [JsonProperty("hp")]
    public int hp { get; set; }
    [JsonProperty("name")]
    public string name { get; set; }
    [JsonProperty("description")]
    public string description { get; set; }
    [JsonProperty("img")]
    public string img { get; set; }
    [JsonProperty("manacost")]
    public int manacost { get; set; }
    [JsonProperty("maxindeck")]
    public int maxindeck { get; set; }

    public Sprite artworkimg { get; set; }
}
public class Deckjson
{
    [JsonProperty("name")]
    public string name { get; set; }
}
public class PackInfo
{
    public string name { get; set; }
    public string desc { get; set; }
    public string version { get; set; }
    public string scripts { get; set; }
}