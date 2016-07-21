using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
* @author: Denholm
* @summary: Loads the lifetime goals as the lifetime goals page is opened
* */
public class LifetimeGoalLoader : MonoBehaviour {
    private Cache cache;
    private RectTransform content;
    private GameObject panelPrefab;
    private Transform panelSpawner;
    private const float rowVertOffset = 60.0f;

    private void Awake() {
        Initialize();
        LoadLifetimeGoals();
    }

    private void Initialize() {
        cache = Utility.LoadObject<Cache>("Cache");
        content = Utility.LoadObject<RectTransform>("LifetimeGoalContent");
        panelPrefab = Resources.Load<GameObject>("Prefabs/UI/Goals/GoalPanel");
        panelSpawner = Utility.LoadObject<Transform>("LifetimeGoalSpawner");
    }

    private void LoadLifetimeGoals() {
        LifetimeGoals ltGoals = this.cache.lifetimeGoals;
        
        Goal[] goals = {
            ltGoals.stepGoal,
            //ltGoals.calorieGoal,
            ltGoals.distanceGoal,
            ltGoals.floorGoal
        };
        content.sizeDelta = new Vector2(0.0f, goals.Length * rowVertOffset);
        Vector3 offset = Vector3.down * rowVertOffset;

        int i = 0;
        foreach (var goal in goals) {
            GameObject clone = (GameObject)Instantiate(panelPrefab, offset * i++, Quaternion.identity);
            clone.transform.SetParent(panelSpawner, false);

            Text goalDescription = clone.transform.GetChild(0).GetComponent<Text>();
            goalDescription.text = goal.description + " - " + goal.progress + " / " + goal.goal;

            Slider goalSlider = clone.transform.GetChild(1).GetComponent<Slider>();
            Text completed = clone.transform.GetChild(2).GetComponent<Text>();

            if (goal.progress < goal.goal) {
                goalSlider.maxValue = goal.goal;
                goalSlider.minValue = 0;
                goalSlider.value = goal.progress;
            } else {
                // goal has been achieved
                Color color = completed.color;
                color.a += 1.0f;
                completed.color = color;
                goalSlider.GetComponent<CanvasGroup>().alpha = 0.0f;
                CompletionDialogLoader dialogScript = clone.transform.GetComponent<CompletionDialogLoader>();
                dialogScript.goal = goal;
            }
        }
    }
}

