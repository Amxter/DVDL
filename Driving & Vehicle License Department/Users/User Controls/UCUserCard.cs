using BusinessDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Driving___Vehicle_License_Department.Users
{
    public partial class UCUserCard : UserControl
    {

        IUserServices _userServices;
       public UserDTO User { get; set; }
        public UCUserCard()
        {
            InitializeComponent();
            _userServices = ServiceFactory.CreateUserServices();
        }
        public void LoaderData(int userID )
        {

            User = _userServices.GetByID(userID) ;

            if (User.UserID != -1)
            {

                ucDetailsPerson1.LoadPersonDetails(User.PersonID);
                lblUserID.Text = User.UserID.ToString();
                lblUserName.Text = User.UserName;
                lblIsActive.Text = User.IsActive ? "Yes" : "No";
            }
            else
            {
                MessageBox.Show("User not found.");

            }
 

        }
    }
}
