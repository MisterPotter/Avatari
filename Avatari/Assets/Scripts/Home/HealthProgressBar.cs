using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class HealthProgressBar : ProgressBar {

    protected override void GetMinAndMaxValue() {
        this.maxValue = this.cache.player.stats.health.maxValue;
        this.minValue = this.cache.player.stats.health.minValue;
    }
    protected override void GetCurrentValue() {
        this.currValue = this.cache.player.stats.health.CurrentValue;
    }
    protected override void FindSlider() {
        this.slider = Utility.LoadObject<Slider>("HealthSlider");
    }

}
