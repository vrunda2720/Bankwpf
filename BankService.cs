using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public static class BankService
    {

        public static int SavingsBalance { get; set; } = 0;
        public static int CurrentBalance { get; set; } = 0;
        public static int FdBalance { get; set; } = 0;


        #region Methods
        public static void Deposit(int amount, string account)
        {
            if (account == "Saving Account")
            {
                SavingsBalance = SavingsBalance + amount;
                
            }
            if (account == "Current Account")
            {
                CurrentBalance = CurrentBalance + amount;
            }
        }

        public static void Withdraw(int amount,string account)
        {
            if (account == "Saving Account")
            {
                SavingsBalance = SavingsBalance - amount;     
               
            }

            if (account == "Current Account")
            {
                CurrentBalance = CurrentBalance - amount;  
            }
        }

        public static void Transfer(int amount,string account)
        {
            if (account == "Saving Account")
            {
                SavingsBalance = SavingsBalance - amount;    
                CurrentBalance = CurrentBalance + amount;   
               
            }

            if (account == "Current Account")
            {
                CurrentBalance = CurrentBalance - amount;    
                SavingsBalance = SavingsBalance + amount;    
            }
        }

        public static void savingFD(int amount, string action)
        {
            if (action == "FdDeposit")
            {
                FdBalance = FdBalance + amount;
                SavingsBalance = SavingsBalance - amount;
            }
            else if (action == "FdWithdraw")
            {
                double interest = 0.1 * amount;
                int amount1 = amount + Convert.ToInt32(interest);
                FdBalance = FdBalance - amount;
                SavingsBalance = SavingsBalance + amount1;
            }
        }

        #endregion

    }
}
