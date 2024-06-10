using Microsoft.Win32;
using System.Globalization;
using System.IO;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Greeting_card
{
    public partial class MainWindow : Window
    {
        private Brush textColor = Brushes.Black; // Початковий колір тексту (чорний)
        private Typeface textFont = new Typeface("Arial Rounded MT Bold"); // Початковий шрифт тексту

        public MainWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
        }

        private void SelectImageButton_Click(object sender, RoutedEventArgs e)
        {
            // Відкрити діалогове вікно вибору зображення
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                // Відобразити вибране зображення на екрані
                SelectedImage.Source = new BitmapImage(new System.Uri(openFileDialog.FileName));
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Створити діалогове вікно для вибору місця збереження листівки
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JPEG Image|*.jpg|PNG Image|*.png";
            if (saveFileDialog.ShowDialog() == true)
            {
                // Створити зображення листівки з текстом
                DrawingVisual drawingVisual = new DrawingVisual();
                using (DrawingContext drawingContext = drawingVisual.RenderOpen())
                {
                    drawingContext.DrawImage(SelectedImage.Source, new Rect(0, 0, SelectedImage.ActualWidth, SelectedImage.ActualHeight));
                    FormattedText titleText = new FormattedText(TitleTextBox.Text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, textFont, 40, textColor);
                    drawingContext.DrawText(titleText, new Point(20, 20));
                    FormattedText messageText = new FormattedText(MessageTextBox.Text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, textFont, 24, textColor);
                    drawingContext.DrawText(messageText, new Point(20, 60));
                }
                RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)SelectedImage.ActualWidth, (int)SelectedImage.ActualHeight, 96, 96, System.Windows.Media.PixelFormats.Default);
                renderTargetBitmap.Render(drawingVisual);

                // Зберегти зображення листівки у вибраному форматі
                BitmapEncoder bitmapEncoder;
                switch (saveFileDialog.FilterIndex)
                {
                    case 1:
                        bitmapEncoder = new JpegBitmapEncoder();
                        break;
                    case 2:
                        bitmapEncoder = new PngBitmapEncoder();
                        break;
                    default:
                        bitmapEncoder = new JpegBitmapEncoder();
                        break;
                }

                bitmapEncoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
                using (Stream stream = File.Create(saveFileDialog.FileName))
                {
                    bitmapEncoder.Save(stream);
                }

                MessageBox.Show("Все збережено", "Успішне збереження ");
                ReloadWindow(); // Перезавантажити вікно
            }
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            // Створити діалогове вікно для вибору принтера
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                // Створити зображення листівки
                RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)SelectedImage.ActualWidth, (int)SelectedImage.ActualHeight, 96, 96, System.Windows.Media.PixelFormats.Default);
                renderTargetBitmap.Render(SelectedImage);

                // Надіслати зображення листівки на друк
                printDialog.PrintVisual(new Image { Source = renderTargetBitmap }, "Greeting Card");
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DarkButton_Click(object sender, RoutedEventArgs e)
        {
            textColor = Brushes.Black; // Встановити колір тексту на чорний
        }

        private void LightButton_Click(object sender, RoutedEventArgs e)
        {
            textColor = Brushes.White; // Встановити колір тексту на білий
        }

        private void ArialBlack_Click(object sender, RoutedEventArgs e)
        {
            textFont = new Typeface("Arial Black"); // Встановити шрифт тексту на Arial Black
        }

        private void Verdana_Click(object sender, RoutedEventArgs e)
        {
            textFont = new Typeface("Verdana"); // Встановити шрифт тексту на Verdana
        }

        private void SegoeUI_Click(object sender, RoutedEventArgs e)
        {
            textFont = new Typeface("Segoe UI"); // Встановити шрифт тексту на Segoe UI
        }

        private void ReloadWindow()
        {
            MainWindow mainWindow = new MainWindow();
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
            this.Close();
        }

        private void SupportButton_Click(object sender, RoutedEventArgs e)
        {
            SupportWindow supportWindow = new SupportWindow();
            supportWindow.Show();
        }
    }
}
