using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
    /// Логика взаимодействия для ModeratorWindow.xaml
    /// </summary>
    public partial class ModeratorWindow : Window
    {
        ZooDatabase Database = new ZooDatabase();
        List<Users> DatabaseUsersList = new List<Users>();
        List<Animals> DatabaseAnimalsList = new List<Animals>();
        public ModeratorWindow()
        {
            InitializeComponent();
            DatabaseUsersList = Database.Users.Where(user => user.Role != 1 && user.Role != 3).ToList();
            DatabaseAnimalsList = Database.Animals.ToList();
            RefreshList();
            RefreshAnimalsList();
        }

        private void Blocked_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Users User = DatabaseUsersList[UsersList.SelectedIndex];
                Users ChangedUser = Database.Users.Where(user => user.ID == User.ID).FirstOrDefault();
                if (ChangedUser == null) MessageBox.Show("Пользователь не найден");
                else
                {
                    ChangedUser.Status = 0;
                    Database.SaveChanges();
                    MessageBox.Show("Пользователь заблокирован");
                    RefreshList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Unblocked_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Users User = DatabaseUsersList[UsersList.SelectedIndex];
                Users ChangedUser = Database.Users.Where(user => user.ID == User.ID).FirstOrDefault();
                if (ChangedUser == null) MessageBox.Show("Пользователь не найден");
                else
                {
                    ChangedUser.Status = 1;
                    Database.SaveChanges();
                    MessageBox.Show("Пользователь разблокирован");
                    RefreshList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AnimalsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Animals SelectedAnimal = DatabaseAnimalsList[AnimalsList.SelectedIndex];
                Animals Animal_Instance_FromDB = Database.Animals.Where(animal => animal.ID == SelectedAnimal.ID).FirstOrDefault();
                Facts.Text = Animal_Instance_FromDB.Facts.ToString();
                About.Text = Animal_Instance_FromDB.About.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RefreshList()
        {
            try
            {
                UsersList.Items.Clear();
                for (int i = 0; i < DatabaseUsersList.Count; i++)
                {
                    UsersList.Items.Add("Имя: " + DatabaseUsersList[i].Name + 
                        " Статус: " + DatabaseUsersList[i].Status + 
                        " Логин: " + DatabaseUsersList[i].Login + 
                        " Почта: " + DatabaseUsersList[i].Email);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void RefreshAnimalsList()
        {
            try
            {
                AnimalsList.Items.Clear();
                for (int i = 0; i < DatabaseAnimalsList.Count; i++)
                {
                    AnimalsList.Items.Add("Название: " + DatabaseAnimalsList[i].Name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AnimalsList.SelectedIndex == -1) MessageBox.Show("Выберите животное информацию о котором вы хотите изменить");
                else if (Facts.Text.Equals(String.Empty) || About.Text.Equals(String.Empty)) MessageBox.Show("Поля должны быть заполнены");
                else
                {
                    Animals Animal = DatabaseAnimalsList[AnimalsList.SelectedIndex];
                    Animals Animal_Instance_FromDB = Database.Animals.Where(animal => animal.ID == Animal.ID).FirstOrDefault();
                    if (Animal_Instance_FromDB == null) MessageBox.Show("Выбранное животное не существует");
                    else
                    {
                        MessageBox.Show("Вы выбрали животное: " + Animal_Instance_FromDB.Name);
                        Animal_Instance_FromDB.Facts = Facts.Text.ToString();
                        Animal_Instance_FromDB.About = About.Text.ToString();
                        Database.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                MainWindow window = new MainWindow();
                window.Show();
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
