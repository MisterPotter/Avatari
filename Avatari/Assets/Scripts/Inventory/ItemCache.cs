using System.Collections.Generic;

/**
 *  @author: Tyler
 *  @summary: A class to cache the items locally on the client. This would more
 *  be more properly done with an actual cache, however, the logic should stay
 *  the same.
 */
public class ItemCache {
    List<Item> itemData;
    IDataSource dataSource;

    public ItemCache(IDataSource dataSource) {
        this.dataSource = dataSource;
        this.itemData = this.dataSource.LoadItems();
    }
}
