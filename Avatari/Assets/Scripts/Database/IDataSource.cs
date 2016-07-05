using System.Collections.Generic;

public interface IDataSource {
    List<Item> LoadItems();
    List<string> LoadEquipped();
}
