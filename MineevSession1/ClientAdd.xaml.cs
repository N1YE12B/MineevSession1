using System;
using System.Collections.Generic;
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
using System.Text.RegularExpressions;
using MineevSession1;
namespace MineevSession1
{
    /// <summary>
    /// Логика взаимодействия для ClientAdd.xaml
    /// </summary>
    public partial class ClientAdd : Window
    {
        Entities content = new Entities();
        public ClientAdd()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Close();
            main.ShowDialog();
            main.DataList.ItemsSource = content.Client.ToList();
        }


        private void BiDay_GotFocus(object sender, RoutedEventArgs e)
        {
            BiDay.Clear();
        }
        private void BiDay_LostFocus(object sender, RoutedEventArgs e)
        {
            if(BiDay.Text == string.Empty)
            {
                BiDay.Text = "ГГ-ММ-ДД";
            }
        }
        private void Gender_GotFocus(object sender, RoutedEventArgs e)
        {
            Gender.Clear();
        }

        private void Gender_LostFocus(object sender, RoutedEventArgs e)
        {
            if(Gender.Text == string.Empty)
            {
                Gender.Text = "М/Ж";
            }
        }
        private void clName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (int.TryParse(e.Text, out val))
            {
                e.Handled = true;
            }
        }
        private void clLName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (int.TryParse(e.Text, out val))
            {
                e.Handled = true;
            }
        }
        private void Patr_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (int.TryParse(e.Text, out val))
            {
                e.Handled = true;
            }
        }
        //private void BiDay_PreviewTextInput(object sender, TextCompositionEventArgs e)
        //{
        //    int val;
        //    if (!int.TryParse(e.Text, out val))
        //    {
        //        e.Handled = true;
        //    }
        //}
        private void Phone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (!int.TryParse(e.Text, out val))
            {
                e.Handled = true;
            }
        }
        private void Gender_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (int.TryParse(e.Text, out val))
            {
                e.Handled = true;
            }
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            //Defense
            if (string.IsNullOrEmpty(clName.Text))
            {
                MessageBox.Show("Введите имя");
                return;
            }
            if (string.IsNullOrEmpty(clLName.Text))
            {
                MessageBox.Show("Введите фамилию");
                return;
            }
            if (string.IsNullOrEmpty(Patr.Text))
            {
                MessageBox.Show("Введите отчество");
                return;
            }
            if (string.IsNullOrEmpty(BiDay.Text))
            {
                MessageBox.Show("Введите дату рождения");
                return;
            }
            if (string.IsNullOrEmpty(Email.Text))
            {
                MessageBox.Show("Введите email");
                return;
            }
            if (string.IsNullOrEmpty(Phone.Text))
            {
                MessageBox.Show("Введите телефон");
                return;
            }
            if (string.IsNullOrEmpty(Gender.Text))
            {
                MessageBox.Show("Введите пол");
                return;
            }
            //BiDay def
            string bday = BiDay.Text;
            string bdayPattern = @"[0-9]{4}-[0-9]{2}-[0-9]{2}";
            bool bdayCheck = Regex.IsMatch(bday, bdayPattern);

            bday = bday.Substring(5);
            bday = bday.Substring(0, bday.Length - 3);

            string byear = BiDay.Text;
            byear = byear.Substring(0, byear.Length - 6);

            string byday = BiDay.Text;
            byday = byday.Substring(8);


            int datday = Convert.ToInt32(byday);
            int datye = Convert.ToInt32(byear);
            int dat = Convert.ToInt32(bday);
            if (datye < 2021)
            {
                if (dat < 13)
                {
                    if (datday < 32)
                    {
                        if (bdayCheck == false)
                        {
                            MessageBox.Show("Неверный формат даты рождения");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неверный формат дня");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Неверный формат месяца");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Неверный формат года");
                return;
            }

            //Email def
            string email = Email.Text;

            string emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            bool emailCheck = Regex.IsMatch(email, emailPattern);
            if (emailCheck == false)
            {
                MessageBox.Show("Неверный формат Email");
                return;
            }

            //Phone def
            if(Phone.Text.Length <11)
            {
                MessageBox.Show("Введите 11 цифр телефона");
                return;
            }

            //Gender def
            if (Gender.Text != "Ж" && Gender.Text != "М")
            {
                MessageBox.Show("Введите корректный пол, М или Ж!");
                return;
            }
            

            DateTime D = DateTime.Parse(BiDay.Text);

            content.Client.Add(new Client
            {
               
                FirstName = clName.Text,
                LastName = clLName.Text,
                Patronymic = Patr.Text,
                Birthday = D,
                RegistrationDate = DateTime.Now,
                Email = Email.Text,
                Phone = Phone.Text,
                GenderCode = Gender.Text

            }) ;
            content.SaveChanges();
            MessageBox.Show("Пользователь добавлен!");
        }

       
    }
}
