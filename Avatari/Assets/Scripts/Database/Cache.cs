using UnityEngine;
using System;
using System.Collections.Generic;

public class Cache : MonoBehaviour, IDataSource {

    public List<Item> LoadItems() {
        throw new NotImplementedException();
    }

    public List<string> LoadEquipped() {
        List<string> equippedItems = new List<string>();
        equippedItems.Add(PlayerPrefs.GetString("Helm"));

        return equippedItems;
    }

}
