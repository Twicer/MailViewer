using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailViewer.Model
{
    public class Letter
    {
        public Letter() { }

        public Letter(string senderName, string senderAdress, string text)
        {
            this.senderName = senderName;
            this.text = text;
            this.senderAdress = senderAdress;
        }
        public string senderName { get; set; }
        public string senderAdress { get; set; }
        public string text { get; set; }
        public override string ToString()
        {
            string result = string.Empty;
            result += "Sender: " + this.senderName + Environment.NewLine;
            result += "Sender Adress: " + this.senderAdress + Environment.NewLine;
            result += "Letter Text: " + Environment.NewLine + this.text;
            result += Environment.NewLine + "===================" + Environment.NewLine;
            return result;
        }
    }
}
