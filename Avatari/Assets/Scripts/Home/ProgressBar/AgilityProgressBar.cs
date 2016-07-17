using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class AgilityProgressBar : ProgressBar {

    protected override void GetMinAndMaxValue() {
        this.maxValue = this.cache.player.stats.agility.maxValue;
        this.minValue = this.cache.player.stats.agility.minValue;
    }
    protected override void GetCurrentValue() {
        this.currValue = this.cache.player.stats.agility.CurrentValue;
    }
    protected override void FindSlider() {
        this.slider = Utility.LoadObject<Slider>("AgilitySlider");
    }

}
