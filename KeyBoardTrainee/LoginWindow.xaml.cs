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

namespace KeyBoardTrainee
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public DB db;
        public LoginWindow()
        {
            InitializeComponent();
        }

        public LoginWindow(DB db)
        {
            InitializeComponent();
            this.db = db;
        }


        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            bool isLoginOk = db.CheckUser(TextBoxLogin.Text, TextBoxPassword.Text);
            MessageBox.Show(isLoginOk + " " + db.userLoggedId);
        }
    }
}
