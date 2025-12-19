using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using CleverTap.AndroidToastSDK;

public class WeatherService : MonoBehaviour
{
    public CleverToast cleverToast;
    public LocationController location;

    public void GetWeather()
    {
        if (location.latitude == 0 && location.longitude == 0)
        {
            cleverToast.toastMessage = "Location not ready. Try again.";
            cleverToast.ShowToast();
            return;
        }

        StartCoroutine(FetchWeather());
    }

    IEnumerator FetchWeather()
    {
        string url =
            $"https://api.open-meteo.com/v1/forecast?latitude={location.latitude}&longitude={location.longitude}&daily=temperature_2m_max&timezone=auto";

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            cleverToast.toastMessage = "Network error. Please try again.";
            cleverToast.ShowToast();
            yield break;
        }

        WeatherResponse response;

        try
        {
            response = JsonUtility.FromJson<WeatherResponse>(request.downloadHandler.text);
        }
        catch
        {
            cleverToast.toastMessage = "Failed to parse weather data.";
            cleverToast.ShowToast();
            yield break;
        }

        if (response == null || response.daily == null || response.daily.temperature_2m_max.Length == 0)
        {
            cleverToast.toastMessage = "Weather data unavailable.";
            cleverToast.ShowToast();
            yield break;
        }

        float temp = response.daily.temperature_2m_max[0];
        cleverToast.toastMessage = $"Today's Max Temp: {temp}°C";
        cleverToast.ShowToast();
    }
}
