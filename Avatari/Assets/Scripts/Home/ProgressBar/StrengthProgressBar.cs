using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class StrengthProgressBar : ProgressBar {

    protected override void GetMinAndMaxValue() {
        this.maxValue = this.cache.player.stats.strength.maxValue;
        this.minValue = this.cache.player.stats.strength.minValue;
    }
    protected override void GetCurrentValue() {
        this.currValue = this.cache.player.stats.strength.CurrentValue;
    }
    protected override void FindSlider() {
        this.slider = Utility.LoadObject<Slider>("StrengthSlider");
    }

    protected override void FindTotal() {
        this.total = Utility.LoadObject<Text>("StrengthValue");
    }

    protected override void UpdateTotal() {
        this.total.text = this.currValue + " / " + this.maxValue;
    }

}
