using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BankApp
{
    public static class UserService
    {
        public static string Username { get; set; }
        public static string Password { get; set; }
        public static string _userLoginFile = @"C:\Users\Public\TestFolder\UserLogin.txt";
        public static string _userFile= @"C:\Users\Public\Testfolder\{0}.txt";
      #region Methods

        public static bool SignUp(string username, string password)
        {
            bool isSignUp = false;

            Login oLogin = new Login();
            oLogin.Username = username;
            oLogin.Password = password;

            if (File.Exists(_userLoginFile))
            {
                string userLogins = File.ReadAllText(_userLoginFile);
                if (userLogins == string.Empty)
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    string jsonData = js.Serialize(oLogin);
                    JObject user = JObject.Parse(jsonData);
                    JArray userArray = new JArray();
                    userArray.Add(user);
                    File.WriteAllText(_userLoginFile, userArray.ToString());
                    isSignUp = true;
                }
                else
                {

                    List<Login> userLoginsList = JsonConvert.DeserializeObject<List<Login>>(userLogins);
                    userLoginsList.Add(oLogin);
                    string newLoginsList = JsonConvert.SerializeObject(userLoginsList);
                    File.WriteAllText(_userLoginFile, newLoginsList);
                    isSignUp = true;

                }

            }
            else
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string jsonData = js.Serialize(oLogin);                 
                JObject user = JObject.Parse(jsonData);             
                JArray userArray = new JArray();                    
                userArray.Add(user);                               
                File.WriteAllText(_userLoginFile, userArray.ToString());
                isSignUp = true;



            }
            return isSignUp;

        }
        public static bool Login(string username, string password)
        {
            bool isLogin = false;

            string userLogins = File.ReadAllText(_userLoginFile);
            List<Login> userLoginsList = JsonConvert.DeserializeObject<List<Login>>(userLogins);
            Password = string.Empty;
            foreach (var login in userLoginsList)
            {
                if (username == login.Username)
                {
                    if (password == login.Password)
                    {
                        isLogin = true;
                        if (File.Exists(string.Format(_userFile, username)))
                        {
                            string userAccount = File.ReadAllText(string.Format(_userFile, Username));
                            Account ouserAccount = JsonConvert.DeserializeObject<Account>(userAccount);
                            BankService.SavingsBalance = ouserAccount.SavingsBalance;
                            BankService.CurrentBalance = ouserAccount.CurrentBalance;
                            BankService.FdBalance = ouserAccount.FdBalance;
                            Username = username;
                            Password = password;

                        }
                        break;
                    }
                }
            }



            return isLogin;

        }

        #endregion





    }
}
