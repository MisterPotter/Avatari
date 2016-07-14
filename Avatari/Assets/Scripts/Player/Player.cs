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

    /**
     *  The gear that the player current has equipped.
     */
    public EquippedGear gear { get; set; }

    /**
     *  The name of the sprite to be loaded for the player.
     */
    public string sprite { get; set; }

    /**
     * The statistics of the player 
     */
    public PlayerStatistic stats;

    public Player() {
        this.gear = null;
        this.sprite = null;
        this.stats = null;
    }
}
