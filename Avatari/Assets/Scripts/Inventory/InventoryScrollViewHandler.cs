using UnityEngine;
using System;

/**
 *  @author: Tyler
 *  Handles the button presses to swap to and from different ScrollViews.
 */
public class InventoryScrollViewHandler : MonoBehaviour {

    private GameObject areaScrollView;
    private GameObject characterScrollView;
    private GameObject itemScrollView;

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
                this.state = State.AreaActive;
                return;
            case State.ItemActive:
                this.characterScrollView.SetActive(true);
                this.itemScrollView.SetActive(false);
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
                this.state = State.CharacterActive;
                return;
            case State.CharacterActive:
                this.characterScrollView.SetActive(false);
                this.itemScrollView.SetActive(true);
                this.state = State.ItemActive;
                return;
            case State.ItemActive:
                return;
            default:
                throw new Exception("Invalid inventory ScrollView state: " + this.state);
        }
    }
}
