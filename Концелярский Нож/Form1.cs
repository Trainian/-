// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Form1.cs" company="Trainian">
//   Тестируем и создаём
// </copyright>
// <summary>
//   Defines the Form1 type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Концелярский_Нож
{
    using System;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml;

    /// <summary>
    /// Main Form.Главная форма.
    /// </summary>
    public partial class Form1 : Form
    {
        private string notepadFilename;

        private bool textChanged;

        public BindingSource bindSrc = new BindingSource();

        public Memo memo = new Memo();
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tsbCurrentTimeInsert.Text = @"Time";
            this.tsslCurrentTime.Text = String.Empty;
            this.textChanged = false;
            this.lblTime.DataBindings.Add("Text", this.tbTimer, "Value");
            this.notepadFilename = Properties.Settings.Default.notepadFilename;
            if (System.IO.File.Exists(this.notepadFilename))
                this.rtbNotepad.LoadFile(this.notepadFilename);
            this.dtp_EndTime.Value = DateTime.Now;
            this.lbl_LifeTime.Text = String.Empty;

            //Binding components RGB
            Rgb rgb = new Rgb();
            rgb.Color = this.pb_ColorCreater.BackColor;

            this.pb_ColorCreater.DataBindings.Add("BackColor", rgb, "Color");

            this.tb_Red.DataBindings.Add("Value", rgb, "Red");
            this.tb_Red.DataBindings[0].DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;

            this.tb_Green.DataBindings.Add("Value", rgb, "Green");
            this.tb_Green.DataBindings[0].DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;

            this.tb_Blue.DataBindings.Add("Value", rgb, "Blue");
            this.tb_Blue.DataBindings[0].DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;

            // Binding Labels Value
            this.redLabel.DataBindings.Add("Text", this.tb_Red, "Value");
            this.blueLabel.DataBindings.Add("Text", this.tb_Blue, "Value");
            this.greenLabel.DataBindings.Add("Text", this.tb_Green, "Value");


            // Calendar
            this.bindSrc.DataSource = typeof(Memo);
            this.bindSrc.AddNew();
            this.bindSrc[0] = this.memo;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.tsslCurrentTime.Text = DateTime.Now.ToString();
        }

        private void tsbCurrentTimeInsert_Click(object sender, EventArgs e)
        {
            string rtftext = @"{\rtf1\par\libe\b " + DateTime.Now.ToString() + @"\b0\par\libe}";
            this.rtbNotepad.Focus();
            this.rtbNotepad.Select(0, 0);
            Clipboard.SetData(DataFormats.Rtf, (object)rtftext);
            this.rtbNotepad.Paste();
            this.rtbNotepad.Select(0, 0);
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.notepadFilename != null)
            {
                Properties.Settings.Default.notepadFilename = this.notepadFilename;
                Properties.Settings.Default.Save();
            }
            if (this.textChanged)
            {
                this.tsmiSaveAs_Click(sender, e);
            }
        }

        private void tsmiSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = @"Файл rtf|*.rtf";
            dlg.InitialDirectory = Directory.GetCurrentDirectory();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.notepadFilename = dlg.FileName;
                this.rtbNotepad.SaveFile(this.notepadFilename);
            }
            this.textChanged = false;
        }

        private void tsmiSave_Click(object sender, EventArgs e)
        {
            if (this.notepadFilename == null)
            {
                this.tsmiSaveAs_Click(sender, e);
            }
            else
            {
                this.rtbNotepad.SaveFile(this.notepadFilename);
            }
            this.textChanged = false;
        }

        private void tsmiLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = @"Файл rtf|*.rtf";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.notepadFilename = dlg.FileName;
                this.rtbNotepad.LoadFile(this.notepadFilename);
            }
            this.textChanged = false;
        }

        private void rtbNotepad_TextChanged(object sender, EventArgs e)
        {
            this.textChanged = true;
        }

        private void btn_StartAlert_Click(object sender, EventArgs e)
        {
            this.timer_Remember.Enabled = !this.timer_Remember.Enabled;
        }

        private void timer_Remember_Tick(object sender, EventArgs e)
        {
            if (this.tbTimer.Value > 0)
            {
                this.tbTimer.Value--;
            }

            else
            {
                this.timer_Remember.Stop();
                System.Media.SystemSounds.Exclamation.Play();
                MessageBox.Show(this.tbMessageText.Text);
            }
        }

        private void tc_Menu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((TabControl)sender).SelectedIndex == 2)
            {
                SetLblLifeTime();
            }
            if (((TabControl)sender).SelectedIndex == 3)
            {
                GetXMLCourse();
            }
        }

        private void dtp_BindingContextChanged(object sender, EventArgs e)
        {
            SetLblLifeTime();
        }
        
        private void SetLblLifeTime()
        {
            int years, days;

            DateTime start = this.dtp_StartTime.Value;
            DateTime end = this.dtp_EndTime.Value;
            TimeSpan difference = end.Subtract(start);
            years = difference.Days / 365;
            days = difference.Days - 365 * years;
            this.lbl_LifeTime.Text = $"{years} лет и {days} дней.";
        }

        private void GetXMLCourse()
        {
            RBKServise.DailyInfo dayInfo = new RBKServise.DailyInfo();
            XmlNode XMLCourse = dayInfo.GetCursOnDateXML(DateTime.Today);
            this.rtb_CoursValue.Text = XMLCourse.InnerXml;

            //ValuteCursOnDate rs = new ValuteCursOnDate();
            //ValuteCursOnDate.LoadFromeXmlFormat(ref rs, XMLCourse.InnerXml);
            //MessageBox.Show(rs.VName);

            //public static void LoadFromeXmlFormat(ref ValuteCursOnDate obj, string stringXML)
            //{
            //    XmlSerializer xmlFormat = new XmlSerializer(typeof(ValuteCursOnDate));
            //    TextReader sr = new StringReader(stringXML);
            //    obj = (xmlFormat.Deserialize(sr) as ValuteCursOnDate);
            //    sr.Close();
            //}
        }
    }
}
