using RW.PMS.CrossCutting.Extender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.BOM
{
    /// <summary>
    /// 用于描述BOM的版本号
    /// </summary>
    public struct BOMVersion
    {
        static string vText = "ABCDEFGHJKLMNPQRSTUVWXYZ";

        static string GetText(int index)
        {
            //A.0、B.0...Z.0..A.1、B.1...
            if (index == 0)
                return "NoneVersion";//未设置版本号

            string t = "";
            t = $"V{vText[index % vText.Length - 1]}.{index / vText.Length}";
            return t;
        }

        static int GetIndex(string text)
        {
            //格式VA.0....
            if (text.Length < 4) throw new FormatException($"版本格式不正确，长度只能是4个字符。");
            if (text[1] < 'A' || text[1] > 'Z') throw new FormatException($"版本格式不正确，第二个字符应该为A-Z的字母。");
            int bigV = text.Substring(3).ToInt32();
            if (bigV < 0) throw new FormatException($"版本格式不正确，第四个字符应是0-9的数字。");

            var index = vText.IndexOf(text[1]) + 1;
            index += bigV * 24;//A-Z支持24个字符，其中字母‘O’和‘I’不计入版本规则。

            return index;
        }

        public BOMVersion(int index) => this.VersionIndex = index;

        public BOMVersion(string text)
        {
            this.VersionIndex = GetIndex(text);
        }

        public int VersionIndex { get; set; }
        public string VersionText => GetText(VersionIndex);

        public BOMVersion Next() => new BOMVersion(VersionIndex + 1);

        public BOMVersion Previous() => new BOMVersion(VersionIndex - 1);

        public override string ToString() => VersionText;

        public static bool operator >(BOMVersion a, BOMVersion b) => a.VersionIndex > b.VersionIndex;

        public static bool operator <(BOMVersion a, BOMVersion b) => a.VersionIndex < b.VersionIndex;

        public static bool operator ==(BOMVersion a, BOMVersion b) => a.VersionIndex == b.VersionIndex;

        public static bool operator !=(BOMVersion a, BOMVersion b) => !(a == b);
    }
}
