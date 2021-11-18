using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;


namespace MyWebcamApp
{
    public partial class Form1 : Form
    {
        /* FIELDS */
        FilterInfoCollection filterInfoCollection; /* enumerates video devices */
        VideoCaptureDevice videoCaptureDevice; /* Class for local video capture devices like webcam */

        /* CONSTRUCTORS */
        public Form1()
        {
            InitializeComponent();
        }

        /* OTHER METHODS */
        private void Form1_Load(object sender, EventArgs e)
        {
            /* identify the camera at start */
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            
            /* update combobox */
            foreach (FilterInfo filterInfo in filterInfoCollection)
                cboCamera.Items.Add(filterInfo.Name);
            cboCamera.SelectedIndex = 0;

            /* In Microsoft COM, a moniker is an object that provides a pointer to the object it identifies.  */
            string deviceMoniker = filterInfoCollection[cboCamera.SelectedIndex].MonikerString; 
            /* select the camera */
            videoCaptureDevice = new VideoCaptureDevice(deviceMoniker);
            /* start aquisition */
            videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            videoCaptureDevice.Start();
        }

        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            picBox.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void cboCamera_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            videoCaptureDevice = new VideoCaptureDevice();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoCaptureDevice.IsRunning)
                videoCaptureDevice.Stop();
        }
    }
}
