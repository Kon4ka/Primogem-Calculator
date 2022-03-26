using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        DateTime today;
        DateTime day_end;
        DateTime[] tehcworks = { new DateTime(2022, 3, 30),
                                 new DateTime(2022, 5, 11),
                                 new DateTime(2022, 8, 3),
                                 new DateTime(2022, 9, 14),
                                 new DateTime(2022, 10, 26),
                                 new DateTime(2022, 12, 7)};
        DateTime[] streams = { new DateTime(2022, 4, 29),
                                 new DateTime(2022, 6, 10),
                                 new DateTime(2022, 7, 22),
                                 new DateTime(2022, 9, 2),
                                 new DateTime(2022, 10, 14),
                                 new DateTime(2022, 11, 26)};
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int result = 0;
            int pink_meet_result = 0;
            int blue_meet_result = 0;
            int start_advR = 0;
            int end_advR = 0;
            int mini_ivents;
            int dust;
            int sparcks;
            int meets_in_cur_month;
            int stars;

            if (day_end < today)
            {
                MessageBox.Show("Конечная дата должна быть больше начальной");
                return;
            }
            if (!int.TryParse(comboBox2.Text, out stars))
            {
                MessageBox.Show("Неверно задано значение:\n \"На сколько закрываете 8-12 этажи\"");
                return;
            }
            if (!int.TryParse(textBox4.Text, out meets_in_cur_month))
            {
                MessageBox.Show("Неверно задано значение \"Кол-во судеб\"");
                return;
            }
            if (!int.TryParse(textBox2.Text, out sparcks))
            {
                MessageBox.Show("Неверно задано значение \"Кол-во блеска\"");
                return;
            }
            if (!int.TryParse(textBox1.Text, out dust))
            {
                MessageBox.Show("Неверно задано значение \"Кол-во пыли\"");
                return;
            }
            if (!int.TryParse(comboBox1.Text, out mini_ivents))
            {
                MessageBox.Show("Неверно задано значение \"Мини Ивенты\"");
                return;
            }

            if (!int.TryParse(textBox3.Text, out start_advR) ||
                !int.TryParse(textBox5.Text, out end_advR) ||
                start_advR > end_advR)
            {
                MessageBox.Show("Вы задали некорректное значение начального или конечного ранга");
            }

            int days = (day_end - today).Days;

            if (checkBox1.Checked)
                result += days * 60;
            if (mini_ivents != 0)
                result += mini_ivents * 420;
            if (checkBox5.Checked)      
                result += 40;

            if (start_advR != end_advR)
            {
                for(int i = start_advR; i <= end_advR; i++)
                {
                    if (i == 4 || i == 12)
                        result += 50;
                    else if (i == 20 || i == 28)
                        result += 75;
                    else if (i == 26 || i == 32 || i == 36 || i == 23 || i == 40 || i == 46 || i == 51)
                        result += 100;
                    else if (i == 45 || i == 50)
                        result += 125;
                }
            }

            if (textBox2.Text != "0" || textBox2.Text != "")
                pink_meet_result += sparcks / 5;

            if (checkBox6.Checked)      //Curr moth Paimon Shop
            {
                if (textBox4.Text != "0" || textBox4.Text != "")
                {
                    if (dust / 75 > meets_in_cur_month)
                        pink_meet_result += meets_in_cur_month;
                    else
                    {
                        pink_meet_result += dust / 75;
                        dust -= 75 * (int)(dust / 75);
                    }
                }
            }

            if (checkBox7.Checked)
            {
                if (checkBox8.Checked && !checkBox9.Checked)  //High 8 lvl
                {
                    result += (int)(((stars) / 3) * 50);
                }
                if(!checkBox8.Checked)
                {
                    int st_et = System.Convert.ToInt32(comboBox3.Text);
                    int st_zl = System.Convert.ToInt32(comboBox6.Text);
                    int ed_et = System.Convert.ToInt32(comboBox4.Text);
                    int ed_zl = System.Convert.ToInt32(comboBox5.Text);
                    if (st_et > ed_et)
                    {
                        MessageBox.Show("Неверно задано значение Этажей");
                        return;
                    }
                    int star_zals = 0;
                    int star_high_zals = 0;

                    if (ed_et > 8)
                    {
                        star_zals = 100 * ((8 - st_et) * 3 + 3 - st_zl);
                        star_high_zals = ((ed_et - 8) * 3 + ed_zl - 3) * 50;
                    }
                    else
                    {
                        star_zals = 100*((ed_et - st_et) * 3 + ed_zl - st_zl);
                    }
                    result += star_zals + star_high_zals;
                }
            }

            for (DateTime i = today; i < day_end; i=i.AddDays(1))
            {
                if (i.Day == 1)     //Next moth Paimon Shop
                {
                    if (dust >= 75)
                    {
                        if (dust / 75 > 5)
                            pink_meet_result += 5;
                        else
                        {
                            pink_meet_result += dust / 75;
                            dust -= 75 * (int)(dust / 75);
                        }
                    }
                    if (checkBox7.Checked && checkBox8.Checked)    //High Bess
                    {
                        result += (int)(((stars) / 3) * 50);
                    }
                }
                if (checkBox7.Checked && i.Day == 16)    //High Bess
                {
                    result += (int)(((stars) / 3) * 50);
                }
                if(checkBox2.Checked && (i.Day == 4 || i.Day == 11 || i.Day == 18)) //Calendar
                {
                    result += 20;
                }
                if (checkBox3.Checked && tehcworks.Contains(i))     //Techworks
                    result += 600;
                if (checkBox4.Checked && streams.Contains(i))       //Streams
                    result += 300;

            }

            label11.Text = result.ToString();
            label15.Text = pink_meet_result.ToString();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
/*            today = DateTime.Today;
            dateTimePicker1.Value = today.AddDays(1);*/
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            day_end = dateTimePicker1.Value;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            today = DateTime.Today;
            dateTimePicker1.Value = today.AddDays(1);
            for (int i = 0; i < 36; i++)
                comboBox2.Items.Add(i+1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Calendar dlg = new Calendar();
            dlg.Show(this);
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Visible = !groupBox2.Visible;
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            groupBox3.Visible = !groupBox3.Visible;
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            label17.Visible = !label17.Visible;
            label18.Visible = !label18.Visible;
            label26.Visible = !label26.Visible;
            comboBox2.Visible = !comboBox2.Visible;
            checkBox9.Visible = !checkBox9.Visible;

            label20.Visible = !label20.Visible;
            label21.Visible = !label21.Visible;
            label22.Visible = !label22.Visible;
            label23.Visible = !label23.Visible;
            label24.Visible = !label24.Visible;
            label25.Visible = !label25.Visible;

            comboBox4.Visible = !comboBox4.Visible;
            comboBox5.Visible = !comboBox5.Visible;
            comboBox6.Visible = !comboBox6.Visible;
            comboBox3.Visible = !comboBox3.Visible;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            checkBox4.Checked = true;
            checkBox5.Checked = true;
            comboBox1.Text = "1";
            checkBox6.Checked = true;
            checkBox7.Checked = true;

            textBox4.Text = "5";
            checkBox8.Checked = true;
            checkBox9.Checked = true;
            comboBox2.Text = "36";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            comboBox1.Text = "0";
            checkBox6.Checked = false;
            checkBox7.Checked = false;

            textBox4.Text = "0";
            checkBox9.Checked = false;
            comboBox2.Text = "0";
        }

    }
}
