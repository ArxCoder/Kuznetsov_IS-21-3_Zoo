using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для User_Page.xaml
    /// </summary>
    public partial class User_Page : Window
    {
        ZooDatabase Database = new ZooDatabase();
        Users User = new Users();
        private readonly string path = Assembly.GetExecutingAssembly().Location.Remove(Assembly.GetExecutingAssembly().Location.LastIndexOf(@"\", StringComparison.Ordinal));

        public readonly string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public const string ImageFolderName = "User_Images";

        public User_Page(Users Current_User)
        {
            InitializeComponent();
            this.User = Current_User;

            User_Name.Text = User.Name.ToString();
            CurrentUser_Name.Text = User.Name.ToString();
            CurrentUser_EMail.Text = User.Email.ToString();
            CurrentUser_Password.Text = User.Password.ToString();

            try
            {
                if (User.Image == null) return;
                else
                {
                    User_Image.Source = BitmapFrame.Create(new Uri(path + @"\User_Images\" + User.Image.ToString()));
                }
            }
            catch
            {
                User_Image.Source = BitmapFrame.Create(new Uri(path + @"\user.png"));
            }
        }

        private void Save_Changes_Click(object sender, RoutedEventArgs e)
        {
            var Current_User_New = Database.Users.Where(user => user.ID == this.User.ID).FirstOrDefault();
            try
            {
                if (CurrentUser_Name.Text.Equals(String.Empty) || CurrentUser_EMail.Text.Equals(String.Empty) ||
                    CurrentUser_Password.Text.Equals(String.Empty)) MessageBox.Show("Заполните все поля");
                else
                {
                    if (!CheckMail(CurrentUser_EMail.Text.ToString())) MessageBox.Show("Введенная почта не соответствует требованиям");
                    else
                    {
                        Current_User_New.Name = CurrentUser_Name.Text.ToString();
                        Current_User_New.Email = CurrentUser_EMail.Text.ToString();
                        Current_User_New.Password = CurrentUser_Password.Text.ToString();
                        Database.SaveChanges();
                        MessageBox.Show("Изменения успешно сохранены");

                        MainWindow window = new MainWindow();
                        window.Show();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool CheckMail(string EMail) 
        {
            try { MailAddress mailAddress = new MailAddress(EMail); return true; }
            catch (FormatException) { MessageBox.Show("Почта введена неверно"); return false; }
        }

        private void Go_To_Main_Click(object sender, RoutedEventArgs e)
        {
            Main window = new Main(User);
            window.Show();
            this.Close();
        }

        private void Change_Photo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png";
            openFileDialog.Title = "Выберите новое изображение";
            string ImageFolderPath = System.IO.Path.Combine(BaseDirectory, ImageFolderName);
            try
            {
                Nullable<bool> res = openFileDialog.ShowDialog();
                if (res == true)
                {
                    try
                    {
                        if (!Directory.Exists(ImageFolderPath))
                        {
                            Directory.CreateDirectory(ImageFolderPath);
                        }
                        string savepath = System.IO.Path.Combine(ImageFolderPath, openFileDialog.SafeFileName);
                        File.Copy(openFileDialog.FileName, savepath);

                        var Current_User_NewImage = Database.Users.Where(user => user.ID == User.ID).FirstOrDefault();
                        Current_User_NewImage.Image = openFileDialog.SafeFileName;
                        Database.SaveChanges();
                        MessageBox.Show("Изменения успешно сохранены");

                        MainWindow window = new MainWindow();
                        window.Show();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
