﻿using System;
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
        public int savingbalance = 0;
        public int currentbalance = 0;
        public string username1;
        public string password1;


        public MainWindow()
        {
            InitializeComponent();

            
            

        }

        private void getbalance()
        {
           
        }

        private void Signup_Click(object sender, RoutedEventArgs e)
        {
            Login l1 = new Login();
            l1.USERNAME = userbox.Text;
            l1.PASSWORD = passwordbox.Text;

            if(File.Exists(@"C:\Users\Public\TestFolder\Bank.txt"))
            {
                string read = File.ReadAllText(@"C:\Users\Public\TestFolder\Bank.txt");
                if(read==string.Empty)
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    string jsonData = js.Serialize(l1);
                    JObject bank = JObject.Parse(jsonData);
                    JArray bankarray = new JArray();
                    bankarray.Add(bank);
                    File.WriteAllText(@"C:\Users\Public\TestFolder\Bank.txt", bankarray.ToString());

                    S.Visibility = Visibility.Collapsed;
                    P.Visibility = Visibility.Visible;
                }
                else
                {
                    List<Login> data = JsonConvert.DeserializeObject<List<Login>>(read);
                    data.Add(l1);
                    string newjson = JsonConvert.SerializeObject(data);
                    File.WriteAllText(@"C:\Users\Public\TestFolder\Bank.txt", newjson);

                    S.Visibility = Visibility.Collapsed;
                    P.Visibility = Visibility.Visible;

                }
                username1 = userbox.Text;
                password1 = passwordbox.Text;
            }
            else
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string jsonData = js.Serialize(l1);
                JObject bank = JObject.Parse(jsonData);
                JArray bankarray = new JArray();
                bankarray.Add(bank);
                File.WriteAllText(@"C:\Users\Public\TestFolder\Bank.txt", bankarray.ToString());

                S.Visibility = Visibility.Collapsed;
                P.Visibility = Visibility.Visible;
            }
            
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string read = File.ReadAllText(@"C:\Users\Public\TestFolder\Bank.txt");
            List<Login> data = JsonConvert.DeserializeObject<List<Login>>(read);
            password1 = string.Empty;
            foreach (var login in data)
            {
                if (userbox.Text == login.USERNAME)
                {


                    if (login.PASSWORD == passwordbox.Text)
                    {
                        MessageBox.Show("Login Successful");
                        S.Visibility = Visibility.Collapsed;
                        P.Visibility = Visibility.Visible;

                        if (File.Exists(string.Format(@"C:\Users\Public\TestFolder\{0}.txt", userbox.Text)))
                        {
                            string read1 = File.ReadAllText(string.Format(@"C:\Users\Public\TestFolder\{0}.txt", userbox.Text));
                            Account a1 = JsonConvert.DeserializeObject<Account>(read1);

                            savingbox.Text = savingbalance.ToString();
                            // currentbalance = a1.Cbalance;



                        }
                        else
                        {

                        }
                     
                        getbalance();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid login");
                    username1 = userbox.Text;
                    password1 = passwordbox.Text;
                }
            }

        }

        

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            
           if(comboaccount.SelectedItem.ToString() == "Saving Account")
            {
                if(radiodeposit.IsChecked==true)
                {
                    int number = 0;

                    number =Convert.ToInt32( amountbox.Text.ToString());
                    Deposit(Convert.ToInt32(number), "Saving Account");
                }
            }
        }

        public static void Deposit(int amount, string account)
        {
            if (account == "Saving Account")
            {
                int savingsbalance;
                savingsbalance = Convert.ToInt32(savingsbalance) + amount;
            }

           
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
