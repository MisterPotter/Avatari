using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ExperienceProgressBar : ProgressBar {

    protected override void GetMinAndMaxValue() {
        int currLevel = this.cache.LoadPlayerLevel();
        int nextLevel = currLevel;
        if ( currLevel != Constants.MaxLevel) {
            nextLevel = currLevel + 1;
        }
        this.maxValue = Constants.ExperienceToLevel[nextLevel];
        this.minValue = Constants.ExperienceToLevel[currLevel];
    }
    protected override void GetCurrentValue() {
        this.currValue = this.cache.player.stats.experience.CurrentValue;
    }
    protected override void FindSlider() {
        this.slider = Utility.LoadObject<Slider>("ExperienceSlider");
    }

}