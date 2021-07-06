using System;
using System.Collections.Generic;
using System.Linq;
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
using System.IO;
using System.Globalization;
using System.Drawing.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Neural_Network_Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            CreateALine();

            Perceptrons.NeuralPerceptron neuron = new Perceptrons.NeuralPerceptron();

            neuron.setup();
            neuron.train();
            neuron.train();
            neuron.train();
            neuron.train();
            neuron.train();


            neuron.results(canvas1);



        }

        double f(double x)
        {
            return 2 * x + 1;
        }

        public void CreateALine()
        {
            canvas1.Children.Clear();

            // Create a Line
            Line redLine = new Line();
            redLine.X1 = -320 + 640 / 2.0;
            redLine.Y1 = f(-320) + 360 / 2.0;
            redLine.X2 = 640 + 640 / 2.0;
            redLine.Y2 = f(640) + 360 / 2.0;

            // Create a red Brush
            SolidColorBrush redBrush = new SolidColorBrush();
            redBrush.Color = Colors.Red;

            // Set Line's width and color
            redLine.StrokeThickness = 1;
            redLine.Stroke = redBrush;

            // Add line to the Grid.
            canvas1.Children.Add(redLine);
        }


        NeuralNework n = null; //new NeuralNework();

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (n == null)
            {
                n = new NeuralNework();
                n.setup();
            }

            n.feed();




        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            // test icin 100 nolu goruntuyu olustur
            int index = 0;

            byte[] resim = new byte[784];
            for (int i = 0; i < 784; i++)
                resim[i] = (byte)(train_data[index, i] * 256);

            CreateImage(resim);

        }





        //float[,] train_data = new float[50000,784];

        float[] train_data_answer = null;
        float[,] train_data = null;

        float[] test_data_answer = null;
        float[,] test_data = null;

        float[] val_data_answer = null;
        float[,] val_data = null;

        private void LoadDataResult()
        {
            byte[] fileBytes = File.ReadAllBytes(@"C:\PythonProjects\mnist\dataR.dat");

            // create a second float array and copy the bytes into it...
            train_data_answer = new float[50000];
            Buffer.BlockCopy(fileBytes, 0, train_data_answer, 0, fileBytes.Length);
        }

        private void LoadData()
        {
            byte[] fileBytes = File.ReadAllBytes(@"C:\PythonProjects\mnist\data.dat");
            // create a second float array and copy the bytes into it...
            train_data = new float[50000, 784];
            Buffer.BlockCopy(fileBytes, 0, train_data, 0, fileBytes.Length);

            // test icin 100 nolu goruntuyu olustur
            int index = 100;

            byte[] resim = new byte[784];
            for (int i = 0; i < 784; i++)
                resim[i] = (byte)(train_data[index, i] * 256);

            CreateImage(resim);
        }


        private void CreateImage(byte[] array)
        {
            // create image
            int width = 28;
            int height = 28;
            //byte[] array = new byte[height * width];

            BitmapSource bitmap = BitmapSource.Create(width, height, 96, 96,
                PixelFormats.Gray8,
                BitmapPalettes.Gray256, array, width);

            // komple siyah background resim
            image1.Source = bitmap;
        }

        private void LoadTestResult()
        {
            byte[] fileBytes = File.ReadAllBytes(@"C:\PythonProjects\mnist\testR.dat");

            // create a second float array and copy the bytes into it...
            test_data_answer = new float[10000];
            Buffer.BlockCopy(fileBytes, 0, test_data_answer, 0, fileBytes.Length);
        }

        private void LoadTestData()
        {
            byte[] fileBytes = File.ReadAllBytes(@"C:\PythonProjects\mnist\test.dat");
            // create a second float array and copy the bytes into it...
            test_data = new float[10000, 784];
            Buffer.BlockCopy(fileBytes, 0, test_data, 0, fileBytes.Length);
        }

        private void LoadValResult()
        {
            byte[] fileBytes = File.ReadAllBytes(@"C:\PythonProjects\mnist\valR.dat");

            // create a second float array and copy the bytes into it...
            val_data_answer = new float[10000];
            Buffer.BlockCopy(fileBytes, 0, val_data_answer, 0, fileBytes.Length);
        }

        private void LoadValData()
        {
            byte[] fileBytes = File.ReadAllBytes(@"C:\PythonProjects\mnist\val.dat");
            // create a second float array and copy the bytes into it...
            val_data = new float[10000, 784];
            Buffer.BlockCopy(fileBytes, 0, val_data, 0, fileBytes.Length);
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            // 2 inputs
            // 3 first layer neuron
            // 1 output layer neuron
            //

            NeuralLogicCircuit.Neuron f_a = new NeuralLogicCircuit.Neuron(2, "input 1");
            NeuralLogicCircuit.Neuron f_b = new NeuralLogicCircuit.Neuron(2, "input 2");
            NeuralLogicCircuit.Neuron f_c = new NeuralLogicCircuit.Neuron(2, "input 3");
            NeuralLogicCircuit.Neuron f_d = new NeuralLogicCircuit.Neuron(2, "input 4");

            int first_layer_neuron_count = 4;

            NeuralLogicCircuit.Neuron s_a = new NeuralLogicCircuit.Neuron(first_layer_neuron_count, "output 1");
            NeuralLogicCircuit.Neuron s_b = new NeuralLogicCircuit.Neuron(first_layer_neuron_count, "output 2");
            NeuralLogicCircuit.Neuron s_c = new NeuralLogicCircuit.Neuron(first_layer_neuron_count, "output 3");
            NeuralLogicCircuit.Neuron s_d = new NeuralLogicCircuit.Neuron(first_layer_neuron_count, "output 4");

            f_a.Connect(s_a);
            f_b.Connect(s_a);
            f_c.Connect(s_a);
            f_d.Connect(s_a);

            f_a.Connect(s_b);
            f_b.Connect(s_b);
            f_c.Connect(s_b);
            f_d.Connect(s_b);

            f_a.Connect(s_c);
            f_b.Connect(s_c);
            f_c.Connect(s_c);
            f_d.Connect(s_c);

            f_a.Connect(s_d);
            f_b.Connect(s_d);
            f_c.Connect(s_d);
            f_d.Connect(s_d);

            Random rnd = new Random(Guid.NewGuid().GetHashCode());

            Logic logic1 = Logic.xor;
            Logic logic2 = Logic.and;
            Logic logic3 = Logic.or;
            Logic logic4 = Logic.logic1;

            for (int i = 0; i < 100000; i++)
            {
                double[] input = { rnd.NextDouble(), rnd.NextDouble() };

                int b0 = (int)Math.Round(input[0]);
                int b1 = (int)Math.Round(input[1]);

                // desired results
                double cir1 = logic_circuit(b0, b1, logic1);
                double cir2 = logic_circuit(b0, b1, logic2);
                double cir3 = logic_circuit(b0, b1, logic3);
                double cir4 = logic_circuit(b0, b1, logic4);

                //input[0] = (double)b0;
                //input[1] = (double)b1;

                f_a.train(input);
                f_b.train(input);
                f_c.train(input);
                f_d.train(input);

                // train the non-input nodes
                s_a.train();
                s_b.train();
                s_c.train();
                s_d.train();

                // train the output node
                // this will trigger back propagation
                s_a.backprop_output(cir1);
                s_b.backprop_output(cir2);
                s_c.backprop_output(cir3);
                s_d.backprop_output(cir4);

            }

            // sonucu test edelim


            int dogru1 = 0;
            int dogru2 = 0;
            int dogru3 = 0;
            int dogru4 = 0;

            for (int i = 0; i < 1000000; i++)
            {
                double[] input = { rnd.NextDouble(), rnd.NextDouble() };

                int b0 = (int)Math.Round(input[0]);
                int b1 = (int)Math.Round(input[1]);

                input[0] = (double)b0;
                input[1] = (double)b1;

                double cir1 = logic_circuit(b0, b1, logic1);
                double cir2 = logic_circuit(b0, b1, logic2);
                double cir3 = logic_circuit(b0, b1, logic3);
                double cir4 = logic_circuit(b0, b1, logic4);

                f_a.train(input);
                f_b.train(input);
                f_c.train(input);
                f_d.train(input);

                double result1 = s_a.result();
                double result2 = s_b.result();
                double result3 = s_c.result();
                double result4 = s_d.result();

                int r1 = (int)Math.Round(result1);
                int r2 = (int)Math.Round(result2);
                int r3 = (int)Math.Round(result3);
                int r4 = (int)Math.Round(result4);

                if (r1 == cir1)
                    dogru1++;

                if (r2 == cir2)
                    dogru2++;

                if (r3 == cir3)
                    dogru3++;

                if (r4 == cir4)
                    dogru4++;

            }



        }

        enum Logic { xor, and, or, logic1 };

        private double logic_circuit(int b0, int b1, Logic logic)
        {
            // xor
            double res = 0;
            int pivot;

            switch (logic)
            {
                case Logic.xor:
                    pivot = b0 + b1;
                    res = pivot == 1 ? 1.0 : 0.0;
                    break;
                case Logic.and:
                    pivot = b0 * b1;
                    res = pivot > 0 ? 1.0 : 0.0;
                    break;
                case Logic.or:
                    pivot = b0 + b1;
                    res = pivot > 0 ? 1.0 : 0.0;
                    break;
                case Logic.logic1:
                    if (b0 > 0)
                        return 1;
                    return b1;
                    break;
                default:
                    break;
            }

            return res;

        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            // 2 inputs
            // 3 first layer neuron
            // 1 output layer neuron
            //

            List<NeuralLogicCircuit.Neuron> first_layer = new List<NeuralLogicCircuit.Neuron>();

            int first_layer_neuron_count = 4;
            int second_layer_neuron_count = 4;

            for (int i = 0; i < first_layer_neuron_count; i++)
            {
                NeuralLogicCircuit.Neuron neuron = new NeuralLogicCircuit.Neuron(2, "input " + i.ToString());
                first_layer.Add(neuron);
            }

            List<NeuralLogicCircuit.Neuron> second_layer = new List<NeuralLogicCircuit.Neuron>();

            for (int i = 0; i < second_layer_neuron_count; i++)
            {
                NeuralLogicCircuit.Neuron neuron = new NeuralLogicCircuit.Neuron(first_layer_neuron_count, "output " + i.ToString());
                second_layer.Add(neuron);
            }

            for (int i = 0; i < first_layer_neuron_count; i++)
                for (int j = 0; j < second_layer_neuron_count; j++)
                    first_layer[i].Connect(second_layer[j]);


            Random rnd = new Random(Guid.NewGuid().GetHashCode());

            Logic logic1 = Logic.xor;
            Logic logic2 = Logic.and;
            Logic logic3 = Logic.or;
            Logic logic4 = Logic.logic1;

            for (int i = 0; i < 100000; i++)
            {
                double[] input = { rnd.NextDouble(), rnd.NextDouble() };

                int b0 = (int)Math.Round(input[0]);
                int b1 = (int)Math.Round(input[1]);

                // desired results
                double cir1 = logic_circuit(b0, b1, logic1);
                double cir2 = logic_circuit(b0, b1, logic2);
                double cir3 = logic_circuit(b0, b1, logic3);
                double cir4 = logic_circuit(b0, b1, logic4);

                //input[0] = (double)b0;
                //input[1] = (double)b1;

                for (int j = 0; j < first_layer_neuron_count; j++)
                    first_layer[j].train(input);

                // train the non-input nodes
                for (int j = 0; j < second_layer_neuron_count; j++)
                    second_layer[j].train();

                // train the output node
                // this will trigger back propagation
                second_layer[0].backprop_output(cir1);
                second_layer[1].backprop_output(cir2);
                second_layer[2].backprop_output(cir3);
                second_layer[3].backprop_output(cir4);

            }

            // sonucu test edelim


            int dogru1 = 0;
            int dogru2 = 0;
            int dogru3 = 0;
            int dogru4 = 0;

            for (int i = 0; i < 1000000; i++)
            {
                double[] input = { rnd.NextDouble(), rnd.NextDouble() };

                int b0 = (int)Math.Round(input[0]);
                int b1 = (int)Math.Round(input[1]);

                input[0] = (double)b0;
                input[1] = (double)b1;

                double cir1 = logic_circuit(b0, b1, logic1);
                double cir2 = logic_circuit(b0, b1, logic2);
                double cir3 = logic_circuit(b0, b1, logic3);
                double cir4 = logic_circuit(b0, b1, logic4);

                for (int j = 0; j < 4; j++)
                    first_layer[j].train(input);

                double result1 = second_layer[0].result();
                double result2 = second_layer[1].result();
                double result3 = second_layer[2].result();
                double result4 = second_layer[3].result();

                int r1 = (int)Math.Round(result1);
                int r2 = (int)Math.Round(result2);
                int r3 = (int)Math.Round(result3);
                int r4 = (int)Math.Round(result4);

                if (r1 == cir1)
                    dogru1++;

                if (r2 == cir2)
                    dogru2++;

                if (r3 == cir3)
                    dogru3++;

                if (r4 == cir4)
                    dogru4++;

            }

        }




        //public static class ExtensionMethods
        //{
        //    private static Action EmptyDelegate = delegate() { };

        //    public static void Refresh(this UIElement uiElement)
        //    {
        //        uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
        //    }
        //}


        List<NeuralLogicCircuit.Neuron> wf_first_layer = new List<NeuralLogicCircuit.Neuron>();
        List<NeuralLogicCircuit.Neuron> wf_second_layer = new List<NeuralLogicCircuit.Neuron>();

        // TRAIN NEURAL NETWORK
        private void OCRTrain()
        {
            //float[] train_data_answer = null;
            //float[,] train_data = null;

            //float[] test_data_answer = null;
            //float[,] test_data = null;

            //float[] val_data_answer = null;
            //float[,] val_data = null;

            // 784 inputs
            // 15 first layer neuron
            // 10 output layer neuron
            //


            int first_layer_neuron_count = 15;
            int second_layer_neuron_count = 10;

            for (int i = 0; i < first_layer_neuron_count; i++)
            {
                NeuralLogicCircuit.Neuron neuron = new NeuralLogicCircuit.Neuron(784, "input " + i.ToString());
                wf_first_layer.Add(neuron);
            }


            for (int i = 0; i < second_layer_neuron_count; i++)
            {
                NeuralLogicCircuit.Neuron neuron = new NeuralLogicCircuit.Neuron(first_layer_neuron_count, "output " + i.ToString());
                wf_second_layer.Add(neuron);
            }

            for (int i = 0; i < first_layer_neuron_count; i++)
                for (int j = 0; j < second_layer_neuron_count; j++)
                    wf_first_layer[i].Connect(wf_second_layer[j]);



            // ayni sayilarla birden fazla defa train ettir
            // 100 defa tekrar train
            for (int train_count = 0; train_count < 10; train_count++)
            {

                for (int data_index = 0; data_index < 50000; data_index++)
                {
                    double[] input = new double[784];

                    for (int j = 0; j < 784; j++)
                        input[j] = train_data[data_index, j];

                    string temp = String.Format("{0} {1}", train_count, data_index);
                    //// non blocking UP refresh
                    this.Dispatcher.BeginInvoke(new Action(() => this.textBox3.Text = temp), null);

                    //var currentProgress = i;
                    //Dispatcher.BeginInvoke(new Action(() =>
                    //{
                    //    myLabel.Content = "Updating..." + currentProgress;
                    //}), DispatcherPriority.Background);  


                    for (int j = 0; j < first_layer_neuron_count; j++)
                        wf_first_layer[j].train(input);

                    // train the non-input nodes
                    for (int j = 0; j < second_layer_neuron_count; j++)
                        wf_second_layer[j].train();

                    // train the output node
                    // this will trigger back propagation
                    // expected result
                    double outval = train_data_answer[data_index];
                    //
                    for (int j = 0; j < second_layer_neuron_count; j++)
                    {
                        double turn_on_off = (outval == j) ? 1.0 : 0.0;
                        wf_second_layer[j].backprop_output(turn_on_off);
                    }
                }

            }


            // sonucu test edelim

            if (1 == 1)
            {

                int[] dogru = new int[10];

                for (int data_index_test = 0; data_index_test < 50000; data_index_test++)
                {
                    double[] input = new double[784];

                    for (int j = 0; j < 784; j++)
                        input[j] = train_data[data_index_test, j];

                    for (int j = 0; j < first_layer_neuron_count; j++)
                        wf_first_layer[j].train(input);

                    double[] result = new double[10];
                    double max = 0;
                    int max_index = 0;
                    for (int j = 0; j < second_layer_neuron_count; j++)
                    {
                        result[j] = wf_second_layer[j].result();
                        if (result[j] > max)
                        {
                            max = result[j];
                            max_index = j;
                        }
                    }

                    dogru[max_index]++;


                }
            }

        }


        byte[] array = new byte[28 * 28];


        private void GenerateImage(int font_index, string text)
        {
            for (int x = 0; x < 28; x++)
                for (int y = 0; y < 28; y++)
                    array[28 * y + x] = (byte)(0);

            BitmapSource bitmap = BitmapSource.Create(28, 28, 96, 96,
                PixelFormats.Gray8,
                BitmapPalettes.Gray256, array, 28);

            // komple siyah background resim
            //image1.Source = bitmap;

            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext dc = dv.RenderOpen())
            {
                dc.DrawImage(bitmap, new Rect(0, 0, bitmap.Width, bitmap.Height));
                DrawString(dc, font_index, text);
                //dc.DrawLine(new Pen(Brushes.White, 2), new Point(0, 0), new Point(2, 0));
                dc.Close();
            }

            RenderTargetBitmap rtb = new RenderTargetBitmap((int)bitmap.Width, (int)bitmap.Height, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(dv);

            // uzerinde yazi olan "atb" bir resim.
            //image1.Source = rtb;
            //return;

            // ekranda goruntu olustu...  
            // bunu BW yap


            MemoryStream file = new MemoryStream();
            BmpBitmapEncoder encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));
            encoder.Save(file);
            file.Seek(0, SeekOrigin.Begin);
            file.Flush();
            byte[] bindata = file.ToArray();

            int indexer = bindata.Length - (28 * 28 * 4);

            int stride = bindata.Length / 4;

            for (int x = 0; x < 28; x++)
            {
                for (int y = 0; y < 28; y++)
                {
                    byte b = bindata[indexer + (28 * 4) * (27 - y) + x * 4];

                    if (b > 0)
                        array[28 * y + x] = (byte)(255);
                    else
                        array[28 * y + x] = (byte)(0);
                }
            }
        }

        private void DrawString(DrawingContext drawingContext, int font_index, string text)
        {
            string testString = text;// "0";    // char (176)

            string font_name = fonts[font_index];
            int font_size = 20;

            if (font_name == "Comic Sans MS")
                font_size = 15;

            // Create the initial formatted text string.
            FormattedText formattedText = new FormattedText(
                testString,
                CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight,
                /*new Typeface("Verdana"),*/
                new Typeface(fonts[font_index]),
                font_size,
                Brushes.LightGray);

            // Set a maximum width and height. If the text overflows these values, an ellipsis "..." appears.
            formattedText.MaxTextWidth = 26;
            formattedText.MaxTextHeight = 26;


            // Use a larger font size beginning at the first (zero-based) character and continuing for 5 characters.
            // The font size is calculated in terms of points -- not as device-independent pixels.
            //formattedText.SetFontSize(36 * (96.0 / 72.0));
            /*
            // Use a Bold font weight beginning at the 6th character and continuing for 11 characters.
            formattedText.SetFontWeight(FontWeights.Bold, 6, 11);
            */

            /*
            // Use a linear gradient brush beginning at the 6th character and continuing for 11 characters.
            formattedText.SetForegroundBrush(
                                    new LinearGradientBrush(
                                    Colors.Orange,
                                    Colors.Red,
                                    90.0));
            */

            Color ObjCol = System.Windows.Media.Colors.White;

            formattedText.SetForegroundBrush(new SolidColorBrush(ObjCol));


            /*
            // Use an Italic font style beginning at the 28th character and continuing for 28 characters.
            formattedText.SetFontStyle(FontStyles.Italic, 28, 28);
            */

            // Draw the formatted text string to the DrawingContext of the control.
            drawingContext.DrawText(formattedText, new Point(1, 1));
            //drawingContext.DrawRectangle(new SolidColorBrush(ObjectColor), null, new Rect(x1 - 2, y1 - 2, 4, 4));
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            LoadTrainOCRData();

            // TRAIN NEURAL NETWORK
            //OCRTrain();
            // 
            // put into a task so we can refresh the UI
            Task.Factory.StartNew(() => OCRTrain());

        }

        bool bDataLoaded = false;

        private void LoadTrainOCRData()
        {
            if (bDataLoaded == false)
            {
                LoadData();
                LoadDataResult();

                LoadTestData();
                LoadTestResult();

                LoadValData();
                LoadValResult();

                bDataLoaded = true;
            }
        }

        List<string> fonts = new List<string>();

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            return;

            fonts = new List<string>();
            fonts.Add("Arial");
            fonts.Add("Verdana");
            fonts.Add("Comic Sans MS");



            // create image
            //byte[] array = new byte[28 * 28];

            train_data = new float[fonts.Count() * 10, 784];

            int index = 0;
            for (int i = 0; i < fonts.Count; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    GenerateImage(i, j.ToString());

                    for (int k = 0; k < 784; k++)
                    {
                        train_data[index, k] = (float)(array[k] / 255.0);
                    }

                    index++;
                }
            }


            BitmapSource bitmap2 = BitmapSource.Create(28, 28, 96, 96,
            PixelFormats.Gray8,
            BitmapPalettes.Gray256, array, 28);

            image1.Source = bitmap2;






            //InstalledFontCollection fontsCollection = new InstalledFontCollection();
            //System.Drawing.FontFamily[] fontFamilies = fontsCollection.Families;
            //foreach (System.Drawing.FontFamily font in fontFamilies)
            //{
            //    if (font.Name.Contains("ding") == true)
            //        continue;

            //    if (font.Name.Contains("Arabic") == true)
            //        continue;

            //    if (font.Name.Contains("Reference") == true)
            //        continue;

            //    if (font.Name.Contains("Extra") == true)
            //        continue;

            //    if (font.Name.Contains("Outlook") == true)
            //        continue;

            //    if (font.Name.Contains("ZWAdobe") == true)
            //        continue;

            //    if (font.Name.Contains("Arial Unicode") == true)
            //        continue;

            //    if (font.Name.Contains("Arial Rounded") == true)
            //        continue;

            //    if (font.Name.Contains("BatangChe") == true)
            //        continue;

            //    fonts.Add(font.Name);
            //}


        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button10_Click(object sender, RoutedEventArgs e)
        {
            // sonucu test edelim

            int first_layer_neuron_count = 15;
            int second_layer_neuron_count = 10;

            int[] dogru = new int[10];

            int data_index_test = Convert.ToInt32(textBox1.Text);
            //for (int data_index_test = 0; data_index_test < 10; data_index_test++)
            {
                for (int i = 0; i < 1; i++)
                {
                    double[] input = new double[784];

                    for (int j = 0; j < 784; j++)
                        input[j] = train_data[data_index_test, j];

                    for (int j = 0; j < first_layer_neuron_count; j++)
                        wf_first_layer[j].train(input);

                    // resim olustur basla
                    byte[] resim = new byte[784];
                    for (int k = 0; k < 784; k++)
                        resim[k] = (byte)(input[k] * 255);
                    CreateImage(resim);
                    /// resim olustur bitir


                    double[] result = new double[10];
                    double max = 0;
                    int max_index = 0;
                    for (int j = 0; j < second_layer_neuron_count; j++)
                    {
                        result[j] = wf_second_layer[j].result();
                        if (result[j] > max)
                        {
                            max = result[j];
                            max_index = j;
                        }
                    }

                    dogru[max_index]++;

                    label1.Content = max_index.ToString() + " DETECTED => [" + train_data_answer[data_index_test].ToString() + "]";


                }
            }

        }

        private void button11_Click(object sender, RoutedEventArgs e)
        {
            Image image = new Image();
            string filePath = @"C:\Ares Birlestir\Neural Network Test\sekiz.jpg";

            if (File.Exists(filePath))
            {
                MemoryStream memoryStream = new MemoryStream();

                byte[] fileBytes = File.ReadAllBytes(filePath);
                memoryStream.Write(fileBytes, 0, fileBytes.Length);
                memoryStream.Position = 0;

                BitmapImage src = new BitmapImage();
                src.BeginInit();
                src.StreamSource = memoryStream;
                //src.UriSource = new Uri("picture.jpg", UriKind.Relative);
                src.CacheOption = BitmapCacheOption.OnLoad;
                src.EndInit();

                image.Source = src;

                image1.Source = src;

                // BW YAP
                // renkli resmi alip binary siyah beyaz olarak cikar
                MemoryStream file = new MemoryStream();
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(src));
                encoder.Save(file);
                file.Seek(0, SeekOrigin.Begin);
                file.Flush();
                byte[] bindata = file.ToArray();

                int w = (int)src.PixelWidth;
                int h = (int)src.PixelHeight;

                array = new byte[(int)(w * h)];
                int indexer = bindata.Length - (w * h * 4);

                int width_line_size = (w) * 4;
                int hight_line_size = h;
                int h1 = h - 1;

                for (int x = 0; x < w; x++)
                {
                    for (int y = 0; y < h; y++)
                    {
                        double B = bindata[indexer + width_line_size * (h1 - y) + x * 4];
                        double G = bindata[indexer + width_line_size * (h1 - y) + x * 4 + 1];
                        double R = bindata[indexer + width_line_size * (h1 - y) + x * 4 + 2];
                        byte bFF = bindata[indexer + width_line_size * (h1 - y) + x * 4 + 3];
                        // convert to grayscale
                        array[(w) * y + x] = (byte)(0.2989 * R + 0.5870 * G + 0.1140 * B);
                    }
                }

                // array icinde resim binary olarak olustu

                BitmapSource bitmap2 = BitmapSource.Create(w, h, 96, 96,
                PixelFormats.Gray8,
                BitmapPalettes.Gray256, array, w);

                image1.Source = bitmap2;



                ///
                ///  neural network ile test et
                ///


                int first_layer_neuron_count = 15;
                int second_layer_neuron_count = 10;

                int[] dogru = new int[10];

                //for (int data_index_test = 0; data_index_test < 10; data_index_test++)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        double[] input = new double[784];

                        for (int j = 0; j < 784; j++)
                            input[j] = (double)(array[j] > 30 ? 1.0 : 0.0);

                        for (int j = 0; j < first_layer_neuron_count; j++)
                            wf_first_layer[j].train(input);

                        // resim olustur basla
                        //byte[] resim = new byte[784];
                        //for (int k = 0; k < 784; k++)
                        //    resim[k] = (byte)(input[k] * 255);
                        CreateImage(array);
                        /// resim olustur bitir


                        double[] result = new double[10];
                        double max = 0;
                        int max_index = 0;
                        for (int j = 0; j < second_layer_neuron_count; j++)
                        {
                            result[j] = wf_second_layer[j].result();
                            if (result[j] > max)
                            {
                                max = result[j];
                                max_index = j;
                            }
                        }

                        dogru[max_index]++;

                        label1.Content = max_index.ToString() + " DETECTED ";

                    }
                }





            }
        }

        private void button12_Click(object sender, RoutedEventArgs e)
        {
            // load training data, if not loaded
            LoadTrainOCRData();

            // test icin 100 nolu goruntuyu olustur
            int index = Convert.ToInt32(textBox2.Text);

            byte[] resim = new byte[784];
            for (int i = 0; i < 784; i++)
                resim[i] = (byte)(train_data[index, i] * 256);

            CreateImage(resim);

            label2.Content = train_data_answer[index];
        }



    }
}
