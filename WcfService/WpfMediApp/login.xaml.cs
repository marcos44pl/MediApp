using MediApp.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ComunicationControllers;

namespace WpfMediApp
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Window
    {
        static DbServices.PatientsContext db = new DbServices.PatientsContext(WcfConfig.WcfUri);

        public login()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxEmail.Text.Length == 0)
            {
                errormessage.Text = "Wprowadź email.";
                textBoxEmail.Focus();
            }
            else if (!Regex.IsMatch(textBoxEmail.Text, @"^[a-zA-Z0-9][\w\.-]*@[a-zA-Z0-9][\w\.-]*\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errormessage.Text = "Wprowadź poprawny email.";
                textBoxEmail.Select(0, textBoxEmail.Text.Length);
                textBoxEmail.Focus();
            }
             else 
            {
                string email = textBoxEmail.Text;
                string password = passwordBox.Password;
                                
                SHA256Managed crypt = new SHA256Managed();
                byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password), 0,
                                                  Encoding.UTF8.GetByteCount(password));

                try
                {
                    if (1 == db.TableUser.Where(p => p.Email == email.ToLower()).Count())
                    {

                    }
                    else
                    {
                        errormessage.Text = "Błędny email i/lub hasło.";
                        return;
                    }
                }
                catch (InvalidOperationException e2)
                {
                    return;
                }

                var pat = db.TableUser.Where(p => p.Email == email.ToLower()).First();

                if (null == pat.Pass) {
                    errormessage.Text = "Błędne hasło.";
                    return;
                }

                if (pat.Pass.SequenceEqual(crypto))
                {
                    UserPersister.Username = email;
                    
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(MainWindow))
                        {
                            (window as MainWindow).login_button.Visibility = Visibility.Hidden;
                        }
                    }
                    
                    Close();
                }

                errormessage.Text = "Email i hasło nie pasują do siebie.";
            }
        }
    }
}
