package edu.maine.seamans.ratemyairport;

import android.Manifest;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.location.Criteria;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Build;
import android.os.Bundle;
import android.os.Handler;
import android.os.ResultReceiver;
import android.provider.Settings;
import android.support.v4.app.ActivityCompat;
import android.support.v4.content.ContextCompat;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.text.TextUtils;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.google.android.gms.location.FusedLocationProviderClient;
import com.google.android.gms.location.LocationServices;
import com.google.android.gms.tasks.OnSuccessListener;

import org.w3c.dom.Text;

import java.util.ArrayList;

import edu.maine.seamans.ratemyairport.R;


public class MainActivity extends AppCompatActivity {

    private FusedLocationProviderClient mFusedLocationClient;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        mFusedLocationClient = LocationServices.getFusedLocationProviderClient(this);
        checkPermission();



        final Button button = findViewById(R.id.btnSendCoords);
        button.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                getCoords();


                // Code here executes on main thread after user presses button
            }
        });
    }

    protected void getCoords() {

        mFusedLocationClient.getLastLocation()
                .addOnSuccessListener(this, new OnSuccessListener<Location>() {
                    @Override
                    public void onSuccess(Location location) {
                        // Got last known location. In some rare situations this can be null.
                        if (location != null) {
                            double lat = location.getLatitude();
                            double lon = location.getLongitude();
                            System.out.println("HEAYYYAYY");
                            TextView tv = (TextView) findViewById(R.id.titleTextLat);
                            tv.setText("Latitude: " + lat);
                            tv = (TextView) findViewById(R.id.titleTextLon);
                            tv.setText("Longitude:" + lon);
                            RequestQueue queue = Volley.newRequestQueue(MainActivity.this);

                            String ip_addr = getResources().getString(R.string.ip_addr);
                            String url = "http://" + ip_addr + "/LocateAirports?lat=" + lat + "&lon=" + lon;

                            StringRequest stringRequest = new StringRequest(Request.Method.GET, url, new Response.Listener<String>() {
                                @Override
                                public void onResponse(String response) {
                                    String s = response;
                                    String noHtml = s.replaceAll("\\<.*?>","");
                                    String[] splitted = noHtml.split("\\|");

                                    String output = "";
                                    for (int i = 1 ; i < splitted.length; ++i)
                                    {
                                        output += i + " "+ splitted[i];
                                    }

                                    final ArrayList<String> list = new ArrayList<String>();
                                    for (int i = 0; i < splitted.length; ++i) {
                                        list.add(splitted[i]);
                                    }
                                    final ListView listview = (ListView) findViewById(R.id.listview);
                                    final ArrayAdapter adapter = new ArrayAdapter(MainActivity.this,
                                            android.R.layout.simple_list_item_1, list);
                                    listview.setAdapter(adapter);

                                    listview.setOnItemClickListener(new
                                                                            AdapterView.OnItemClickListener() {
                                                                                @Override
                                                                                public void onItemClick(AdapterView<?> parent, final View
                                                                                        view, int position, long id) {
                                                                                    final String item = (String)
                                                                                            parent.getItemAtPosition(position);
                                                                                    /*
                                                                                    Context context = getApplicationContext();
                                                                                    CharSequence text = item;
                                                                                    int duration = Toast.LENGTH_SHORT;

                                                                                    Toast toast = Toast.makeText(context, text, duration);
                                                                                    toast.show();
                                                                                    */
                                                                                    Intent myIntent = new Intent(MainActivity.this, AddOrEditReviewActivity.class);
                                                                                    myIntent.putExtra("airport", item); //Optional parameters
                                                                                    MainActivity.this.startActivity(myIntent);

                                                                                }
                                                                            });

                                }
                            }, new Response.ErrorListener() {
                                @Override
                                public void onErrorResponse(VolleyError error) {
                                    String s = "Error!";
                                }

                            });
                            queue.add(stringRequest);
                        }
                        // Logic to handle location object
                    }
                });
    }

    public void checkPermission(){
        if (ContextCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED ||
                ContextCompat.checkSelfPermission(this,Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED
                ){//Can add more as per requirement

            ActivityCompat.requestPermissions(this,
                    new String[]{Manifest.permission.ACCESS_FINE_LOCATION,Manifest.permission.ACCESS_COARSE_LOCATION},
                    123);
        }
    }
}