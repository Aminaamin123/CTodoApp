using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TodoApp
{
    public partial class Form1 : Form
    {

        private TaskManager taskManager;

        private string fileName = Application.StartupPath + "\\Task.txt";
        public Form1()
        {
            InitializeComponent();
            InitializeGUI();
        }

        private void InitializeGUI()
        {

            // setting the start values
            this.Text = "ToDo Reminder by Amina Amin";

            taskManager = new TaskManager();

            cmbBoxPrio.Items.Clear();
            cmbBoxPrio.Items.AddRange(Enum.GetNames(typeof(PriorityType)));
            cmbBoxPrio.SelectedIndex = (int)PriorityType.Normal;

            listBox1.Items.Clear();

            txtboxToDo.Text = string.Empty;

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd  HH:mm";

            openDataFileToolStripMenuItem.Enabled = true;
            saveDataFileToolStripMenuItem.Enabled = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Task task = ReadInput();
            // if a task is fulfilled then add to items and update view
            if (taskManager.AddNewTask(task))
            {
                UpdateGUI();
            }

        }

        private Task ReadInput()
        {
            Task task = new Task();
            // controling that there is a description before createing an todo
            if (string.IsNullOrEmpty(txtboxToDo.Text))
            {
                MessageBox.Show("No subject? Write something..");
                return null;
            }

            task.Description = txtboxToDo.Text;
            task.TaskDate = dateTimePicker1.Value;
            task.Priority = (PriorityType)cmbBoxPrio.SelectedIndex;

            return task;

        }


        private void UpdateGUI()
        {
            listBox1.Items.Clear();
            string[] infoStrings = taskManager.GetInfoStringList();
            if(infoStrings != null)
            {
                listBox1.Items.AddRange(infoStrings);
            }
        }



        private void openDataFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try to open file
            bool ok = taskManager.ReadDataFromFile(fileName);
            if (!ok)
            {
                string errMessage = "Something went wrong when opeening the file";
                MessageBox.Show(errMessage);
            }
            else
            {
                UpdateGUI();
            }
        }

        private void saveDataFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try to save data
            string errMessage = "Something wnet wrong while saving data to file";
            bool ok = taskManager.WriteDataToFile(fileName);
            if (!ok)
                MessageBox.Show(errMessage);
            else
                MessageBox.Show("Data saved to file: " + Environment.NewLine + fileName);
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            if (index >= 0)
            {
                Task task = ReadInput();
                bool ok = taskManager.ChangeTaskAt(task, index);
                if (ok)
                    UpdateGUI();
            }
            else
                MessageBox.Show("Select an item to change");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            if (index >= 0)
            {
                Task task = ReadInput();
                bool ok = taskManager.DeleteTaskAt(index);
                if (ok)
                    UpdateGUI();
            }
            else
                MessageBox.Show("Select an item to delete");

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult dlgResult = MessageBox.Show("Sure to exit program?", "Exit?",
                MessageBoxButtons.OKCancel);
            if (dlgResult == DialogResult.OK)
                Application.Exit();
        }
    }
}
