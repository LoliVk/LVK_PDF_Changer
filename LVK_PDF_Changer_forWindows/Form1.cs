using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;

namespace LVK_PDF_Changer_forWindows
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // �]�w���ƥ�
            pictureBox1.AllowDrop = true;
            pictureBox1.DragEnter += PictureBox1_DragEnter;
            pictureBox1.DragDrop += PictureBox1_DragDrop;
        }

        private string pdfPath = "example.pdf";  // PDF �ɮ׸��|
        private string txtPath = "output.txt";  // ��X�� TXT �ɮ׸��|

        private void PictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            // �ˬd��J����ƬO�_���ɮ�
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy; // ��ܩ��ĪG
            }
            else
            {
                e.Effect = DragDropEffects.None; // �����\���
            }
        }

        private void PictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            // ���o��J���ɮ׸��|
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length > 0)
            {
                pdfPath = files[0]; // ���o�Ĥ@���ɮת����|
                MessageBox.Show($"�ɮ׸��|: {pdfPath}", "�ɮ׸�T");

                OutputTXT();

                // �p�G�ݭn��ܹϤ��A�i�N�Ϥ��[���� PictureBox
                //pictureBox1.ImageLocation = filePath;
            }
        }

        private void OutputTXT() {
            string pdfPath = this.pdfPath;  // PDF �ɮ׸��|
            string txtPath = Path.ChangeExtension(pdfPath, ".txt"); // ��X�� TXT �ɮ׸��|

            try
            {
                // Ū�� PDF
                using (PdfReader pdfReader = new PdfReader(pdfPath))
                using (PdfDocument pdfDoc = new PdfDocument(pdfReader))
                {
                    using (StreamWriter writer = new StreamWriter(txtPath))
                    {
                        for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
                        {
                            // �����C����r
                            string pageText = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(i));
                            writer.WriteLine(pageText);
                        }
                    }
                }

                Console.WriteLine("PDF �w���\�ഫ�� TXT�I���|�G" + txtPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("�o�Ϳ��~�G" + ex.Message);
            }
        }
    }
}