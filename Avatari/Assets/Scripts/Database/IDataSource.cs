using System.Collections.Generic;

public interface IDataSource {

    /*
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
}
