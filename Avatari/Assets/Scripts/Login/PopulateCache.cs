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

    private const uint ExpectedCalls = 5;

    private void Awake() {
        this.callCount = 0;
        this.cache = Utility.LoadObject<Cache>("Cache");
        this.mutex = new Mutex();
    }

    public void Populate() {
        StartCoroutine(PopulateFitbitData());
        StartCoroutine(PopulateItems());
        StartCoroutine(PopulateAreas());
        StartCoroutine(PopulateTaris());
        StartCoroutine(PopulatePlayer());
        StartCoroutine(LoadHomeScreen());
    }

    /**
     *  Populate the cache with all our fitbit data.
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
        PopulatePlayerGoalProgress();
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

    private void PopulatePlayerGoalProgress() {
        PopulateLifetimeGoalProgress();
        PopulateDailyGoalProgress();
        PopulateChallengeProgress();
    }

    private void PopulateDailyGoalProgress() {
        DailyGoals goals = cache.dailyGoals;
        float totalActiveMin = (float)PlayerStatistic.GetTodaysData(cache.fitbit.fairlyActive);
        totalActiveMin += (float)PlayerStatistic.GetTodaysData(cache.fitbit.veryActive);
        goals.activeMinGoal.progress = totalActiveMin;
        goals.calorieGoal.progress = (float)PlayerStatistic.GetTodaysData(cache.fitbit.calories);
        goals.distanceGoal.progress = (float)PlayerStatistic.GetTodaysData(cache.fitbit.activeDistance);
        goals.stepGoal.progress = (float)PlayerStatistic.GetTodaysData(cache.fitbit.activeSteps);
    }

    private void PopulateLifetimeGoalProgress() {
        LifetimeGoals goals = cache.lifetimeGoals;
        goals.stepGoal.progress = (float)cache.fitbit.lifetime.steps;
        //goals.calorieGoal.progress  = (float)cache.fitbit.lifetime.caloriesOut;
        goals.distanceGoal.progress = (float)cache.fitbit.lifetime.distance;
        goals.floorGoal.progress = (float)cache.fitbit.lifetime.floor;
    }

    // TODO: USE REAL DATA HERE. Right now we're just flubbing it since we don't have an endpoint for the activity type.
    private void PopulateChallengeProgress() {
        Challenges goals = cache.challenges;
        goals.biking.progress = Constants.BikeChallenge * 0.6f;
        goals.hiking.progress = Constants.HikingChallenge * 0.4f;
        goals.running.progress = Constants.RunningChallenge * 0.7f;
    }

    /**
     *  Populate cache with items from AWS.
     */
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
     *  Populate Areas with values from Database.
     */
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
     *  Populate cache with Tari information from AWS.
     */
    private IEnumerator PopulateTaris() {
        WWWForm form = new WWWForm();
        form.AddField(Config.SessionKey, this.cache.sessionKey);
        WWW www = new WWW(Config.ControllerURLTaris, form);

        yield return www;

        var data = JSON.Parse(www.text);
        int response = data["status"].AsInt;

        if (response == 200) {
            FillCacheWithTaris(data["data"]);
            this.mutex.WaitOne();
            this.callCount++;
            this.mutex.ReleaseMutex();
            Debug.Log(callCount);
        } else {
            throw new Exception("FATAL: Taris data could not be obtained.");
        }
    }

    private void FillCacheWithTaris(JSONNode data) {
        JSONArray taris = data["taris"].AsArray;
        foreach(JSONNode tari in taris) {
            this.cache.AddCharacterToInventory(
                new Tari(
                    tari["id"].AsInt,
                    tari["name"].Value,
                    tari["name"].Value,
                    tari["description"].Value
                )
            );
        }
    }

    /**
     * Populates player with info from AWS.
     */
    private IEnumerator PopulatePlayer() {
        WWWForm form = new WWWForm();
        form.AddField(Config.SessionKey, this.cache.sessionKey);
        WWW www = new WWW(Config.ControllerURLPlayer, form);

        yield return www;

        var data = JSON.Parse(www.text);
        int response = data["status"].AsInt;

        if (response == 200) {
            FillCacheWithPlayerInfo(data["data"]);
            this.mutex.WaitOne();
            this.callCount++;
            this.mutex.ReleaseMutex();
            Debug.Log(callCount);
        } else {
            throw new Exception("FATAL: Taris data could not be obtained.");
        }
    }

    private void FillCacheWithPlayerInfo(JSONNode data) {
        JSONNode player = data["avatar"];
        PlayerStatistic stats = new PlayerStatistic(
            player["level"].AsInt,
            player["exp"]["exp_current"].AsInt,
            player["health"]["health_current"].AsInt,
            player["stats"]["strength"].AsInt,
            player["stats"]["agility"].AsInt,
            player["stats"]["defence"].AsInt
        );

        JSONNode areaNode = data["area"];
        Area area = new Area(
            areaNode["id"].AsInt,
            areaNode["name"].Value,
            areaNode["spriteName"].Value,
            areaNode["description"]
        );
        this.cache.player = new Player(
                new Player.EquippedGear(),
                player["name"].Value,
                // We have access to the tari here if we happen to need it
                player["tari"]["spriteName"].Value,
                stats,
                area
        );

        // Populate items
        JSONArray items = player["items"].AsArray;
        foreach(JSONNode item in items) {
            this.cache.player.EquipItem(
                null //TODO
             );
        }
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
