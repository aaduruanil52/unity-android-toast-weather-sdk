using UnityEngine;
using System.Collections;
#if UNITY_ANDROID
using UnityEngine.Android;
#endif

public class LocationController : MonoBehaviour
{
    public float latitude;
    public float longitude;

    IEnumerator Start()
    {
#if UNITY_ANDROID
        // 1️⃣ Request permission if not granted
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);

            // wait until user responds
            yield return new WaitUntil(() =>
                Permission.HasUserAuthorizedPermission(Permission.FineLocation));
        }
#endif

        // 2️⃣ Start location service
        if (!Input.location.isEnabledByUser)
        {
            Debug.LogError("Location is disabled by user");
            yield break;
        }

        Input.location.Start();

        // 3️⃣ Wait until service starts
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (Input.location.status != LocationServiceStatus.Running)
        {
            Debug.LogError("Location service failed");
            yield break;
        }

        // 4️⃣ Get data
        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;

        Debug.Log($"Location: {latitude}, {longitude}");
    }
}
