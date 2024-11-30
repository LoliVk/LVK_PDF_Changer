using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;

namespace LVK_PDF_Changer_forWindows
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // 設定拖放事件
            pictureBox1.AllowDrop = true;
            pictureBox1.DragEnter += PictureBox1_DragEnter;
            pictureBox1.DragDrop += PictureBox1_DragDrop;
        }

        private string pdfPath = "example.pdf";  // PDF 檔案路徑
        private string txtPath = "output.txt";  // 輸出的 TXT 檔案路徑

        private void PictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            // 檢查拖入的資料是否為檔案
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy; // 顯示拖放效果
            }
            else
            {
                e.Effect = DragDropEffects.None; // 不允許拖放
            }
        }

        private void PictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            // 取得拖入的檔案路徑
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length > 0)
            {
                pdfPath = files[0]; // 取得第一個檔案的路徑
                MessageBox.Show($"檔案路徑: {pdfPath}", "檔案資訊");

                OutputTXT();

                // 如果需要顯示圖片，可將圖片加載到 PictureBox
                //pictureBox1.ImageLocation = filePath;
            }
        }

        private void OutputTXT() {
            string pdfPath = this.pdfPath;  // PDF 檔案路徑
            string txtPath = Path.ChangeExtension(pdfPath, ".txt"); // 輸出的 TXT 檔案路徑

            try
            {
                // 讀取 PDF
                using (PdfReader pdfReader = new PdfReader(pdfPath))
                using (PdfDocument pdfDoc = new PdfDocument(pdfReader))
                {
                    using (StreamWriter writer = new StreamWriter(txtPath))
                    {
                        for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
                        {
                            // 提取每頁文字
                            string pageText = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(i));
                            writer.WriteLine(pageText);
                        }
                    }
                }

                Console.WriteLine("PDF 已成功轉換為 TXT！路徑：" + txtPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("發生錯誤：" + ex.Message);
            }
        }
    }
}