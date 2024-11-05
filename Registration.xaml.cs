using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kuznetsov_IS_21_3_Zoo
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        ZooDatabase Database = new ZooDatabase();
        List<Users> UsersList = new List<Users>();  
        public Registration()
        {
            InitializeComponent();
            UsersList = Database.Users.ToList();
        }

        private void Enter_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UserName.Text.Equals(String.Empty) || EMail.Text.Equals(String.Empty) || Login.Text.Equals(String.Empty) ||
                    Password.Text.Equals(String.Empty) || PasswordRepeat.Text.Equals(String.Empty)) 
                    MessageBox.Show("Для регистрации все поля должны быть заполнены");
                else
                {
                    if (!Password.Text.Equals(PasswordRepeat.Text.ToString())) MessageBox.Show("Введенные пароли не совпадают");
                    else
                    {
                        if (CheckMail(EMail.Text.ToString()))
                        {
                            if (LoginCheck(Login.Text.ToString()))
                            {
                                Users user = new Users();
                                user.Name = UserName.Text.ToString();
                                user.Email = EMail.Text.ToString();
                                user.Login = Login.Text.ToString();
                                user.Password = Password.Text.ToString();
                                user.Status = 0;
                                user.Role = 2;
                                MessageBox.Show("Регистрация прошла успешно!\n Для доступа к приложению просьба обратиться к администратору.");
                                Database.Users.Add(user);
                                Database.SaveChangesAsync();
                                MainWindow window = new MainWindow();
                                this.Hide();
                                window.Show();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool CheckMail(string EMail) // А тут я решил послать регулярное выражение далеко и надолго
        {
            try { MailAddress mailAddress = new MailAddress(EMail); return true; }
            catch (FormatException) { MessageBox.Show("Почта введена неверно"); return false; }
        }

        private bool LoginCheck(string Login)
        {
            try
            {
                bool Access = false;
                for (int i = 0; i < UsersList.Count; i++)
                {
                    if (Login == UsersList[i].Login)
                    {
                        MessageBox.Show("Пользователь с таким логином уже существует.");
                        Access = false;
                        break;
                    }
                    else { Access = true; }
                }
                return Access;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow window = new MainWindow();
            this.Hide();
            window.Show();
        }
    }
}
