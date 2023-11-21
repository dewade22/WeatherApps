namespace WeatherApp.Dto.Weather
{
    public class WeatherDto
    {
        public string Location { get; set; }

        public int Time { get; set; }

        public decimal Wind { get; set; }

        public int Visibility { get; set; }

        public string SkyCondition { get; set; }

        public decimal TemperatureCelcius { get; set; }

        public decimal TemperatureFahrenheit { get; set; }

        public int DewPoint { get; set; }

        public decimal RelativeHumidity { get; set; }

        public int Preasure { get; set; }
    }
}
