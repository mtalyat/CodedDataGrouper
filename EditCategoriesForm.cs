using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodedDataGrouper
{
    public partial class EditCategoriesForm : Form
    {
        private readonly MainForm _mainForm;

        private bool isCategorySelected => CategoriesListBox.SelectedIndex >= 0;
        private int categoryIndex => CategoriesListBox.SelectedIndex;
        private Category selectedCategory => _mainForm.Data.Categories[categoryIndex];

        public EditCategoriesForm(MainForm mainForm)
        {
            _mainForm = mainForm;

            InitializeComponent();
        }

        private void RefreshList()
        {
            //save the index from before, if able
            int index = categoryIndex;

            CategoriesListBox.DataSource = null;
            CategoriesListBox.DataSource = _mainForm.Data.Categories;

            CategoriesListBox.SelectedIndex = Math.Min(Math.Max(index, 0), _mainForm.Data.Categories.Count - 1);
        }

        private void OnChange()
        {
            if (isCategorySelected)
            {
                NameTextBox.Text = selectedCategory.Name;
                NameTextBox.Enabled = true;

                TimeThresholdUpDown.Value = (decimal)selectedCategory.Threshold;
                TimeThresholdUpDown.Enabled = true;
            }
            else
            {
                NameTextBox.Text = "";
                NameTextBox.Enabled = false;

                TimeThresholdUpDown.Value = 1;
                TimeThresholdUpDown.Enabled = false;
            }
        }

        private void EditCategoriesForm_Load(object sender, EventArgs e)
        {
            RefreshList();
            OnChange();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            _mainForm.UpdateConfigurationData();
            Close();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            //add a new category
            _mainForm.Data.Categories.Add(new Category("New Category", 1.0f));

            RefreshList();

            //set index to the end of the list
            CategoriesListBox.SelectedIndex = _mainForm.Data.Categories.Count - 1;

            OnChange();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            //delete selected category
            if (isCategorySelected)
            {
                _mainForm.Data.Categories.RemoveAt(categoryIndex);

                RefreshList();
                OnChange();
            }
        }

        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (isCategorySelected)
            {
                selectedCategory.Name = NameTextBox.Text;

                RefreshList();
            }
        }

        private void TimeThresholdUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (isCategorySelected)
            {
                selectedCategory.Threshold = (float)TimeThresholdUpDown.Value;

                RefreshList();
            }
        }

        private void CategoriesListBox_Click(object sender, EventArgs e)
        {
            OnChange();
        }
    }
}
