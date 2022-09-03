using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace CodedDataGrouper
{
    /// <summary>
    /// Holds the data that configures how the Excel file is read.
    /// This data is inputted from the User, and saved to the disc.
    /// </summary>
    [Serializable]
    internal class ConfigurationData
    {
        public string EventsSheetName { get; set; } = "";
        public string ColumnID { get; set; } = "";
        public string ColumnGroup { get; set; } = "";
        public string ColumnCategory { get; set; } = "";
        public float GlobalThreshold { get; set; } = 0.0f;
        public bool GenerateExcelSheet { get; set; } = true;
        public bool SearchForPatterns { get; set; } = true;
        public string InterRaterReliability { get; set; } = string.Empty;
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<string> ColumnsTime { get; set; } = new List<string>();
        public List<string> Patterns { get; set; } = new List<string>();
        public List<SuperGroup> SuperGroups { get; set; } = new List<SuperGroup>();

        [JsonIgnore]
        private Dictionary<int, List<int>> _superGroupIDs = new Dictionary<int, List<int>>();

        public ConfigurationData()
        {
        }

        /// <summary>
        /// Gets the category with the given name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Category GetCategory(string name)
        {
            return Categories.FirstOrDefault(c => c.Name == name) ?? new Category(name);
        }

        /// <summary>
        /// Gets the number of columns that will be used, based on the stored data.
        /// </summary>
        /// <returns></returns>
        public int GetColumnCount()
        {
            int count = 0;

            //if(!string.IsNullOrEmpty(ColumnID)) count++;
            //if(!string.IsNullOrEmpty(ColumnGroup)) count++;
            //if(!string.IsNullOrEmpty(ColumnCategory)) count++;
            count += 3 + ColumnsTime.Count + 1;//adding 1 for group ID

            return count;
        }

        public bool CheckMatchingSuperGroups(int leftGroupID, int rightGroupID)
        {
            foreach(int lID in GetSuperGroupIDs(leftGroupID))
            {
                foreach(int rID in GetSuperGroupIDs(rightGroupID))
                {
                    if(lID == rID)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool CheckMatchingSuperGroups(int groupID, int[] superGroupIDs)
        {
            foreach(int sID in superGroupIDs)
            {
                foreach(int gsID in GetSuperGroupIDs(groupID))
                {
                    if(gsID == sID)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public int[] GetSuperGroupIDs(int groupID)
        {
            if(_superGroupIDs.TryGetValue(groupID, out List<int>? ids))
            {
                return ids.ToArray();
            }

            return Array.Empty<int>();
        }

        public void SetGroupIDs(Dictionary<string, int> groupIDs)
        {
            //clear old group IDs
            _superGroupIDs.Clear();

            //for each group ID number, we want to keep track of what super group it is in
            foreach(var pair in groupIDs)
            {
                //find the super group with this group name
                for(int i = 0; i < SuperGroups.Count; i++)
                {
                    SuperGroup group = SuperGroups[i];

                    if (group.Contains(pair.Key))
                    {
                        //add to super groups
                        List<int>? ids;
                        if(_superGroupIDs.TryGetValue(pair.Value, out ids))
                        {
                            ids.Add(i);
                        } else
                        {
                            _superGroupIDs.Add(pair.Value, new List<int>(new int[1] { i }));
                        }
                    }
                }
            }

            //DEBUG
            //MainForm.Instance?.Logger.Log($"Super groups: {string.Join(" | ", _superGroupIDs.Select(pair => $"[{pair.Key}: {string.Join(", ", pair.Value)}]"))}");
        }

        /// <summary>
        /// Returns an array of strings containing the names of every column that will be used.
        /// </summary>
        /// <returns></returns>
        public string[] ToStringArray()
        {
            List<string> list = new List<string>(GetColumnCount());
            list.AddRange(new string[] { "Group ID", ColumnID, ColumnGroup, ColumnCategory });
            list.AddRange(ColumnsTime);

            return list.ToArray();
        }
    }
}
