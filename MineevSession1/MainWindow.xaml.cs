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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace MineevSession1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {
        Entities content = new Entities();
       
        public MainWindow()
        {

            InitializeComponent();
            DataList.ItemsSource = content.Client.ToList();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            ClientAdd clAdd = new ClientAdd();
            this.Close();
            clAdd.ShowDialog();
            DataList.ItemsSource = content.Client.ToList();
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            //if(DataList.SelectedItem is false)
            //{
            //    MessageBox.Show("Выберите запись клиента!");
            //    return; 
            //}
          
            
            if (DataList.SelectedItems.Count > 0)
            {
                var result = MessageBox.Show("Удалить клиента?", "Удаление клиента", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (DataList.SelectedItem is Client client)
                    {
                        content.Client.Remove(content.Client.Where(i => i.ID == client.ID).FirstOrDefault());
                        content.SaveChanges();
                        DataList.ItemsSource = content.Client.ToList();
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите запись клиента!");
                return;
            }
        }
    }
}
