using Android.App;
using Android.OS;
using Android;
using Android.Support.V4.App;
using Android.Content;
using Android.Content.PM;
using Android.Util;
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

        private Marker mOrsay;
        private static readonly LatLng ORSAY = new LatLng(8.957303, -79.541413);



        //Required permissions
        private static readonly string[] RUNTIME_PERMISSIONS = {
        Manifest.Permission.WriteExternalStorage,
        Manifest.Permission.ReadExternalStorage,
        Manifest.Permission.AccessCoarseLocation,
        Manifest.Permission.AccessFineLocation,
        Manifest.Permission.Internet
        };

        //Permission request code.
        private const int REQUEST_CODE = 100;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            //Check permissions
            if (!HasPermissions(this, RUNTIME_PERMISSIONS))
            {
                ActivityCompat.RequestPermissions(this, RUNTIME_PERMISSIONS, REQUEST_CODE);
            }


            mMapView = (MapView)FindViewById(Resource.Id.mapView);
            Bundle mapViewBundle = null;
            if (savedInstanceState != null)
            {
                mapViewBundle = savedInstanceState.GetBundle(MAPVIEW_BUNDLE_KEY);
            }
        
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
            hMap.MapType = HuaweiMap.MapTypeNormal;
            hMap.UiSettings.MyLocationButtonEnabled = true;
            hMap.MyLocationEnabled = true;
            hMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(8.9778032, -79.5618084), 13));
            mOrsay = hMap.AddMarker(new MarkerOptions().InvokePosition(ORSAY)
                   .InvokeTitle("Tienda Huawei")
                   .InvokeSnippet("casco antiguo")
                   .Clusterable(true)
                   .InvokeIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.badge_nj)));
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
