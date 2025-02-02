using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.DAL
{
    public class Candle
    {
        /// <summary>
        /// Цена открытия
        /// </summary>
        public long OpenPrice { get; set; }

        /// <summary>
        /// Максимальная цена
        /// </summary>
        public long HighPrice { get; set; }

        /// <summary>
        /// Минимальная цена
        /// </summary>
        public long LowPrice { get; set; }

        /// <summary>
        /// Цена закрытия
        /// </summary>
        public long ClosePrice { get; set; }


        /// <summary>
        /// Количество валюты, торгующейся за определённый период времени (frametime).
        /// </summary>
        public decimal Volume { get; set; }
        
        /// <summary>
        /// Миллисекундная отметка
        /// </summary>
        public long Mts { get; set; }

    }
}
