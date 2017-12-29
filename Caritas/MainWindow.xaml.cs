using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using TestStack.White.UIItems.Finders;


namespace Caritas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        Process[] proc;
        TestStack.White.UIItems.WindowItems.Window U6;
        string comboxSelect;
        string ProcessName = "AUT_SampleUI";
        string itemName, itemClass, itemATM_ID;
        

        public MainWindow()
        {
            InitializeComponent();
            
            
        }

        public int getProcessID(string procname)
        {
            proc = Process.GetProcessesByName(procname);
            var flexID = proc[0].Id;
            return flexID;

        }

        public string getWindowTitile(string procname)
        {
            proc = Process.GetProcessesByName(procname);
            var flexTitle = proc[0].MainWindowTitle;
            return flexTitle;
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {


            TestStack.White.UIItems.IUIItem[] listitem = U6.GetMultiple(SearchCriteria.ByFramework("WPF")); 

            int i = 0;

            foreach (TestStack.White.UIItems.UIItem item in listitem)
            {

                i++;
             
         
                
                itemName = item.AutomationElement.Current.Name;
                itemClass = item.AutomationElement.Current.ClassName;
                itemATM_ID = item.AutomationElement.Current.AutomationId;

                if(itemATM_ID == "")
                    itemATM_ID = "No AutomationID";
                if(itemName == "")
                    itemName = "No Name";


                listBox.Items.Add(i + ":  " + itemClass + " - " + itemATM_ID + " - " + itemName);




            }
        }

        private void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)sender;

            if(item.Items.Count != 1 || item.Items[0] != null)
            {
                return;
            }

            item.Items.Clear();

            var subitem_tag = (string)item.Tag;

            var subitem_list = new List<string>();


        }

        string curItem;

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the currently selected item in the ListBox.
            curItem = listBox.SelectedItem.ToString();

           
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
    
            
            try
            {
                listBox.Items.Add("FlexBAMRS's process id is: " + getProcessID(ProcessName));
                TestStack.White.Application app = TestStack.White.Application.Attach(getProcessID(ProcessName));

                U6 = app.GetWindow(getWindowTitile(ProcessName));

            } 
            catch(Exception ex)
            {
                listBox.Items.Add(ex.Message);
            }
      

            



        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Clear();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboxSelect = comboBox.SelectedIndex.ToString();
        }


        //playback
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            TestStack.White.UIItems.IUIItem[] listitem = U6.GetMultiple(SearchCriteria.ByFramework("WPF"));

            int itemID = 0;
            foreach(TestStack.White.UIItems.IUIItem item in listitem)
            {
                itemID++;
                if(itemID == 19)
                {
                    item.Enter("Eh");
                }


                if (itemID == 27)
                {
                    item.Enter("23/12/1994");
                }
            }

        }

        private void button3_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(curItem);
        }
    }
}
