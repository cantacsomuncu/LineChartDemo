using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using MikePhil.Charting.Charts;
using Java.Lang;
using System.Collections.Generic;
using MikePhil.Charting.Data;
using Android.Graphics;

namespace LineChartDemo
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape, MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            ChartLine();
        }




        public void ChartLine()
        {
            LineChart lineChart = (LineChart)FindViewById(Resource.Id.lineChart);
            lineChart.Legend.TextColor = Color.White;
            lineChart.AxisLeft.TextColor = Color.White;
            lineChart.AxisRight.TextColor = Color.White;
            lineChart.XAxis.TextColor = Color.White;
            lineChart.Description.Text = "";
            lineChart.SetScaleEnabled(false);
            lineChart.DragXEnabled = false;
            lineChart.DragYEnabled = false;

            Random rand = new Random();
            List<Entry> valsComp1 = new List<Entry>();
            for (int i = 0; i < 50; i++)
            {
                valsComp1.Add(new Entry(i, rand.Next(100)));
            }
            LineDataSet dataSet = new LineDataSet(valsComp1, "Basınç");
            dataSet.Colors = new List<Integer>() { (Integer)GetColor(Resource.Color.colorPlotRed) };
            dataSet.SetDrawValues(false);
            dataSet.SetDrawCircles(false);
            dataSet.SetDrawFilled(true);
            //dataSet.FillColor = Color.Red;
            //dataSet.SetMode(LineDataSet.Mode.CubicBezier);

            //dataSet.set(Axis.X)
            //var dataSets = new List<LineDataSet>();
            LineData data = new LineData(dataSet);
            lineChart.Data = data;
            lineChart.Invalidate();

            Thread t = new Thread(() =>
            {
                var start1 = 250f;
                while (true)
                {
                    var newX1 = start1++;
                    dataSet.AddEntry(new Entry(newX1, rand.Next(100)));
                    lineChart.NotifyDataSetChanged();
                    data.NotifyDataChanged();
                    lineChart.SetVisibleXRangeMaximum(200f);
                    lineChart.MoveViewTo(newX1, 0f, MikePhil.Charting.Components.YAxis.AxisDependency.Left);
                    Thread.Sleep(120);
                }
            });
            t.Start();
        }
    }
}
