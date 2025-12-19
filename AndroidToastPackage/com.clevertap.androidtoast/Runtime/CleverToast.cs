using UnityEngine;

namespace CleverTap.AndroidToastSDK
{
    public class CleverToast : MonoBehaviour
    {
        [TextArea]
        public string toastMessage = "Hello from CleverToast SDK";

        public void ShowToast()
        {
            AndroidToastBridge.ShowToast(toastMessage);
        }
    }
}
