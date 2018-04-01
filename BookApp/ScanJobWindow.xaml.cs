﻿using BarcodeLib;
using BookLib;
using ModernChrome;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

            Barcode barcode = new Barcode();
            Image img = barcode.Encode(TYPE.ISBN, job.ISBN);

            // Convert Image to BitmapImage
            var bi = new BitmapImage();
            using (var ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.Position = 0;

                bi.BeginInit();
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.StreamSource = ms;
                bi.EndInit();
            }

            BarcodeImage.Source = bi;

            switch(job.site)
            {
                case 0:
                    SiteBlock.Text = "WeBuyBooks";
                    break;
                case 1:
                    SiteBlock.Text = "Ziffit";
                    break;
                case 2:
                    SiteBlock.Text = "MusicMagpie";
                    break;
                case 3:
                    SiteBlock.Text = "Momox";
                    break;
                case 4:
                    SiteBlock.Text = "Zapper";
                    break;
                default:
                    throw new Exception("Site provided was unknown.");
            }
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
