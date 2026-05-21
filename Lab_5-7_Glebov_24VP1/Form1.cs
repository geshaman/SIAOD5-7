using System;
using System.Windows.Forms;

namespace Lab_5_7_Glebov_24VP1
{
    public partial class Form1 : Form
    {
        private long[] array;
        private int N = 10000000; 
        int iterations = 1000000;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Random rnd = new Random();
            array = new long[N+1];
            long current = 0;

            for (int i = 0; i < N; i++)
            {
                current += rnd.Next(1, 5);
                array[i] = current;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int key = (int)numericUpDown2.Value;

            binary_search_non_optimal(key);
            binary_search_optimal(key);
            binary_interpol_search(key);
            sequential_binary_search(key);
            sequential_ordered_search(key);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void binary_search_non_optimal(int key)
        {
            int resultIndex = -1;

            int start = Environment.TickCount;

            for (int j = 0; j < iterations; j++)
            {
                // Неоптимальный бинарный поиск
                int L = 0;
                int R = N - 1;

                while (L <= R)
                {
                    int i = (L + R) / 2;

                    if (array[i] == key)
                    {
                        resultIndex = i;
                        break;
                    }

                    if (array[i] > key)
                        R = i - 1;
                    else
                        L = i + 1;
                }
            }

            int resultTicks = Environment.TickCount - start;

            textBox1.Text = resultTicks.ToString();

            if (resultIndex == -1)
                textBox2.Text = "Элемент не найден";
            else
                textBox2.Text = resultIndex.ToString();
        }
        private void binary_search_optimal(int key)
        {
            int resultIndex = -1;

            int start = Environment.TickCount;

            for (int j = 0; j < iterations; j++)
            {
                // Оптимальный бинарный поиск
                int L = 0;
                int R = N - 1;

                while (L < R)
                {
                    int i = (L + R) / 2;
                    if (key <= array[i])
                        R = i;
                    else
                        L = i + 1;
                }
                if (array[R] == key)
                    resultIndex = R;
                else
                    resultIndex = -1;
            }

            int resultTicks = Environment.TickCount - start;

            textBox3.Text = resultTicks.ToString();

            if (resultIndex == -1)
                textBox4.Text = "Элемент не найден";
            else
                textBox4.Text = resultIndex.ToString();
        }
        private void binary_interpol_search(int key)
        {
            long resultIndex = -1;

            int start = Environment.TickCount;

            for (int j = 0; j < iterations; j++)
            {
                // Интерполяционный поиск
                long L = 0;
                long R = N - 1;
                resultIndex = -1;

                while (key > array[L] && key < array[R])
                {
                    long i = L + ((key - array[L]) * (R - L)) / (array[R] - array[L]);

                    if (key == array[i])
                    {
                        resultIndex = i;
                        break;
                    }
                    else if (key < array[i])
                    {
                        R = i - 1;
                    }
                    else
                    {
                        L = i + 1;
                    }
                }

                if (resultIndex == -1)
                {
                    if (key == array[L])
                        resultIndex = L;
                    else if (key == array[R])
                        resultIndex = R;
                }
            }

            int resultTicks = Environment.TickCount - start;
            textBox5.Text = resultTicks.ToString();
            textBox6.Text = resultIndex == -1 ? "Элемент не найден" : resultIndex.ToString();
        }

        private void sequential_binary_search(int key)
        {
            int resultIndex = -1;

            int start = Environment.TickCount;

            for (int j = 0; j < iterations; j++)
            {
                // "Последовательный" бинарный поиск
                int P = 0;
                int B = N / 2;

                while (B > 0)
                {
                    while ((P + B < N) && (array[P + B] <= key))
                    {
                        P += B;
                    }
                    B /= 2;
                }

                if (P < N && array[P] == key)
                    resultIndex = P;
                else
                    resultIndex = -1;
            }

            int resultTicks = Environment.TickCount - start;

            textBox7.Text = resultTicks.ToString();

            if (resultIndex == -1)
                textBox8.Text = "Элемент не найден";
            else
                textBox8.Text = resultIndex.ToString();
        }

        private void sequential_ordered_search(int key)
        {
            int it = 500;
            int resultIndex = -1;
            array[N] = key + 1;

            int startTime = Environment.TickCount;

            for (int j = 0; j < it; j++)
            {
                // Последовательный поиск в упорядоченном массиве
                int i = 0;
                while (key > array[i])
                {
                    i++;
                }

                if (array[i] == key)
                    resultIndex = i;
                else
                    resultIndex = -1;
            }

            int resultTime = (Environment.TickCount - startTime) * (iterations / it);

            textBox11.Text = resultTime.ToString();

            if (resultIndex == -1)
                textBox12.Text = "Элемент не найден";
            else
                textBox12.Text = resultIndex.ToString();
        }
    }
}
