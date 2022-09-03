using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodedDataGrouper
{
    internal class RowData
    {
        public int GroupIDID = -1;
        public string ID = "";
        public int IDID = -1;
        public string Group = "";
        public int GroupID = -1;
        public Category Category = new Category();
        public int CategoryID = -1;
        private List<double> _times = new List<double>();
        public double TotalTime { get; private set; } = 0.0;
        public double AverageTime => TotalTime / _times.Count;
        public int ColumnCount => 3 + _times.Count;

        public string this[int columnIndex]
        {
            get
            {
                switch ((MainForm.Columns)columnIndex)
                {
                    case MainForm.Columns.GroupID:
                        return GroupIDID.ToString();
                    case MainForm.Columns.ID:
                        return ID;
                    case MainForm.Columns.Group:
                        return Group;
                    case MainForm.Columns.Category:
                        return Category.Name.ToString();
                    default:
                        return _times[columnIndex - (int)MainForm.Columns.Times].ToString();
                }
            }
        }

        public void AddTime(double time)
        {
            _times.Add(time);
            TotalTime += time;
        }

        public override bool Equals(object? obj)
        {
            return obj != null && obj.GetType() == GetType() && obj.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            return IDID.GetHashCode() ^ GroupID.GetHashCode();
        }

        public static bool operator ==(RowData left, RowData right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(RowData left, RowData right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return $"[Row Data: (ObsID: {ID}({IDID}), Behave: {Group}({GroupID}), BehaveType: {Category}({CategoryID}), Avg. Time: {AverageTime}s]";
        }
    }
}
