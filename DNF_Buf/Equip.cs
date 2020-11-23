using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DNF_Buf
{
    class Equip
    {
        //정신력	지능+	스텟%	물공%	마공%	독공%	고정	%	축렙	포렙	증폭	전직패	보징크크	1각패	2각패	2각	3각패	신념	신실  진각렙

        //스탯 관련은 아포 스탯 = 인포 내 스탯을 불러옴
        //버프강화 관련은 스탯-장착장비에 붙어있는 스탯들 + 버프강화에 붙어있는 장비의 스탯 ()
        public int str, ap, hp, mind, attackPoint, magicPoint, indiPoint, e_fire, e_water, e_light, e_dark, e_best = 0; // 스텟창에 표시되는 스텟
        public ArrayList dmg_up, dmg_critical, dmg_all, dmg_bonus, str_int_up, dmg_adap, dmg_skillup;//그외 증뎀,크증뎀,모공,추뎀,공격력증가,스증뎀
        //아리아,은총,퍼펫티어 레벨
        //아리아,은총,퍼펫티어 스탯
        //신념의오라 체정, 열정 소악마 힘지능
        //신화 mythologyInfo - buffExplain 값 텍스트를 읽어서 변환시켜야함..
        //시로코픽 upgradeInfo로 텍스트 읽어서 변환
        //스탯,30렙 %, 30렙 +, 50렙 %, 50렙 +,100렙+
        //공격력 30렙 %
        public Equip()
        {
            dmg_up = new ArrayList();
            dmg_critical = new ArrayList();
            dmg_all = new ArrayList();
            dmg_bonus = new ArrayList();
            str_int_up = new ArrayList();
            dmg_adap = new ArrayList();
            dmg_skillup = new ArrayList();
        }
    }
}
