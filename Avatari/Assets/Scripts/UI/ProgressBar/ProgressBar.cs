using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public abstract class ProgressBar : MonoBehaviour {

    protected int maxValue;
    protected int minValue;

    protected int currValue;

    protected Slider slider;
    protected Cache cache;

	void Start () {
        FindCache();
        GetMinAndMaxValue();
        GetCurrentValue();
        FindSlider();
        UpdateSlider();
	}

    void Update() {
        UpdateSlider();
    }

    private void FindCache() {
        cache = Utility.LoadObject<Cache>("Cache");
    }

    // Get the minimum and maximum values for this progress bar
    protected abstract void GetMinAndMaxValue();

    // Get the current value of the statistic in the progress bar
    protected abstract void GetCurrentValue();

    // Find the desired slider game object
    protected abstract void FindSlider();

    // Change the slider value to reflect the current value
    protected void UpdateSlider() {
        slider.maxValue = this.maxValue;
        slider.minValue = this.minValue;
        slider.value = this.currValue;
    }
}
