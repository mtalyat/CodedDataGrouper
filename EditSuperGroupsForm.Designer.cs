namespace CodedDataGrouper
{
    partial class EditSuperGroupsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuperGroupListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuperGroupNameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BehaviorsTextBox = new System.Windows.Forms.RichTextBox();
            this.AddButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SuperGroupListBox
            // 
            this.SuperGroupListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.SuperGroupListBox.FormattingEnabled = true;
            this.SuperGroupListBox.ItemHeight = 15;
            this.SuperGroupListBox.Location = new System.Drawing.Point(12, 15);
            this.SuperGroupListBox.Name = "SuperGroupListBox";
            this.SuperGroupListBox.Size = new System.Drawing.Size(215, 259);
            this.SuperGroupListBox.TabIndex = 0;
            this.SuperGroupListBox.Click += new System.EventHandler(this.SuperGroupListBox_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(454, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 15);
            this.label1.TabIndex = 2;
            // 
            // SuperGroupNameTextBox
            // 
            this.SuperGroupNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SuperGroupNameTextBox.Location = new System.Drawing.Point(300, 15);
            this.SuperGroupNameTextBox.Name = "SuperGroupNameTextBox";
            this.SuperGroupNameTextBox.Size = new System.Drawing.Size(256, 23);
            this.SuperGroupNameTextBox.TabIndex = 3;
            this.SuperGroupNameTextBox.TextChanged += new System.EventHandler(this.SuperGroupNameTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(233, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(233, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Behaviors:";
            // 
            // BehaviorsTextBox
            // 
            this.BehaviorsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BehaviorsTextBox.Location = new System.Drawing.Point(300, 52);
            this.BehaviorsTextBox.Name = "BehaviorsTextBox";
            this.BehaviorsTextBox.Size = new System.Drawing.Size(256, 252);
            this.BehaviorsTextBox.TabIndex = 6;
            this.BehaviorsTextBox.Text = "";
            this.BehaviorsTextBox.TextChanged += new System.EventHandler(this.BehaviorsTextBox_TextChanged);
            // 
            // AddButton
            // 
            this.AddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AddButton.Location = new System.Drawing.Point(12, 281);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(75, 23);
            this.AddButton.TabIndex = 7;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeleteButton.Location = new System.Drawing.Point(152, 281);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteButton.TabIndex = 8;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // EditSuperGroupsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 315);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.BehaviorsTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SuperGroupNameTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SuperGroupListBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditSuperGroupsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Super Groups";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.EditSuperGroupsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListBox SuperGroupListBox;
        private Label label1;
        private TextBox SuperGroupNameTextBox;
        private Label label2;
        private Label label3;
        private RichTextBox BehaviorsTextBox;
        private Button AddButton;
        private Button DeleteButton;
    }
}