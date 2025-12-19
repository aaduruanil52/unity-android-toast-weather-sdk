using System.Diagnostics;
using UnityEngine;

namespace CleverTap.AndroidToastSDK
{
    public static class AndroidToastBridge
    {
        public static void ShowToast(string message)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass unityPlayer =
                new AndroidJavaClass("com.unity3d.player.UnityPlayer");

            AndroidJavaObject activity =
                unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            AndroidJavaClass toastClass =
                new AndroidJavaClass("android.widget.Toast");

            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toast =
                    toastClass.CallStatic<AndroidJavaObject>(
                        "makeText",
                        activity,
                        message,
                        toastClass.GetStatic<int>("LENGTH_SHORT")
                    );
                toast.Call("show");
            }));
#else
            UnityEngine.Debug.Log("[Toast SDK] " + message);
#endif
        }
    }
}
