using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OculusMundus
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class Form1 : Form
    {
        /* Speech Recognition */
        private SpeechSynthesizer sSynth;
        private PromptBuilder pBuilder;
        private SpeechRecognitionEngine sRecognize;
        private Boolean isSpeechRunning = false;

        public Form1()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB");

            sSynth = new SpeechSynthesizer();
            pBuilder = new PromptBuilder();
            sRecognize = new SpeechRecognitionEngine(new CultureInfo("en-GB"));

            button2.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /* Setup webbrowser */
            webBrowser.AllowWebBrowserDrop = false;
            webBrowser.IsWebBrowserContextMenuEnabled = false;
            webBrowser.WebBrowserShortcutsEnabled = false;
            webBrowser.ObjectForScripting = this;
            webBrowser.ScrollBarsEnabled = false;
            // Uncomment the following line when you are finished debugging.
            //webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser.DocumentText = Properties.Resources.GoogleEarth;
        }


        

        private void button1_Click(object sender, EventArgs e)
        {
            object[] name = { "Hi to you to!" };
            webBrowser.Document.InvokeScript("helloWorld", name);
        }

        /* Enable speech recognition */
        public void EnableSpeechRecognition()
        {
            Choices sList = new Choices();
            sList.Add(new string[] { "zoom in", "zoom out", "center"});
            Grammar gr = new Grammar(new GrammarBuilder(sList));
            try
            {
                sRecognize.RequestRecognizerUpdate();
                sRecognize.LoadGrammar(gr);
                sRecognize.SpeechRecognized += sRecognise_SpeechRecognized;
                sRecognize.SetInputToDefaultAudioDevice();
                sRecognize.RecognizeAsync(RecognizeMode.Multiple);
                sRecognize.Recognize();

            }

            catch (Exception ex)
            {
                return;
            }
        }

        private void sRecognise_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            txtSpeechOutput.AppendText(" " + e.Result.Text.ToString());
            object[] command = { e.Result.Text.ToString() };
            webBrowser.Document.InvokeScript("zoomHandler", command);
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            if (isSpeechRunning) {
                button2.Text = "Enable Speech";
                sRecognize.RecognizeAsyncCancel();
                isSpeechRunning = false;
            }
            else
            {
                button2.Text = "Disable Speech";
                EnableSpeechRecognition();
                isSpeechRunning = true;
            }
        
        }

        /* Called from JavaScript */
        public void LocationUpdate(double lat, double lon)
        {
            lblLocation.Text = "Lat: " + lat + Environment.NewLine + " Lon: " + lon;
        }

        public void EarthLoaded()
        {
            button2.Enabled = true;
        }

        public void SendVersion(String version)
        {
            Form1.ActiveForm.Text = "OculusMondus - Google Earth - " + version;
        }

    }
}
