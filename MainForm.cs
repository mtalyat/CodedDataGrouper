using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using BPAD;
using System.Text;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Linq;

namespace CodedDataGrouper
{
    public partial class MainForm : Form
    {
        public static MainForm? Instance;

        #region Configuration

        public enum Columns : int
        {
            GroupID,
            ID,
            Group,
            Category,
            Times
        }

        private const string IRR_NUMBER_FORMAT = "0.000";

        private int columnCount => _columnNames.Length;
        private string[] _columnNames = new string[0];

        private string directoryPath => System.Windows.Forms.Application.CommonAppDataPath;

        private string filePath => directoryPath + @"\settings.txt";

        private string lastAnalyzedFilePath = "";

        #endregion

        internal ConfigurationData Data { get; private set; } = new ConfigurationData();

        internal Logger Logger { get; private set; }

        private EventList _events;

        public MainForm()
        {
            Instance = this;

            InitializeComponent();

            Logger = new Logger(OutputTextbox);

            _events = new EventList();
        }

        private void AnalyzeData(string filePath, string existingSheet = "")
        {
            if(string.IsNullOrEmpty(filePath))
            {
                Logger.Log("Could not analyzed, no file selected.");
                return;
            }

            lastAnalyzedFilePath = filePath;

            _events = new EventList();

            Logger.Log($"Analyzing file \"{filePath}\"");

            //open excel
            Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
            application.DisplayAlerts = false;

            //get the workbooks
            Workbooks workbooks = application.Workbooks;

            //open the workbook (file)
            Workbook workbook = workbooks.Open(filePath);

            //open the sheet we want to use (index starts at 1)
            Worksheet worksheet;
           
            string sheetName = string.IsNullOrEmpty(existingSheet) ? Data.EventsSheetName : existingSheet;

            try
            {
                worksheet = (Worksheet)workbook.Worksheets[sheetName];
            }
            catch
            {
                Logger.Log($"Could not find worksheet \"{sheetName}\".");
                return;
            }

            //get the range of cells that this worksheet contains
            Microsoft.Office.Interop.Excel.Range rangeAll = worksheet.UsedRange;
            int rangeHeight = rangeAll.Rows.Count;
            int rangeWidth = rangeAll.Columns.Count;

            //find the columns we care about
            Microsoft.Office.Interop.Excel.Range?[] columns = new Microsoft.Office.Interop.Excel.Range[Data.GetColumnCount()];

            //do work
            GetColumns(rangeAll, rangeWidth, columns);
            _events = ParseEvents(rangeHeight, columns);

            //DEBUG
            //show counts of each behavior
            //Dictionary<string, int> behaviors = new Dictionary<string, int>();
            //foreach (RowData rd in _events.GetRowDatas())
            //{
            //    if (!behaviors.TryAdd(rd.Group, 1))
            //    {
            //        behaviors[rd.Group]++;
            //    }
            //}

            ////sort from largest to smallest to print
            //Logger.Log($"Behavior counts: {string.Join(",\t", behaviors.OrderByDescending(b => b.Value).Select(pair => $"[{pair.Key}: {pair.Value}]"))}");

            if (Data.GenerateExcelSheet)
            {
                //create sheet with events
                CreateSheetWithEvents(workbook, _events, columns);

                //create shet with IRR
                switch (Data.InterRaterReliability)
                {
                    case "Krippendorf's Alpha":
                        CreateSheetWithInterRaterReliability(workbook, _events, InterRaterReliability.KrippendorfsAlpha);
                        break;
                    case "Percentage":
                        CreateSheetWithInterRaterReliability(workbook, _events, InterRaterReliability.RaterPercentage);
                        break;
                        //any other option is considered "none"
                }

                //create sheet with patterns
                if (Data.SearchForPatterns)
                {
                    Pattern[] patterns = CreatePatterns();
                    CreateSheetWithPatterns(workbook, _events, patterns);
                }

                Logger.Log("Showing Excel application result...");

                //show application
                application.Visible = true;
                application.DisplayAlerts = true;
            }
            else
            {
                //if do not create sheet but still search for patterns
                if (Data.SearchForPatterns)
                {
                    Pattern[] patterns = CreatePatterns();
                    int[][][] identifiedPatterns = FindPatterns(patterns);

                    Logger.Log("Found patterns at:");
                    for (int i = 0; i < patterns.Length; i++)
                    {
                        StringBuilder sb = new StringBuilder($"Found {identifiedPatterns[i].Length} patterns for {patterns[i]}: ");

                        foreach (int[] p in identifiedPatterns[i])
                        {
                            sb.Append("[");

                            for (int j = 0; j < p.Length; j++)
                            {
                                sb.Append(p[j] + 2);//add 2: 1 to skip header, 1 to account for excel starting indexing at 1

                                if (j != p.Length - 1)
                                {
                                    sb.Append(", ");
                                }
                            }

                            sb.Append("]");
                        }

                        Logger.Log(sb.ToString());
                    }
                }

                //still IRR but not a sheet
                switch (Data.InterRaterReliability)
                {
                    case "Krippendorf's Alpha":
                        Logger.Log(InterRaterReliability.All(_events, InterRaterReliability.KrippendorfsAlpha, "Krippendorf's Alpha"));
                        break;
                    case "Percentage":
                        Logger.Log(InterRaterReliability.All(_events, InterRaterReliability.RaterPercentage, "Rater Percentage"));
                        break;
                        //any other option is considered "none"
                }

            }

            Logger.Log("Cleaning up...");

            //close data
            Marshal.ReleaseComObject(rangeAll);
            for (int i = 0; i < columns.Length; i++)
            {
                if (columns[i] != null)
                    Marshal.ReleaseComObject(columns[i]);
            }
            Marshal.ReleaseComObject(worksheet);
            Marshal.ReleaseComObject(workbook);
            Marshal.ReleaseComObject(workbooks);
            Marshal.ReleaseComObject(application);

            GC.Collect();
            GC.WaitForPendingFinalizers();

            Logger.Log("Done!");
        }

