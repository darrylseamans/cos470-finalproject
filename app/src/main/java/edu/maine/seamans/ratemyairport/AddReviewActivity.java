package edu.maine.seamans.ratemyairport;

import android.content.Context;
import android.content.Intent;
import android.media.Rating;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;
import android.widget.RatingBar;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;

import java.net.URLEncoder;
import java.util.ArrayList;

public class AddReviewActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_add_review);
        Intent intent = getIntent();
        final String value = intent.getStringExtra("airport");

        final Button button = findViewById(R.id.btnSubmitReview);
        button.setOnClickListener(new View.OnClickListener() {
                                      public void onClick(View v) {

                                          RatingBar bar = findViewById(R.id.ratingBar);
                                          int rating = (int) bar.getRating();
                                          TextView tvReview = findViewById(R.id.textEditReview);
                                          String review = "" + tvReview.getText();
                                          review = review.replace("\n","").replace("\r","");
                                          String noHtml = "";

                                          try {

                                              RequestQueue queue = Volley.newRequestQueue(AddReviewActivity.this);
                                              String ip_addr = getResources().getString(R.string.ip_addr);
                                              String url = "http://" + ip_addr + "/SetAirportInfo?airport_name=" + URLEncoder.encode(value, "UTF-8") + "&comments=" + URLEncoder.encode(review) + "&custservice=" + rating; // URLEncoder.encode(value, "UTF-8");
                                              Toast toast = Toast.makeText(getApplicationContext(),url,Toast.LENGTH_LONG);
                                              toast.show();
                                              StringRequest stringRequest = new StringRequest(Request.Method.GET, url, new Response.Listener<String>() {
                                                  @Override
                                                  public void onResponse(String response) {
                                                      String s = response;
                                                      finish();

                                                  }
                                              }, new Response.ErrorListener() {
                                                  @Override
                                                  public void onErrorResponse(VolleyError error) {
                                                      String s = "Error!";
                                                      Toast toast = Toast.makeText(getApplicationContext(),error.toString(),Toast.LENGTH_LONG);
                                                      toast.show();
                                                  }

                                              });
                                              queue.add(stringRequest);
                                          } catch (Exception ex) {
                                              Context context = getApplicationContext();
                                              CharSequence text = ex.toString();
                                              int duration = Toast.LENGTH_LONG;
                                              Toast toast = Toast.makeText(context, text, duration);
                                              toast.show();
                                          }
                                      }
                                  });

        TextView tvAirport = findViewById(R.id.textReviewAirport);
        tvAirport.setText(value);
    }
}