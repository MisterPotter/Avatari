using UnityEngine;
using SimpleJSON;
using System;
using System.Collections;
using System.Threading;

public class Requests {

    public uint requestCount;
    private uint requestLimit;
    private int sessionKey;

    private Cache cache;
    private Mutex mutex;

    public Requests(Cache cache) {
        this.requestCount = 0U;
        this.requestLimit = 0U;
        this.sessionKey = cache.sessionKey;

        this.cache = cache;
        this.mutex = new Mutex();
    }

    public IEnumerator PushEquippedItems() {
        Player.EquippedGear gear = this.cache.player.gear;
        this.requestLimit += this.cache.player.gear.Count;
        foreach(Item item in gear) {
            yield return PushEquippedItem(item == null? null : item);
        }
    }

    private IEnumerator PushEquippedItem(Item item) {
        WWWForm form = new WWWForm();
        form.AddField(Config.SessionKey, this.cache.sessionKey);
        if (item.itemID != -1) {
            form.AddField(Config.Item, item.itemID);
        }
        form.AddField(Config.ItemLocation, item.itemType.ToString().ToLower());
        Debug.Log(item.itemType.ToString());
        WWW www = new WWW(Config.ControllerURLEquipItem, form);

        yield return www;

        var data = JSON.Parse(www.text);
        int response = data["status"].AsInt;
        if (response == 200) {
            this.mutex.WaitOne();
            this.requestCount++;
            this.mutex.ReleaseMutex();
        } else {
            throw new Exception(
                String.Format(
                    "Item: {0} could not be equipped and persisted to the server.",
                    item.itemName
                )
            );
        }
    }

    public IEnumerator PushStats() {

        PlayerStatistic stats = this.cache.player.stats;
        this.requestLimit += this.cache.player.stats.Count;
        foreach (Statistic stat in stats) {
            yield return PushStat(stat);
        }
    }

    public IEnumerator PushStat(Statistic stat) {
        WWWForm form = new WWWForm();
        form.AddField(Config.SessionKey, this.cache.sessionKey);
        form.AddField(stat.statType.ToString().ToLower(), stat.CurrentValue);
        Debug.Log(stat.statType.ToString().ToLower());
        WWW www = new WWW(Config.ControllerURLPlayerSet, form);

        yield return www;

        var data = JSON.Parse(www.text);
        int response = data["status"].AsInt;
        if (response == 200) {
            this.mutex.WaitOne();
            this.requestCount++;
            this.mutex.ReleaseMutex();
        } else {
            throw new Exception(
                String.Format(
                    "Stat: {0} could not be persisted to the server.",
                    stat.statType.ToString().ToLower()
                )
            );
        }
    }

    public IEnumerator PushTari() {
        this.requestLimit += 1;
        WWWForm form = new WWWForm();
        Tari tari = FindTari(this.cache.player.sprite);
        form.AddField(Config.SessionKey, this.cache.sessionKey);
        form.AddField("tari", tari.id);
        WWW www = new WWW(Config.ControllerURLPlayerSet, form);

        yield return www;

        var data = JSON.Parse(www.text);
        int response = data["status"].AsInt;
        if (response == 200) {
            this.mutex.WaitOne();
            this.requestCount++;
            this.mutex.ReleaseMutex();
        } else {
            throw new Exception(
                String.Format(
                    "Sprite: {0} could not be persisted to the server.",
                    this.cache.player.sprite
                )
            );
        }
    }

    private Tari FindTari(string sprite) {
        foreach(Tari tari in this.cache.LoadCharacters()) {
            if (sprite == tari.spriteName) return tari;
        }
        return null;
    }

    public bool RequestNotFinished() {
        return this.requestCount < this.requestLimit;
    }

}
