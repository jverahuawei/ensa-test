using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Core.App;
using Huawei.Hms.Maps;
using Huawei.Hms.Maps.Model;

namespace ensa_test
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : Activity, IOnMapReadyCallback
    {
        private const string TAG = "MapViewDemoActivity";

        private const string MAPVIEW_BUNDLE_KEY = "MapViewBundleKey";

        private HuaweiMap hMap;

        private MapView mMapView;

        //Permission request code.
        private const int REQUEST_CODE = 100;

        //Required permissions
        private static readonly string[] RUNTIME_PERMISSIONS = {
        Manifest.Permission.WriteExternalStorage,
        Manifest.Permission.ReadExternalStorage,
        Manifest.Permission.AccessCoarseLocation,
        Manifest.Permission.AccessFineLocation,
        Manifest.Permission.Internet
        };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);


            //Check permissions
            if (!HasPermissions(this, RUNTIME_PERMISSIONS))
            {
                AndroidX.Core.App.ActivityCompat.RequestPermissions(this, RUNTIME_PERMISSIONS, REQUEST_CODE);
            }

            mMapView = (MapView)FindViewById(Resource.Id.mapView);
            Bundle mapViewBundle = null;
            if (savedInstanceState != null)
            {
                mapViewBundle = savedInstanceState.GetBundle(MAPVIEW_BUNDLE_KEY);
            }
            // please replace "Your API key" with api_key field value in
            // agconnect-services.json if the field is null.
            MapsInitializer.SetApiKey(Constants.API_KEY);
            mMapView.OnCreate(mapViewBundle);
            mMapView.GetMapAsync(this);
        }

    

        protected override void OnStart()
        {
            base.OnStart();
            mMapView.OnStart();
        }

        protected override void OnStop()
        {
            base.OnStop();
            mMapView.OnStop();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            mMapView.OnDestroy();
        }

        protected override void OnPause()
        {
            base.OnPause();
            mMapView.OnPause();
        }

        protected override void OnResume()
        {
            base.OnResume();
            mMapView.OnResume();
        }

        public override void OnLowMemory()
        {
            base.OnLowMemory();
            mMapView.OnLowMemory();
        }

        public void OnMapReady(HuaweiMap map)
        {
            Log.Debug(TAG, "onMapReady: ");
            hMap = map;
            hMap.MyLocationEnabled = false;
            hMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(48.893478, 2.334595), 10));
        }

        /// <summary>
        /// Check whether permissions are granted
        /// </summary>
        private static bool HasPermissions(Context context, string[] permissions)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M && permissions != null)
            {
                foreach (string permission in permissions)
                {
                    if (ActivityCompat.CheckSelfPermission(context, permission) != Permission.Granted)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }




}
