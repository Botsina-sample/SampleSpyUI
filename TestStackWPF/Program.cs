using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;
using System.Windows.Automation;
using System.Windows.Forms;
using System.Drawing;

namespace TestStackWPF
{
    class Program
    {
        static void Main(string[] args)
        {
            Process[] proc = Process.GetProcessesByName("DevEX_SampleUI");


            int x, y;


            foreach (Process dx in proc)
            {
                Console.WriteLine(dx.Id);

                try
                {
                    TestStack.White.Application application = TestStack.White.Application.Attach(dx.Id);
                    Window MainWindow = application.GetWindow("MainWindow");

                    Thread.Sleep(1000);
                    MainWindow.Focus();

                    Console.WriteLine("\n\n\n");
                    TestStack.White.UIItems.IUIItem[] listitem = MainWindow.GetMultiple(SearchCriteria.ByControlType(ControlType.Edit));

                    int i = 0;
                    foreach(TestStack.White.UIItems.UIItem item in listitem)
                    {

                        Console.WriteLine(i + " - " + item);

                        if(i == 6)
                        {
                            item.Click();

                            x = Convert.ToInt16(item.Bounds.X);
                            y = Convert.ToInt16(item.Bounds.Y);

                            var h = Convert.ToInt16(item.Bounds.Height);
                            var w = Convert.ToInt16(item.Bounds.Width);

        

                            Console.WriteLine(x + "-" + y);
                            IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;
                            Graphics g = Graphics.FromHwnd(handle);


                            
                            g.DrawRectangle(new Pen(Color.Red, 5), x,y ,w,h);
                            g.ScaleTransform(24F, 3F);
                            
                        }

                        i++;
                    }

                } catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            //Thread.Sleep(1000);
            //SearchCriteria searchCriteria = SearchCriteria.ByAutomationId("textBoxUI");
            //TextBox textBox = window.Get<TextBox>(searchCriteria);

            //Thread.Sleep(1000);
            //try
            //{
            //    textBox.BulkText = "Text added to textbox";
            //}
            //catch(Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
        }
    }
}
