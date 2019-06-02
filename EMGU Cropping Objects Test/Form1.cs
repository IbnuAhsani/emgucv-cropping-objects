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
            if (imageInput == null)
            {
                return;
            }

            try
            {
                //  Convert img to grayscale -> convert img to binary using max threshold of 100
                //  (if value is more than 100, set it to the max value [255]) -> close effects
                //  using dilation then erosion
                var temp = imageInput.Convert<Gray, byte>().ThresholdBinary(new Gray(100), new Gray(255))
                            .Dilate(1).Erode(1);

                // Matrix to store the labels from the image
                Mat labels = new Mat();

                // Find the number of connected components
                int nLabels = CvInvoke.ConnectedComponents(temp, labels);

                //  Konvesi labels menjadi sebuah image
                connectedComponents = labels.ToImage<Gray, byte>();

                // connectedComponents can not be shown because it is a value between 1 - 0
                rightPictureBox.Image = temp.Bitmap;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
