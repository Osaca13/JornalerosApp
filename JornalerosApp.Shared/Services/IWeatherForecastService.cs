using System;
using System.Threading.Tasks;

namespace JornalerosApp.Shared.Services
{
    public interface IWeatherForecastService
    {
        Task<WeatherForecast[]> GetForecastAsync(DateTime startDate);
    }
}