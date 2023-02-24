namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var mathHerlper = new MathHelper();
            label5.Text = "x";
            label6.Text = "P(x)";
            label7.Text = "x";
            label8.Text = "F(x)";
            List<double> data = new List<double>();

            foreach (var line in File.ReadAllLines(mathHerlper.Path))
            {
                try
                {
                    var text = line.Replace(".", ",");
                    var number = Convert.ToDouble(text);
                    data.Add(number);

                }
                catch (Exception)
                {
                    Console.WriteLine(line + " is not number");
                }
            }
            const int lenght = 50;
            var max = (int)data.Max() + 5;
            var min = (int)data.Min() - 5;
            double range = max - min;
            var volume = data.Count();
            var middle = data.Sum() / volume;
            double[] Vx = new double[lenght - 1];
            double[] Vy = new double[lenght - 1];

            var delta = range / (lenght - 1);

            Vx[0] = min + delta / 2;

            Vx[lenght - 2] = max - delta / 2;
            for (int i = 1; i < lenght - 2; i++)
            {
                Vx[i] = (min + delta * i);
            }
            foreach (var item in data)
            {
                for (int i = 0; i < lenght - 1; i++)
                {
                    if (item >= Vx[i] && item < Vx[i] + delta)
                    {
                        Vy[i]++;
                    }
                }
            }
                        for (int i = 0; i < Vy.Length; i++)
            {
                Vy[i] = Vy[i] / (data.Count());
            }
            label1.Text = $"max - {data.Max()}";
            label2.Text = $"min - {data.Min()}";
            label3.Text = $"мода - {GetModaAsString(data)}";
            chart1.Series["Series1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["Series1"].Points.DataBindXY(Vx, Vy);


            List<double> dataS = new List<double>();

            foreach (var line in File.ReadAllLines(mathHerlper.Path))
            {
                try
                {
                    var text = line.Replace(".", ",");
                    var number = Convert.ToDouble(text);
                    dataS.Add(number);

                }
                catch (Exception)
                {
                    Console.WriteLine(line + " is not number");
                }
            }
            dataS.Sort();
            const int lenghtS = 50;
            var maxS = (int)dataS.Max() + 5;
            var minS = (int)dataS.Min() - 5;
            double rangeS = maxS - minS;
            var volumeS = dataS.Count();
            var middleS = dataS.Sum() / volumeS;
            double[] VxS = new double[lenghtS - 1];
            double[] VyS = new double[lenghtS - 1];

            var deltaS = rangeS / (lenghtS - 1);

            VxS[0] = minS + deltaS / 2;

            VxS[lenghtS - 2] = maxS - deltaS / 2;
            for (int i = 1; i < lenghtS - 2; i++)
            {
                VxS[i] = (minS + deltaS * i);
            }
            foreach (var item in dataS)
            {
                for (int i = 0; i < lenghtS - 1; i++)
                {
                    if (item >= VxS[i] && item < VxS[i] + deltaS)
                    {
                        VyS[i]++;
                    }
                }
            }
            VyS[0] = VyS[0] / (dataS.Count());
            for (int i = 1; i < VyS.Length; i++)
            {
                VyS[i] += VyS[i - 1];
            }

            for (int i = 0; i < VyS.Length; i++)
            {
                VyS[i] = VyS[i] / (dataS.Count());
            }

            chart2.Series["Series1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart2.Series["Series1"].Points.DataBindXY(VxS, VyS);


            mathHerlper.WriteAns(label4);
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        static List<double> GetModa(List<double> data)
        {
            var dict = new Dictionary<double, int>();
            var result = new List<double>();
            foreach (var item in data)
            {
                dict[item] += 1;
            }
            var maxCount = dict.Values.Max();
            foreach (var item in dict.Keys)
            {
                if (dict[item] == maxCount && result.Contains(dict[item]))
                {
                    result.Add(dict[item]);
                }
            }
            return result;
        }
        static string GetModaAsString(List<double> data)
        {
            var dict = new Dictionary<double, int>();
            var result = new List<double>();
            foreach (var item in data)
            {
                if (!dict.ContainsKey(item))
                {
                    dict.Add(item, 0);
                }
                dict[item] += 1;
            }
            var maxCount = dict.Values.Max();
            foreach (var item in dict.Keys)
            {
                if (dict[item] == maxCount && !result.Contains(dict[item]))
                {
                    result.Add(item);
                }
            }
            string resultString = "";
            foreach (var item in result)
            {
                resultString += $" {item};";
            }
            return resultString;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}