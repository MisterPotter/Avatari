using System;
using System.Globalization;
public class FitbitPair<T> {

    public DateTime date { get; private set; }
    public T value { get; private set; }

    public FitbitPair(string date, T value) {
        this.date = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.CurrentCulture);
        this.value = value;
    }
}
