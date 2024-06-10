using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Greeting_card
{
    /// <summary>
    /// Логика взаимодействия для SupportWindow.xaml
    /// </summary>
    public partial class SupportWindow : Window
    {
        public SupportWindow()
        {
            InitializeComponent();
            Loaded += Loaded_Txt;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Loaded_Txt(object sender, RoutedEventArgs e)
        {
            // Завантаження тексту з файлу
            string txtPath = "data/Info.txt";
            string text = File.ReadAllText(txtPath);
            textBlock.Text = text;
        }
    }
}
