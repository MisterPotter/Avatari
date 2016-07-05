using UnityEngine;
using System.Collections.Generic;

public class LoadEquipped : MonoBehaviour {

    private void Awake() {
        GameObject cacheGameObject = GameObject.FindGameObjectWithTag("Cache");
        IDataSource cache = cacheGameObject.GetComponent<IDataSource>();
        List<string> equippedItems = cache.LoadEquipped();

        foreach(string equippedItem in equippedItems) {
            print(equippedItem);
        }
    }
}
