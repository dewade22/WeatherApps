namespace WeatherApp.Framework
{
    public class GenericResponse<T> : BasicResponse
    {
        public T Data { get; set; }
    }
}
