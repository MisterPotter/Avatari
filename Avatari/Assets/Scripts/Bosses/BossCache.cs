using System.Collections.Generic;

public class BossCache {
    List<Item> bossList;
    IDataSource dataSource;

    public BossCache(IDataSource dataSource)
    {
        this.dataSource = dataSource;
        this.bossList = this.dataSource.LoadItems();
    }
}
