using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace arduino_project
{
    public partial class Form1 : Form
    {
        MqttService mqttService = new MqttService();
        String cs = @"server=localhost;userid=root;password=;database=rfid_thermometer";
        String sql = "";
        String receivedString = "";
        String[] mqttData;
        String message = "";
        String login = "";
        int currentId = 0;
        int newId = 0;
        public Form1()
        {
            InitializeComponent();
            listBoxLogs.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            listBoxLogs.MeasureItem += listBoxLogs_MeasureItem;
            listBoxLogs.DrawItem += listBoxLogs_DrawItem;

            void listBoxLogs_MeasureItem(object sender, MeasureItemEventArgs e)
            {
                e.ItemHeight = (int)e.Graphics.MeasureString(listBoxLogs.Items[e.Index].ToString(), listBoxLogs.Font, listBoxLogs.Width).Height;
            }

            void listBoxLogs_DrawItem(object sender, DrawItemEventArgs e)
            {
                e.DrawBackground();
                e.DrawFocusRectangle();
                e.Graphics.DrawString(listBoxLogs.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds);
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            receivedString = mqttService.subscribeMessage("meteo", textBoxIP.Text);
            mqttData = receivedString.Split(';');

            if (receivedString != "")
            {
                newId = Int32.Parse(mqttData[0]);

                if (currentId != newId)
                {
                    MySqlConnection con = new MySqlConnection(cs);
                    con.Open();
                    sql = "SELECT login FROM employees WHERE rfid = '" + mqttData[2] + "'";
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        login = rdr.GetString(0);
                    }
                    con.Close();

                    if (login != "")
                    {
                        message = "Hello, " + login + "! The temperature today is: " + mqttData[1] + "C. Have a nice day!";
                        listBoxLogs.Items.Add(message);
                        currentId = newId;

                        con = new MySqlConnection(cs);
                        con.Open();
                        sql = "INSERT INTO logs(temperature, rfid, employee) VALUES('" + mqttData[1] + "','" + mqttData[2] + "'," + mqttData[3] + ")";
                        cmd = new MySqlCommand(sql, con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        message = "Unauthorized access by RFID: " + mqttData[2];
                        listBoxLogs.Items.Add(message);
                        currentId = newId;

                        con = new MySqlConnection(cs);
                        con.Open();
                        sql = "INSERT INTO logs(temperature, rfid, employee) VALUES('" + mqttData[1] + "','" + mqttData[2] + "'," + mqttData[3] + ")";
                        cmd = new MySqlCommand(sql, con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    login = "";
                    listBoxLogs.TopIndex = listBoxLogs.Items.Count - 1;
                }
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }
    }
}
