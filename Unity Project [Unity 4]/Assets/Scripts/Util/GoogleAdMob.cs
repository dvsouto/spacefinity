using UnityEngine;
using System.Collections;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class GoogleAdMob : MonoBehaviour {

	public bool requestBanner = false;
	public bool requestInterstitial = false;

	private BannerView bannerView = null;
	private InterstitialAd interstitial = null;

	// Use this for initialization
	void Start () {
		if (requestBanner) 
		{
			this.RequestBanner();
			// this.hideBanner();
		}
		if (requestInterstitial) this.RequestInterstitial();
	}
	
	public void showBanner()
	{
		bannerView.Show();
	}

	public void hideBanner()
	{
		bannerView.Hide();
	}

	public void showInterstitial()
	{
		if (interstitial.IsLoaded())
			interstitial.Show();
	}

	private void RequestBanner()
	{
		#if UNITY_ANDROID
		string adUnitId = "ca-app-pub-3289813992119986/3111133958";
		#elif UNITY_IPHONE
		string adUnitId = "INSERT_IOS_BANNER_AD_UNIT_ID_HERE";
		#else
		string adUnitId = "unexpected_platform";
		#endif
		
		// Create a 320x50 banner at the top of the screen.
		this.bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Top);
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the banner with the request.
		bannerView.LoadAd(request);
	}
	
	private void RequestInterstitial()
	{
		#if UNITY_ANDROID
		string adUnitId = "ca-app-pub-3289813992119986/7401732753";
		#elif UNITY_IPHONE
		string adUnitId = "INSERT_IOS_INTERSTITIAL_AD_UNIT_ID_HERE";
		#else
		string adUnitId = "unexpected_platform";
		#endif
		
		// Initialize an InterstitialAd.
		this.interstitial = new InterstitialAd(adUnitId);
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the interstitial with the request.
		interstitial.LoadAd(request);
	}

	public void destroyAdmob()
	{
		if (requestBanner) bannerView.Destroy();
		if (requestInterstitial) interstitial.Destroy();		                     
	}
}