        #region Form Events

        private void MainForm_Load(object sender, EventArgs e)
        {
            //load data
            if (File.Exists(filePath))
            {
                string settings = File.ReadAllText(filePath);
                ConfigurationData? d = JsonConvert.DeserializeObject<ConfigurationData>(settings);
                if (d != null)
                {
                    //if it worked properly, we don't need to notify, so no Log statement here
                    Data = d;
                }
                else
                {
                    Data = new ConfigurationData();
                    Logger.Log($"Could not load settings file from \"{filePath}\".");
                }
            }
            else
            {
                Data = new ConfigurationData();
                Logger.Log($"Created new settings file at \"{filePath}\".");
            }

            UpdateConfigurationData();

            //test
            //MessageBox.Show(BPAD.Pattern.Test());
            ////MessageBox.Show(BPAD.Pattern.Parse("Ask for input -> {5s}Acknowledge input -> {20s}Use input => Ask for information").ToString());
            //Environment.Exit(0);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //save data
            string settings = JsonConvert.SerializeObject(Data, Formatting.Indented);
            File.WriteAllText(filePath, settings);
        }

        private void SelectFileButton_Click(object sender, EventArgs e)
        {
            //compile configuration data so it is updated
            if (!CompileConfigurationData())
            {
                //not all data is valid
                //do nothing
                return;
            }

            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "Select an Excel file with Boris data.";
            ofd.Filter = "Excel file (*.xlsx)|*xlsx";
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //file selected

                //disable button
                SelectFileButton.Enabled = false;

                //analyze data
                AnalyzeData(ofd.FileName);

                //restore button
                SelectFileButton.Enabled = true;
            }
        }

        private void EditCategoriesButton_Click(object sender, EventArgs e)
        {
            EditCategoriesForm form = new EditCategoriesForm(this);

            form.Show();
        }

        private void IDColumnTextBox_TextChanged(object sender, EventArgs e)
        {
            Data.ColumnID = IDColumnTextBox.Text;
        }

        private void GroupColumnTextBox_TextChanged(object sender, EventArgs e)
        {
            Data.ColumnGroup = GroupColumnTextBox.Text;
        }

        private void GlobalThresholdUpDown_ValueChanged(object sender, EventArgs e)
        {
            Category.GlobalThreshold = (float)GlobalThresholdUpDown.Value;
            Data.GlobalThreshold = Category.GlobalThreshold;
        }

        private void CategoryColumnTextBox_TextChanged(object sender, EventArgs e)
        {
            Data.ColumnCategory = CategoryColumnTextBox.Text;

            CheckCategoryColumn();
        }

