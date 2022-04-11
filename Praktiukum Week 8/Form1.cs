using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Praktiukum_Week_8
{
    public partial class FormPertandingan : Form
    {
        public static string sqlConnection = "server=localhost;uid=root;pwd=;database=premier_league";
        public MySqlConnection sqlConnect = new MySqlConnection(sqlConnection);
        public MySqlCommand sqlCommand;
        public MySqlDataAdapter sqlAdapter;


        string sqlQuery;
        public FormPertandingan()
        {
            InitializeComponent();
        }

        private void FormPertandingan_Load(object sender, EventArgs e)
        {
            sqlConnect.Open();
            DataTable dtKiri = new DataTable();
            sqlQuery = "select team_name as'Nama Tim',team_id as 'ID Team',manager_id from team";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtKiri);
            cBoxKiri.DataSource = dtKiri;
            cBoxKiri.DisplayMember = "Nama Tim";
            cBoxKiri.ValueMember = "ID Team";

            DataTable dtKanan = new DataTable();
            sqlQuery = "select team_name as'Nama Tim',team_id as 'ID Team',manager_id from team";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtKanan);
            cBoxKanan.DataSource = dtKanan;
            cBoxKanan.DisplayMember = "Nama Tim";
            cBoxKanan.ValueMember = "ID Team";

            labelAngkaCapacity.Text = "";
            labelNamaCaptainKiri.Text = "";
            labelNamaManagerKiri.Text = "";
            labelNamaStadium.Text = "";
            labelNamaManagerKanan.Text = "";
            labelNamaCaptainKanan.Text = "";
        }

        private void cBoxKiri_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtKiri = new DataTable();
                sqlQuery = "SELECT manager_name,player_name,concat(home_stadium,', ',city) ,capacity FROM team,manager,player WHERE team.manager_id=manager.manager_id and team.captain_id=player.player_id and  team.team_id = '" + cBoxKiri.SelectedValue + "';";
                sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
                sqlAdapter = new MySqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(dtKiri);
                labelNamaCaptainKiri.Text = dtKiri.Rows[0][1].ToString();
                labelNamaManagerKiri.Text = dtKiri.Rows[0][0].ToString();
                labelNamaStadium.Text = dtKiri.Rows[0][2].ToString();
                labelAngkaCapacity.Text = dtKiri.Rows[0][3].ToString();
            }
            catch (Exception)
            {
            }
        }

        private void cBoxKanan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtKanan = new DataTable();
                sqlQuery = "SELECT manager_name,player_name FROM team,manager,player WHERE team.manager_id=manager.manager_id and team.captain_id=player.player_id and  team.team_id = '" + cBoxKanan.SelectedValue + "';";
                sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
                sqlAdapter = new MySqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(dtKanan);
                labelNamaCaptainKanan.Text = dtKanan.Rows[0][1].ToString();
                labelNamaManagerKanan.Text = dtKanan.Rows[0][0].ToString();
            }
            catch (Exception)
            {
            }
        }
    }
}
