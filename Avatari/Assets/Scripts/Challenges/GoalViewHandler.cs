using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

/**
* @author: Denholm
* @summary: Allows you to move between goal views on the goals scene
* */
public class GoalViewHandler : MonoBehaviour {

    //Different views
    private GameObject dailyGoalView;
    private GameObject challengeGoalView;
    private GameObject lifetimeGoalView;

    private Text goalViewText;

    //Text for the goal view switcher
    private const string dailyViewText = "Daily Goals";
    private const string challengeViewText = "Challenges";
    private const string lifetimeViewText = "Lifetime Goals";

    private activeView active;

    private enum activeView {
        dailyActive,
        challengeActive,
        lifetimeActive
    }

    private void Awake() {
        Initialize();
    }

    private void Initialize() {
        this.lifetimeGoalView = GameObject.FindGameObjectWithTag("LifetimeGoalView");
        this.lifetimeGoalView.SetActive(false);
        this.challengeGoalView = GameObject.FindGameObjectWithTag("ChallengeGoalView");
        this.challengeGoalView.SetActive(false);
        this.dailyGoalView = GameObject.FindGameObjectWithTag("DailyGoalView");
        this.dailyGoalView.SetActive(true);

        this.goalViewText = Utility.LoadObject<Text>("GoalSwitcherText");
        this.goalViewText.text = dailyViewText;
        this.active = activeView.dailyActive;
    }

    /**
   *  Transitions the current active inventory ScrollView to the right
   */
    public void TransitionRight() {
        switch (this.active) {
            case activeView.dailyActive:
                this.challengeGoalView.SetActive(true);
                this.dailyGoalView.SetActive(false);
                this.goalViewText.text = challengeViewText;
                this.active = activeView.challengeActive;
                return;
            case activeView.challengeActive:
                this.lifetimeGoalView.SetActive(true);
                this.challengeGoalView.SetActive(false);
                this.goalViewText.text = lifetimeViewText;
                this.active = activeView.lifetimeActive;
                return;
            case activeView.lifetimeActive:
                this.dailyGoalView.SetActive(true);
                this.lifetimeGoalView.SetActive(false);
                this.goalViewText.text = dailyViewText;
                this.active = activeView.dailyActive;
                return;
            default:
                throw new Exception("Invalid inventory ScrollView state: " + this.active);
        }
    }

    /**
     *  Transitions the current active inventory ScrollView to the left
     */
    public void TransitionLeft() {
        switch (this.active) {
            case activeView.dailyActive:
                this.lifetimeGoalView.SetActive(true);
                this.dailyGoalView.SetActive(false);
                this.goalViewText.text = lifetimeViewText;
                this.active = activeView.lifetimeActive;
                return;
            case activeView.challengeActive:
                this.dailyGoalView.SetActive(true);
                this.challengeGoalView.SetActive(false);
                this.goalViewText.text = dailyViewText;
                this.active = activeView.dailyActive;
                return;
            case activeView.lifetimeActive:
                this.challengeGoalView.SetActive(true);
                this.lifetimeGoalView.SetActive(false);
                this.goalViewText.text = challengeViewText;
                this.active = activeView.challengeActive;
                return;
            default:
                throw new Exception("Invalid inventory ScrollView state: " + this.active);
        }
    }
}
