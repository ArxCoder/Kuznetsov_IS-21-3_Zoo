using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
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
using static System.Net.Mime.MediaTypeNames;

namespace Kuznetsov_IS_21_3_Zoo
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        private MediaPlayer player = new MediaPlayer();
        private ZooDatabase Database = new ZooDatabase();
        List<Animals> AnimalsList;
        private Users User = new Users();
        int ID;
        public Main(Users users)
        {
            InitializeComponent();
            User = users;
            AnimalsList = Database.Animals.ToList();
            switch (User.Role)
            {
                case 2:
                    UserName.Content = User.Name;
                    break;
                case 3:
                    UserName.Content = "Гость";
                    Animals.IsEnabled = false;
                    break;
            }
        }

        public void SetAnimalInfo(int ID)
        {
            if (player.CanPause) player.Pause();
            AboutText.Visibility = Visibility.Visible;
            FactsText.Visibility = Visibility.Visible;
            string path = Assembly.GetExecutingAssembly().Location.Remove(Assembly.GetExecutingAssembly().Location.LastIndexOf(@"\", StringComparison.Ordinal))
                + @"\Images\" + AnimalsList[ID].ImageSRC;
            Facts.Text = AnimalsList[ID].Facts;
            About.Text = AnimalsList[ID].About;
            AnimalImage.Source = BitmapFrame.Create(new Uri(path));
            AnimalImage.MouseLeftButtonUp += new MouseButtonEventHandler(AnimalImage_MouseLeftButtonUp);
            this.ID = ID;
        }

        public void BlockedMessage()
        {
            MessageBox.Show("Вы зашли под учетной запистью гостя.\nЧтобы просматривать информацию о животных просьба зарегистрироваться в приложении.");
        }

        private void Capybara_Click(object sender, RoutedEventArgs e)
        {
            SetAnimalInfo(4);
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            SetAnimalInfo(8);
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            SetAnimalInfo(3);
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            SetAnimalInfo(6);
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            SetAnimalInfo(1);
        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            SetAnimalInfo(7);
        }

        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            SetAnimalInfo(9);
        }

        private void Button8_Click(object sender, RoutedEventArgs e)
        {
            SetAnimalInfo(0);
        }

        private void Button9_Click(object sender, RoutedEventArgs e)
        {
            SetAnimalInfo(5);
        }

        private void Button10_Click(object sender, RoutedEventArgs e)
        {
            SetAnimalInfo(2);
        }

        private void AnimalImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PlaySound();   
        }

        private void PlaySound()
        {
            string path = Assembly.GetExecutingAssembly().Location.Remove(Assembly.GetExecutingAssembly().Location.LastIndexOf(@"\", StringComparison.Ordinal))
                + @"\Sounds\" + AnimalsList[ID].SoundSRC;
            try
            {
                if (File.Exists(path))
                {
                    if (player != null && player.Position != TimeSpan.Zero) player.Stop();
                    player.Open(new Uri(path));
                    player.Play();
                }
                else MessageBox.Show("Файл не найден: " + path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow window = new MainWindow(); 
            window.Show();
            this.Close();
        }

        private void Image_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            User_Page window = new User_Page(this.User);
            window.Show();
            this.Close();
        }
    }
}
