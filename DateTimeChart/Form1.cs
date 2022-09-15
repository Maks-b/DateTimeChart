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
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using DateTimeChart;
using ChartData.Repositories;

namespace DateTimeChart
{
    public partial class Form1 : Form
    {
        //Создаем два объекта DateTime для определения начальной и конечной даты
        public static DateTime startdate = new DateTime(2022, 1, 1);
        public static DateTime finaldate= new DateTime(2022, 12, 31);
        private readonly BBL.ChartService _chartService;
        public Form1()
        {
            InitializeComponent();
            ChartData.DBProvider dBProvider = new ChartData.DBProvider();
            dBProvider.Initialize();
            SunriseSunsetRepository sunriseSunsetRepository = new SunriseSunsetRepository(dBProvider);
            BBL.ChartService chartService= new BBL.ChartService(sunriseSunsetRepository);
            _chartService = chartService;
        }

        public void chart1_Click(object sender, EventArgs e)
        {

        }
        public void chart2_Click(object sender, EventArgs e)
        {

        }

        public void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Удаляем ранее построенные графики
            chart1.Series[0].Points.Clear();
            chart2.Series[0].Points.Clear();
            //Обрабатываем условие при котором начальная дата больше конечной
            if ((finaldate - startdate).Days > 0)
            {
                _chartService.ShowGraph(ChartData.Models.Enums.SunTimes.Sunrise, chart1);
                _chartService.ShowGraph(ChartData.Models.Enums.SunTimes.Sunset, chart2);
            }
            else
            {
                MessageBox.Show("Начальная дата больше конечной",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //Задаем значение начальной даты
            SunriseSunsetRepository.GetStartDate= dateTimePicker1.Value;
            startdate= dateTimePicker1.Value;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            //Задаем значение конечной даты
            SunriseSunsetRepository.GetFinalDate= dateTimePicker2.Value;
            finaldate= dateTimePicker2.Value;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart2.Series[0].Points.Clear();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

