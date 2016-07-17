using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class DefenseProgressBar : ProgressBar {

    protected override void GetMinAndMaxValue() {
        this.maxValue = this.cache.player.stats.defense.maxValue;
        this.minValue = this.cache.player.stats.defense.minValue;
    }
    protected override void GetCurrentValue() {
        this.currValue = this.cache.player.stats.defense.CurrentValue;
    }
    protected override void FindSlider() {
        this.slider = Utility.LoadObject<Slider>("DefenseSlider");
    }

}