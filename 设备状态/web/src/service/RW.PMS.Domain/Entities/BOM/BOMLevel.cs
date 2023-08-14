using RW.PMS.CrossCutting.Extender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.BOM
{
    /// <summary>
    /// BOM级别
    /// 导出Excel时，BOM的级别
    /// 可以是1.1、1.1.1、1.2.1、1.2.1.1、1.2.1.1.1...
    /// </summary>
    public struct BOMLevel : IComparable
    {
        public static BOMLevel Empty = new BOMLevel();

        public BOMLevel(int level) : this(level, 1)
        {
        }

        public BOMLevel(int level, int order)
        {
            this.Level = level;
            this.Levels = new List<int>();
            this.Levels.Add(order);
            this.Order = order;
        }

        //使用层级和序号初始化
        public BOMLevel(BOMLevel parent, int level)
        {
            this.Level = level;
            this.Levels = new List<int>();

            if (parent.Level == level)//同级排序。
            {
                this.Levels.AddRange(parent.Levels);
                this.Order = this.Levels.Last() + 1;
                this.Levels.RemoveAt(this.Levels.Count - 1);
                this.Levels.Add(this.Order);
            }
            else if (level > parent.Level)//下级排序增加
            {
                if (parent.Levels != null)
                    this.Levels.AddRange(parent.Levels);
                this.Order = 1;
                this.Levels.Add(this.Order);
                this.Level = this.Levels.Count;
            }
            else//上级
            {
                //当前级别小于上一个级别，可能的情况，当前1,上一个3，当前2上一个3
                var lvs = parent.Levels.Take(level).ToList();
                var index = lvs.Last() + 1;
                lvs[lvs.Count - 1] = index;
                this.Levels.AddRange(lvs);

                this.Order = this.Levels.Last();
            }
        }

        public BOMLevel(string text)
        {
            var arr = text.Split('.');
            this.Level = arr.Length;
            this.Levels = arr.Select(x => x.ToInt32()).ToList();
            this.Order = 1;
            if (arr.Length > 1)
                this.Order = this.Levels.Last();
        }

        public int Level;
        public List<int> Levels;
        public int Order;

        public string LevelText => Levels == null ? "empty" : Levels.Select(x => x.ToString()).Aggregate((a, b) => a + "." + b);

        public BOMLevel GetParent()
        {
            if (Level == 1)
                return new BOMLevel(1, Order);
            //移除最后一个
            this.Levels.RemoveAt(this.Levels.Count - 1);
            this.Level--;
            var order = this.Levels.Last();
            return new BOMLevel(this.GetParent(), order);
        }

        public void AddLevel(int level)
        {
            Level++;
            //if (Levels == null) Levels = new List<int>();
            Levels.Add(level);
        }

        public BOMLevel Child(int order)
        {
            var lv = new BOMLevel(this, this.Level + 1);
            lv.Order = order;
            return lv;
        }

        public BOMLevel Next()
        {
            return Next(this.Level);
        }

        public BOMLevel Next(int level)
        {
            return new BOMLevel(this, level);
        }

        public BOMLevel Previous()
        {
            if (Levels.Count == 0)
                throw new Exception("级别不能为空。");
            return new BOMLevel(this, this.Order - 1);
        }

        public void Parent()
        {
            if (Levels.Count <= 1)
                throw new Exception("无父节点");
        }

        public static BOMLevel operator +(BOMLevel level, int order)
        {
            level.Order = level.Order + order;
            return level;
        }

        public static BOMLevel operator -(BOMLevel level, int order)
        {
            if (level.Order <= order)
                throw new Exception("序号不能相减。");
            level.Order = level.Order - order;
            return level;
        }

        /// <summary>
        /// 判断2个序号是否相同，并返回true/false
        /// </summary>
        public static bool operator ==(BOMLevel a, BOMLevel b)
        {
            return a.Level == b.Level && a.Order == b.Order && a.LevelText == b.LevelText;
        }

        public static bool operator !=(BOMLevel a, BOMLevel b)
        {
            return !(a == b);
        }

        public static bool operator >(BOMLevel a, BOMLevel b)
        {
            if (a == null && b != null) return false;
            if (a != null && b == null) return true;
            if (a == null && b == null) return false;

            for (int i = 0; i < a.Levels.Count && i < b.Levels.Count; i++)
            {
                if (a.Levels[i] > b.Levels[i]) return true;
                else if (a.Levels[i] < b.Levels[i]) return false;
            }
            return a.Level > b.Level;
        }

        public static bool operator <(BOMLevel a, BOMLevel b)
        {
            return !(a > b) && a != b;
        }

        public override string ToString()
        {
            return LevelText;
        }

        public int CompareTo(object obj)
        {
            if (obj is BOMLevel a)
            {
                if (this > a) return 1;
                else if (this == a) return 0;
                return -1;
            }
            else
            {
                throw new Exception("不支持比较此类型.");
            }
        }
    }
}
