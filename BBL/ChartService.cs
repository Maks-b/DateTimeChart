using System;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

namespace BBL
{
    public class ChartService
    {
        private readonly ChartData.Repositories.SunriseSunsetRepository _sunriseSunsetRepository;

        public ChartService(ChartData.Repositories.SunriseSunsetRepository sunriseSunsetRepository)
        {
            _sunriseSunsetRepository = sunriseSunsetRepository;
        }

        // Метод выполняет построение графика 
        public void ShowGraph(ChartData.Models.Enums.SunTimes sunTimesType, Chart chart)
        {
            SetGraph(chart);

            ChartData.Models.Charts chartData = _sunriseSunsetRepository.GetChartData(sunTimesType);

            List<DateTime> dataX = chartData.DateAxis;
            List<DateTime> dataY = chartData.TimeAxis;

            for (int i = 0; i < dataX.Count; i++)
            {
                chart.Series[0].Points.AddXY(dataX[i], dataY[i]);
            }
        }
        // Метод задает наименование осей, назначает тип данных для оси X,Y 
        private void SetGraph(Chart chart)
        {
            chart.Series[0].XValueType = ChartValueType.Date;
            chart.Series[0].YValueType = ChartValueType.Time;
            chart.ChartAreas[0].AxisX.LabelStyle.Format = "dd.MM";
            chart.ChartAreas[0].AxisX.Title = "Дата";
            chart.ChartAreas[0].AxisY.Title = "Время";
        }
        
    }
}
