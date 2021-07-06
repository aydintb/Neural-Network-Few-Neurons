using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Collections;

namespace Neural_Network_Test
{
    public class NeuralNework
    {
        public class Connecion
        {
            Neuron from;
            Neuron to;

            double weight;

            public Connecion(Neuron from, Neuron to, double weight)
            {
                this.from = from;
                this.to = to;
                this.weight = weight;
            }

            public void feedforward(double val)
            {                
                to.feedforward(val * weight);
            }

            public double CalculateDistance(Vector location1, Vector location2)
            {
                return (location1 - location2).Length;
            }

        }
        
        public class Neuron
        {
            public  Vector location;
            List<Connecion> connections;

            public Neuron(double x, double y)
            {
                location = new Vector(x, y);
                connections = new List<Connecion>();
            }

            internal void addConnection(Connecion connection)
            {
                connections.Add(connection);
            }

            double sum = 0;

            public void feedforward(double input)
            {
                sum += input;

                if (sum > 1)
                {
                    fire();
                    sum = 0;
                }
            }

            private void fire()
            {
                foreach (Connecion connection in connections)
                    connection.feedforward(sum);
            }


        }



        
        public class Network
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());

            List<Neuron> neurons;
            Vector location;

            public Network(double x, double y)
            {
                location = new Vector(x, y);
                neurons = new List<Neuron>();
            }

            public void addNeuron(Neuron n)
            {
                neurons.Add(n);
            }

            internal void connect(Neuron from, Neuron to)
            {
                Connecion c = new Connecion(from, to, rnd.NextDouble());
                from.addConnection(c);

            }

            public void feedforward(double input)
            {
                Neuron start = neurons[0];
                start.feedforward(input);
            }

        }



        Network network;
        double width = 680;
        double height = 340;

        public void setup()
        {
            
            network = new Network(width/2.0, height/2.0);

            Neuron a = new Neuron(-200, 0);
            Neuron b = new Neuron(0, 100);
            Neuron c = new Neuron(0, -100);
            Neuron d = new Neuron(200, 0);

            network.connect(a, b);
            network.connect(a, c);
            network.connect(b, d);
            network.connect(c, d);

            network.addNeuron(a);
            network.addNeuron(b);
            network.addNeuron(c);
            network.addNeuron(d);


        }

        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        
        internal void feed()
        {
            
            network.feedforward(rnd.NextDouble());
        }
    }


}
