using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.Structure;

namespace EMGU_Cropping_Objects_Test
{
    public partial class Form1 : Form
    {
        Image<Bgr, byte> imageInput;
        Image<Gray, byte> connectedComponents;

        public Form1()
        {
            InitializeComponent();
        }

        private void fileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            if(dialog.ShowDialog() == DialogResult.OK)
            {
                imageInput = new Image<Bgr, byte>(dialog.FileName);
                leftPictureBox.Image = imageInput.Bitmap;
            }
        }

        private void processToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(imageInput == null)
            {
                return;
            }

            try
            {
                var temp = imageInput.Convert<Gray, byte>().ThresholdBinary(new Gray(100), new Gray(255))
                            .Dilate(1).Erode(1);
                

                    
            } catch(Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
