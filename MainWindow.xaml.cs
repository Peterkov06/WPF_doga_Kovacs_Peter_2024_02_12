using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace WPF_doga_Kovács_Péter_2024_02_12
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Duties tasks = new("szeleromu.txt");
            ListBox listBox = new ListBox();
            StackPanel stackPanel = new StackPanel();
            Grid.SetColumn(stackPanel, 1);
            MainGrid.Children.Add(stackPanel);
            Grid.SetColumn(listBox, 0);
            foreach (var element in tasks.dataList)
            {
                listBox.Items.Add(new ListBoxItem() { Content = $"{element.Location}: egységek száma: {element.Parts}, teljesítmény: {element.PowerOutput} W, beüzemelés: {element.StartYear}"});
            }

            MainGrid.Children.Add(listBox);
            Button task2Btn = new Button() { Content = "Legnagyobb teljesítményű erőmű", Margin = new Thickness(5) };
            task2Btn.Click += (s, e) => { tasks.Task2(); };
            stackPanel.Children.Add(task2Btn);

            Button task3Btn = new Button() { Content = "2010-es erőművek", Margin = new Thickness(5) };
            task3Btn.Click += (s, e) => { tasks.Task3(); };
            stackPanel.Children.Add(task3Btn);

            TextBox textBox = new TextBox() { ToolTip = "Max teljesítmény", Margin = new Thickness(5), Name="task4Inpt" };
            stackPanel.Children.Add(textBox);

            ListBox listBox1 = new();
            Grid.SetColumn(listBox1, 2);
            MainGrid.Children.Add(listBox1);

            Button task4Btn = new Button() { Content = "Keresés!", Margin = new Thickness(5) };
            stackPanel.Children.Add(task4Btn);
            Label label2 = new Label();
            stackPanel.Children.Add(label2);
            task4Btn.Click += (s, e) =>
            {
                try
                {
                    int length = 0;
                    Convert.ToInt32(textBox.Text);
                    listBox1.Items.Clear();
                    tasks.dataList.ForEach(element =>
                    {
                        ListBoxItem item = new() { Content = $"Egységek száma: {element.Parts}, teljesítmény: {element.PowerOutput} W" };

                        if (element.PowerOutput == Convert.ToInt32(textBox.Text))
                        {
                            item.Background = Brushes.Green;
                            item.Foreground = Brushes.White;
                            length++;   
                        }
                        listBox1.Items.Add(item);


                    });
                    label2.Content = length;
                }
                catch (Exception)
                {

                    MessageBox.Show("Hibás adatbevitel!");
                    
                }
            };

            Button task5Btn = new Button() { Content = "Átlagteljesítmény kategóriánként", Margin = new Thickness(5) };
            task5Btn.Click += (s, e) =>
            {
                Dictionary<char, double> avgs = new();
                var groups = tasks.dataList.GroupBy(x => x.Category());
                foreach ( var group in groups )
                {
                    avgs[group.Key] = group.Average(x => x.PowerOutput);
                }
                MessageBox.Show($"'A' kategória: {Math.Round(avgs['A'], 2)}, 'B' kategória: {Math.Round(avgs['B'], 2)}, 'C' kategória: {Math.Round(avgs['C'], 2)}");
            };
            stackPanel.Children.Add(task5Btn);

            TextBox textBox2 = new TextBox() { ToolTip = "Helyszín", Margin = new Thickness(5)};
            stackPanel.Children.Add(textBox2);
            Button task7Btn = new Button() { Content = "Keresés hely alapján!", Margin = new Thickness(5) };
            stackPanel.Children.Add(task7Btn);

            Label label1 = new Label();
            stackPanel.Children.Add(label1);
            task7Btn.Click += (s, e) =>
            {
                try
                {
                    int parts = 0;
                    listBox1.Items.Clear();
                    var items = tasks.dataList.Where(x => x.Location.Contains(textBox2.Text)).ToList();
                    items.ForEach(item => 
                    {
                        listBox1.Items.Add((new ListBoxItem() { Content = $"{item.Location}: egységek száma: {item.Parts}, teljesítmény: {item.PowerOutput} W, beüzemelés: {item.StartYear}" }));
                        parts += item.Parts;
                    });
                    label1.Content = "Helynek megfelelt egységek összáma:" + " " + parts.ToString();
                }
                catch (Exception)
                { 
                    MessageBox.Show("Hibás adatbevitel!");
                }
            };


        }
    }
}