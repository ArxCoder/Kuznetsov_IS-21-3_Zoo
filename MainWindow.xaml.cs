using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Win32;

namespace Kuznetsov_IS_21_3_Zoo
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // ------------------------------------------ Переменные для блокировки 
        DispatcherTimer timer1 = new DispatcherTimer();
        private int secSave = 0;
        private int minSave = 0;
        private int hourSave = 0;
        private int secCur = 0;
        private int minCur = 0;
        private int hourCur = 0;
        private int lastSeconds = 0;
        // ------------------------------------------

        // ------------------------------------------ Работа с БД
        private ZooDatabase Database = new ZooDatabase();
        private List<Users> UsersList;
        private List<Roles> ListRoles;
        // ------------------------------------------
        private int Count = 0; //Счетчик попыток кол-ва входов
        public MainWindow()
        {
            InitializeComponent();
            UsersList = Database.Users.ToList();
            ListRoles = Database.Roles.ToList();
            GetRegistryTime();
        }

        private void Enter_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                if (login.Text == String.Empty || password.Text == String.Empty) MessageBox.Show("Заполните данные для входа");
                else
                {
                    Users User = UsersList.Where(user => user.Login == login.Text.ToString()).FirstOrDefault();
                    if (User != null)
                    {
                        Count++;
                        if (!CheckAcess(User)) { return; }
                        else
                        {
                            if (User.Role == ListRoles[0].ID)
                            {
                                ModeratorWindow Moderatorwindow = new ModeratorWindow();
                                Moderatorwindow.Show();
                                this.Close();
                            }
                            else
                            {
                                Main window = new Main(User);
                                window.Show();
                                this.Close();
                            }
                        }
                    }
                    else MessageBox.Show("Введенного пользователя не существует");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private bool CheckAcess(Users User) //Проверка логина и пароля
        {
            bool Access = false;
            try
            {
                switch (User.Status)
                {
                    case 0:
                        MessageBox.Show("Отказано в доступе.\n Для разблокировки обратитесь к администратору.");
                        break;
                    case 1:
                        if (User.Password == password.Text.ToString() && Count < 2)
                        {
                            Access = true;
                            Count = 0;
                            CreateRegistryTime(-1, -1, -1);
                        }
                        else if (User.Password == password.Text.ToString() && Count >= 2)
                        {
                            if (CheckCaptcha())
                            {
                                Access = true;
                                CreateRegistryTime(-1, -1, -1);
                            }
                            else { CheckCount(); }
                        }
                        else
                        {
                            MessageBox.Show("Неверный логин или пароль");
                            CheckCount();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return Access;
        }

        private void CheckCount() //Проверка кол-ва попыток ввода логина и пароля пользователем
        {
            if (Count < 2) { return; }
            else if (Count == 2 || Count == 3) {
                MessageBox.Show("Введите капчу");
                GetCaptcha();
            }
            else if (Count == 4)
            {
                MessageBox.Show("Следующая попытка входа доступна через 1 минуту");
                SetBlock(true);
            }
            else if (Count < 0 || Count > 4)
            {
                MessageBox.Show("Fatal Error!");
                Application.Current.Shutdown();
            }
        }

        private void GetCaptcha() //Генерация капчи
        {
            Captcha.Visibility = Visibility.Visible;
            CaptchaGetted.Visibility = Visibility.Visible;
            CaptchaInput.Visibility = Visibility.Visible;
            CaptchaText.Visibility = Visibility.Visible;
            string allowchar = "";
            allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";
            allowchar += "1,2,3,4,5,6,7,8,9,0";
            char[] a = { ',' };
            String[] ar = allowchar.Split(a);
            String pwd = " ";
            string temp;
            Random r = new Random();
            for (int i = 0; i < 6; i++)
            {
                temp = ar[(r.Next(0, ar.Length))];
                pwd += temp;
            }
            CaptchaGetted.Text = pwd;
        }

        private bool CheckCaptcha()
        {
            
            if (!CaptchaInput.Text.ToString().Replace(" ", "").Equals(CaptchaGetted.Text.ToString().Replace(" ", ""))) 
            { MessageBox.Show("Капча введена неверно"); }
            else
            {
                Count = 0;
                Captcha.Visibility = Visibility.Hidden;
                CaptchaText.Visibility = Visibility.Hidden;
                CaptchaInput.Text = "";
                CaptchaGetted.Text = "";
                CreateRegistryTime(-1, -1, -1);
                return true;
            }
            return false;
        }







        private void timer1_Tick(object sender, EventArgs e)
        {
            secCur = GetSeconds();
            minCur = GetMinutes();
            hourCur = GetHours();
            lastSeconds = 60 - ((hourCur * 3600 + minCur * 60 + secCur) - (hourSave * 3600 + minSave * 60 +
            secSave));
            if (lastSeconds <= 0)
            {
                SetUnblock();
            }
            else
            {
                //MessageBox.Show("Неверный логин или пароль.\nCледующая попытка входа через: " + lastSeconds);
                timer1.Stop();
                timer1.Interval = TimeSpan.FromSeconds(1);
                timer1.Tick += timer1_Tick;
                timer1.Start();
            }
        }
        //событие для записи времени блокировки в регистр
        private void SetBlock(bool setTime = false)//по умолчанию параметр в значении ложь
        {
            if (setTime)
            {
                secSave = GetSeconds();//запоминаем текущую секунду
                minSave = GetMinutes();//запоминаем текущую минуту
                hourSave = GetHours();//запоминаем текущий час
                CreateRegistryTime(secSave, minSave, hourSave);//сохраняем значения в регистр
            }
            login.IsEnabled = false;
            password.IsEnabled = false;
            EnterButton.IsEnabled = false;
            timer1.Interval = TimeSpan.FromSeconds(1);
            timer1.Tick += timer1_Tick;
            timer1.Start();
            login.Text = "";
            password.Text = "";
            MessageBox.Show("Следующая попытка входа через минуту");
        }

        //снятие блокировки с кнопок и текстбоксов
        private void SetUnblock()
        {
            login.IsEnabled = true;
            password.IsEnabled = true;
            EnterButton.IsEnabled = true;
            Count = 0;
            timer1.Stop();
        }
        //События для получения значений времени
        private int GetSeconds()
        {
            return Convert.ToInt32(DateTime.Now.ToString().Substring((DateTime.Now.ToString().Length - 2), 2)); ;
        }
        private int GetMinutes()
        {
            return Convert.ToInt32(DateTime.Now.ToString().Substring((DateTime.Now.ToString().Length - 5), 2));
        }
        private int GetHours()
        {
            return Convert.ToInt32(DateTime.Now.ToString().Substring((DateTime.Now.ToString().Length - 8), 2));
        }
        //запись времени в регистр (записывает время пользователя)
        private void CreateRegistryTime(int sec, int min, int hour)
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey GIBDD_KEY = currentUserKey.CreateSubKey("GIBDD_TIME");
            GIBDD_KEY.SetValue("seconds", sec.ToString());
            GIBDD_KEY.SetValue("minutes", min.ToString());
            GIBDD_KEY.SetValue("hours", hour.ToString());
            GIBDD_KEY.Close();
        }
        //проверка на доступность входа по времени
        private void GetRegistryTime()
        {
            if (Registry.CurrentUser.OpenSubKey("GIBDD_TIME") == null)
            {
                //значения -1 для проверки возможности входа в программу.
                CreateRegistryTime(-1, -1, -1);
                return;
            }
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey GIBDD_KEY = currentUserKey.OpenSubKey("GIBDD_TIME");
            secSave = Convert.ToInt32(GIBDD_KEY.GetValue("seconds"));
            minSave = Convert.ToInt32(GIBDD_KEY.GetValue("minutes"));
            hourSave = Convert.ToInt32(GIBDD_KEY.GetValue("hours"));
            GIBDD_KEY.Close();
            secCur = GetSeconds();
            minCur = GetMinutes();
            hourCur = GetMinutes();
            if (secSave == -1 && minSave == -1 && hourSave == -1)//если из всех ключей регистра получает
            {
                CreateRegistryTime(-1, -1, -1);
                return;
            }
            lastSeconds = 60 - ((hourCur * 3600 + minCur * 60 + secCur) - (hourCur * 3600 + minSave * 60 +
            secSave));//если нельзя входить, то блокирует и ругается
            if (lastSeconds > 0)
            {
                SetBlock();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Users user = new Users();
                user = UsersList[2];
                Main window = new Main(user);
                window.Show();
                this.Hide();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error: " + exception.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Registration window = new Registration();
            this.Hide();
            window.Show();
        }
    }
}
