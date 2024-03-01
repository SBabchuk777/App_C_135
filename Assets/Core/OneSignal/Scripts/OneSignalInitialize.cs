using OneSignalSDK;
using UnityEngine;

namespace Core
{
    public class OneSignalInitialize : MonoBehaviour
    {
        private async void Start () 
        {
#if UNITY_IOS
            OneSignal.Initialize(Settings.OneSignalAppID());
        
            //OneSignal.Notifications.PromptForPushNotificationsWithUserResponse();
            var result = await OneSignal.Notifications.RequestPermissionAsync(true);

            if (result)
                Debug.Log("Notification permission accepeted");
            else
                Debug.Log("Notification permission denied");

#else
            Debug.LogError($"Initializing error, OS != IOS");
#endif
        }
    }
}
