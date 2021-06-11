using System;
using System.IO;
using System.Windows.Forms;
using WindowsFormsAppSummerPracticeMVC.Controllers;

namespace WindowsFormsAppSummerPracticeMVC
{
    public partial class FormMainView : Form
    {
        private readonly string pathForLogging = $"{Directory.GetCurrentDirectory()}\\log.log";
        private readonly MainController router = new MainController();
        public FormMainView()
        {
            InitializeComponent();
        }
        private async void ButtonGetStatistics_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                string requestUrl = textBoxURL.Text;
                await router.GetTextAsync(requestUrl);
                Logging(requestUrl, "Successfully");
                MessageBox.Show("Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Logging(textBoxURL.Text, ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Logging(string URL, string Message)
        {
            StreamWriter writer = new StreamWriter(pathForLogging, true);
            writer.WriteLine($"# URL: {URL}  <!>  RESPONSE: {Message}  <!>  DATE: {DateTime.Now}   <!>");
            writer.Close();
        }
    }
}