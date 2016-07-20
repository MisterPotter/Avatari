using UnityEngine;
using UnityEngine.UI;
/**
* @author: Denholm
* @summary: Loaders the challenges as the challenge page is opened
* */
public class ChallengeLoader : MonoBehaviour {

    private Cache cache;
    private RectTransform content;
    private GameObject panelPrefab;
    private Transform panelSpawner;
    private const float rowVertOffset = 60.0f;

    private void Awake() {
        Initialize();
        LoadChallenges();
    }

    private void Initialize() {
        cache = Utility.LoadObject<Cache>("Cache");
        content = Utility.LoadObject<RectTransform>("ChallengeGoalContent");
        panelPrefab = Resources.Load<GameObject>("Prefabs/UI/Goals/GoalPanel");
        panelSpawner = Utility.LoadObject<Transform>("ChallengeSpawner");
    }

    private void LoadChallenges() {
        Challenges challenges = this.cache.challenges;
        Goal[] goals = {
            challenges.biking,
            challenges.running,
            challenges.hiking,
        };
        content.sizeDelta = new Vector2(0.0f, 4 * rowVertOffset);
        Vector3 offset = Vector3.down * rowVertOffset;

        int i = 0;
        foreach (var goal in goals) {
            GameObject clone = (GameObject)Instantiate(panelPrefab, offset * i++, Quaternion.identity);
            clone.transform.SetParent(panelSpawner, false);

            Text goalDescription = clone.transform.GetChild(0).GetComponent<Text>();
            goalDescription.text = goal.description + " - " + goal.progress + " / " + goal.goal;

            Slider goalSlider = clone.transform.GetChild(1).GetComponent<Slider>();
            goalSlider.maxValue = goal.goal;
            goalSlider.minValue = 0;
            goalSlider.value = goal.progress;
        }
    }
}
