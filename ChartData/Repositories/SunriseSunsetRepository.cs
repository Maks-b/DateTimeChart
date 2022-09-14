using ChartData.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ChartData.Models.Enums;

namespace ChartData.Repositories
{
    public class SunriseSunsetRepository
    {
        private readonly DataTableCollection _db;
        // Наименование таблицы из БД
        private const string TABLE_TIME_KEY = "Table";
        public SunriseSunsetRepository(DBProvider dbProvider)
        {
            _db = dbProvider.DB;
        }
        public Charts GetChartData(SunTimes sunTimesType)
        {
            List<DateTime> dateAxis = GetDateAxis();
            List<DateTime> timeAxis = GetTimeAxis(sunTimesType);

            Charts chartData = new Charts(dateAxis, timeAxis);

            return chartData;
        }

        // Метод возвращает список DateTime для координационной оси графика, которая содержит значение дат 
        private List<DateTime> GetDateAxis()
        {
            List<DateTime> dateAxis = new List<DateTime>();
            
            for (int i = 0; i <= (GetFinalDate - GetStartDate).Days; i++)
            {
                dateAxis.Add(GetStartDate.AddDays(i));
            }

            return dateAxis;
        }

        // Метод возвращает список DateTime для координационной оси графика, которая содержит значение времени 
        private List<DateTime> GetTimeAxis(SunTimes sunTimesType)
        {
            // Таблица хранит в себе данные из БД
            DataTable tableTime = _db[TABLE_TIME_KEY];
            // Коллекция содержит список строк выбранного столбца таблицы tableTime за соответствующий временной промежуток
            List<string> dateTimeToList = tableTime.AsEnumerable().Join(GetDateAxis(),
                row => new { x1 = row.Field<long>("NumberDay"), x2 = row.Field<long>("MonthId") },
                c => new { x1 = (long)c.Day, x2 = (long)c.Month },
                (row, c) => row.Field<string>($"{sunTimesType}")).ToList();

            // Коллекция содержит список данных TimeSpan преобразованный из коллекции dateTimeToList
            List<TimeSpan> dateTimeSpan = new List<TimeSpan>();
            for (int i = 0; i < dateTimeToList.Count; i++)
            {
                dateTimeSpan.Add(ConvertToTimeSpan(dateTimeToList.ToList()[i]));
            }
            // Коллекция хранит данные по времени и датам за определенный промежуток
            List<DateTime> timeAxis = new List<DateTime>();
            for (int i = 0; i < dateTimeSpan.Count; i++)
            {
                timeAxis.Add(GetDateAxis()[i].Add(dateTimeSpan[i]));
            }
            return timeAxis;
        }

        // Метод преобразует string в TimeSpan
        private TimeSpan ConvertToTimeSpan(string str)
        {
            return TimeSpan.Parse(str);
        }
        // Свойство содержит значение начальной даты
        public static DateTime GetStartDate { get; set; } = new DateTime(2022, 1, 1);
        // Свойство содержит значение конечной даты
        public static DateTime GetFinalDate { get; set; } = new DateTime(2022, 12, 31);

    }
}
