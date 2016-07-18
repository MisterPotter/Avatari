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

    private const uint ExpectedCalls = 3;

    private void Awake() {
        this.callCount = 0;
        this.cache = Utility.LoadObject<Cache>("Cache");
        this.mutex = new Mutex();
    }

    public void Populate() {
        StartCoroutine(PopulateFitbitData());
        StartCoroutine(PopulateItems());
        StartCoroutine(PopulateAreas());
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
            Debug.Log(callCount);
        } else {
            throw new Exception("FATAL: Fitbit data could not be obtained.");
        }
    }

    private void FillCacheWithFitbitData(JSONNode data) {
        PopulateLifetimeStats(data["lifetime"]["lifetime"]["total"]);
        PopulateCalories(data["calories"]["activities-calories"]);
        PopulateActivityCalories(data["activityCalories"]["activities-activityCalories"]);
        PopulateSteps(data["steps"]["activities-steps"]);
        PopulateDistance(data["distance"]["activities-distance"]);
        PopulateFairlyActive(data["minutesFairlyActive"]["activities-minutesFairlyActive"]);
        PopulateVeryActive(data["minutesVeryActive"]["activities-minutesVeryActive"]);
    }

    /**
     *  Populate the cache with lifetime stats
     *  of a fitbit users from JSON data.
     */
    private void PopulateLifetimeStats(JSONNode data) {
        this.cache.fitbit.lifetime = new LifetimeStats(
            data["activeScore"].AsInt,
            data["caloriesOut"].AsInt,
            data["distance"].AsFloat,
            data["floors"].AsInt,
            data["steps"].AsInt
        );
    }

    private void PopulateCalories(JSONNode data) {
        foreach(JSONNode record in data.AsArray) {
            this.cache.AddPairToCalories(new FitbitPair<int>(
                record["dateTime"].Value,
                record["value"].AsInt
            ));
        }
    }

    private void PopulateActivityCalories(JSONNode data) {
        foreach (JSONNode record in data.AsArray) {
            this.cache.AddPairToActivityCalories(new FitbitPair<int>(
                record["dateTime"].Value,
                record["value"].AsInt
            ));
        }
    }

    private void PopulateSteps(JSONNode data) {
        foreach (JSONNode record in data.AsArray) {
            this.cache.AddPairToSteps(new FitbitPair<int>(
                record["dateTime"].Value,
                record["value"].AsInt
            ));
        }
    }

    private void PopulateDistance(JSONNode data) {
        foreach (JSONNode record in data.AsArray) {
            this.cache.AddPairToDistance(new FitbitPair<double>(
                record["dateTime"].Value,
                record["value"].AsDouble
            ));
        }
    }

    private void PopulateFairlyActive(JSONNode data) {
        foreach (JSONNode record in data.AsArray) {
            this.cache.AddPairToFairlyActive(new FitbitPair<int>(
                record["dateTime"].Value,
                record["value"].AsInt
            ));
        }
    }

    private void PopulateVeryActive(JSONNode data) {
        foreach (JSONNode record in data.AsArray) {
            this.cache.AddPairToVeryActive(new FitbitPair<int>(
                record["dateTime"].Value,
                record["value"].AsInt
            ));
        }
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

    private IEnumerator PopulateAreas() {
        WWWForm form = new WWWForm();
        form.AddField(Config.SessionKey, this.cache.sessionKey);
        WWW www = new WWW(Config.ControllerURLAreas, form);

        yield return www;

        var data = JSON.Parse(www.text);
        int response = data["status"].AsInt;

        if (response == 200) {
            FillCacheWithAreas(data["data"]);
            this.mutex.WaitOne();
            this.callCount++;
            this.mutex.ReleaseMutex();
            Debug.Log(callCount);
        } else {
            throw new Exception("FATAL: Area data could not be obtained.");
        }
    }

    /**
     *  Populate the areas list in cache with the areas from the response.
     */
    private void FillCacheWithAreas(JSONNode data) {
        JSONArray areas = data["areas"].AsArray;
        foreach(JSONNode area in areas) {
            this.cache.AddAreaToInventory(
                new Area(
                    area["id"].AsInt,
                    area["name"].Value,
                    area["name"].Value,
                    area["description"].Value
                )
            );
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
            (Item.ItemRarity)item["rarity"].AsInt,
            (Statistic.Type)4,
            1
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
