using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gu.Wpf;
using System.Diagnostics;
using System.Windows.Automation;
using Gu.Wpf.UiAutomation;
using System.IO;
using System.Threading;
using Newtonsoft.Json;

namespace GUWPF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            SR.username = Environment.UserName;

            string GUWPF = @"C:\Users\" + SR.username + @"\Documents\GUWPF";
            string SpyIMG = @"C:\Users\" + SR.username + @"\Documents\GUWPF\SpyIMG";

            if (!Directory.Exists(GUWPF))
            {
                Directory.CreateDirectory(GUWPF);
                Directory.CreateDirectory(SpyIMG);

            }

    
        }
        
        class LogWriterInfo
        {
            private string _username;
            private string _direcory;
            private string _currenttime;

            public string username { get { return _username; } set { _username = value; } }
            public string directory { get { return _direcory; } set { _direcory = value; } }
            public string currenttime { get { return _currenttime; } set { _currenttime = value; } }
            

        }

        class SpyIMG
        {
            private int _imgID;
            private string _imgClassName;

            public int imgID { get { return _imgID; } set { _imgID = value; } }
            public string imgClassName { get { return _imgClassName; } set { _imgClassName = value; } }
        }

        class SpyResult : LogWriterInfo
        {
            private int _index;
            private string _autoID;
            private string _classname;
            private string _name;
           

            public int index { get { return _index; } set { _index = value; } }
            public string autoDI { get { return _autoID; } set { _autoID = value; } }
            public string classname { get { return _classname; } set { _classname = value; } }
            public string name { get { return _name; } set { _name = value; } }
        }



        class SpyOption
        {
            private string _option;

            public string option
            {
                get { return _option; }
                set { _option = value; }
            }

            public enum _selection
            {
                WPF,
                Win32,
                TextBlock,
                Button
            }
        }




        SpyResult SR = new SpyResult();
        SpyOption SO = new SpyOption();

        Process[] flexProc = Process.GetProcessesByName("AUT_SampleUI");
        Gu.Wpf.UiAutomation.Application App;
        IReadOnlyList<Gu.Wpf.UiAutomation.UiElement> Element;
        Gu.Wpf.UiAutomation.Window MainWindow;

        public IReadOnlyList<Gu.Wpf.UiAutomation.UiElement> ElementClass(string type)
        {
            return MainWindow.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.ClassNameProperty, type));
        }
        public IReadOnlyList<Gu.Wpf.UiAutomation.UiElement> SearchbyFramework(string type)
        {
            return MainWindow.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.FrameworkIdProperty, type));
        }

        int id;


        // ATTACH BUTTON
        private void button1_Click(object sender, EventArgs e)
        {

            Task.Factory.StartNew(() =>
            {
                try
                {
                    App = Gu.Wpf.UiAutomation.Application.Attach(flexProc[0].Id);
                    MainWindow = App.MainWindow;
                    listBox1.Items.Add("AUT's id: " + MainWindow.ProcessId);
                }
                catch (Exception ex)
                {
                    mebox(ex.Message);
                }
            });
        }

        private void deletefiles()
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(@"C:\Users\" + SR.username + @"\Documents\GUWPF\SpyIMG");
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }

        private void mebox(string mess)
        {
            System.Windows.Forms.MessageBox.Show(mess);
        }

        // SPY BUTTON
        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            int getselectitem = comboBox1.SelectedIndex;

            if (getselectitem == 0)
            {
                SO.option = SpyOption._selection.WPF.ToString();
                Spy(SO.option);
            }
            else if (getselectitem == 1)
            {
                SO.option = SpyOption._selection.Win32.ToString();
                Spy(SO.option);
            }
            else if (getselectitem == 2)
            {
                SO.option = SpyOption._selection.TextBlock.ToString();
                Spy(SO.option);
            }
            else if (getselectitem == 3)
            {
                SO.option = SpyOption._selection.Button.ToString();
                Spy(SO.option);
            }
        }


        // SPY FUNCTION
        public void Spy(string type)
        {
            if (type == "WPF" || type == "Win32")
            {
                Element = SearchbyFramework(type);
                //Element = MainWindow.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.FrameworkIdProperty, type));
            }
            else if (type == "Button" || type == "TextBlock")
            {
                Element = ElementClass(type);
            }



            try
            {
                //Clear SpyIMG files
                deletefiles();

                var curtime = DateTime.Now;
                var day = curtime.Day;
                var month = curtime.Month;
                var year = curtime.Year;

                var sec = curtime.Second;
                var hour = curtime.Hour;
                var minute = curtime.Minute;
                var longTimeString = DateTime.Now.ToLongTimeString();
                

                var reformat = day + "-" + month + "-" + year + "__" + hour + "-" + minute + "-" + sec + "-";
                

                using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(@"C:\Users\" + SR.username + @"\Documents\GUWPF\"+reformat+"SpyResult.json"))
                {
                    this.WindowState = FormWindowState.Minimized;
                    Thread.Sleep(500);
                    SR.currenttime = longTimeString.ToString();

                    id = 0;
                    foreach (UiElement UIE in Element)
                    {
                        if(!UIE.Bounds.IsEmpty)
                        { 
                            SR.index = id;
                            SR.autoDI = UIE.AutomationId;
                            SR.classname = UIE.ClassName;
                            SR.name = UIE.Name;
                            //SR.currenttime = curtime.ToString();


                            if (UIE.AutomationId == "")
                                SR.autoDI = "No AutomationID";
                            if (UIE.Name == "")
                                SR.name = "No Name";

                            UIE.CaptureToFile(@"C:\Users\" + SR.username + @"\Documents\GUWPF\SpyIMG\" + SR.index + " - " + SR.classname + ".png");
                            id++;

                         

                            listBox1.Items.Add("ID: " + SR.index + " - " + SR.autoDI + " - " + SR.classname + " - " + SR.name);
                            string ObjectUI = JsonConvert.SerializeObject(SR, Formatting.Indented);

                            if (SR.index == 0)
                            {
                                file.WriteLine("[");
                                file.WriteLine(ObjectUI + ",");
                            }
                            else if (SR.index == Element.Count - 1)
                            {
                                file.WriteLine(ObjectUI);
                                file.WriteLine("]");
                            }

                            else file.WriteLine(ObjectUI + ",");
                           
                        }
            
                    }
                    file.WriteLine("]");
                    this.WindowState = FormWindowState.Normal;
                }
            }
            catch (Exception err)
            {
                listBox1.Items.Add(id + " - " + SR.classname + " - " + err.Message);
            }

        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }



        public void checkbox()
        {
            Element = SearchbyFramework("WPF");
            id = 0;
            var selectID = Convert.ToInt16(textBox1.Text);
            try
            {

                foreach (UiElement UIE in Element)
                {
                    if (!UIE.Bounds.IsEmpty)
                    {
                        if(id == selectID)
                        {
                            //listBox1.Items.Add("ID: " + id + " - " + UIE.AutomationId + " - " + UIE.ClassName + " - " + UIE.Name + " - " + UIE.Bounds.IsEmpty);
                            UIE.AsButton().DrawHighlight();
                        }
                        id++;
                    }

     
                }

         
              
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            checkbox();
        }


        // Highligh object when clicked
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int currentSelect = listBox1.SelectedIndex;

            if (SO.option == "WPF")
            {
                Element = MainWindow.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.FrameworkIdProperty, SO.option));
            }
            else if (SO.option == "Button")
            {
                Element = MainWindow.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.ClassNameProperty, SO.option));
            }
            else if (SO.option == "TextBlock")
            {
                Element = MainWindow.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.ClassNameProperty, SO.option));
            }


            foreach (UiElement hlitem in Element)
            {
                if (!Double.IsInfinity(hlitem.Bounds.Top))
                {

                    if (id == currentSelect)
                    {
                        try
                        {
                            hlitem.DrawHighlight();
                        }
                        catch (Exception err)
                        {
                            System.Windows.Forms.MessageBox.Show(err.Message);
                        }
                    }
                    id++;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        // CAPTURE BUTTON
        private void button5_Click(object sender, EventArgs e)
        {
            var current_time = DateTime.Now;

            this.WindowState = FormWindowState.Minimized;

            Thread.Sleep(500);

            Element = MainWindow.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.FrameworkIdProperty, "WPF"));
            id = 0;

            using (StreamWriter writeLog = new StreamWriter(@"C:\Users\"+SR.username+@"\Documents\SpyResult_Capture.txt", true))
            {
                writeLog.WriteLine("\n");
                writeLog.WriteLine("----------------------------------" + current_time + "----------------------------------");
                writeLog.WriteLine("\n");

                foreach (UiElement UIE in Element)
                {

                    try
                    {
                        //listBox1.Items.Add(id + " - " + UIE);
                        UIE.CaptureToFile(@"C:\Users\"+SR.username+@"\Documents\GUWPF\SpyIMG\" + id + " - " + UIE.ClassName + ".png");

                    }
                    catch (Exception ex)
                    {

                        listBox1.Items.Add(id + UIE.ClassName + " - " + UIE.IsOffscreen);
                        writeLog.WriteLine(id + " - " + UIE.ClassName + " - " + ex.Message);
                        writeLog.Flush();
                    }
                    id++;
                }
            }

            Thread.Sleep(500);

            this.WindowState = FormWindowState.Normal;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
