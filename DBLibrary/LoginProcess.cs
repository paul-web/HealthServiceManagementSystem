using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DBLibrary
{
    public class LoginProcess
    {
        // method for validating email, password input
        public bool valdateEmailPasswordInput(string email, string password)
        {
            bool isValidEmailPassword = false;
            // catch invalid sign in attempts and print error message
            if (email.Length == 0 || password.Length == 0)
            {
                MessageBox.Show("Fields can not be blank! \nYou must enter a value in each field", "Error signing in", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            // catch invalid password length and print error message
            else if (password.Length < 5)
            {
                MessageBox.Show("Passwords must have at least 5 characters!", "Error signing in", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            // catch invalid password length and print error message
            else if (password.Length > 30)
            {
                MessageBox.Show("Passwords cannot be greater than 30 characters in length!", "Error signing in", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            /* (since my DB was already uploaded to blackboard I will not implement this password check)
            // check that password contains at least one number
            else if (!password.Any(x => Char.IsDigit(x)))
            {
                MessageBox.Show("Passwords must have at least 1 number!", "Error signing in", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            */
            // catch invalid email format and print error message
            else if (!email.Contains('@') || !email.Contains('.'))
            {
                MessageBox.Show("Please use an accepted email format!", "Error signing in", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            // if log in fields are valid run check on DB for valid user 
            else
            {
                isValidEmailPassword = true;
            }
            return isValidEmailPassword;
        }
    }
}
