namespace CodedDataGrouper
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.SelectFileButton = new System.Windows.Forms.Button();
            this.OutputTextbox = new System.Windows.Forms.RichTextBox();
            this.OutputProgressBar = new System.Windows.Forms.ProgressBar();
            this.OutputTextboxLabel = new System.Windows.Forms.Label();
            this.ConfigurationGroupBox = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.ShowExcelWhenDoneCheckBox = new System.Windows.Forms.CheckBox();
            this.GenerateEventsSheetCheckBox = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.EventsSheetNameTextBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.InterRaterReliabilityComboBox = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.SearchForPatternsCheckBox = new System.Windows.Forms.CheckBox();
            this.GenerateExcelSheetCheckBox = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.EditSuperGroupsButton = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.GlobalThresholdUpDown = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CategoriesRichTextBox = new System.Windows.Forms.RichTextBox();
            this.EditCategoriesButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.CategoryColumnTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TimeColumnsRichTextBox = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.GroupColumnTextBox = new System.Windows.Forms.TextBox();
            this.IDColumnTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BpadPatternsTextBox = new System.Windows.Forms.RichTextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BpadGroupBox = new System.Windows.Forms.GroupBox();
            this.OpenSettingsFileLocationLinkLabel = new System.Windows.Forms.LinkLabel();
            this.OutputSubProgressBar = new System.Windows.Forms.ProgressBar();
            this.ConfigurationGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GlobalThresholdUpDown)).BeginInit();
            this.BpadGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // SelectFileButton
            // 
            this.SelectFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectFileButton.Location = new System.Drawing.Point(14, 16);
            this.SelectFileButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SelectFileButton.Name = "SelectFileButton";
            this.SelectFileButton.Size = new System.Drawing.Size(1393, 31);
            this.SelectFileButton.TabIndex = 0;
            this.SelectFileButton.Text = "Select File(s)";
            this.SelectFileButton.UseVisualStyleBackColor = true;
            this.SelectFileButton.Click += new System.EventHandler(this.SelectFileButton_Click);
            // 
            // OutputTextbox
            // 
            this.OutputTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputTextbox.Location = new System.Drawing.Point(506, 561);
            this.OutputTextbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.OutputTextbox.Name = "OutputTextbox";
            this.OutputTextbox.ReadOnly = true;
            this.OutputTextbox.Size = new System.Drawing.Size(900, 325);
            this.OutputTextbox.TabIndex = 1;
            this.OutputTextbox.Text = "";
            // 
            // OutputProgressBar
            // 
            this.OutputProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputProgressBar.Location = new System.Drawing.Point(14, 55);
            this.OutputProgressBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.OutputProgressBar.Name = "OutputProgressBar";
            this.OutputProgressBar.Size = new System.Drawing.Size(1393, 31);
            this.OutputProgressBar.TabIndex = 2;
            // 
            // OutputTextboxLabel
            // 
            this.OutputTextboxLabel.AutoSize = true;
            this.OutputTextboxLabel.Location = new System.Drawing.Point(506, 537);
            this.OutputTextboxLabel.Name = "OutputTextboxLabel";
            this.OutputTextboxLabel.Size = new System.Drawing.Size(58, 20);
            this.OutputTextboxLabel.TabIndex = 4;
            this.OutputTextboxLabel.Text = "Output:";
            this.toolTip1.SetToolTip(this.OutputTextboxLabel, "The text output from working on a file.");
            // 
            // ConfigurationGroupBox
            // 
            this.ConfigurationGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ConfigurationGroupBox.Controls.Add(this.label15);
            this.ConfigurationGroupBox.Controls.Add(this.ShowExcelWhenDoneCheckBox);
            this.ConfigurationGroupBox.Controls.Add(this.GenerateEventsSheetCheckBox);
            this.ConfigurationGroupBox.Controls.Add(this.label14);
            this.ConfigurationGroupBox.Controls.Add(this.EventsSheetNameTextBox);
            this.ConfigurationGroupBox.Controls.Add(this.label13);
            this.ConfigurationGroupBox.Controls.Add(this.InterRaterReliabilityComboBox);
            this.ConfigurationGroupBox.Controls.Add(this.label12);
            this.ConfigurationGroupBox.Controls.Add(this.label11);
            this.ConfigurationGroupBox.Controls.Add(this.SearchForPatternsCheckBox);
            this.ConfigurationGroupBox.Controls.Add(this.GenerateExcelSheetCheckBox);
            this.ConfigurationGroupBox.Controls.Add(this.label10);
            this.ConfigurationGroupBox.Controls.Add(this.EditSuperGroupsButton);
            this.ConfigurationGroupBox.Controls.Add(this.label9);
            this.ConfigurationGroupBox.Controls.Add(this.label8);
            this.ConfigurationGroupBox.Controls.Add(this.GlobalThresholdUpDown);
            this.ConfigurationGroupBox.Controls.Add(this.label5);
            this.ConfigurationGroupBox.Controls.Add(this.label4);
            this.ConfigurationGroupBox.Controls.Add(this.CategoriesRichTextBox);
            this.ConfigurationGroupBox.Controls.Add(this.EditCategoriesButton);
            this.ConfigurationGroupBox.Controls.Add(this.label7);
            this.ConfigurationGroupBox.Controls.Add(this.CategoryColumnTextBox);
            this.ConfigurationGroupBox.Controls.Add(this.label6);
            this.ConfigurationGroupBox.Controls.Add(this.TimeColumnsRichTextBox);
            this.ConfigurationGroupBox.Controls.Add(this.label3);
            this.ConfigurationGroupBox.Controls.Add(this.GroupColumnTextBox);
            this.ConfigurationGroupBox.Controls.Add(this.IDColumnTextBox);
            this.ConfigurationGroupBox.Controls.Add(this.label2);
            this.ConfigurationGroupBox.Controls.Add(this.label1);
            this.ConfigurationGroupBox.Location = new System.Drawing.Point(14, 133);
            this.ConfigurationGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ConfigurationGroupBox.Name = "ConfigurationGroupBox";
            this.ConfigurationGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ConfigurationGroupBox.Size = new System.Drawing.Size(486, 755);
            this.ConfigurationGroupBox.TabIndex = 5;
            this.ConfigurationGroupBox.TabStop = false;
            this.ConfigurationGroupBox.Text = "Configuration";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(7, 615);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(168, 20);
            this.label15.TabIndex = 34;
            this.label15.Text = "Show Excel When Done:";
            this.toolTip1.SetToolTip(this.label15, "A list of the names of columns that have relevant time information, such as Start" +
        " and Stop. These values will be averaged.");
            // 
            // ShowExcelWhenDoneCheckBox
            // 
            this.ShowExcelWhenDoneCheckBox.AutoSize = true;
            this.ShowExcelWhenDoneCheckBox.Location = new System.Drawing.Point(190, 617);
            this.ShowExcelWhenDoneCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ShowExcelWhenDoneCheckBox.Name = "ShowExcelWhenDoneCheckBox";
            this.ShowExcelWhenDoneCheckBox.Size = new System.Drawing.Size(18, 17);
            this.ShowExcelWhenDoneCheckBox.TabIndex = 33;
            this.ShowExcelWhenDoneCheckBox.UseVisualStyleBackColor = true;
            this.ShowExcelWhenDoneCheckBox.CheckedChanged += new System.EventHandler(this.ShowExcelWhenDoneCheckBox_CheckedChanged);
            // 
            // GenerateEventsSheetCheckBox
            // 
            this.GenerateEventsSheetCheckBox.AutoSize = true;
            this.GenerateEventsSheetCheckBox.Location = new System.Drawing.Point(191, 532);
            this.GenerateEventsSheetCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GenerateEventsSheetCheckBox.Name = "GenerateEventsSheetCheckBox";
            this.GenerateEventsSheetCheckBox.Size = new System.Drawing.Size(18, 17);
            this.GenerateEventsSheetCheckBox.TabIndex = 32;
            this.GenerateEventsSheetCheckBox.UseVisualStyleBackColor = true;
            this.GenerateEventsSheetCheckBox.CheckedChanged += new System.EventHandler(this.GenerateEventsSheetCheckBox_CheckedChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 533);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(159, 20);
            this.label14.TabIndex = 31;
            this.label14.Text = "Generate Events Sheet:";
            this.toolTip1.SetToolTip(this.label14, "A list of the names of columns that have relevant time information, such as Start" +
        " and Stop. These values will be averaged.");
            // 
            // EventsSheetNameTextBox
            // 
            this.EventsSheetNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EventsSheetNameTextBox.Location = new System.Drawing.Point(191, 29);
            this.EventsSheetNameTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.EventsSheetNameTextBox.Name = "EventsSheetNameTextBox";
            this.EventsSheetNameTextBox.Size = new System.Drawing.Size(281, 27);
            this.EventsSheetNameTextBox.TabIndex = 30;
            this.EventsSheetNameTextBox.TextChanged += new System.EventHandler(this.EventsSheetNameTextBox_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 29);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(139, 20);
            this.label13.TabIndex = 29;
            this.label13.Text = "Events Sheet Name:";
            this.toolTip1.SetToolTip(this.label13, "The name of the column that has the ID of the rater.");
            // 
            // InterRaterReliabilityComboBox
            // 
            this.InterRaterReliabilityComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InterRaterReliabilityComboBox.FormattingEnabled = true;
            this.InterRaterReliabilityComboBox.Items.AddRange(new object[] {
            "None",
            "Krippendorf\'s Alpha",
            "Percentage"});
            this.InterRaterReliabilityComboBox.Location = new System.Drawing.Point(191, 580);
            this.InterRaterReliabilityComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.InterRaterReliabilityComboBox.Name = "InterRaterReliabilityComboBox";
            this.InterRaterReliabilityComboBox.Size = new System.Drawing.Size(281, 28);
            this.InterRaterReliabilityComboBox.TabIndex = 28;
            this.InterRaterReliabilityComboBox.SelectedIndexChanged += new System.EventHandler(this.InterRaterReliabilityComboBox_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 584);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(147, 20);
            this.label12.TabIndex = 27;
            this.label12.Text = "InterRater Reliability:";
            this.toolTip1.SetToolTip(this.label12, "A list of the names of columns that have relevant time information, such as Start" +
        " and Stop. These values will be averaged.");
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 553);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(135, 20);
            this.label11.TabIndex = 26;
            this.label11.Text = "Search for Patterns:";
            this.toolTip1.SetToolTip(this.label11, "A list of the names of columns that have relevant time information, such as Start" +
        " and Stop. These values will be averaged.");
            // 
            // SearchForPatternsCheckBox
            // 
            this.SearchForPatternsCheckBox.AutoSize = true;
            this.SearchForPatternsCheckBox.Location = new System.Drawing.Point(191, 555);
            this.SearchForPatternsCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SearchForPatternsCheckBox.Name = "SearchForPatternsCheckBox";
            this.SearchForPatternsCheckBox.Size = new System.Drawing.Size(18, 17);
            this.SearchForPatternsCheckBox.TabIndex = 25;
            this.SearchForPatternsCheckBox.UseVisualStyleBackColor = true;
            this.SearchForPatternsCheckBox.CheckedChanged += new System.EventHandler(this.SearchForPatternsCheckBox_CheckedChanged);
            // 
            // GenerateExcelSheetCheckBox
            // 
            this.GenerateExcelSheetCheckBox.AutoSize = true;
            this.GenerateExcelSheetCheckBox.Location = new System.Drawing.Point(191, 512);
            this.GenerateExcelSheetCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GenerateExcelSheetCheckBox.Name = "GenerateExcelSheetCheckBox";
            this.GenerateExcelSheetCheckBox.Size = new System.Drawing.Size(18, 17);
            this.GenerateExcelSheetCheckBox.TabIndex = 24;
            this.GenerateExcelSheetCheckBox.UseVisualStyleBackColor = true;
            this.GenerateExcelSheetCheckBox.CheckedChanged += new System.EventHandler(this.GenerateExcelSheetCheckBox_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 513);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(183, 20);
            this.label10.TabIndex = 23;
            this.label10.Text = "Generate Excel Document:";
            this.toolTip1.SetToolTip(this.label10, "A list of the names of columns that have relevant time information, such as Start" +
        " and Stop. These values will be averaged.");
            // 
            // EditSuperGroupsButton
            // 
            this.EditSuperGroupsButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EditSuperGroupsButton.Location = new System.Drawing.Point(191, 473);
            this.EditSuperGroupsButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.EditSuperGroupsButton.Name = "EditSuperGroupsButton";
            this.EditSuperGroupsButton.Size = new System.Drawing.Size(281, 31);
            this.EditSuperGroupsButton.TabIndex = 22;
            this.EditSuperGroupsButton.Text = "Edit Super Groups";
            this.toolTip1.SetToolTip(this.EditSuperGroupsButton, "Edit the above list of types.");
            this.EditSuperGroupsButton.UseVisualStyleBackColor = true;
            this.EditSuperGroupsButton.Click += new System.EventHandler(this.EditSuperGroupsButton_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 479);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 20);
            this.label9.TabIndex = 21;
            this.label9.Text = "Super Groups:";
            this.toolTip1.SetToolTip(this.label9, "A list of the names of columns that have relevant time information, such as Start" +
        " and Stop. These values will be averaged.");
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 700);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(118, 20);
            this.label8.TabIndex = 20;
            this.label8.Text = "*Required fields.";
            // 
            // GlobalThresholdUpDown
            // 
            this.GlobalThresholdUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GlobalThresholdUpDown.DecimalPlaces = 2;
            this.GlobalThresholdUpDown.Location = new System.Drawing.Point(191, 145);
            this.GlobalThresholdUpDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GlobalThresholdUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.GlobalThresholdUpDown.Name = "GlobalThresholdUpDown";
            this.GlobalThresholdUpDown.Size = new System.Drawing.Size(281, 27);
            this.GlobalThresholdUpDown.TabIndex = 19;
            this.GlobalThresholdUpDown.ValueChanged += new System.EventHandler(this.GlobalThresholdUpDown_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(196, 20);
            this.label5.TabIndex = 18;
            this.label5.Text = "Default Grouping Threshold:";
            this.toolTip1.SetToolTip(this.label5, "The amount of time in seconds each group should be grouped by.");
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 725);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(390, 20);
            this.label4.TabIndex = 17;
            this.label4.Text = "Hover over the labels to learn more about how they work.";
            // 
            // CategoriesRichTextBox
            // 
            this.CategoriesRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CategoriesRichTextBox.Location = new System.Drawing.Point(191, 228);
            this.CategoriesRichTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CategoriesRichTextBox.Name = "CategoriesRichTextBox";
            this.CategoriesRichTextBox.ReadOnly = true;
            this.CategoriesRichTextBox.Size = new System.Drawing.Size(281, 80);
            this.CategoriesRichTextBox.TabIndex = 16;
            this.CategoriesRichTextBox.Text = "";
            // 
            // EditCategoriesButton
            // 
            this.EditCategoriesButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EditCategoriesButton.Location = new System.Drawing.Point(191, 317);
            this.EditCategoriesButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.EditCategoriesButton.Name = "EditCategoriesButton";
            this.EditCategoriesButton.Size = new System.Drawing.Size(281, 31);
            this.EditCategoriesButton.TabIndex = 15;
            this.EditCategoriesButton.Text = "Edit Types";
            this.toolTip1.SetToolTip(this.EditCategoriesButton, "Edit the above list of types.");
            this.EditCategoriesButton.UseVisualStyleBackColor = true;
            this.EditCategoriesButton.Click += new System.EventHandler(this.EditCategoriesButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 228);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "Type:";
            this.toolTip1.SetToolTip(this.label7, "The possible types in the type column.");
            // 
            // CategoryColumnTextBox
            // 
            this.CategoryColumnTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CategoryColumnTextBox.Location = new System.Drawing.Point(191, 184);
            this.CategoryColumnTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CategoryColumnTextBox.Name = "CategoryColumnTextBox";
            this.CategoryColumnTextBox.Size = new System.Drawing.Size(281, 27);
            this.CategoryColumnTextBox.TabIndex = 12;
            this.CategoryColumnTextBox.TextChanged += new System.EventHandler(this.CategoryColumnTextBox_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 188);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "Type Column:";
            this.toolTip1.SetToolTip(this.label6, "The name of the column that has the type of code, such as POINT or STATE. \r\nCan b" +
        "e used to set custom thresholds per type.");
            // 
            // TimeColumnsRichTextBox
            // 
            this.TimeColumnsRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TimeColumnsRichTextBox.Location = new System.Drawing.Point(191, 356);
            this.TimeColumnsRichTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TimeColumnsRichTextBox.Name = "TimeColumnsRichTextBox";
            this.TimeColumnsRichTextBox.Size = new System.Drawing.Size(281, 108);
            this.TimeColumnsRichTextBox.TabIndex = 8;
            this.TimeColumnsRichTextBox.Text = "";
            this.TimeColumnsRichTextBox.TextChanged += new System.EventHandler(this.TimeColumnsRichTextBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 360);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Time Column(s):*";
            this.toolTip1.SetToolTip(this.label3, "A list of the names of columns that have relevant time information, such as Start" +
        " and Stop. These values will be averaged.");
            // 
            // GroupColumnTextBox
            // 
            this.GroupColumnTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupColumnTextBox.Location = new System.Drawing.Point(191, 107);
            this.GroupColumnTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GroupColumnTextBox.Name = "GroupColumnTextBox";
            this.GroupColumnTextBox.Size = new System.Drawing.Size(281, 27);
            this.GroupColumnTextBox.TabIndex = 3;
            this.GroupColumnTextBox.TextChanged += new System.EventHandler(this.GroupColumnTextBox_TextChanged);
            // 
            // IDColumnTextBox
            // 
            this.IDColumnTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IDColumnTextBox.Location = new System.Drawing.Point(191, 68);
            this.IDColumnTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.IDColumnTextBox.Name = "IDColumnTextBox";
            this.IDColumnTextBox.Size = new System.Drawing.Size(281, 27);
            this.IDColumnTextBox.TabIndex = 1;
            this.IDColumnTextBox.TextChanged += new System.EventHandler(this.IDColumnTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Behavior Column:*";
            this.toolTip1.SetToolTip(this.label2, "The name of the column you would like to group events by.");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID Column:";
            this.toolTip1.SetToolTip(this.label1, "The name of the column that has the ID of the rater.");
            // 
            // BpadPatternsTextBox
            // 
            this.BpadPatternsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BpadPatternsTextBox.Location = new System.Drawing.Point(7, 25);
            this.BpadPatternsTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BpadPatternsTextBox.Name = "BpadPatternsTextBox";
            this.BpadPatternsTextBox.Size = new System.Drawing.Size(886, 365);
            this.BpadPatternsTextBox.TabIndex = 21;
            this.BpadPatternsTextBox.Text = "";
            this.BpadPatternsTextBox.TextChanged += new System.EventHandler(this.BpadPatternsTextBox_TextChanged);
            // 
            // BpadGroupBox
            // 
            this.BpadGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BpadGroupBox.Controls.Add(this.BpadPatternsTextBox);
            this.BpadGroupBox.Location = new System.Drawing.Point(506, 133);
            this.BpadGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BpadGroupBox.Name = "BpadGroupBox";
            this.BpadGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BpadGroupBox.Size = new System.Drawing.Size(901, 400);
            this.BpadGroupBox.TabIndex = 23;
            this.BpadGroupBox.TabStop = false;
            this.BpadGroupBox.Text = "Behavior Pattern Authoring and Detection";
            // 
            // OpenSettingsFileLocationLinkLabel
            // 
            this.OpenSettingsFileLocationLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenSettingsFileLocationLinkLabel.AutoSize = true;
            this.OpenSettingsFileLocationLinkLabel.Location = new System.Drawing.Point(1219, 537);
            this.OpenSettingsFileLocationLinkLabel.Name = "OpenSettingsFileLocationLinkLabel";
            this.OpenSettingsFileLocationLinkLabel.Size = new System.Drawing.Size(190, 20);
            this.OpenSettingsFileLocationLinkLabel.TabIndex = 24;
            this.OpenSettingsFileLocationLinkLabel.TabStop = true;
            this.OpenSettingsFileLocationLinkLabel.Text = "Open Settings File Location";
            this.OpenSettingsFileLocationLinkLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.OpenSettingsFileLocationLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OpenSettingsFileLocationLinkLabel_LinkClicked);
            // 
            // OutputSubProgressBar
            // 
            this.OutputSubProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputSubProgressBar.Location = new System.Drawing.Point(12, 94);
            this.OutputSubProgressBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.OutputSubProgressBar.Name = "OutputSubProgressBar";
            this.OutputSubProgressBar.Size = new System.Drawing.Size(1393, 31);
            this.OutputSubProgressBar.TabIndex = 25;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1421, 904);
            this.Controls.Add(this.OutputSubProgressBar);
            this.Controls.Add(this.OpenSettingsFileLocationLinkLabel);
            this.Controls.Add(this.BpadGroupBox);
            this.Controls.Add(this.ConfigurationGroupBox);
            this.Controls.Add(this.OutputTextboxLabel);
            this.Controls.Add(this.OutputProgressBar);
            this.Controls.Add(this.OutputTextbox);
            this.Controls.Add(this.SelectFileButton);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(815, 771);
            this.Name = "MainForm";
            this.Text = "Coded Data Grouper";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ConfigurationGroupBox.ResumeLayout(false);
            this.ConfigurationGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GlobalThresholdUpDown)).EndInit();
            this.BpadGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button SelectFileButton;
        private RichTextBox OutputTextbox;
        private ProgressBar OutputProgressBar;
        private Label OutputTextboxLabel;
        private GroupBox ConfigurationGroupBox;
        private TextBox GroupColumnTextBox;
        private TextBox IDColumnTextBox;
        private Label label2;
        private Label label1;
        private Label label3;
        private RichTextBox TimeColumnsRichTextBox;
        private Label label7;
        private TextBox CategoryColumnTextBox;
        private Label label6;
        private Button EditCategoriesButton;
        private RichTextBox CategoriesRichTextBox;
        private ToolTip toolTip1;
        private Label label4;
        private NumericUpDown GlobalThresholdUpDown;
        private Label label5;
        private Label label8;
        private RichTextBox BpadPatternsTextBox;
        private GroupBox BpadGroupBox;
        private Button EditSuperGroupsButton;
        private Label label9;
        private CheckBox GenerateExcelSheetCheckBox;
        private Label label10;
        private ComboBox InterRaterReliabilityComboBox;
        private Label label12;
        private Label label11;
        private CheckBox SearchForPatternsCheckBox;
        private LinkLabel OpenSettingsFileLocationLinkLabel;
        private TextBox EventsSheetNameTextBox;
        private Label label13;
        private CheckBox GenerateEventsSheetCheckBox;
        private Label label14;
        private Label label15;
        private CheckBox ShowExcelWhenDoneCheckBox;
        private ProgressBar OutputSubProgressBar;
    }
}