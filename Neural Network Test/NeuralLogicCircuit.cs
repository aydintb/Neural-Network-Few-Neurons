using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neural_Network_Test
{
    // 2 girisi olacak 1 cikisi olacak
    // cikisi logic gate gibi olacak
    // mesela xor icin egitilmisse
    // 2 girisi 
    //        00, 01, 10, 11
    // xor ->  0   1   1   0

    // giris 3 neuron olacak
    // cikis tek neuron


    public class NeuralLogicCircuit
    {

        public class Neuron  // SINGLE NEURON
        {
            public string name = "";
            public List<Neuron> previous_layer = new List<Neuron>();
            public List<Neuron> next_layer = new List<Neuron>();

            public double[] inputs;
            //
            public double[] weights;
            public double bias;
            double learning_rate = 1.0;    // cost function, learning rate

            public Neuron(int n, string name = "")
            {
                this.name = name;
                //
                weights = new double[n];
                inputs = new double[n];
                //
                for (int i = 0; i < weights.Length; i++)
                    weights[i] = new RND().random(-1.0, 1.0);
                bias = new RND().random(-1.0, 1.0);


                //if (name == "input 1")
                //{
                //    weights[0] = 0.28056097;
                //    weights[1] = 0.34288359;
                //    bias = -0.11469333;
                //}

                //if (name == "input 2")
                //{
                //    weights[0] = 0.1960882;
                //    weights[1] = 0.23316787;
                //    bias = 0.71506159;
                //}

                //if (name == "input 3")
                //{
                //    weights[0] = -0.44580425;
                //    weights[1] = -0.17040152;
                //    bias = 1.03153529;
                //}


                //if (name == "output")
                //{
                //    weights[0] = 0.81171114;
                //    weights[1] = 0.6302649;
                //    weights[2] = 0.79376035;
                //    bias = 0.52544591;
                //}


            }

            public double feedforward(double[] inputs)
            {
                // bias
                double sum = bias;
                for (int i = 0; i < weights.Length; i++)
                {
                    sum += inputs[i] * weights[i];
                }

                return sum;
            }

            double sigmoid(double sum)
            {
                // ReLU(a) = max(0, a);
                // sigmoid learning is slow.

                // sigmoid
                return 1.0 / (1.0 + Math.Exp(-sum));
            }

            // derivative of the sigmoid can be expressed by the function of itself
            // S'(t) = S(t) * ( 1 - S(t) )
            double sigmoid_prime(double z)
            {
                return sigmoid(z) * (1 - sigmoid(z));
            }


            public double output;
            public double sigmoid_output;

            public void train(double[] inputs)
            {
                this.inputs = inputs;
                train();
            }

            public void train()
            {
                // calculate the weighted+biassed output value according to the input
                output = feedforward(inputs);
                sigmoid_output = sigmoid(output);

                // pass the new calculated 
                // sigmoid_output and output to the following neurons
                // update all the connected neurons
                for (int i = 0; i < next_layer.Count; i++)
                {
                    next_layer[i].NextLayerNeuronInputSetter(this, output, sigmoid_output);
                }
            }


            private bool NextLayerNeuronInputSetter(Neuron previous_neuron, double prev_output, double prev_sigmoid_output)
            {
                // the previous connection will give us the input value index 
                for (int i = 0; i < previous_layer.Count; i++)
                {
                    if (previous_neuron == previous_layer[i])
                    {
                        inputs[i] = prev_sigmoid_output;
                        return true;
                    }
                }

                return false;
            }


            internal void Connect(Neuron neuron)
            {
                next_layer.Add(neuron);
                neuron.ConnectParent(this);
            }

            private void ConnectParent(Neuron neuron)
            {
                previous_layer.Add(neuron);
            }

            internal void backprop_output(double desired)
            {
                // calculated in train
                //double this.output = feedforward(this.inputs);
                //double this.sigmoid_output = sigmoid(output);
                //            
                // learning rate ile delta carpimi extra bir degisken tanimlanabilir.
                // delta durmak zorunda, backprop icin.  belki de learning rateli version da backprop yapilabilir.
                double delta = (sigmoid_output - desired) * sigmoid_prime(output);
                //
                double[] nabla_w = new double[inputs.Count()];
                double[] old_weights = new double[weights.Count()];
                //
                for (int i = 0; i < nabla_w.Length; i++)
                {
                    old_weights[i] = weights[i];
                    nabla_w[i] = delta * this.inputs[i];
                    weights[i] = weights[i] - nabla_w[i] * learning_rate;
                }
                bias = bias - learning_rate * delta;
                // back propagate
                for (int i = 0; i < previous_layer.Count; i++)
                {
                    previous_layer[i].backprop(delta, old_weights[i]);
                }


            }

            // sonraki neurondan geldigi icin "prev"  (callback func)
            private void backprop(double prev_delta, double prev_weight)
            {
                //double error = guess - errorrate * neuron_output;
                double delta = prev_weight * prev_delta * sigmoid_prime(output);

                double[] nabla_w = new double[inputs.Count()];
                double[] old_weights = new double[weights.Count()];
                //
                for (int i = 0; i < nabla_w.Length; i++)
                {
                    old_weights[i] = weights[i];
                    nabla_w[i] = delta * this.inputs[i];
                    weights[i] = weights[i] - nabla_w[i] * learning_rate;
                }
                bias = bias - learning_rate * delta;
                // back propagate
                for (int i = 0; i < previous_layer.Count; i++)
                {
                    previous_layer[i].backprop(delta, old_weights[i]);
                }
                
            }



            internal double result()
            {
                double guess = feedforward(this.inputs);
                double sigmoid_guess = sigmoid(guess);
                return sigmoid_guess;
                //return guess;
            }
        }




    }

}
