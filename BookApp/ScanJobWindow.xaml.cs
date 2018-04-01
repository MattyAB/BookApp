using BookLib;
using ModernChrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookApp
{
    /// <summary>
    /// Interaction logic for BookAddWindow.xaml
    /// </summary>
    public partial class ScanJobWindow : ModernWindow
    {
        BookLib.BookLib lib;

        List<ScanJob> jobs;
        ScanJob job;

        public ScanJobWindow(BookLib.BookLib lib, int depth)
        {
            this.lib = lib;
            InitializeComponent();
            jobs = lib.GetScanJob(depth);
            DisplayNextJob();
            BorderBrush = Application.Current.FindResource($"StatusBarPurpleBrushKey") as SolidColorBrush;
        }

        void DisplayNextJob()
        {
            job = jobs[0];
            jobs.RemoveAt(0);

            TitleBlock.Text = job.ISBN;
            // TODO: BARCODE AND OTHER DISPLAY
        }

        void SubmitJob()
        {
            // TODO: SUBMIT JOB
        }

        private void PriceBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                SubmitJob();
                DisplayNextJob();
            }
        }
    }
}
