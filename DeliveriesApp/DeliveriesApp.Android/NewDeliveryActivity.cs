using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DeliveriesApp.Model;
using Android.Gms.Maps;
using Android.Locations;
using Android.Gms.Maps.Model;
using Android.Support.V7.App;
using Android.Support.V4.App;
using Android.Support.V4.View;

namespace DeliveriesApp.Droid
{
    [Activity(Label = "NewDeliveryActivity")]
    public class NewDeliveryActivity : Activity, IOnMapReadyCallback, ILocationListener
    {
        Button saveButton;
        EditText packageNameEditText;
        MapFragment mapFragment, destinationMapFragment;
        double latitude, longitude;
        LocationManager locationManager;
        GoogleMap originMap, destinationMap;

        public void OnLocationChanged(Location location)
        {
            latitude = location.Latitude;
            longitude = location.Longitude;
            mapFragment.GetMapAsync(this);
            destinationMapFragment.GetMapAsync(this);
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            MarkerOptions marker = new MarkerOptions();
            marker.SetPosition(new LatLng(latitude, longitude));
            marker.SetTitle("Your location");
            googleMap.AddMarker(marker);

            googleMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(latitude, longitude), 10));
        }

        public void OnProviderDisabled(string provider)
        {
            
        }

        public void OnProviderEnabled(string provider)
        {
           
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.NewDelivery);

            saveButton = FindViewById<Button>(Resource.Id.saveButton);
            packageNameEditText = FindViewById<EditText>(Resource.Id.packageNameEditText);
            mapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.mapFragment);
            destinationMapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.destinationMapFragment);

            saveButton.Click += SaveButton_Click;
        }

        protected override void OnResume()
        {
            base.OnResume();

            locationManager = GetSystemService(Context.LocationService) as LocationManager;
            string provider = LocationManager.GpsProvider;

            if (locationManager.IsProviderEnabled(provider))
            {
                locationManager.RequestLocationUpdates(provider, 5000, 1, this);
            }

            var location = locationManager.GetLastKnownLocation(LocationManager.NetworkProvider);
            latitude = location.Latitude;
            longitude = location.Longitude;
            mapFragment.GetMapAsync(this);
            destinationMapFragment.GetMapAsync(this);
        }

        protected override void OnPause()
        {
            base.OnPause();
            locationManager.RemoveUpdates(this);
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            //var originLocation = mapFragment.Map.CameraPosition.Target;
            //var destinationLocation = destinationMapFragment.Map.CameraPosition.Target;
            //Delivery delivery = new Delivery()
            //{
            //    Name = packageNameEditText.Text,
            //    Status = 0,
            //    OriginLatitude = originLocation.Latitude,
            //    OriginLongitude = originLocation.Longitude,
            //    DestinationLatitude = destinationLocation.Latitude,
            //    DestinationLongitude = destinationLocation.Longitude
            //};

            //await Delivery.InsertDelivery(delivery);
        }
    }
}