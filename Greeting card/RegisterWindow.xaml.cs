using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Linq;

namespace Greeting_card
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Password;
            string loginFilePath = "login.xml";

            // перевірка, чи існує вже користувач з таким логіном
            if (File.Exists(loginFilePath) && XElement.Load(loginFilePath).Elements("user").Any(x => (string)x.Attribute("name") == username))
            {
                MessageBox.Show("Користувач з таким логіном вже існує.");
                return;
            }

            // запис логіна та пароля у відповідні файли
            XElement userXml = new XElement("user", new XAttribute("name", username), new XAttribute("password", password));
            if (File.Exists(loginFilePath))
            {
                XElement loginXml = XElement.Load(loginFilePath);
                loginXml.Add(userXml);
                loginXml.Save(loginFilePath);
            }
            else
            {
                XDocument loginXml = new XDocument(new XElement("users", userXml));
                loginXml.Save(loginFilePath);
            }

            MessageBox.Show("Реєстрація успішна! Тепер ви можете увійти до системи.");
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }
    }
}