        private void CheckCategoryColumn()
        {
            //special, disable editing categories if there is no column name
            bool enabled = !string.IsNullOrEmpty(Data.ColumnCategory);

            GlobalThresholdUpDown.Enabled = !enabled;

            CategoriesRichTextBox.Enabled = enabled;
            EditCategoriesButton.Enabled = enabled;
        }

        private void TimeColumnsRichTextBox_TextChanged(object sender, EventArgs e)
        {
            Data.ColumnsTime = TimeColumnsRichTextBox.Lines.ToList();
        }

        private void BpadPatternsTextBox_TextChanged(object sender, EventArgs e)
        {
            Data.Patterns = BpadPatternsTextBox.Lines.ToList();
        }

        private void EditSuperGroupsButton_Click(object sender, EventArgs e)
        {
            EditSuperGroupsForm form = new EditSuperGroupsForm(this);

            form.Show();
        }

        private void GenerateExcelSheetCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Data.GenerateExcelSheet = GenerateExcelSheetCheckBox.Checked;
        }

        private void SearchForPatternsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Data.SearchForPatterns = SearchForPatternsCheckBox.Checked;

            BpadGroupBox.Enabled = Data.SearchForPatterns;
        }

        private void InterRaterReliabilityComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data.InterRaterReliability = InterRaterReliabilityComboBox.SelectedItem?.ToString() ?? "";
        }

        private void OpenSettingsFileLocationLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                Arguments = directoryPath,
                FileName = "explorer.exe"
            };

            Process.Start(startInfo);
        }

        private void EventsSheetNameTextBox_TextChanged(object sender, EventArgs e)
        {
            Data.EventsSheetName = EventsSheetNameTextBox.Text;
        }

        #endregion

        #region Configuration Data/Settings

        private bool CompileConfigurationData()
        {
            _columnNames = Data.ToStringArray();

            //make sure the data is valid

            if (string.IsNullOrWhiteSpace(Data.EventsSheetName))
            {
                MessageBox.Show(this, "The events sheet name cannot be empty!", "Invalid data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(Data.ColumnGroup))
            {
                MessageBox.Show(this, "The group column name cannot be empty!", "Invalid data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (Data.ColumnsTime.Count == 0)
            {
                MessageBox.Show(this, "The time column names list cannot be empty!", "Invalid data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (Data.ColumnsTime.Any(s => string.IsNullOrWhiteSpace(s)))
            {
                MessageBox.Show(this, "The time column name cannot be empty!", "Invalid data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        internal void UpdateConfigurationData()
        {
            EventsSheetNameTextBox.Text = Data.EventsSheetName;
            IDColumnTextBox.Text = Data.ColumnID;
            GroupColumnTextBox.Text = Data.ColumnGroup;
            GlobalThresholdUpDown.Value = (decimal)Data.GlobalThreshold;
            GenerateExcelSheetCheckBox.Checked = Data.GenerateExcelSheet;
            SearchForPatternsCheckBox.Checked = Data.SearchForPatterns;
            int irrIndex = InterRaterReliabilityComboBox.Items.IndexOf(Data.InterRaterReliability);
            InterRaterReliabilityComboBox.SelectedIndex = irrIndex == -1 ? 0 : irrIndex;
            CategoryColumnTextBox.Text = Data.ColumnCategory;
            CategoriesRichTextBox.Lines = Data.Categories.Select(c => c.ToString()).ToArray();
            TimeColumnsRichTextBox.Lines = Data.ColumnsTime.ToArray();
            BpadGroupBox.Enabled = Data.SearchForPatterns;
            BpadPatternsTextBox.Lines = Data.Patterns.ToArray();

            CheckCategoryColumn();
        }

        #endregion

        #region Code Grouper Work

        private void SetProgressTotal(int total, int startingValue = 0)
        {
            OutputProgressBar.Maximum = total;
            OutputProgressBar.Value = startingValue;
        }

        private void ShowProgress(int progress)
        {
            OutputProgressBar.Value = Math.Clamp(progress, OutputProgressBar.Minimum, OutputProgressBar.Maximum);
        }

        private void Abort()
        {
            Logger.Log("Aborting process.");
        }

        private int GetColor(int index)
        {
            //if index is invalid, return white
            if (index < 0)
            {
                return ColorTranslator.ToOle(Color.White);
            }

            int r, g, b;

            HsvToRgb(index * 98.0, 0.2, 0.96, out r, out g, out b);

            return ColorTranslator.ToOle(Color.FromArgb(r, g, b));
        }

        //https://stackoverflow.com/questions/1335426/is-there-a-built-in-c-net-system-api-for-hsv-to-rgb
        private void HsvToRgb(double h, double S, double V, out int r, out int g, out int b)
        {
            double H = h;
            while (H < 0) { H += 360; };
            while (H >= 360) { H -= 360; };
            double R, G, B;
            if (V <= 0)
            { R = G = B = 0; }
            else if (S <= 0)
            {
                R = G = B = V;
            }
            else
            {
                double hf = H / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = V * (1 - S);
                double qv = V * (1 - S * f);
                double tv = V * (1 - S * (1 - f));
                switch (i)
                {

                    // Red is the dominant color

                    case 0:
                        R = V;
                        G = tv;
                        B = pv;
                        break;

                    // Green is the dominant color

                    case 1:
                        R = qv;
                        G = V;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = V;
                        B = tv;
                        break;

                    // Blue is the dominant color

                    case 3:
                        R = pv;
                        G = qv;
                        B = V;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = V;
                        break;

                    // Red is the dominant color

                    case 5:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                    case 6:
                        R = V;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // The color is not defined, we should throw an error.

                    default:
                        //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                        R = G = B = V; // Just pretend its black/white
                        break;
                }
            }
            r = Clamp((int)(R * 255.0));
            g = Clamp((int)(G * 255.0));
            b = Clamp((int)(B * 255.0));
        }

        /// <summary>
        /// Clamp a value to 0-255
        /// </summary>
        private int Clamp(int i)
        {
            if (i < 0) return 0;
            if (i > 255) return 255;
            return i;
        }

        #endregion

        #region Excel Work

        /// <summary>
        /// Finds the columns that are going to be used, and hides the rest.
        /// </summary>
        /// <param name="rangeAll"></param>
        /// <param name="rangeWidth"></param>
        /// <param name="columns"></param>
        private void GetColumns(Microsoft.Office.Interop.Excel.Range rangeAll, int rangeWidth, Microsoft.Office.Interop.Excel.Range?[] columns)
        {
            Logger.Log("Finding columns...");

            Microsoft.Office.Interop.Excel.Range cell;

            for (int i = 1; i <= rangeWidth; i++)
            {
                //get the top cell of each column
                cell = rangeAll.Cells[1, i];
                string headerName = cell.Value;

                //try to assign it to a column
                for (int j = 0; j < columns.Length; j++)
                {
                    //the name matches
                    if (!string.IsNullOrEmpty(_columnNames[j]) && headerName == _columnNames[j])
                    {
                        columns[j] = rangeAll.Columns[i];

                        break;
                    }
                }

                ShowProgress(i);
            }
        }

        /// <summary>
        /// Gets the ID, or creates a new one, from the given ID Dictionary.
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private int GetID(Dictionary<string, int> dict, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return -1;
            }

            int id;

            if (dict.TryGetValue(key, out id))
            {
                return id;
            }

            id = dict.Count;

            dict.Add(key, id);

            return id;
        }

        private EventList ParseEvents(int rangeHeight, Microsoft.Office.Interop.Excel.Range?[] columns)
        {
            Logger.Log("Parsing Excel data...");

            EventList events = new EventList();

            List<RowData> rowDatas = new List<RowData>();

            Dictionary<string, int> IDIDs = new Dictionary<string, int>();
            Dictionary<string, int> groupIDs = new Dictionary<string, int>();
            Dictionary<string, int> categoryIDs = new Dictionary<string, int>();

            RowData rd;
            Microsoft.Office.Interop.Excel.Range? groupIDCell;
            Microsoft.Office.Interop.Excel.Range? observationIdCell;
            Microsoft.Office.Interop.Excel.Range? behaviorCell;
            Microsoft.Office.Interop.Excel.Range? behaviorTypeCell;

            SetProgressTotal(rangeHeight - 1);

            //cycle once, just gather data
            for (int i = 2; i <= rangeHeight; i++)
            {
                //get the cells we use multiple times
                groupIDCell = columns[Columns.GroupID.Index()]?.Cells[i];
                observationIdCell = columns[Columns.ID.Index()]?.Cells[i];
                behaviorCell = columns[Columns.Group.Index()]?.Cells[i];
                behaviorTypeCell = columns[Columns.Category.Index()]?.Cells[i];

                try
                {
                    rd = new RowData
                    {
                        GroupIDID = (int)(groupIDCell?.Value ?? -1),
                        ID = observationIdCell?.Value.ToString() ?? "",
                        IDID = GetID(IDIDs, observationIdCell?.Value.ToString() ?? "-1"),
                        Group = behaviorCell?.Value.ToString() ?? "",
                        GroupID = GetID(groupIDs, behaviorCell?.Value.ToString() ?? "-1"),
                        Category = Data.GetCategory(behaviorTypeCell?.Value.ToString() ?? ""),
                        CategoryID = GetID(categoryIDs, behaviorTypeCell?.Value.ToString() ?? "-1"),
                    };

                    //get the times
                    for (int j = 0; j < Data.ColumnsTime.Count; j++)
                    {
                        rd.AddTime(double.Parse(columns[Columns.Times.Index() + j]?.Cells[i].Value.ToString()));
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log($"Could not parse data correctly on row {i}: {ex.Message}");
                    continue;
                }

                rowDatas.Add(rd);

                ShowProgress(i - 1);
            }

            ShowProgress(rangeHeight - 1);

            //update super groups
            Data.SetGroupIDs(groupIDs);

            //add to event list
            foreach (RowData row in rowDatas)
            {
                events.Add(row, Data);
            }

            Logger.Log($"Grouped {events.RowCount} events into {events.GetEventCount()} groups.");

            return events;
        }

        private Pattern[] CreatePatterns()
        {
            Logger.Log("Creating patterns...");

            string[] patternStrs = BpadPatternsTextBox.Lines;
            Pattern[] patterns = new Pattern[patternStrs.Length];

            //create all the pattern objects
            for (int i = 0; i < patternStrs.Length; i++)
            {
                try
                {
                    patterns[i] = Pattern.Parse(patternStrs[i]);
                }
                catch (ParsingException pe)
                {
                    Logger.Log($"Error parsing \"{patternStrs[i]}\". Error message: \"{pe.Message}\".");
                    patterns[i] = Pattern.Empty;
                }
            }

            return patterns;
        }

        private int[][][] FindPatterns(Pattern[] patterns)
        {
            if (!patterns.Any())
            {
                return Array.Empty<int[][]>();
            }

            Logger.Log("Searching for patterns...");

            //get names and times
            RowData[] rds = _events.GetRowDatas().ToArray();
            string[] names = rds.Select(e => e.Group).ToArray();
            float[] times = rds.Select(e => (float)e.AverageTime).ToArray();

            //evaluate
            //one at a time so we can report progress

            List<int[][]> outputs = new List<int[][]>();

            SetProgressTotal(patterns.Length);

            for (int i = 0; i < patterns.Length; i++)
            {
                outputs.Add(patterns[i].Evaluate(names, times));

                ShowProgress(i + 1);
            }

            return outputs.ToArray();
        }

        private void NameSheet(Worksheet sheet, string name)
        {
            try
            {
                sheet.Name = name;
            }
            catch
            {
                bool named = false;
                int attempts = 1;
                do
                {
                    try
                    {
                        sheet.Name = $"{name} {attempts}";
                        named = true;
                        break;
                    }
                    catch
                    {
                        //keep going
                    }
                } while (!named);
            }
        }

        private Worksheet CreateSheet(Workbook workbook, string name)
        {
            Sheets? sheets = workbook.Sheets;

            //create the sheet
            Worksheet sheet = (Worksheet)sheets.Add(Type.Missing, sheets[sheets.Count], Type.Missing, Type.Missing);
            NameSheet(sheet, name);

            //clean up
            Marshal.ReleaseComObject(sheets);

            return sheet;
        }

        private Worksheet GetSheet(Workbook workbook, string name)
        {
            Worksheet worksheet;

            try
            {
                worksheet = (Worksheet)workbook.Worksheets[Data.EventsSheetName];
            }
            catch
            {
                //if could not find the sheet with the name, then create one
                worksheet = CreateSheet(workbook, name);
            }


            return worksheet;
        }

        private void CreateSheetWithEvents(Workbook workbook, EventList events, Microsoft.Office.Interop.Excel.Range?[] columns)
        {
            Logger.Log("Creating events sheet...");

            int columnCount = columns.Length;

            //create the sheet
            Worksheet sheet = CreateSheet(workbook, "Grouped events");

            //print each row
            List<EventList.Event> eventsList = events.GetEvents();

            SetProgressTotal(columnCount);

            Microsoft.Office.Interop.Excel.Range cell;

            //start with headers
            for (int i = 0; i < columnCount; i++)
            {
                cell = sheet.Cells[1, i + 1];
                cell.Value = columns[i]?.Cells[1, 1].Value;
                cell.Font.Bold = true;

                ShowProgress(i + 1);
            }

            //group ID
            cell = sheet.Cells[1, 1];
            cell.Value = "Group ID";
            cell.Font.Bold = true;

            //now print every column

            SetProgressTotal(eventsList.Count);

            int row = 1;//start under headers
            int progress = 0;
            RowData rd;
            Microsoft.Office.Interop.Excel.Range? tempRow;
            Microsoft.Office.Interop.Excel.Range? tempCell;

            int groupID = 0;

            foreach (EventList.Event e in eventsList)
            {
                for (int r = 0; r < e.RowDatas.Count; r++)
                {
                    rd = e.RowDatas[r];

                    tempRow = sheet.Range[sheet.Cells[row + r + 1, 2], sheet.Cells[row + r + 1, columnCount + 1]];

                    //color row
                    tempRow.Interior.Color = GetColor(rd.IDID);

                    //add top border if on top
                    //this will also set one for the previous event's bottom
                    if (r == 0)
                    {
                        tempRow.Borders.SetBorders(XlLineStyle.xlContinuous, XlBorderWeight.xlThick, XlRgbColor.rgbRed, XlBordersIndex.xlEdgeTop);
                    }

                    //finally set all data in cells
                    for (int c = 0; c < columnCount; c++)
                    {
                        tempCell = sheet.Cells[row + r + 1, c + 1];
                        dynamic value = rd[c];

                        if(c == (int)Columns.GroupID && value == "-1")
                        {
                            value = groupID;
                        }

                        tempCell.Value = value;

                        //set decimal places for all numbers
                        if (c >= (int)Columns.Times)
                        {
                            tempCell.NumberFormat = "#,##0.00";
                        }
                    }
                }

                row += e.RowDatas.Count;

                groupID++;

                progress++;
                ShowProgress(progress);
            }

            //autofit columns
            SetProgressTotal(columnCount);

            for (int i = 0; i < columnCount; i++)
            {
                Microsoft.Office.Interop.Excel.Range? c = sheet.Cells[1, i + 1];
                c.EntireColumn.AutoFit();

                ShowProgress(i + 1);
            }

            //clean up
            Marshal.ReleaseComObject(sheet);
        }

        private void CreateSheetWithPatterns(Workbook workbook, EventList events, Pattern[] patterns)
        {
            if (!patterns.Any())
            {
                return;
            }

            int[][][] identifiedPatterns = FindPatterns(patterns);

            if (!identifiedPatterns.Any() || !identifiedPatterns[0].Any())
            {
                Logger.Log("No patterns found!");

                return;
            }

            Logger.Log("Creating patterns sheet...");

            Worksheet sheet = CreateSheet(workbook, "Patterns");

            List<RowData> rowDatas = events.GetRowDatas();

            Microsoft.Office.Interop.Excel.Range cell;
            RowData rd;

            int row = 1;

            for (int i = 0; i < identifiedPatterns.Length; i++)
            {
                //write pattern name
                cell = sheet.Cells[row++, 1];
                cell.Value = patterns[i].Name;
                cell.Font.Bold = true;

                int[][] identifiedPatternsData = identifiedPatterns[i];

                //write each of the found patterns
                for (int j = 0; j < identifiedPatternsData.Length; j++)
                {
                    int[] identifiedPatternsEvents = identifiedPatternsData[j];

                    //writing individual events
                    for (int k = 0; k < identifiedPatternsEvents.Length; k++)
                    {
                        rd = rowDatas[identifiedPatternsEvents[k]];

                        //individual row data values
                        for (int l = 0; l < rd.ColumnCount; l++)
                        {
                            cell = sheet.Cells[row, 2 + l];
                            cell.Value = rd[l];
                        }

                        //move to next row
                        row++;
                    }

                    //space
                    row++;
                }
            }

            //print out the patterns in a organized list, basically

            Marshal.ReleaseComObject(sheet);
        }

        private void CreateSheetWithInterRaterReliability(Workbook workbook, EventList events, InterRaterReliability.IRR irrMethod)
        {
            Logger.Log("Creating IRR sheet...");

            Worksheet sheet = CreateSheet(workbook, "InterRater Reliability");

            //create a matrix with the IRR between each observer
            int observerCount = events.ObvserverCount;

            List<string> observerIDs = events.GetObserverIDs();

            Microsoft.Office.Interop.Excel.Range cell;

            int row = 1;

            //print overall IRR
            cell = sheet.Cells[row, 1];
            cell.Value = "All observers:";
            cell = sheet.Cells[row, 2];
            cell.Value = irrMethod(events, Enumerable.Range(0, events.ObvserverCount).ToArray());
            cell.NumberFormat = IRR_NUMBER_FORMAT;

            row += 2;

            //print names first
            for (int i = 0; i < observerCount; i++)
            {
                cell = sheet.Cells[row, i + 2];
                cell.Value = observerIDs[i];
                cell.Font.Bold = true;
                cell = sheet.Cells[i + row + 1, 1];
                cell.Value = observerIDs[i];
                cell.Font.Bold = true;
            }

            //print the IRR values in a matrix form
            for (int r = 0; r < observerCount; r++)
            {
                for (int c = 0; c < observerCount; c++)
                {
                    //if in the diagonal, it is always one
                    if (r == c)
                    {
                        cell = sheet.Cells[r + row + 1, c + 2];
                        cell.Value = 1.0;
                        cell.NumberFormat = IRR_NUMBER_FORMAT;
                    }
                    else
                    {
                        //otherwise, do IRR and set for both sides of the matrix
                        double irr = irrMethod(events, r, c);

                        cell = sheet.Cells[r + row + 1, c + 2];
                        cell.Value = irr;
                        cell.NumberFormat = IRR_NUMBER_FORMAT;
                        cell = sheet.Cells[c + row + 1, r + 2];
                        cell.Value = irr;
                        cell.NumberFormat = IRR_NUMBER_FORMAT;
                    }
                }
            }

            //clean up
            Marshal.ReleaseComObject(sheet);
        }

        #endregion
    }

    internal static class Extensions
    {
        /// <summary>
        /// Sets the Border style in one method.
        /// </summary>
        /// <param name="border"></param>
        /// <param name="style"></param>
        /// <param name="weight"></param>
        /// <param name="color"></param>
        public static void SetBorder(this Border border, XlLineStyle style, XlBorderWeight weight, XlRgbColor color)
        {
            border.LineStyle = style;
            border.Color = color;
            border.Weight = weight;
        }

        /// <summary>
        /// Sets the given Border styles for each given side.
        /// </summary>
        /// <param name="border"></param>
        /// <param name="style"></param>
        /// <param name="weight"></param>
        /// <param name="color"></param>
        /// <param name="indexes"></param>
        public static void SetBorders(this Borders border, XlLineStyle style, XlBorderWeight weight, XlRgbColor color, params XlBordersIndex[] indexes)
        {
            for (int i = 0; i < indexes.Length; i++)
            {
                border[indexes[i]].SetBorder(style, weight, color);
            }
        }

        /// <summary>
        /// Sets the given Border styles for all 4 sides of the Range.
        /// </summary>
        /// <param name="border"></param>
        /// <param name="style"></param>
        /// <param name="weight"></param>
        /// <param name="color"></param>
        public static void SetAllBorders(this Borders border, XlLineStyle style, XlBorderWeight weight, XlRgbColor color) => SetBorders(border, style, weight, color, XlBordersIndex.xlEdgeBottom, XlBordersIndex.xlEdgeRight, XlBordersIndex.xlEdgeTop, XlBordersIndex.xlEdgeLeft);

        public static int Index(this MainForm.Columns column)
        {
            return (int)column;
        }

        public static Microsoft.Office.Interop.Excel.Range GetRange(this Microsoft.Office.Interop.Excel.Range range, int row1, int col1, int row2, int col2)
        {
            return range.Range[range.Cells[row1, col1], range.Cells[row2, col2]];
        }

        public static int GetValueAsInt(this Microsoft.Office.Interop.Excel.Range? range)
        {
            int result;

            if (int.TryParse(range?.Value, out result))
            {
                return result;
            }

            return -1;
        }
    }
}