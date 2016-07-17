using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleJSON;
using System;
using System.Collections;
using System.Threading;


public class PopulateCache : MonoBehaviour {

    private uint callCount;
    private Cache cache;
    private Mutex mutex;

    private const uint ExpectedCalls = 2;

    private void Awake() {
        this.callCount = 0;
        this.cache = Utility.LoadObject<Cache>("Cache");
        this.mutex = new Mutex();
    }

    public void Populate() {
        StartCoroutine(PopulateFitbitData());
        StartCoroutine(PopulateItems());
        StartCoroutine(LoadHomeScreen());
    }

    /**
     *  Populate the cache with all our fitbit data. Start with this, than
     *  move onto items.
     */
    private IEnumerator PopulateFitbitData() {
        WWWForm form = new WWWForm();
        form.AddField(Config.SessionKey, this.cache.sessionKey);
        WWW www = new WWW(Config.ControllerURLAPI, form);

        yield return www;

        var data = JSON.Parse(www.text);
        int response = data["status"].AsInt;
        if(response == 200) {
            FillCacheWithFitbitData(data["data"]);
            this.mutex.WaitOne();
            this.callCount++;
            this.mutex.ReleaseMutex();
            Debug.Log(this.callCount);
        } else {
            throw new Exception("FATAL: Fitbit data could not be obtained.");
        }
    }

    private void FillCacheWithFitbitData (JSONNode data) {

    }

    private IEnumerator PopulateItems () {
        WWWForm form = new WWWForm();
        form.AddField(Config.SessionKey, this.cache.sessionKey);
        WWW www = new WWW(Config.ControllerURLItems, form);

        yield return www;

        var data = JSON.Parse(www.text);
        int response = data["status"].AsInt;

        if (response == 200) {
            FillCacheWithItems(data["data"]);
            this.mutex.WaitOne();
            this.callCount++;
            this.mutex.ReleaseMutex();
            Debug.Log(this.callCount);
        } else {
            throw new Exception("FATAL: Item data could not be obtained.");
        }
    }

    private void FillCacheWithItems(JSONNode data) {
        JSONArray items = data["items"].AsArray;
        foreach (JSONNode item in items) {
            this.cache.AddItemToInventory(CreateItemFromJSON(item));
        }
    }

    /**
 *  Converts our JSON node to an item.
 */
    private Item CreateItemFromJSON(JSONNode item) {
        return new Item(
            item["name"].Value,
            item["name"].Value,
            item["description"].Value,
            item["id"].AsInt,
            (Item.ItemType)item["type"].AsInt,
            (Item.ItemRarity)item["rarity"].AsInt
        );
    }

    /**
     *  TODO: Loading screen?
     *  Wait untill all requests have completed, than load the home screen.
     */
    private IEnumerator LoadHomeScreen() {
        while(this.callCount < ExpectedCalls) {
            yield return null;
        }

        SceneManager.LoadScene("home");
    }

}
