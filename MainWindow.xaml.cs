﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
        // List<User> cbUsers = new List<User>();
        public MainWindow()
        {
            InitializeComponent();

            users.Add(new User() { Team = "윈도우", Name = "임경준" });
            //cbUsers.Add(new User() { Team = "윈도우", Name = "임경준" });
            lsMember.ItemsSource = users;
            cbRmName.ItemsSource = users;

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
                cbRmName.SelectedItem = null;
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
                cbRmName.ItemsSource = users;
                cbRmName.Items.Refresh();
                tbAddName.Text = "";
                tbAddTeam.Text = "";

            } else
            {
                MessageBox.Show("입력 값을 입력하세요!", TITLE_NAME);
            }
        }

        private void btnRm_Click(object sender, RoutedEventArgs e)
        {
            if (cbRmName.SelectedItem != null)
            {
                users.Remove(cbRmName.SelectedItem as User);
                cbRmName.ItemsSource = users;
                cbRmName.Items.Refresh();
                lsMember.ItemsSource = users;
                lsMember.Items.Refresh();
            }
            else
            {
                MessageBox.Show("리스트에서 삭제 할 팀원을 " + Environment.NewLine + "선택 후 다시 클릭해 주세요!", TITLE_NAME);
            }
        }

        public void dbConnect(string insertQuery)
        {
            using (MySqlConnection connection = new MySqlConnection("Server=localhost;Port=3306;Database=teammanagementprogram;Uid=root;Pwd=winty0320;"))
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(insertQuery, connection);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        tbLog.Text = tbLog.Text + "Query 전송 성공";
                    } else
                    {
                        tbLog.Text = tbLog.Text + "Query 전송 실패";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("데이터베이스 오류" + Environment.NewLine + ex.Message, TITLE_NAME);
                }
            }
        }

        public void selectDb()
        {
            using (MySqlConnection connection = new MySqlConnection("Server=localhost;Port=3306;Database=teammanagementprogram;Uid=root;Pwd=winty0320;"))
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT * FROM user;", connection);
                    MySqlDataReader reader = command.ExecuteReader();

                    while(reader.Read())
                    {
                        MessageBox.Show(reader["team"] + " " + reader["name"]);
                    }
                    reader.Close();
                } catch (Exception ex)
                {
                    MessageBox.Show("데이터베이스 오류" + Environment.NewLine + ex.Message, TITLE_NAME);
                }
            }
        }

        private void btnConnectTest_Click(object sender, RoutedEventArgs e)
        {
            //dbConnect("SHOW TABLES;");
            selectDb();
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
