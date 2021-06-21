using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Limilabs.Mail;
using Limilabs.Client.POP3;
using MailViewer.Model;


namespace MailViewer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            comboBoxServer.Items.Add("pop3.yandex.ua");
            comboBoxServer.Items.Add("imap.gmail.com");
            comboBoxServer.Items.Add("imap.ukr.net");
            comboBoxServer.Items.Add("pop.i.ua");

            comboBoxServer.SelectedIndex = 0;
        }

        User currentUser;
        List<Letter> letters;
        private void buttonGo_Click(object sender, EventArgs e)
        {
            currentUser = new Model.User((string)comboBoxServer.SelectedItem, textBoxLogin.Text, textBoxPassword.Text);
            letters = new List<Letter>();
            try
            {
                backgroundWorker.RunWorkerAsync();
                timer.Enabled = true;
            }
            catch (Exception exep)
            {
                MessageBox.Show("Error: " + exep.Message);
            }
        }

        bool firstLetter = true;

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Pop3 pop3 = new Pop3();
                pop3.ConnectSSL(currentUser.mailUrl);
                pop3.Login(currentUser.login, currentUser.password);

                MailBuilder builder = new MailBuilder();
                int i = pop3.GetAll().Count;
                foreach (string uid in pop3.GetAll())
                {
                    var email = builder.CreateFromEml(pop3.GetMessageByUID(uid));
                    letters.Add(new Letter(email.Sender.Name, email.Sender.Address, email.Text));
                    backgroundWorker.ReportProgress(i);
                    i++;
                }
                pop3.Close();
            }
            catch (Exception exep)
            {
                MessageBox.Show("Ошибка подключения: " + exep.Message);
            }
        }
        int currentLetter = 0;
        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (firstLetter)
            {
                DisplayLetter();
                firstLetter = false;
            }
        }

        private void DisplayLetter()
        {
            if (letters.Count > 0)
            {
                textBoxResult.Text = "Letter №" + Convert.ToString(currentLetter + 1) + Environment.NewLine;
                textBoxResult.Text += letters[currentLetter].ToString();
            }

        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {
            if (currentLetter < letters.Count & currentLetter > 0)
            {
                currentLetter--;
                DisplayLetter();
            }
        }

        private void buttonRight_Click(object sender, EventArgs e)
        {
            if (currentLetter < letters.Count-1 & currentLetter >= 0)
            {
                currentLetter++;
                DisplayLetter();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            label4.Text = Convert.ToString(currentLetter + 1) + "/" + Convert.ToString(letters.Count);
        }
    }
}
