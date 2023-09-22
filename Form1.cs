using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Maximum_Rotation
{



    public partial class Form1 : Form
    {
        int[,] massiv = new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
        //int[,] massiv = new int[3, 3] { { 5, 3, 10, 7 }, { 13, 6, 16, 12 }, { 8, 1, 15, 4 }, { 2, 9, 14, 11 } }; 
        int indexN = 1, indexM = 1;
        int[,] level = new int[3, 3];

        private GenetateLevel genetateLevel = new GenetateLevel();



        public Form1()
        {
            InitializeComponent();


           

            level = genetateLevel.level();
            textBox1.Text = genetateLevel.step.ToString();
            textBox2.Text = genetateLevel.kolll.ToString();
            zapoln(level);
            //button6.BackColor = Color.Red;
            //button7.BackColor = Color.Red;
            //button10.BackColor = Color.Red;
            //button11.BackColor = Color.Red;
            indexM = 1;
            indexN = 1;

        }

        public void zapoln(int[,] mas)
        {
            button1.Text = mas[0, 0].ToString();
            button2.Text = mas[0, 1].ToString();
            button3.Text = mas[0, 2].ToString();

            button5.Text = mas[1, 0].ToString();
            button6.Text = mas[1, 1].ToString();
            button7.Text = mas[1, 2].ToString();

            button9.Text = mas[2, 0].ToString();
            button10.Text = mas[2, 1].ToString();
            button11.Text = mas[2, 2].ToString();


        }

        private Stack<int[,]> play = new Stack<int[,]>();



        private async void button8_Click(object sender, EventArgs e)
        {
            AI aI = new AI();
            play = await aI.getSolution(level);
            //play1.Start();
            //play = play1.Result;
            play.Pop();
            if (play.Count > 0)
            {
                Console.WriteLine("Поиск в Ширину//Глубину");
                Console.WriteLine("Количество итераций - " + aI.iterations);
                Console.WriteLine("Количество шагов - " + aI.step);
                Console.WriteLine("Количество раскрытий - " + aI.reveling);
                Console.WriteLine("Количество раскрытых - " + aI.revelated);
                Console.WriteLine("Максимальное открытых - " + aI.maxOpen);
                Console.WriteLine("Максимальное закрытых - " + aI.maxClose);
                Console.WriteLine("Максимальное количество состояний - " + aI.maxCondition);
                Console.WriteLine();
                label1.Text = "Решение найдено";
                buttonNext.Enabled = true;
                buttonSearch.Enabled = false;
                buttonSearchDouble.Enabled = false;
            }
        }

        private void buttonSearchDouble_Click(object sender, EventArgs e)
        {
            AIdouble aI = new AIdouble();
            play = aI.getSolution(level, massiv);
            play.Pop();
            if (play.Count > 0)
            {
                Console.WriteLine("Двунаправленный поиск");
                Console.WriteLine("Количество итераций - " + aI.iterations);
                Console.WriteLine("Количество шагов - " + aI.step);
                Console.WriteLine("Количество раскрытий - " + aI.reveling);
                Console.WriteLine("Количество раскрытых - " + aI.revelated);
                Console.WriteLine("Максимальное открытых - " + aI.maxOpen);
                Console.WriteLine("Максимальное закрытых - " + aI.maxClose);
                Console.WriteLine("Максимальное количество состояний - " + aI.maxCondition);
                Console.WriteLine();
                label1.Text = "Решение найдено";
                buttonNext.Enabled = true;
                buttonSearch.Enabled = false;
                buttonSearchDouble.Enabled = false;
            }
        }



        private void button4_Click(object sender, EventArgs e)
        {
            int h = 0;
            Evristic aI = new Evristic();
            if (radioButton1.Checked)
            {
                aI = new Evristic(1);
                h = 1;
            }
            else if (radioButton2.Checked)
            {
                aI = new Evristic(2);
                h = 2;
            }
            play = aI.getSolution(level);
            play.Pop();
            if (play.Count > 0)
            {
                Console.WriteLine("Эвристика " + h);
                Console.WriteLine("Количество итераций - " + aI.iterations);
                Console.WriteLine("Количество шагов - " + aI.step);
                Console.WriteLine("Количество раскрытий - " + aI.reveling);
                Console.WriteLine("Количество раскрытых - " + aI.revelated);
                Console.WriteLine("Максимальное открытых - " + aI.maxOpen);
                Console.WriteLine("Максимальное закрытых - " + aI.maxClose);
                Console.WriteLine("Максимальное количество состояний - " + aI.maxCondition);
                Console.WriteLine();
                label1.Text = "Решение найдено";
                buttonNext.Enabled = true;
                buttonSearch.Enabled = false;
                buttonSearchDouble.Enabled = false;
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        private void Form1_Load(object sender, EventArgs e)
        {
            AllocConsole();

        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            //if (play.Count>0)

            zapoln(play.Pop());
            if (play.Count == 0)
            {
                buttonNext.Enabled = false;
                label2.Text = "Сыграно";
                label1.Text = "Решение пройдено";
                buttonRefresh.Enabled = true;
            }


        }

        private void buttonNewGame_Click(object sender, EventArgs e)
        {
            //genetateLevel = new GenetateLevel(this);
            level = genetateLevel.level();
            textBox1.Text = genetateLevel.step.ToString();
            textBox2.Text = genetateLevel.kolll.ToString();
            zapoln(level);
            buttonSearch.Enabled = true;
            buttonSearchDouble.Enabled = true;
            buttonRefresh.Enabled = false;
            label1.Text = "Решение не искали";
            label2.Text = "";

        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {

        }

        private void buttonRefresh_Click_1(object sender, EventArgs e)
        {
            zapoln(level);
            buttonSearch.Enabled = true;
            buttonSearchDouble.Enabled = true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
            {
                var tmp = level[indexN + 1, indexM + 1];
                level[indexN + 1, indexM + 1] = level[indexN + 1, indexM];
                level[indexN + 1, indexM] = level[indexN, indexM];
                level[indexN, indexM] = level[indexN, indexM + 1];
                level[indexN, indexM + 1] = tmp;
                zapoln(level);
                //if(massiv == massivRes)
                {
                    //  MessageBox.Show("Вы прошли игру");
                }
            }

            if (e.KeyCode == Keys.E)
            {
                var tmp = level[indexN, indexM];
                level[indexN, indexM] = level[indexN + 1, indexM];
                level[indexN + 1, indexM] = level[indexN + 1, indexM + 1];
                level[indexN + 1, indexM + 1] = level[indexN, indexM + 1];
                level[indexN, indexM + 1] = tmp;
                zapoln(level);
                //if(massiv == massivRes)
                {
                    //  MessageBox.Show("Вы прошли игру");
                }
            }

            if (e.KeyCode == Keys.W)
            {

                if (button5.BackColor == Color.Red && button6.BackColor == Color.Red && button9.BackColor == Color.Red && button10.BackColor == Color.Red)
                {
                    button1.BackColor = Color.Red;
                    button2.BackColor = Color.Red;
                    button9.BackColor = Color.Gray;
                    button10.BackColor = Color.Gray;
                    indexN = 0; indexM = 0;
                    return;
                }
                if (button6.BackColor == Color.Red && button7.BackColor == Color.Red && button10.BackColor == Color.Red && button11.BackColor == Color.Red)
                {
                    button2.BackColor = Color.Red;
                    button3.BackColor = Color.Red;
                    button10.BackColor = Color.Gray;
                    button11.BackColor = Color.Gray;
                    indexN = 0; indexM = 1;
                    return;
                }
                //if (button7.BackColor == Color.Red && button8.BackColor == Color.Red && button11.BackColor == Color.Red && button12.BackColor == Color.Red)
                //{
                //    button3.BackColor = Color.Red;
                //    button4.BackColor = Color.Red;
                //    button11.BackColor = Color.Gray;
                //    button12.BackColor = Color.Gray;
                //    indexN = 0; indexM = 2;
                //    return;
                //}

                //    if (button9.BackColor == Color.Red && button10.BackColor == Color.Red && button13.BackColor == Color.Red && button14.BackColor == Color.Red)
                //    {
                //        button5.BackColor = Color.Red;
                //        button6.BackColor = Color.Red;
                //        button13.BackColor = Color.Gray;
                //        button14.BackColor = Color.Gray;
                //        indexN = 1; indexM = 0;
                //        return;
                //    }
                //    if (button10.BackColor == Color.Red && button11.BackColor == Color.Red && button14.BackColor == Color.Red && button15.BackColor == Color.Red)
                //    {
                //        button6.BackColor = Color.Red;
                //        button7.BackColor = Color.Red;
                //        button14.BackColor = Color.Gray;
                //        button15.BackColor = Color.Gray;
                //        indexN = 1; indexM = 1;
                //        return;
                //    }
                //    if (button11.BackColor == Color.Red && button12.BackColor == Color.Red && button15.BackColor == Color.Red && button16.BackColor == Color.Red)
                //    {
                //        button7.BackColor = Color.Red;
                //        button8.BackColor = Color.Red;
                //        button15.BackColor = Color.Gray;
                //        button16.BackColor = Color.Gray;
                //        indexN = 1; indexM = 2;
                //        return;
                //    }
            }
            if (e.KeyCode == Keys.S)
            {
                if (button1.BackColor == Color.Red && button2.BackColor == Color.Red && button5.BackColor == Color.Red && button6.BackColor == Color.Red)
                {
                    button9.BackColor = Color.Red;
                    button10.BackColor = Color.Red;
                    button1.BackColor = Color.Gray;
                    button2.BackColor = Color.Gray;
                    indexN = 1; indexM = 0;
                    return;
                }
                if (button2.BackColor == Color.Red && button3.BackColor == Color.Red && button6.BackColor == Color.Red && button7.BackColor == Color.Red)
                {
                    button10.BackColor = Color.Red;
                    button11.BackColor = Color.Red;
                    button2.BackColor = Color.Gray;
                    button3.BackColor = Color.Gray;
                    indexN = 1; indexM = 1;
                    return;
                }
                //if (button3.BackColor == Color.Red && button4.BackColor == Color.Red && button7.BackColor == Color.Red && button8.BackColor == Color.Red)
                //{
                //    button11.BackColor = Color.Red;
                //    button12.BackColor = Color.Red;
                //    button3.BackColor = Color.Gray;
                //    button4.BackColor = Color.Gray;
                //    indexN = 1; indexM = 2;
                //    return;
                //}

                //if (button5.BackColor == Color.Red && button6.BackColor == Color.Red && button9.BackColor == Color.Red && button10.BackColor == Color.Red)
                //{
                //    button13.BackColor = Color.Red;
                //    button14.BackColor = Color.Red;
                //    button5.BackColor = Color.Gray;
                //    button6.BackColor = Color.Gray;
                //    indexN = 2; indexM = 0;
                //    return;
                //}
                //if (button6.BackColor == Color.Red && button7.BackColor == Color.Red && button10.BackColor == Color.Red && button11.BackColor == Color.Red)
                //{
                //    button14.BackColor = Color.Red;
                //    button15.BackColor = Color.Red;
                //    button6.BackColor = Color.Gray;
                //    button7.BackColor = Color.Gray;
                //    indexN = 2; indexM = 1;
                //    return;
                //}
                //if (button7.BackColor == Color.Red && button8.BackColor == Color.Red && button11.BackColor == Color.Red && button12.BackColor == Color.Red)
                //{
                //    button15.BackColor = Color.Red;
                //    button16.BackColor = Color.Red;
                //    button7.BackColor = Color.Gray;
                //    button8.BackColor = Color.Gray;
                //    indexN = 2; indexM = 2;
                //    return;
                //}
            }
            if (e.KeyCode == Keys.A)
            {
                if (button2.BackColor == Color.Red && button6.BackColor == Color.Red && button3.BackColor == Color.Red && button7.BackColor == Color.Red)
                {
                    button1.BackColor = Color.Red;
                    button5.BackColor = Color.Red;
                    button3.BackColor = Color.Gray;
                    button7.BackColor = Color.Gray;
                    indexN = 0; indexM = 0;
                    return;
                }
                if (button6.BackColor == Color.Red && button10.BackColor == Color.Red && button7.BackColor == Color.Red && button11.BackColor == Color.Red)
                {
                    button5.BackColor = Color.Red;
                    button9.BackColor = Color.Red;
                    button7.BackColor = Color.Gray;
                    button11.BackColor = Color.Gray;
                    indexN = 1; indexM = 0;
                    return;
                }
                //if (button10.BackColor == Color.Red && button14.BackColor == Color.Red && button11.BackColor == Color.Red && button15.BackColor == Color.Red)
                //{
                //    button9.BackColor = Color.Red;
                //    button13.BackColor = Color.Red;
                //    button11.BackColor = Color.Gray;
                //    button15.BackColor = Color.Gray;
                //    indexN = 2; indexM = 0;
                //    return;
                //}

                //if (button3.BackColor == Color.Red && button4.BackColor == Color.Red && button7.BackColor == Color.Red && button8.BackColor == Color.Red)
                //{
                //    button2.BackColor = Color.Red;
                //    button6.BackColor = Color.Red;
                //    button4.BackColor = Color.Gray;
                //    button8.BackColor = Color.Gray;
                //    indexN = 0; indexM = 1;
                //    return;
                //}
                //if (button7.BackColor == Color.Red && button8.BackColor == Color.Red && button11.BackColor == Color.Red && button12.BackColor == Color.Red)
                //{
                //    button6.BackColor = Color.Red;
                //    button10.BackColor = Color.Red;
                //    button8.BackColor = Color.Gray;
                //    button12.BackColor = Color.Gray;
                //    indexN = 1; indexM = 1;
                //    return;
                //}
                //if (button11.BackColor == Color.Red && button12.BackColor == Color.Red && button15.BackColor == Color.Red && button16.BackColor == Color.Red)
                //{
                //    button10.BackColor = Color.Red;
                //    button14.BackColor = Color.Red;
                //    button12.BackColor = Color.Gray;
                //    button16.BackColor = Color.Gray;
                //    indexN = 2; indexM = 1;
                //    return;
                //}
            }
            if (e.KeyCode == Keys.D)
            {
                if (button1.BackColor == Color.Red && button2.BackColor == Color.Red && button5.BackColor == Color.Red && button6.BackColor == Color.Red)
                {
                    button3.BackColor = Color.Red;
                    button7.BackColor = Color.Red;
                    button1.BackColor = Color.Gray;
                    button5.BackColor = Color.Gray;
                    indexN = 0; indexM = 1;
                    return;
                }
                if (button5.BackColor == Color.Red && button6.BackColor == Color.Red && button9.BackColor == Color.Red && button10.BackColor == Color.Red)
                {
                    button7.BackColor = Color.Red;
                    button11.BackColor = Color.Red;
                    button5.BackColor = Color.Gray;
                    button9.BackColor = Color.Gray;
                    indexN = 1; indexM = 1;
                    return;
                }
                //if (button9.BackColor == Color.Red && button10.BackColor == Color.Red && button13.BackColor == Color.Red && button14.BackColor == Color.Red)
                //{
                //    button11.BackColor = Color.Red;
                //    button15.BackColor = Color.Red;
                //    button9.BackColor = Color.Gray;
                //    button13.BackColor = Color.Gray;
                //    indexN = 2; indexM = 1;
                //    return;
                //}

                //if (button2.BackColor == Color.Red && button3.BackColor == Color.Red && button6.BackColor == Color.Red && button7.BackColor == Color.Red)
                //{
                //    button4.BackColor = Color.Red;
                //    button8.BackColor = Color.Red;
                //    button2.BackColor = Color.Gray;
                //    button6.BackColor = Color.Gray;
                //    indexN = 0; indexM = 2;
                //    return;
                //}
                //if (button6.BackColor == Color.Red && button7.BackColor == Color.Red && button10.BackColor == Color.Red && button11.BackColor == Color.Red)
                //{
                //    button8.BackColor = Color.Red;
                //    button12.BackColor = Color.Red;
                //    button6.BackColor = Color.Gray;
                //    button10.BackColor = Color.Gray;
                //    indexN = 1; indexM = 2;
                //    return;
                //}
                //if (button10.BackColor == Color.Red && button11.BackColor == Color.Red && button14.BackColor == Color.Red && button15.BackColor == Color.Red)
                //{
                //    button12.BackColor = Color.Red;
                //    button16.BackColor = Color.Red;
                //    button10.BackColor = Color.Gray;
                //    button14.BackColor = Color.Gray;
                //    indexN = 2; indexM = 2;
                //    return;
                //}
            }
        }
    }
}
