using NUnit.Framework;
using UnityEngine;
using CleverTap.AndroidToastSDK;

public class CleverToastTests
{
    [Test]
    public void ToastMessage_ShouldExist()
    {
        var go = new GameObject();
        var toast = go.AddComponent<CleverToast>();
        toast.toastMessage = "Test";
        Assert.IsNotNull(toast.toastMessage);
    }
}
