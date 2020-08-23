using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace TeamManagementProgram
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public const string TITLE_NAME = "팀원 관리 프로그램";
        List<User> users = new List<User>();
        public MainWindow()
        {
            InitializeComponent();

            users.Add(new User() { Team = "윈도우", Name = "임경준" });
            lsMember.ItemsSource = users;
            
        }

        private void btnRmInList_Click(object sender, RoutedEventArgs e)
        {
            if (lsMember.SelectedItem != null)
            {

                foreach (User item in lsMember.SelectedItems)
                {
                    users.Remove(item);
                }
                lsMember.ItemsSource = users;          
                lsMember.Items.Refresh();
            } else
            {
                MessageBox.Show("리스트에서 삭제 할 팀원을 " + Environment.NewLine + "선택 후 다시 클릭해 주세요!", TITLE_NAME);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!(tbAddName.Text.Equals("") || tbAddTeam.Text.Equals("")))
            {
                foreach (User user in lsMember.Items)
                {
                    if (user.Name.Equals(tbAddName.Text))
                    {
                        MessageBox.Show("같은 이름의 팀원이 이미 존재합니다!", TITLE_NAME);
                        tbAddName.Text = "";
                        tbAddTeam.Text = "";
                        return;
                    }
                }
                users.Add(new User() { Team = tbAddTeam.Text, Name = tbAddName.Text });
                lsMember.ItemsSource = users;
                lsMember.Items.Refresh();
                
            } else
            {
                MessageBox.Show("입력 값을 입력하세요!", TITLE_NAME);
            }
        }

        private void btnRm_Click(object sender, RoutedEventArgs e)
        {
            if (!(tbRmName.Text.Equals("")))
            {

            }
            else
            {
                MessageBox.Show("입력 값을 입력하세요!", TITLE_NAME);
            }
        }

        public class User
        {
            public string Team { get; set; }

            public string Name { get; set; }

            public override string ToString()
            {
                return this.Team + " " + this.Name; 
            }
        }

        public class UserForCb
        {
            public string Name { get; set; }

            public override string ToString()
            {
                return this.Name;
            }
        }
    }
}
