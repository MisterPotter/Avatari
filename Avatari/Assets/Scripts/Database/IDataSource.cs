﻿using UnityEngine;
using System.Collections.Generic;

public interface IDataSource {

    /**
     *  @summary: Loads inventory areas from cache.
     *  @return: A list of inventory areas from cache.
     */
    List<Area> LoadAreas();

    /**
     *  @summary: Loads inventory character sprites from cache.
     *  @return: A list of inventory character sprites from cache.
     */
    List<Tari> LoadCharacters();

    /**
     *  @summary: Loads inventory items from cache.
     *  @return: A list of inventory items that the player owns.
     */
    List<Item> LoadItems();

    /**
     *  @summary: Loads items that the player has on.
     *  @return: An object of equipped items the player is wearing.
     */
    Player.EquippedGear LoadEquippedItems();

    /**
     *  @summary: Loads the name of the character sprite.
     *  @return: The name of the resource to load in.
     */
    string LoadCharacterSprite();

    /**
     *  @summary: Load an inventory sprite.
     *  @param spriteName: The name of the sprite to be loaded.
     *  @return: The inventory inventory sprite.
     */
    Sprite LoadInventorySprite(string spriteName);
}
