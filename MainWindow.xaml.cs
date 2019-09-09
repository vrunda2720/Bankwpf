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
        private static int _savingBalance = 0;
        private static int _currentBalance = 0;
        private static int _fdBalance = 0;
        private string _username;
        private string _password;

        private string _userLoginFile = @"C:\Users\Public\TestFolder\UserLogin.txt";
        private string _userFile = @"C:\Users\Public\Testfolder\{0}.txt";
        #endregion


        #region Constructor

        public MainWindow()
        {
            InitializeComponent();

        }

        #endregion

        #region Events
        private void Signup_Click(object sender, RoutedEventArgs e)
        {
            Login oLogin = new Login();
            oLogin.Username = inUsername.Text;
            oLogin.Password = inPassword.Text;

            if (File.Exists(_userLoginFile))
            {
                string userLogins = File.ReadAllText(_userLoginFile);  //duplicate
                if (userLogins == string.Empty)
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();//duplicate
                    string jsonData = js.Serialize(oLogin);                 //duplicate
                    JObject user = JObject.Parse(jsonData);             //duplicate
                    JArray userArray = new JArray();                    //duplicate
                    userArray.Add(user);                                //duplicate
                    File.WriteAllText(_userLoginFile, userArray.ToString());//duplicate

                    signUpPanel.Visibility = Visibility.Collapsed;  //duplicate
                    profilePanel.Visibility = Visibility.Visible;    //duplicate
                }
                else
                {
                    List<Login> userLoginsList = JsonConvert.DeserializeObject<List<Login>>(userLogins);//duplicate
                    userLoginsList.Add(oLogin);
                    string newLoginsList = JsonConvert.SerializeObject(userLoginsList);
                    File.WriteAllText(_userLoginFile, newLoginsList);

                    signUpPanel.Visibility = Visibility.Collapsed;   //duplicate
                    profilePanel.Visibility = Visibility.Visible;     //duplicate

                }
                _username = inUsername.Text;       //duplicate
                _password = inPassword.Text;   //duplicate
            }
            else
            {
                JavaScriptSerializer js = new JavaScriptSerializer();//duplicate
                string jsonData = js.Serialize(oLogin);                  //duplicate
                JObject user = JObject.Parse(jsonData);             //duplicate
                JArray userArray = new JArray();                    //duplicate
                userArray.Add(user);                                //duplicate
                File.WriteAllText(_userLoginFile, userArray.ToString());//duplicate

                signUpPanel.Visibility = Visibility.Collapsed;    //duplicate
                profilePanel.Visibility = Visibility.Visible;      //duplicate
            }

        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
                string userLogins = File.ReadAllText(_userLoginFile); //duplicate
                List<Login> userLoginsList = JsonConvert.DeserializeObject<List<Login>>(userLogins);    //duplicate
                _password = string.Empty;
                foreach (var login in userLoginsList)
                {
                    if (inUsername.Text == login.Username)
                    {

                        if (inPassword.Text == login.Password)
                        {
                            MessageBox.Show("Login Successful");
                            signUpPanel.Visibility = Visibility.Collapsed;    //duplicate
                            profilePanel.Visibility = Visibility.Visible;      //duplicate

                            _username = inUsername.Text;       //duplicate
                            _password = inPassword.Text;   //duplicate 

                            if (File.Exists(string.Format(_userFile, inUsername.Text)))
                            {
                                string userAccount = File.ReadAllText(string.Format(_userFile, inUsername.Text));
                                Account ouserAccount = JsonConvert.DeserializeObject<Account>(userAccount);

                                outSavingBox.Text = ouserAccount.SavingsBalance.ToString();
                                outCurrentBox.Text = ouserAccount.CurrentBalance.ToString();
                                outFdBox.Text = ouserAccount.FdBalance.ToString();
                                _savingBalance = ouserAccount.SavingsBalance;   //duplicate
                                _currentBalance = ouserAccount.CurrentBalance;  //duplicate
                                _fdBalance = ouserAccount.FdBalance;
                                break;


                            }
                            else
                            {
                                outSavingBox.Text = _savingBalance.ToString();
                                outCurrentBox.Text = _currentBalance.ToString();
                                outFdBox.Text = _fdBalance.ToString();
                            }

                            break;
                        }
                    }
                    else
                         {
                             MessageBox.Show("Invalid login");
                            _username = inUsername.Text;       //duplicate
                            _password = inPassword.Text;   //duplicate
                         }
                }

            }

            
            
        

        private void Submit_Click(object sender, RoutedEventArgs e)
        {

            if (comboaccount.SelectionBoxItem.ToString() == "Saving Account")
            {
                if (radiodeposit.IsChecked == true)//duplicate
                {
                    int number = 0;//duplicate

                    number = Convert.ToInt32(amountbox.Text.ToString());//duplicate
                    Deposit(Convert.ToInt32(number), "Saving Account");
                }

                if (radiowithdraw.IsChecked == true)//duplicate
                {
                    int number = 0;//duplicate
                    number = Convert.ToInt32(amountbox.Text.ToString());//duplicate

                    if (Convert.ToInt32(number) > _savingBalance)
                    {
                        MessageBox.Show("Your saving account balance is not enough");
                    }
                    else
                    {
                        Withdraw(Convert.ToInt32(number), "Saving Account");
                        MessageBox.Show("Your withdraw complete...");                   //duplicate
                        MessageBox.Show("Now your saving balance is:" + _savingBalance); //duplicate
                    }

                }

                if (radiotransfer.IsChecked == true)//duplicate
                {
                    int number = 0;                                     //duplicate
                    number = Convert.ToInt32(amountbox.Text.ToString());//duplicate

                    if (Convert.ToInt32(number) > _savingBalance)
                    {
                        MessageBox.Show("Your saving account balance is not enough");
                    }
                    else
                    {
                        Transfer(Convert.ToInt32(number), "Saving Account");
                        MessageBox.Show("Your Transfer complete...");                   //duplicate
                        MessageBox.Show("Now your saving balance is:" + _savingBalance); //duplicate
                    }

                }

                if (radiofd.IsChecked == true)
                {
                    

                    if (radioFdDeposit.IsChecked == true)
                    {
                        int number = 0;                                     //duplicate
                        number = Convert.ToInt32(amountbox.Text.ToString());//duplicate

                        if (Convert.ToInt32(number) > _savingBalance)
                        {
                            MessageBox.Show("Your savings balance is not enough.");
                        }
                        else
                        {
                            savingFD(Convert.ToInt32(number), "FdDeposit");
                            MessageBox.Show("your FD deposit complete...");
                            MessageBox.Show("Now your FD balance is:" + _fdBalance);
                            MessageBox.Show("Your saving balance is:" + _savingBalance);
                        }

                    }

                    else if (radioFdWithdraw.IsChecked == true)
                    {

                        int number = 0;                                     //duplicate
                        number = Convert.ToInt32(amountbox.Text.ToString());//duplicate
                        if (Convert.ToInt32(number) > _fdBalance)
                        {
                            Console.WriteLine("Your FD balance is not enough.");

                        }
                        else
                        {
                            savingFD(Convert.ToInt32(number), "FdWithdraw");
                            MessageBox.Show("Your FD withdraw complete...");
                            MessageBox.Show("Now your FD balance is:" + _fdBalance);
                            MessageBox.Show("Your saving balance is:" + _savingBalance);

                        }
                    }

                }


                if (comboaccount.SelectionBoxItem.ToString() == "Current Account")
                {
                    if (radiodeposit.IsChecked == true)//duplicate
                    {
                        int number = 0;                                 //duplicate

                        number = Convert.ToInt32(amountbox.Text.ToString());//duplicate
                        Deposit(Convert.ToInt32(number), "Current Account");
                    }

                    if (radiowithdraw.IsChecked == true)//duplicate

                    {
                        int number = 0;                                     //duplicate
                        number = Convert.ToInt32(amountbox.Text.ToString());//duplicate

                        if (Convert.ToInt32(number) > _currentBalance)
                        {
                            MessageBox.Show("Your current account balance is not enough");
                        }
                        else
                        {
                            Withdraw(Convert.ToInt32(number), "Current Account");
                            MessageBox.Show("Your withdraw complete...");       //duplicate
                            MessageBox.Show("Now your current balance is:" + _currentBalance);//duplicate

                        }
                    }

                    if (radiotransfer.IsChecked == true)//duplicate
                    {
                        int number = 0;                                     //duplicate
                        number = Convert.ToInt32(amountbox.Text.ToString());//duplicate

                        if (Convert.ToInt32(number) > _currentBalance)
                        {
                            MessageBox.Show("Your current account balance is not enough");

                        }
                        else
                        {
                            Transfer(Convert.ToInt32(number), "Current Account");
                            MessageBox.Show("Your Transfer complete...");       //duplicate
                            MessageBox.Show("Now your current balance is:" + _currentBalance);//duplicate
                        }
                    }
                }


            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Account oAccount = new Account();
            oAccount.SavingsBalance = _savingBalance;  //duplicate
            oAccount.CurrentBalance = _currentBalance; //duplicate
            oAccount.FdBalance = _fdBalance;
            string accountData = JsonConvert.SerializeObject(oAccount, Formatting.Indented);
            File.WriteAllText(string.Format(_userFile, _username), accountData);

            MessageBox.Show("Balance is save.");
        }

        #endregion

        #region Methods
        private void savingFD(int amount, string action)
        {
            if (action == "FdDeposit")
            {
                _fdBalance = _fdBalance + amount;
                outFdBox.Text = _fdBalance.ToString();
                _savingBalance = _savingBalance - amount;
                outSavingBox.Text = _savingBalance.ToString();
            }
            else if (action == "FdWithdraw")
            {

                double interest = 0.1 * amount;
                int amount1 = amount + Convert.ToInt32(interest);
                _fdBalance = _fdBalance - amount;
                outFdBox.Text = _fdBalance.ToString();

                _savingBalance = _savingBalance + amount1;
                outSavingBox.Text = _savingBalance.ToString();

            }
        }

        private void Transfer(int amount, string account)
        {
            if (account == "Saving Account")//duplicate
            {
                _savingBalance = _savingBalance - amount;     //duplicate
                _currentBalance = _currentBalance + amount;   //duplicate
                outCurrentBox.Text = _currentBalance.ToString();//duplicate
                outSavingBox.Text = _savingBalance.ToString();  //duplicate


            }

            if (account == "Current Account")//duplicate
            {
                _currentBalance = _currentBalance - amount;    //duplicate
                _savingBalance = _savingBalance + amount;      //duplicate
                outCurrentBox.Text = _currentBalance.ToString(); //duplicate
                outSavingBox.Text = _savingBalance.ToString();   //duplicate
            }
        }

        private void Withdraw(int amount, string account)
        {
            if (account == "Saving Account")//duplicate
            {
                _savingBalance = _savingBalance - amount;     //duplicate
                outSavingBox.Text = _savingBalance.ToString();  //duplicate
            }

            if (account == "Current Account")//duplicate
            {
                _currentBalance = _currentBalance - amount;   //duplicate
                outCurrentBox.Text = _currentBalance.ToString();//duplicate
            }
        }

        public void Deposit(int amount, string account)
        {
            if (account == "Saving Account")//duplicate
            {
                _savingBalance = _savingBalance + amount;     //duplicate
                outSavingBox.Text = _savingBalance.ToString();  //duplicate
            }

            if (account == "Current Account")//duplicate
            {
                _currentBalance = _currentBalance + amount;   //duplicate
                outCurrentBox.Text = _currentBalance.ToString();//duplicate
            }
        }
        #endregion

        private void Radiofd_Checked(object sender, RoutedEventArgs e)
        {
            radioFDPanel.Visibility = Visibility.Visible;
        }
    }
}
