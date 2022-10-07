using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;

namespace Neural_Network_Test
{
    public class RND : Random
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        internal double random(double min, double max)
        {
            return rnd.NextDouble() * (max - min) + min;
        }
    }

    // Neural Network Test
    public class Perceptrons
    {

        // single neuron neural network
        // find a linear line side
        //
        // perceptron = array of inputs, single output.
        // bu tur perceptron'lar sadece lenear ayrimlarda calisiyormus.
        //
        class Perceptron  // SINGLE NEURON
        {

            public double[] weights;
            double c = 0.001;    // cost function, learning rate

            public Perceptron(int n)
            {
                weights = new double[n];
                for (int i = 0; i < weights.Length; i++)
                    weights[i] = new RND().random(-1.0, 1.0);
            }

            public int feedforward(double[] inputs)
            {
                double sum = 0;
                for (int i = 0; i < weights.Length; i++)
                {
                    sum += inputs[i] * weights[i];
                }

                return activate(sum);
            }

            int activate(double sum)
            {
                // sigmoid
                //return 1.0 / (1.0 + Math.Exp(-sum));
                return (sum > 0) ? 1 : -1;
            }

            public void train(double[] inputs, int desired)
            {
                int guess = feedforward(inputs);
                double error = desired - guess;
                //
                for (int i = 0; i < weights.Length; i++)
                    weights[i] += c * error * inputs[i];

            }
        }

        class Trainer
        {
            public double[] inputs;
            public int answer;
            // x, y and a = the real answer 
            public Trainer(double x, double y, int answer)
            {
                inputs = new double[3];
                inputs[0] = x;
                inputs[1] = y;
                inputs[2] = 1; // so the bias could affect
                this.answer = answer;
            }
        }

        public class NeuralPerceptron
        {
            Random rndom = new Random(Guid.NewGuid().GetHashCode());

            double rect_width = 640;
            double rect_height = 360;

            Perceptron ptron;
            //
            Trainer[] training = new Trainer[10000];

            public double f(double x)
            {
                return (2 * x) + 1;
            }

            public void setup()
            {
                // create a perception class with three weight values
                // CREATE THE SINGLE NEURON
                ptron = new Perceptron(3);

                RND rnd = new RND();

                // populate values to train the neuron
                for (int i = 0; i < training.Length; i++)
                {
                    double x = rnd.random(-rect_width / 2, rect_width / 2);
                    double y = rnd.random(-rect_height / 2, rect_height / 2);
                    int answer = (y < f(x)) ? -1 : 1;
                    training[i] = new Trainer(x, y, answer);
                }
            }

            internal void train()
            {
                for (int i = 0; i < training.Length; i++)
                {
                    ptron.train(training[i].inputs, training[i].answer);
                }
            }

            internal void results(System.Windows.Controls.Canvas canvas1)
            {
                // weight'leri bulduk
                // simdi dogru train edip etmedigimizi test edelim
                //
                RND rnd = new RND();
                // populate values to train the neuron
                for (int i = 0; i < training.Length; i++)
                {
                    double x = rnd.random(-rect_width / 2, rect_width / 2);
                    double y = rnd.random(-rect_height / 2, rect_height / 2);
                    int answer = (y < f(x)) ? -1 : 1;
                    int found = ptron.feedforward(new double[] { x, y, 1.0 });

                    Circle(canvas1, x, y, answer > 0, answer == found);


                }

            }

            private void Circle(System.Windows.Controls.Canvas canvas1, double x, double y, bool bBelow, bool bCorrect)
            {
                Ellipse e = new Ellipse();
                e.Height = 5.0;
                e.Width = 5.0;


                //// Create a red Brush
                //SolidColorBrush redBrush = new SolidColorBrush();
                //redBrush.Color = Colors.Red;

                //SolidColorBrush blueBrush = new SolidColorBrush();
                //blueBrush.Color = Colors.Blue;
                e.Margin = new Thickness(
                                x + rect_width / 2.0,
                                y + rect_height / 2.0,
                                0, 0);

                //if (bBelow)
                if (bCorrect == true)
                    e.Fill = bBelow == true ? Brushes.Red : Brushes.Blue;
                else
                    e.Fill = Brushes.Green;

                //{
                //    e.Stroke = Brushes.Green;
                //    e.StrokeThickness = 2;
                //}
                // Add line to the Grid.
                canvas1.Children.Add(e);

            }



        }

    }


}
