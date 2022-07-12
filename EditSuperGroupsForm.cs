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
    public partial class EditSuperGroupsForm : Form
    {
        private readonly MainForm _mainForm;

        private int superGroupIndex => SuperGroupListBox.SelectedIndex;
        private bool isSuperGroupSelected => superGroupIndex >= 0;
        private SuperGroup? selectedSuperGroup => SuperGroupListBox.SelectedItem as SuperGroup;

        public EditSuperGroupsForm(MainForm mainForm)
        {
            _mainForm = mainForm;

            InitializeComponent();
        }

        private void RefreshList()
        {
            //save the index
            int index = superGroupIndex;

            SuperGroupListBox.DataSource = null;
            SuperGroupListBox.DataSource = _mainForm.Data.SuperGroups;

            SuperGroupListBox.SelectedIndex = Math.Min(Math.Max(0, index), _mainForm.Data.SuperGroups.Count - 1);
        }

        private void OnChange()
        {
            if(isSuperGroupSelected)
            {
                SuperGroupNameTextBox.Text = selectedSuperGroup?.Name;
                SuperGroupNameTextBox.Enabled = true;

                BehaviorsTextBox.Lines = selectedSuperGroup?.Groups.ToArray();
                BehaviorsTextBox.Enabled = true;
            } else
            {
                SuperGroupNameTextBox.Text = "";
                SuperGroupNameTextBox.Enabled = false;

                BehaviorsTextBox.Lines = new string[0];
                BehaviorsTextBox.Enabled = false;
            }
        }

        private void EditSuperGroupsForm_Load(object sender, EventArgs e)
        {
            RefreshList();

            OnChange();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            //add a new group
            _mainForm.Data.SuperGroups.Add(new SuperGroup());

            RefreshList();

            //set index to the end of the list
            SuperGroupListBox.SelectedIndex = _mainForm.Data.SuperGroups.Count - 1;

            OnChange();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            //delete selected category
            if (isSuperGroupSelected)
            {
                _mainForm.Data.SuperGroups.RemoveAt(superGroupIndex);

                RefreshList();
                OnChange();
            }
        }

        private void SuperGroupNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (isSuperGroupSelected)
            {
                selectedSuperGroup.Name = SuperGroupNameTextBox.Text;

                RefreshList();
            }
        }

        private void BehaviorsTextBox_TextChanged(object sender, EventArgs e)
        {
            if(isSuperGroupSelected)
            {
                selectedSuperGroup.Groups = BehaviorsTextBox.Lines.ToList();
            }
        }

        private void SuperGroupListBox_Click(object sender, EventArgs e)
        {
            OnChange();
        }
    }
}
