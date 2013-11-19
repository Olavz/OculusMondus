using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OculusMundus
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser.AllowWebBrowserDrop = false;
            webBrowser.IsWebBrowserContextMenuEnabled = false;
            webBrowser.WebBrowserShortcutsEnabled = false;
            webBrowser.ObjectForScripting = this;
            webBrowser.ScrollBarsEnabled = false;
            // Uncomment the following line when you are finished debugging.
            //webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser.DocumentText = Properties.Resources.GoogleEarth;
        }


        /* Called from JavaScript */
        public void Test(String message)
        {
            MessageBox.Show(message, "AWESOME");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            object[] name = { "Hi to you to!" };
            webBrowser.Document.InvokeScript("helloWorld", name);
        }
    }
}
