using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScheduleWizard
{
    public partial class Form1 : Form
    {
        public List<Label> ClassIDLabels = new List<Label>();
        public List<Label> ClassNameLabels = new List<Label>();
        public List<Label> ClassLocLabels = new List<Label>();
        ComboBox[] TimeSlotDateFields;
        ComboBox[] TimeSlotStartFields;
        ComboBox[] TimeSlotEndFields;

        public Form1()
        {

            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ClassIDLabels.Add(this.class1IDLabel);
            ClassIDLabels.Add(this.class2IDLabel);
            ClassIDLabels.Add(this.class3IDLabel);
            ClassIDLabels.Add(this.class4IDLabel);
            ClassIDLabels.Add(this.class5IDLabel);

            ClassNameLabels.Add(this.class1NameLabel);
            ClassNameLabels.Add(this.class2NameLabel);
            ClassNameLabels.Add(this.class3NameLabel);
            ClassNameLabels.Add(this.class4NameLabel);
            ClassNameLabels.Add(this.class5NameLabel);

            ClassLocLabels.Add(this.class1LocLabel);
            ClassLocLabels.Add(this.class2LocLabel);
            ClassLocLabels.Add(this.class3LocLabel);
            ClassLocLabels.Add(this.class4LocLabel);
            ClassLocLabels.Add(this.class5LocLabel);

            TimeSlotDateFields = new ComboBox[] { time1DayBox, time2DayBox, time3DayBox, time4DayBox, time5DayBox };
            TimeSlotStartFields = new ComboBox[] { time1StartBox, time2StartBox, time3StartBox, time4StartBox, time5StartBox };
            TimeSlotEndFields = new ComboBox[] { time1EndBox, time2EndBox, time3EndBox, time4EndBox, time5EndBox };

            labelName.Text = User.activeUser.Name;
            DisplayClassesToday();

            for (int j = 0; j < 5; j++)
            {
                AddDays(TimeSlotDateFields[j]);
                AddTimes(TimeSlotStartFields[j]);
                AddTimes(TimeSlotEndFields[j]);
            }
            classSchedPanel.BringToFront();
            classSchedPanel.Show();
        }

        private void btnSchedule_Click(object sender, EventArgs e)
        {
            btnSchedule.BackColor = Color.FromArgb(3, 33, 56);
        }

        private void btnSchedule_Leave(object sender, EventArgs e)
        {
            btnSchedule.BackColor = Color.FromArgb(38, 64, 84);
        }

        private void btnDeadlines_Click(object sender, EventArgs e)
        {
            btnDeadlines.BackColor = Color.FromArgb(3, 33, 56);
        }

        private void btnDeadlines_Leave(object sender, EventArgs e)
        {
            btnDeadlines.BackColor = Color.FromArgb(38, 64, 84);
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            btnProfile.BackColor = Color.FromArgb(3, 33, 56);
        }

        private void btnProfile_Leave(object sender, EventArgs e)
        {
            btnProfile.BackColor = Color.FromArgb(38, 64, 84);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            btnSettings.BackColor = Color.FromArgb(3, 33, 56);
        }

        private void btnSettings_Leave(object sender, EventArgs e)
        {
            btnSettings.BackColor = Color.FromArgb(38, 64, 84);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            User.activeUser.CreateXML($"activeuser.xml");
            User.activeUser.CreateXML($"{User.activeUser.FirstName}{User.activeUser.LastName}.xml");
        }

        private void btnAddClass_Click(object sender, EventArgs e)
        {
            modifyClassLabel.Text = "Add a class";
        }

        private void btnEditClass_Click(object sender, EventArgs e)
        {
            modifyClassLabel.Text = "Edit a class";
            editClassDropdown.Items.Clear();
            foreach (Class c in User.activeUser.ClassList)
            {
                editClassDropdown.Items.Add($"{c.Code} - {c.Name}");
            }
        }

        private void classApplyChangesButton_Click(object sender, EventArgs e)
        {
            if (classApplyChangesButton.Text.Equals("Add class"))
            {
                Class newClass = new Class(classCodeTextBox.Text, classNameTextBox.Text, classLocTextBox.Text);
                for (int i = 0; i < 5; i++)
                {
                    if (TimeSlotDateFields[i].SelectedIndex != -1 && TimeSlotStartFields[i].SelectedIndex != -1 && TimeSlotEndFields[i].SelectedIndex != -1)
                    {
                        newClass.AddTimeSlot((Day)(TimeSlotDateFields[i].SelectedIndex), TimeSlotStartFields[i].SelectedIndex * 30, TimeSlotEndFields[i].SelectedIndex * 30);
                    }
                }
                User.activeUser.addClass(newClass);
            }
            DisplayClassesToday();
        }

        private void AddDays(ComboBox box)
        {
            foreach (Day d in Enum.GetValues(typeof(Day)))
            {
                box.Items.Add(d.ToString());
            }
        }

        private void AddTimes(ComboBox box)
        {
            // My attempt to iterate through all times rather than adding them manually (30 min increments from 12:00 AM to 11:30 PM)

            for (int i = 0; i < 1440; i+= 30)
            {
                string timeType = i < 720 ? "AM" : "PM";
                int hours = (i % 720) / 60 == 0 ? 12 : (i % 720) / 60;
                int mins = i % 60;
                string minFormat = mins < 10 ? $"0{mins}" : mins.ToString();
                box.Items.Add($"{hours}:{minFormat} {timeType}");
            }
        }

        private void DisplayClassesToday()
        {
            var i = 0;
            //First, hide all empty labels
            for (int j = 0; j < 5; j++)
            {
                ClassIDLabels[j].Hide();
                ClassNameLabels[j].Hide();
                ClassLocLabels[j].Hide();
            }

            foreach (ClassTimeSlot slot in User.activeUser.ClassesToday())
            {
                Class slottedClass = slot.Parent;
                ClassIDLabels[i].Show();
                ClassNameLabels[i].Show();
                ClassLocLabels[i].Show();

                ClassIDLabels[i].Text = slottedClass.Code;
                ClassNameLabels[i].Text = slottedClass.Name;
                ClassLocLabels[i].Text = slottedClass.Location;
                i++;
            }
        }

        private void classRemoveButton_Click(object sender, EventArgs e)
        {
            if (editClassDropdown.SelectedIndex != -1)
            {
                User.activeUser.ClassList.RemoveAt(editClassDropdown.SelectedIndex);
            }
        }

        private void editClassDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            Class selectedClass = User.activeUser.ClassList[editClassDropdown.SelectedIndex];
            classCodeTextBox.Text = selectedClass.Code;
            classNameTextBox.Text = selectedClass.Name;
            classLocTextBox.Text = selectedClass.Location;
            int timeSlotLen = selectedClass.TimeSlots.Count();
            for (int i = 0; i < timeSlotLen; i++)
            {
                TimeSlotDateFields[i].SelectedIndex = (int)(selectedClass.TimeSlots[i].Day);
                TimeSlotStartFields[i].SelectedIndex = selectedClass.TimeSlots[i].StartTime / 30;
                TimeSlotEndFields[i].SelectedIndex = selectedClass.TimeSlots[i].EndTime / 30;
            }
        }
    }
}
