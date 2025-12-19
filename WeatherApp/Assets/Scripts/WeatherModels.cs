[System.Serializable]
public class WeatherResponse
{
    public Daily daily;
}

[System.Serializable]
public class Daily
{
    public float[] temperature_2m_max;
}
