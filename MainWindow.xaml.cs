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
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BankApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
     #region Fields

         public static string _accountType;

     #endregion

      #region Constructor

        public MainWindow()
        {
            InitializeComponent();
            _accountType = comboaccount.SelectionBoxItem.ToString();

        }

        #endregion

        #region Event  
        public  void Signup_Click(object sender, RoutedEventArgs e)
        {
            bool mainSignUp = UserService.SignUp(inUsername.Text, inPassword.Text);

            if (mainSignUp)
            {
             
                MessageBox.Show("SignUp Success");
                signUpPanel.Visibility = Visibility.Collapsed;   
                profilePanel.Visibility = Visibility.Visible;     
            }
            else
            {
                MessageBox.Show("Something went wrong");
            }
            
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {

            bool mainLogin = UserService.Login(inUsername.Text, inPassword.Text);

            if (mainLogin)
            {
                MessageBox.Show("Login Successful");
               
                signUpPanel.Visibility = Visibility.Collapsed;
                profilePanel.Visibility = Visibility.Visible;
                RefreshOutput();
            }
            else
            {
                MessageBox.Show("Something went wrong");
            }
            
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            _accountType = comboaccount.SelectionBoxItem.ToString();
            int number = 0;
            number = Convert.ToInt32(amountbox.Text.ToString());


            if (radiodeposit.IsChecked == true)//duplicate
            {
                BankService.Deposit(number, _accountType);
                RefreshOutput();
            }

            if (radiowithdraw.IsChecked == true)//duplicate
            {
                int balance = _accountType == "Saving Account" ? BankService.SavingsBalance : BankService.CurrentBalance;
                if (Convert.ToInt32(number) > balance)
                {
                    MessageBox.Show("Your account balance is not enough", _accountType);
                }
                else
                {
                    BankService.Withdraw(number, _accountType);
                    RefreshOutput();
                }

            }

            if (radiotransfer.IsChecked == true)//duplicate
            {
                int balance = _accountType == "Saving Account" ? BankService.SavingsBalance : BankService.CurrentBalance;
                if (Convert.ToInt32(number) > balance)
                {
                    MessageBox.Show("Your {0} balance is not enough", _accountType);
                }
                else
                {
                    BankService.Transfer(number, _accountType);
                    RefreshOutput();
                }
            }

            if (radiofd.IsChecked == true)
            {
                if (radioFdDeposit.IsChecked == true)
                {

                    if (Convert.ToInt32(number) > BankService.SavingsBalance)
                    {
                        MessageBox.Show("Your savings balance is not enough.");
                    }
                    else
                    {
                        BankService.savingFD(number, "FdDeposit");
                        RefreshOutput();
                    }

                }

                else if (radioFdWithdraw.IsChecked == true)
                {

                    if (Convert.ToInt32(number) > BankService.FdBalance)
                    {
                        MessageBox.Show("Your FD balance is not enough.");

                    }
                    else
                    {
                        BankService.savingFD(number, "FdWithdraw");
                        RefreshOutput();
                    }
                }

            }

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Account oAccount = new Account();
            oAccount.SavingsBalance = BankService.SavingsBalance;  //duplicate
            oAccount.CurrentBalance = BankService.CurrentBalance; //duplicate
            oAccount.FdBalance = BankService.FdBalance;
            string accountData = JsonConvert.SerializeObject(oAccount, Formatting.Indented);
            File.WriteAllText(string.Format(UserService._userFile, UserService.Username), accountData);

            MessageBox.Show("Balance is save.");
        }

        private void Radiofd_Checked(object sender, RoutedEventArgs e)
        {
            radioFDPanel.Visibility = Visibility.Visible;
        }

        #endregion

        
        private  void RefreshOutput()
        {
            outSavingBox.Text = BankService.SavingsBalance.ToString();
            outCurrentBox.Text = BankService.CurrentBalance.ToString();
            outFdBox.Text = BankService.FdBalance.ToString();
        }

    }

}

