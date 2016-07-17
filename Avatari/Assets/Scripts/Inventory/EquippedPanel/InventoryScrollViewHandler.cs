using UnityEngine;
using UnityEngine.UI;
using System;

/**
 *  @author: Tyler
 *  Handles the button presses to swap to and from different ScrollViews.
 */
public class InventoryScrollViewHandler : MonoBehaviour {

    /*
     *  Our different scroll views.
     */
    private GameObject areaScrollView;
    private GameObject characterScrollView;
    private GameObject itemScrollView;

    /*
     *  Components for the scroll view switcher.
     */
    private Text transitionText;

    /*
     *  Text for the scroll view switcher.
     */
    private const string ItemText = "Items";
    private const string CharacterText = "Characters";
    private const string AreaText = "Areas";

    /*
     *  Our current state.
     */
    private State state;

    private enum State {
        AreaActive,
        CharacterActive,
        ItemActive
    }

    private void Awake() {
        Initialize();
    }

    /**
     *  We want the item scroll view to be the only active one to start.
     */
    private void Initialize() {
        this.areaScrollView = GameObject.FindGameObjectWithTag("AreaScrollView");
        this.areaScrollView.SetActive(false);
        this.characterScrollView = GameObject.FindGameObjectWithTag("CharacterScrollView");
        this.characterScrollView.SetActive(false);
        this.itemScrollView = GameObject.FindGameObjectWithTag("ItemScrollView");

        this.transitionText = Utility.LoadObject<Text>("InventoryScrollViewText");
        this.transitionText.text = ItemText;

        this.state = State.ItemActive;
    }

    /**
     *  Transitions the current active inventory ScrollView to the right
     */
    public void TransitionRight() {
        switch(this.state) {
            case State.AreaActive:
                return;
            case State.CharacterActive:
                this.areaScrollView.SetActive(true);
                this.characterScrollView.SetActive(false);
                this.transitionText.text = AreaText;
                this.state = State.AreaActive;
                return;
            case State.ItemActive:
                this.characterScrollView.SetActive(true);
                this.itemScrollView.SetActive(false);
                this.transitionText.text = CharacterText;
                this.state = State.CharacterActive;
                return;
            default:
                throw new Exception("Invalid inventory ScrollView state: " + this.state);
        }
    }

    /**
     *  Transitions the current active inventory ScrollView to the left
     */
    public void TransitionLeft() {
        switch(this.state) {
            case State.AreaActive:
                this.areaScrollView.SetActive(false);
                this.characterScrollView.SetActive(true);
                this.transitionText.text = CharacterText;
                this.state = State.CharacterActive;
                return;
            case State.CharacterActive:
                this.characterScrollView.SetActive(false);
                this.itemScrollView.SetActive(true);
                this.transitionText.text = ItemText;
                this.state = State.ItemActive;
                return;
            case State.ItemActive:
                return;
            default:
                throw new Exception("Invalid inventory ScrollView state: " + this.state);
        }
    }
}
