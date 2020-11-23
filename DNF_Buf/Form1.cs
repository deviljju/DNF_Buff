using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DNF_Buf
{ //d0f2649f85b9832b70d5a01b911c5402 데빌쭈 id
    public partial class Form1 : Form
    {
        private string serverId = "cain";
        private string charId, jobId, jobGrowId = "";
        private string apiKey = "0cCpm2OKIJEKkPyfeBALa0JOlxRFKU4T";
        private JObject data_status, data_skill_style;
        private JArray data_equip;
        HttpWebRequest request;
        custom custom;

        public int[] enchant30 = { 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //상하어벨신 무기 팔찌 목걸이 반지 보장 법석
        public int[] enchant50 = { 750, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //상하어벨신 무기 팔찌 목걸이 반지 보장 법석
        private int stat30;
        private int stat50;
        private int lv100;
        private int middle;
        private int calc30_stat;
        private int calc30_attk;
        private int calc50_stat;
        private int calc50_stats;
        private int total_stat;
        private int total_stats;
        private int total_attk;
        private int total_buff;
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btn_submit_Click(sender, e);
            }
        }

        private void Serverbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Serverbox.SelectedIndex >= 0)
            {
                switch (Serverbox.SelectedIndex)
                {
                    case 0: serverId = "cain"; break;
                    case 1: serverId = "diregie"; break;
                    case 2: serverId = "siroco"; break;
                    case 3: serverId = "prey"; break;
                    case 4: serverId = "casillas"; break;
                    case 5: serverId = "hilder"; break;
                    case 6: serverId = "anton"; break;
                    case 7: serverId = "bakal"; break;
                }
            }
        }

        private void buff_calc(int m)
        {
            //m은 직업군, s는 버프적용스탯
            //double coe; //계수 계산용
            try
            {
                int s30 = int.Parse(lv30_stat.Text);
                int s50 = int.Parse(lv50_stat.Text);
                int c100 = int.Parse(lv100ss.Text); //2렙일때 110, 125
                double[] coe = { 620.00, 665.00, 665.00};
                double temps = (s30 / coe[m] + 1);
                double temp50 = (s50 / 750.00 + 1);
                double temp100 = (1 + (8 + c100) / 100.00);
                double temp100s = (1 + (23 + c100) / 100.00);
                switch (m)
                    {
                        case 0: //세인트 계수 620 750
                            calc30_stat = (int)(temps * int.Parse(lv30_s.Text));
                            calc30_attk = (int)(temps * int.Parse(lv30_a.Text));
                            calc50_stat = (int)((temp50 * int.Parse(lv50_s.Text))*temp100);
                            calc50_stats = (int)((temp50 * int.Parse(lv50_s.Text)) * temp100s);
                        break;
                        case 1: //세라핌 계수 665 750, 버프력은 아리아 15%를 30lv에 추가해야함
                            calc30_stat = (int)(temps * int.Parse(lv30_s.Text));
                            calc30_attk = (int)(temps * int.Parse(lv30_a.Text));
                            calc50_stat = (int)((temp50 * int.Parse(lv50_s.Text)) * (1 + (8 + c100) / 100));
                            calc50_stats = (int)((temp50 * int.Parse(lv50_s.Text)) * (1 + (23 + c100) / 100));
                        break;
                        case 2: //헤카테 계수 665 750 , 버프력은 편애 15% 퍼펫 25%를 30lv에 추가해야함
                            calc30_stat = (int)(temps * int.Parse(lv30_s.Text));
                            calc30_attk = (int)(temps * int.Parse(lv30_a.Text));
                            calc50_stat = (int)((temp50 * int.Parse(lv50_s.Text)) * (1 + (8 + c100) / 100));
                            calc50_stats = (int)((temp50 * int.Parse(lv50_s.Text)) * (1 + (23 + c100) / 100));
                        break;
                    }
                total_stat = calc30_stat + calc50_stat + int.Parse(lv48_s.Text)+int.Parse(addstat.Text);
                total_stats = calc30_stat + calc50_stats + int.Parse(lv48_s.Text) + int.Parse(addstat.Text);
                total_attk = calc30_attk + int.Parse(addatk.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("입력값이 잘못되었습니다. 문자나 공백이 있는지 확인해주세요", "경고");
            }
            
            string s = string.Format("  (힘지능) {0}  (공격력) {1}", calc30_stat, calc30_attk);
            label_result30.Text =s;
            s = string.Format("  (힘지능) {0} ({1})", calc50_stat,calc50_stats);
            label_result50.Text = s;
            s = string.Format("  1각연동 (힘지능) {0}  (공격력) {1}",total_stat,total_attk);
            label_totals.Text = s;
            s = string.Format("  2각연동 (힘지능) {0}  (공격력) {1}", total_stats, total_attk);
            label_totala.Text = s;

        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            string characterName = Encoding_utf8(text_box_nickname.Text);
            if (GetData(characterName))
            {
                GetData_status();
                //data_equip = GetData_equip_item_list();
                //data_skill_style = GetData_skill_style();
                //Compare_item_weapon();
                Console.WriteLine(data_equip);
                //status.Print();
                img_character.Image = GetCharacterImage(charId);
                label_result_nickname.Text = characterName;
                label_result30.Text = charId;
            }
            else
            {
                img_character.Image = null;
                label_result_nickname.Text = "결과 없음";
                label_result30.Text = "";
            }
        }

        HttpWebResponse response;
        public Form1()
        {
            InitializeComponent();
            saint.Checked = true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            String[] server_data = { "카인", "디레지에", "시로코", "프레이", "카시야스", "힐더", "안톤", "바칼" };
            this.Serverbox.Items.AddRange(server_data);
            this.Serverbox.SelectedIndex = 0;
            int tstat = 9517;
            double tempss = 630.00;
            int tatk = 156;
            double temps = (tstat/tempss+1)*tatk;
            int x = (int)temps;
            //label_result30.Text =x.ToString();
        }
        private string Encoding_utf8(string s)
        {
            Encoding enc = new UTF8Encoding(true, true);
            string result = "";
            try
            {
                byte[] bytes = enc.GetBytes(s);
                foreach (var byt in bytes)
                {
                    result += byt;
                }
                result = Encoding.UTF8.GetString(bytes);
            }
            catch (EncoderFallbackException er)
            {
                Console.WriteLine("Unable to encode {0} at index {1}",
                                  er.IsUnknownSurrogate() ?
                                     String.Format("U+{0:X4} U+{1:X4}",
                                                   Convert.ToUInt16(er.CharUnknownHigh),
                                                   Convert.ToUInt16(er.CharUnknownLow)) :
                                     String.Format("U+{0:X4}",
                                                   Convert.ToUInt16(er.CharUnknown)),
                                  er.Index);
            }
            return result;
        }
        private bool GetData(string characterName)
        {
            string url = "https://api.neople.co.kr/df/servers/" + serverId + "/characters?characterName=" + characterName + "&limit=" + 10 + "&wordType=full&apikey="+apiKey;
            request = (HttpWebRequest)WebRequest.Create(url);
            response = (HttpWebResponse)request.GetResponse();
            string content = new StreamReader(response.GetResponseStream()).ReadToEnd();
            JArray list = ((JArray)(JObject.Parse(content))["rows"]);
            if (list.Count != 0)
            {
                charId = (string)((JObject)list[0])["characterId"];
                jobGrowId = (string)((JObject)list[0])["jobGrowId"];
                jobId = (string)((JObject)list[0])["jobId"];
                Console.WriteLine((string)((JObject)list[0])["characterName"] + " : " + charId);
                Console.WriteLine((string)((JObject)list[0])["jobName"] + " : " + jobId);
                Console.WriteLine((string)((JObject)list[0])["jobGrowName"] + " : " + jobGrowId);
                return true;
                //기본 정보 호출

            }
            else return false;
        }
        private void GetData_status()
        {
            string url = "https://api.neople.co.kr/df/servers/" + serverId + "/characters/" + charId + "/status?apikey=0cCpm2OKIJEKkPyfeBALa0JOlxRFKU4T";
            request = (HttpWebRequest)WebRequest.Create(url);
            response = (HttpWebResponse)request.GetResponse();
            string content = new StreamReader(response.GetResponseStream()).ReadToEnd();
            JArray obj = (JArray)JObject.Parse(content)["status"];

            //데이터 입력
            int index = 0;
            foreach (JObject fElement in obj)
            {
                var fName = fElement["value"] ?? "<NULL>";
                switch (index)
                {
                    /*
                    case 2: status.str = (int)fName; break;
                    case 3: status.ap = (int)fName; break;
                    case 4: status.hp = (int)fName; break;
                    case 5: status.mind = (int)fName; break;
                    case 6: status.attackPoint = (int)fName; break;
                    case 7: status.magicPoint = (int)fName; break;
                    case 8: status.indiPoint = (int)fName; break;
                    case 23: status.e_fire = (int)fName; break;
                    case 25: status.e_water = (int)fName; break;
                    case 27: status.e_light = (int)fName; break;
                    case 29: status.e_dark = (int)fName; break;*/
                }
                index++;
            }
            //status.Get_e_best();

        }

        private void 마법부여커스텀ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            custom = new custom(this);
            custom.Show();
            custom.enchant_c30 = enchant30;
            custom.enchant_c50 = enchant50;
            custom.setting();
            //custom.Invalidate();
        }

        //직업선택
        private void saint_CheckedChanged(object sender, EventArgs e)
        {
            middle = 0;
            Invalidate();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            middle = 1;
            Invalidate();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            middle = 2;
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            jacket.Text = enchant30[0].ToString();
            switch (middle)
            {
                case 0:
                    label1.Text = "세인트";
                    break;
                case 1:
                    label1.Text = "세라핌";
                    break;
                case 2:
                    label1.Text = "헤카테";
                    break;
                default:
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            buff_calc(middle);
        }

        private JArray GetData_equip_item_list()
        {
            string url = "https://api.neople.co.kr/df/servers/" + serverId + "/characters/" + charId + "/equip/equipment?apikey=0cCpm2OKIJEKkPyfeBALa0JOlxRFKU4T";
            request = (HttpWebRequest)WebRequest.Create(url);
            response = (HttpWebResponse)request.GetResponse();
            string content = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return (JArray)(JObject.Parse(content))["equipment"];
        }

        private JObject GetData_skill_style()
        {
            //아이템 및 장비를 통한 강화 제외
            string url = "https://api.neople.co.kr/df/servers/" + serverId + "/characters/" + charId + "/skill/style?apikey=0cCpm2OKIJEKkPyfeBALa0JOlxRFKU4T";
            request = (HttpWebRequest)WebRequest.Create(url);
            response = (HttpWebResponse)request.GetResponse();
            string content = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return JObject.Parse(content);

        }
        private Bitmap GetCharacterImage(string characterId)
        {
            string url = "https://img-api.neople.co.kr/df/servers/" + serverId + "/characters/" + characterId + "?zoom=1";
            try
            {
                WebClient Downloader = new WebClient();
                Stream ImageStream = Downloader.OpenRead(url);
                Bitmap DownloadImage = Bitmap.FromStream(ImageStream) as Bitmap;
                return DownloadImage;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
