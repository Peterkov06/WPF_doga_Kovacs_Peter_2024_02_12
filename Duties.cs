using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_doga_Kovács_Péter_2024_02_12
{
    public class Duties
    {
        public List<DataClass> dataList;

        public Duties(string path)
        {
            dataList = new List<DataClass>();

            using (FileStream fileStream = new(path, FileMode.Open))
            {
                using (StreamReader reader = new(fileStream))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(';');
                        dataList.Add(new(parts[0], Convert.ToInt32(parts[1]), Convert.ToInt32(parts[2]), Convert.ToInt32(parts[3])));
                    }
                }
            }

        }

        public void Task2()
        {
            int maxPower = dataList.Max(x => x.PowerOutput);
            var plants = dataList.Where(x => x.PowerOutput == maxPower).ToList();

            try
            {
                string maxPowerPlants = "Legnagyobb teljesítményű erőmű(vek): ";
                foreach (var plant in plants)
                {
                    maxPowerPlants += $"\n{plant.Location}: {plant.PowerOutput} W, {plant.StartYear}";
                }
                MessageBox.Show(maxPowerPlants);
            }
            catch (Exception)
            {
            }
        }

        public void Task3()
        {
            double avgPower = dataList.Where(x => x.StartYear == 2010).Average(x => x.PowerOutput);
            int partSum = dataList.Where(x => x.StartYear == 2010).Sum(x => x.Parts);

            MessageBox.Show($"A 2010-ben teleített erőművek:\nátlagteljesítménye: {Math.Round(avgPower, 2)}W\negységek összáma: {partSum}");
        }

    }
}
