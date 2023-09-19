using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Tasks
{
    class User
    {
        public string User_name { get; set; }
        public string Password { get; set; }
        public User(string User_name, string Password)
        {
            this.User_name = User_name;
            this.Password = Password;
        }
        #region Change Password
        public void NewPassword(string newPass)
        {
            Password = newPass;
        }
        #endregion

        #region Save Password
        public void SavePass(string pass)
        {
            string filePath = "password.txt";
            File.WriteAllText(filePath, pass);
        }
        #endregion
  
        #region Load Password
        public static string LoadPass()
        {
            string filePath = "password.txt";
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            return null;
        }
        #endregion
    }
}
            

    

