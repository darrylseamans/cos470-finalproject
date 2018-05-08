package edu.maine.seamans.ratemyairport;

import android.content.Context;
import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
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

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.URL;
import java.net.URLEncoder;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;

public class AddOrEditReviewActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_add_or_edit_review);
        Intent intent = getIntent();
        final String value = intent.getStringExtra("airport");

        final Button button = findViewById(R.id.btnAddReview);
        button.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                Intent myIntent = new Intent(AddOrEditReviewActivity.this, AddReviewActivity.class);
                myIntent.putExtra("airport", value); //Optional parameters
                AddOrEditReviewActivity.this.startActivity(myIntent);
            }
        });

        final TextView tvAirport = findViewById(R.id.textAirport);
        tvAirport.setText(value);
        String noHtml = "";

        try {

            RequestQueue queue = Volley.newRequestQueue(AddOrEditReviewActivity.this);
            String ip_addr = getResources().getString(R.string.ip_addr);
            String url =  "http://" + ip_addr + "/AirportInfo?airport=" +  URLEncoder.encode(value,"UTF-8"); // URLEncoder.encode(value, "UTF-8");

            StringRequest stringRequest = new StringRequest(Request.Method.GET, url, new Response.Listener<String>() {
                @Override
                public void onResponse(String response) {
                    String s = response;
                    String noHtml = s.replaceAll("\\<.*?>","");
                    String[] splitted = noHtml.split("\\|");

                    String output = "";
                    for (int i = 1 ; i < splitted.length; ++i) {
                        output += i + " " + splitted[i];
                    }

                    final ArrayList<String> list = new ArrayList<String>();

                    for (int i = 1; i < splitted.length; ++i) {
                        System.out.println("THIS ONE:" + splitted[i]);
                        String[] parts = splitted[i].split(",");
                        System.out.println("LENGTH: " + parts.length);
                        int unixTime = Integer.parseInt(parts[7].replace("\n","").replace("\r",""));

                        Date date = new java.util.Date(unixTime*1000L);

                        SimpleDateFormat sdf = new java.text.SimpleDateFormat("yyyy-MM-dd HH:mm:ss z");
                        sdf.setTimeZone(java.util.TimeZone.getTimeZone("GMT-4"));
                        String formattedDate = sdf.format(date);
                        String fancyOutput = formattedDate + " - (" + parts[2] + "/5) - " + parts[6];
                        list.add(fancyOutput);
                    }
                    final ListView listview = (ListView) findViewById(R.id.listViewReviews);
                    final ArrayAdapter adapter = new ArrayAdapter(AddOrEditReviewActivity.this,
                            android.R.layout.simple_list_item_1, list);
                    listview.setAdapter(adapter);


                }
            }, new Response.ErrorListener() {
                @Override
                public void onErrorResponse(VolleyError error) {
                    String s = "Error!";
                }

            });
            queue.add(stringRequest);
        }
        catch(Exception ex) {
            Context context = getApplicationContext();
            CharSequence text = ex.toString();
            int duration = Toast.LENGTH_LONG;
            Toast toast = Toast.makeText(context, text, duration);
            toast.show();
        }
    }
}
