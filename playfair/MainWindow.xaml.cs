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

namespace playfair
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Encrypt_Button_OnClick(object sender, RoutedEventArgs e)
        {
            KeyTable currentTable = new KeyTable(KeyBox.Text);
            EncryptedMessageBox.Text = Program.Encrypt(DecryptedMessageBox.Text, currentTable);
        }

        private void Decrypt_Button_OnClick(object sender, RoutedEventArgs e)
        {
            KeyTable currentTable = new KeyTable(KeyBox.Text);
            DecryptedMessageBox.Text = Program.Decrypt(EncryptedMessageBox.Text, currentTable);
        }
    }
}
