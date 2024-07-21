using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoogleAdsenseManager : MonoBehaviour
{
    private BannerView bannerView;

#if UNITY_ANDROID || UNITY_EDITOR
    private string _adUnitId = "ca-app-pub-3940256099942544/6300978111";
#else
    private string _adUnitId = "unused";
#endif
    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.

            
        });

        /*OnCreateBannerView();
        OnListenAdEvents();

*/

    }

    public void OnCreateBannerView()
    {
        Debug.Log("Creating banner View");

        if ( bannerView != null)
        {
            /*DestroyAd();*/
        }

        bannerView = new BannerView(_adUnitId, AdSize.Banner, AdPosition.Top);
        AdRequest request = new AdRequest();
        bannerView.LoadAd(request);


    }

    private void OnListenAdEvents()
    {
        bannerView.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner view loaded an ad with response : "
                + bannerView.GetResponseInfo());
        };
        // Raised when an ad fails to load into the banner view.
        bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("Banner view failed to load an ad with error : "
                + error);
        };
        // Raised when the ad is estimated to have earned money.
        bannerView.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(string.Format("Banner view paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        bannerView.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Banner view recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        bannerView.OnAdClicked += () =>
        {
            Debug.Log("Banner view was clicked.");
        };
        // Raised when an ad opened full screen content.
        bannerView.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Banner view full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        bannerView.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Banner view full screen content closed.");
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
