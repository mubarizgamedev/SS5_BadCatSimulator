namespace Firebase.Sample.Analytics
{
    using Firebase;
    using Firebase.Analytics;
    using Firebase.Extensions;
    using System;
    using System.Threading.Tasks;
    using UnityEngine;

    public class FirebaseHandler : MonoBehaviour
    {
        [Header("Timer // Active")]
        public float FB_Timer;
        public bool FB_Active;

        protected bool firebaseInitialized = false;
        DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;

        void Start()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    Invoke("wait", FB_Timer);
                }
                else
                {

                }
            });
        }

        void wait()
        {
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            firebaseInitialized = true;
            FB_Active = true;
            AdmobAdsManager.Instance.Check_Firebase = true;
        }
    }
}