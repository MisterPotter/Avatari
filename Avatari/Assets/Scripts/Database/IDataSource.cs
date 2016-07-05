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
}
