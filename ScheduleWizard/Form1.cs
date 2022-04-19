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

            ComboBox[] TimeSlotDateFields = { time1DayBox, time2DayBox, time3DayBox, time4DayBox, time5DayBox };
            ComboBox[] TimeSlotStartFields = { time1StartBox, time2StartBox, time3StartBox, time4StartBox, time5StartBox };
            ComboBox[] TimeSlotEndFields = { time1EndBox, time2EndBox, time3EndBox, time4EndBox, time5EndBox };

            labelName.Text = User.activeUser.Name;
            var i = 0;
            
            foreach (ClassTimeSlot slot in User.activeUser.ClassesToday())
            {
                Class slottedClass = slot.Parent;
                ClassIDLabels[i].Text = slottedClass.Code;
                ClassNameLabels[i].Text = slottedClass.Name;
                ClassLocLabels[i].Text = slottedClass.Location;
                i++;
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
                User.activeUser.ClassList.Add(new Class(classCodeTextBox.Text, classNameTextBox.Text, classLocTextBox.Text));
                
            }
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

        }
    }
}
