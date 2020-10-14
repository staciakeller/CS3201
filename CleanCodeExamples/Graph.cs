namespace Graph.View
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Windows.Forms.DataVisualization.Charting;
    using Graph.Model;

    /// <summary>
    /// The form display a graph.
    /// </summary>
    public partial class Graph : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Graph"/> class.
        /// </summary>
        public Graph()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Loads the graph information.
        /// </summary>
        /// <param name="sender">Sending object.</param>
        /// <param name="e">Click on object.</param>
        private void Graph_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            this.RunReport();
            System.Windows.Forms.Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Runs the report chart data.
        /// </summary>
        private void RunReport()
        {
            // Clear Chart
            this.chart1.Series.Clear();
            this.chart1.Legends.Clear();
            this.chart1.ChartAreas.Clear();
            this.chart1.Titles.Clear();

            // Build Chart
            this.ChartSeries(1, "y = 2x");
            this.ChartSeries(2, "y = x^2 - 1");
            this.ChartAreas("Video");
            this.ChartTitle("Video");
            this.chart1.Invalidate();
        }

        /// <summary>
        /// Sets up the look and style of the chart, Areas.
        /// </summary>
        /// <param name="title">Title of the chart.</param>
        private void ChartAreas(string title)
        {
            var axisX = new System.Windows.Forms.DataVisualization.Charting.Axis
            {
                Interval = 1,
            };

            var axisY = new System.Windows.Forms.DataVisualization.Charting.Axis
            {
                Minimum = 0,
                Maximum = 110,
                Title = title,
            };

            var chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea
            {
                AxisX = axisX,
                AxisY = axisY,
            };

            this.chart1.ChartAreas.Add(chartArea1);
        }

        /// <summary>
        /// Sets up the look and style of the chart, Title.
        /// </summary>
        /// <param name="title">Title of the chart.</param>
        private void ChartTitle(string title)
        {
            var titles1 = new System.Windows.Forms.DataVisualization.Charting.Title
            {
                Name = title,
                Text = title + "Graph Data",
                Visible = true,
            };
            this.chart1.Titles.Add(titles1);
        }

        /// <summary>
        /// Sets up the look and style of the chart, Legends.
        /// </summary>
        /// <param name="name">Name of the chart data.</param>
        private void ChartLegends(string name)
        {
            var legends1 = new System.Windows.Forms.DataVisualization.Charting.Legend
            {
                Name = name,
            };
            this.chart1.Legends.Add(legends1);
        }

        /// <summary>
        /// Sets up the look and style of the chart, Series.
        /// </summary>
        /// <param name="data">The data type.</param>
        /// <param name="name">The name of the data.</param>
        private void ChartSeries(int data, string name)
        {
            var series1 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = name,
                Color = System.Drawing.Color.Blue,
                BorderWidth = 5,
                IsVisibleInLegend = true,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };

            for (int i = 0; i < 11; i++)
            {
                int yValue = 0;
                if (data == 1)
                {
                    yValue = Data.GetData1(i);
                }
                else if (data == 2)
                {
                    yValue = Data.GetData2(i);
                }

                series1.Points.AddXY(i, yValue);
            }

            this.chart1.Series.Add(series1);
            this.ChartLegends(name);
        }
    }
}