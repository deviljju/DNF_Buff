using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DNF_Buf
{
    public partial class custom : Form
    {
        Form1 form1;
        public int[] enchant_c30 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //상하어벨신 무기 팔찌 목걸이 반지 보장 법석
        public int[] enchant_c50 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //상하어벨신 무기 팔찌 목걸이 반지 보장 법석


        public custom(Form1 form)
        {
            InitializeComponent();
            form1 = form;
        }
        public void setting()
        {
            jacket.Text = enchant_c30[0].ToString();
            pants.Text = enchant_c30[1].ToString();
            shoulder.Text = enchant_c30[2].ToString();
            waist.Text = enchant_c30[3].ToString();
            shoes.Text = enchant_c30[4].ToString();
            weapon.Text = enchant_c30[5].ToString();
            wrist.Text = enchant_c30[6].ToString();
            amulet.Text = enchant_c30[7].ToString();
            ring.Text = enchant_c30[8].ToString();
            support.Text = enchant_c30[9].ToString();
            magic_ston.Text = enchant_c30[10].ToString();

            jacket50.Text = enchant_c50[0].ToString();
            pants50.Text = enchant_c50[1].ToString();
            shoulder50.Text = enchant_c50[2].ToString();
            waist50.Text = enchant_c50[3].ToString();
            shoes50.Text = enchant_c50[4].ToString();
            weapon50.Text = enchant_c50[5].ToString();
            wrist50.Text = enchant_c50[6].ToString();
            amulet50.Text = enchant_c50[7].ToString();
            ring50.Text = enchant_c50[8].ToString();
            support50.Text = enchant_c50[9].ToString();
            magic_ston50.Text = enchant_c50[10].ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                enchant_c30[0] = int.Parse(jacket.Text);
                enchant_c30[1] = int.Parse(pants.Text);
                enchant_c30[2] = int.Parse(shoulder.Text);
                enchant_c30[3] = int.Parse(waist.Text);
                enchant_c30[4] = int.Parse(shoes.Text);
                enchant_c30[5] = int.Parse(weapon.Text);
                enchant_c30[6] = int.Parse(wrist.Text);
                enchant_c30[7] = int.Parse(amulet.Text);
                enchant_c30[8] = int.Parse(ring.Text);
                enchant_c30[9] = int.Parse(support.Text);
                enchant_c30[10] = int.Parse(magic_ston.Text);

                enchant_c50[0] = int.Parse(jacket50.Text);
                enchant_c50[1] = int.Parse(pants50.Text);
                enchant_c50[2] = int.Parse(shoulder50.Text);
                enchant_c50[3] = int.Parse(waist50.Text);
                enchant_c50[4] = int.Parse(shoes50.Text);
                enchant_c50[5] = int.Parse(weapon50.Text);
                enchant_c50[6] = int.Parse(wrist50.Text);
                enchant_c50[7] = int.Parse(amulet50.Text);
                enchant_c50[8] = int.Parse(ring50.Text);
                enchant_c50[9] = int.Parse(support50.Text);
                enchant_c50[10] = int.Parse(magic_ston50.Text);

                form1.enchant30 = enchant_c30;
                form1.enchant50 = enchant_c50;
                form1.Invalidate();
                this.Close();
            }
            catch(Exception)
            {
                MessageBox.Show("입력값이 잘못되었습니다. 문자나 공백이 있는지 확인해주세요","경고");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
