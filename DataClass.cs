using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_doga_Kovács_Péter_2024_02_12
{
    public class DataClass
    {
        public string Location { get; set; }
        public int Parts { get; set; }
        public int PowerOutput { get; set; }
        public int StartYear { get; set; }

        public DataClass(string location, int parts, int powerOutput, int startYear)
        {
            Location = location ?? throw new ArgumentNullException(nameof(location));
            Parts = parts;
            PowerOutput = powerOutput;
            StartYear = startYear;
        }

        public char Category()
        {
            char category;
            if (PowerOutput >= 1000)
            {
                category = 'A';
            }
            else if (PowerOutput > 500)
            {
                category = 'B';
            }
            else
            {
                category = 'C';
            }
            return category;
        }
    }
}
