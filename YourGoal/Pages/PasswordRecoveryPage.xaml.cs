using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using YourGoal.Services;

namespace YourGoal.Pages
{
    public partial class PasswordRecoveryPage : Page
    {
        private int _code;
        private string _email;
        public PasswordRecoveryPage()
        {
            InitializeComponent();
            
        }
        //отправка
        public void SendMail()
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("Giga-Pekka@yandex.ru");
                mailMessage.To.Add(new MailAddress(InformationTextBox.Text));
                mailMessage.Subject = $"Тема - Восстановление пароля пользователя";
                Random rnd = new Random();
                _code = rnd.Next(1000, 9999);
                mailMessage.Body = $"{_code}";
                SmtpClient Smtpclient = new SmtpClient();
                Smtpclient.Host = "smtp.yandex.ru";
                Smtpclient.Port = 587;
                Smtpclient.EnableSsl = true;
                Smtpclient.Credentials = new NetworkCredential("Giga-Pekka@yandex.ru", "QW12er#$TY56");
                Smtpclient.Send(mailMessage);
            }
            catch
            {
                WarningTextBlock.Text = "Неверно введён почтовый адрес";
            }
        }

        public bool Verification(int code)
        {
            if (code == _code)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //кнопка отправки
        private void SendCodeButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (InformationTextBox.Text != "")
            {
                SendMail();
                Thread.Sleep(1000);
                _email = InformationTextBox.Text;
                SendPanel.Visibility = Visibility.Collapsed;
                VerificationPanel.Visibility = Visibility.Visible;
                InformationTextBox.Text = "";
                RequirementTextBlock.Text = "Введите код с вашей почты";
            }
            else
            {
                WarningTextBlock.Text = "Неверно введён почтовый адрес";
            }
        }
        //кнопка проверки
        private void VerificationButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (InformationTextBox.Text != "")
            {
                if (Verification(Convert.ToInt32(InformationTextBox.Text)))
                {
                    Thread.Sleep(1000);
                    VerificationPanel.Visibility = Visibility.Collapsed;
                    RecoveryPasnel.Visibility = Visibility.Visible;
                    InformationTextBox.Text = "";
                    RequirementTextBlock.Text = "Введите новый пароль";
                }
                else
                {
                    WarningTextBlock.Text = "Ошибка введённого кода, повторите попытку";
                }
            }
            else
            {
                WarningTextBlock.Text = "Неверно введён почтовый адрес";
            }
        }
        //кнопка восстановления
        private void RecoveryButton_OnClick(object sender, RoutedEventArgs e)
        {
            AllUserService userService = new AllUserService();
            if (userService.RecoveryPassword(_email, InformationTextBox.Text))
            {
                NavigationService.Navigate(new AuthPage());
            }
            else
            {
                WarningTextBlock.Text = "Ошибка";
            }
            
        }
    }
}