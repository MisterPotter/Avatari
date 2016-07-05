using System.Collections.Generic;

/**
 *  @author Tyler
 */
public class Player {

    public class EquippedGear {
        public Item headGear;
        public Item chestGear;
        public Item footGear;
        public Item handGear;
        public Item wingGear;

        public EquippedGear(Item head, Item chest, Item foot, Item hand, Item wing) {
            this.headGear = head;
            this.chestGear = chest;
            this.footGear = foot;
            this.handGear = hand;
            this.wingGear = wing;
        }
    }

    private EquippedGear _gear;
    public EquippedGear gear {
        get { return this._gear; }
        set { this._gear = value; }
    }

    public Player() {
        this.gear = null;
    }
}
