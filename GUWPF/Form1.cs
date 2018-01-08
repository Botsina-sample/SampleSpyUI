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
using System.Security.AccessControl;
using System.Security.Principal;

namespace GUWPF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            
            if(!Directory.Exists(@"C:\Users\EDI\Documents\SpyIMG"))
            {
                Directory.CreateDirectory(@"C:\Users\EDI\Documents\SpyIMG");
            }
        
        }

        class SpyResult
        {
            private int _index;
            private string _automantionID;
            private string _classname;
            private string _name;
            private string _framework;
            private string _imagelocation;
            private string _username;
            private string _resultlocation;

            public int index { get { return _index; } set { _index = value; } }
            public string automationID { get { return _automantionID; } set { _automantionID = value; } }
            public string classname { get { return _classname; } set { _classname = value; } }
            public string name { get { return _name; } set { _name = value; } }
            public string framework { get { return _framework; } set { _framework = value; } }
            public string imagelocation { get { return _imagelocation; } set { _imagelocation = value; } }
            public string username { get { return _username; } set { _username = value; } }
            public string resultlocation { get { return _resultlocation; } set { _resultlocation = value; } }

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

        


        SpyOption SO = new SpyOption();
        Process[] flexProc = Process.GetProcessesByName("AUT_SampleUI");
        Gu.Wpf.UiAutomation.Application App;
        IReadOnlyList<Gu.Wpf.UiAutomation.UiElement> Element;
        Gu.Wpf.UiAutomation.Window MainWindow;
        public string localfolder;
        SpyResult sr = new SpyResult();






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
<<<<<<< HEAD
   
            //Task.Factory.StartNew(() =>
            //{
                
            //});

            App = Gu.Wpf.UiAutomation.Application.Attach(flexProc[0].Id);
            MainWindow = App.MainWindow;
            listBox1.Items.Add("FlexBARMS's id: " + MainWindow.ProcessId);
            sr.username = Environment.UserName;
            sr.resultlocation = @"C:\Users\" + sr.username + @"\Documents\GuWPF";
            Directory.CreateDirectory(sr.resultlocation);
            GrantAccess(sr.resultlocation);
        }

        private void GrantAccess(string fullPath)
        {
            DirectoryInfo dInfo = new DirectoryInfo(fullPath);
            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            dSecurity.AddAccessRule(new FileSystemAccessRule(sr.username, FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.InheritOnly, AccessControlType.Allow));
            dInfo.SetAccessControl(dSecurity);
=======
            Task.Factory.StartNew(() =>
            {
                App = Gu.Wpf.UiAutomation.Application.Attach(flexProc[0].Id);
                MainWindow = App.MainWindow;
                listBox1.Items.Add("AUT's id: " + MainWindow.ProcessId);
            });
>>>>>>> df2c4af12d5d693c90c50208fab4f846e9745743
        }


        // SPY BUTTON
        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            int getselectitem = comboBox1.SelectedIndex;

            if(getselectitem == 0)
            {
                SO.option = SpyOption._selection.WPF.ToString();
                Spy(SO.option);
            } 
            else if(getselectitem == 1)
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


        public void mbox(string mess)
        {
            System.Windows.Forms.MessageBox.Show(mess);
        }

        // SPY FUNCTION
        public void Spy(string type)
        {

            

            if (type == "WPF" || type == "Win32")
            {
                Element = SearchbyFramework(type);
<<<<<<< HEAD
            }
            else if (type == "Button" || type == "TextBlock")
            {
                Element = ElementClass(type);
=======
                //Element = MainWindow.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.FrameworkIdProperty, type));
            }
            else if (type == "Button" || type == "TextBlock")
            {

                //ElementClass(type);
>>>>>>> df2c4af12d5d693c90c50208fab4f846e9745743
            }

           

            try
            {
<<<<<<< HEAD
                #region writetofile
                //using (StreamWriter writeLog = new StreamWriter(@"C:\SpyResult.txt", true))
                //{
                //    var current_time = DateTime.UtcNow;

                //    writeLog.WriteLine("\n");
                //    writeLog.WriteLine("----------------------------------" + current_time + "----------------------------------");
                //    writeLog.WriteLine("\n");
                //    id = -1;
                //    foreach (UiElement UIE in Element)
                //    {
                //        //System.Windows.Forms.MessageBox.Show(id.ToString());
                //        if (!Double.IsInfinity(UIE.Bounds.Top))
                //        {
                //            var el_name = UIE.Name;
                //            var el_auid = UIE.AutomationId;

                //            if (UIE.Name == "")
                //            {
                //                el_name = "No Name";
                //            }
                //            if (UIE.AutomationId == "")
                //            {
                //                el_auid = "No AutomationID";
                //            }

                //            id++;
                //            listBox1.Items.Add("ID: " + id + " - " + el_auid + " - " + UIE.ClassName + " - " + el_name + " - " + UIE.Bounds.Top);
                //            writeLog.WriteLine("ID: " + id + " - " + el_auid + " - " + UIE.ClassName + " - " + el_name + " - " + UIE.Bounds.Top);
                //            writeLog.Flush();

                //        }
                //        //id++;

                //    }
                #endregion
                using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(sr.resultlocation, true))
                {
                    id = 0;
             
=======
                using (StreamWriter writeLog = new StreamWriter(@"C:\Users\EDI\Documents\SpyResult.txt", true))
                {
                    var current_time = DateTime.UtcNow;
                    writeLog.WriteLine("[");
                    writeLog.WriteLine("\n");
                    writeLog.WriteLine("----------------------------------" + current_time + "----------------------------------");
                    writeLog.WriteLine("\n");
                    id = -1;

>>>>>>> df2c4af12d5d693c90c50208fab4f846e9745743
                    foreach (UiElement UIE in Element)
                    {
                        if (!Double.IsInfinity(UIE.Bounds.Top))
                        {

                            sr = new SpyResult
                            {
                                index = id,
                                automationID = UIE.AutomationId,
                                classname = UIE.ClassName,
                                name = UIE.Name
                            };
                            if (UIE.Name == "")
                                sr.name = "No Name";
                            if (UIE.AutomationId == "")
                                sr.automationID = "No AutomationID";
                            id++;

                            string ObjectUI = JsonConvert.SerializeObject(sr, Formatting.Indented);
                            if (sr.index == 0)
                            {
                                file.WriteLine("[");
                                file.WriteLine(ObjectUI + ",");
                            }
                            else if (sr.index == Element.Count - 1)
                            {
                                file.WriteLine(ObjectUI);
                                file.WriteLine("]");
                            }

<<<<<<< HEAD
=======
                            id++;
                            listBox1.Items.Add("ID: " + id + " - " + el_auid + " - " + UIE.ClassName + " - " + el_name);


                            // BEGIN WRITE RESULT TO FILE
                            
                            writeLog.WriteLine("ID: " + id + " - " + el_auid + " - " + UIE.ClassName + " - " + el_name);
                            writeLog.Flush();
>>>>>>> df2c4af12d5d693c90c50208fab4f846e9745743

                            listBox1.Items.Add("ID: " + sr.index + " - " + sr.automationID + " - " + sr.classname + " - " + sr.name + " - " + UIE.Bounds.Top);
                        }
<<<<<<< HEAD
=======
                        //id++;
                     
>>>>>>> df2c4af12d5d693c90c50208fab4f846e9745743
                    }
                    writeLog.WriteLine("]");
                }
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message);
            }

        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }



        public void checkbox()
        {
            Element = MainWindow.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.FrameworkIdProperty, "WPF"));
            var selectID = Convert.ToInt16(textBox1.Text);
            try
            {
                Element[selectID].AsButton().Click();
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
                if(!Double.IsInfinity(hlitem.Bounds.Top))
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
            SpyResult sr = new SpyResult();

            var current_time = DateTime.UtcNow;

            this.WindowState = FormWindowState.Minimized;

            Thread.Sleep(500);

            Element = MainWindow.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.FrameworkIdProperty, "WPF"));
            id = 0;

<<<<<<< HEAD
            using (StreamWriter writeLog = new StreamWriter(sr.resultlocation + ".txt", true))
=======
            using (StreamWriter writeLog = new StreamWriter(@"C:\Users\EDI\Documents\SpyResult_Capture.txt", true))
>>>>>>> df2c4af12d5d693c90c50208fab4f846e9745743
            {
                writeLog.WriteLine("\n");
                writeLog.WriteLine("----------------------------------" + current_time + "----------------------------------");
                writeLog.WriteLine("\n");

                foreach (UiElement UIE in Element)
                {

                    try
                    {
                        //listBox1.Items.Add(id + " - " + UIE);
                        UIE.CaptureToFile(@"C:\Users\EDI\Documents\SpyIMG\" + id + " - " + UIE.ClassName + ".png");

                    }
                    catch (Exception ex)
                    {
                       
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
