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
            labelTanggalOutput.Text = "";
            labelSkorOutput.Text = "";
            labelValueKanan.Visible = false;
            labelValueKiri.Visible = false;
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
                labelValueKiri.Text = cBoxKiri.SelectedValue.ToString();
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
                labelValueKanan.Text = cBoxKanan.SelectedValue.ToString();
                labelNamaCaptainKanan.Text = dtKanan.Rows[0][1].ToString();
                labelNamaManagerKanan.Text = dtKanan.Rows[0][0].ToString();
            }
            catch (Exception)
            {
            }
        }

        private void dGVPertandingan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonCheck_Click(object sender, EventArgs e)
        {
            DataTable dtCheck = new DataTable();
            sqlQuery = "select d.minute as minute, if(d.team_id=m.team_home,if(d.type!='GW',p.player_name,''),if(d.type='GW',p.player_name,'')) as 'Player Name 1',if(d.team_id=m.team_home,if(d.type!='GW',case when d.type='CY' then 'Yellow Card' when d.type='CR' then 'Red Card' when d.type='GO' then 'Goal' when d.type='GP' then 'Goal Penalty' when d.type='GW' then 'Own Goal' when d.type='PM' then 'Penalty Miss' end,''),if(d.type='GW','Own Goal','')) as 'Tipe 1',if(d.team_id=m.team_away,if(d.type!='GW',p.player_name,''),if(d.type='GW',p.player_name,'')) as 'Player Name 2',if(d.team_id=m.team_away,if(d.type!='GW',case when d.type='CY' then 'Yellow Card' when d.type='CR' then 'Red Card' when d.type='GO' then 'Goal' when d.type='GP' then 'Goal Penalty' when d.type='GW' then 'Own Goal' when d.type='PM' then 'Penalty Miss' end,''),if(d.type='GW','Own Goal','')) as 'Tipe 2' from dmatch d, `match` m, player p where m.match_id=d.match_id and p.player_id=d.player_id and m.team_home='" + cBoxKiri.SelectedValue + "' and m.team_away ='" + cBoxKanan.SelectedValue + "';";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtCheck);
            dGVPertandingan.DataSource = dtCheck;

            DataTable dtID = new DataTable();
            sqlQuery = "select d.match_id as id, d.minute as minute,d.team_id, if(d.team_id=m.team_home,if(d.type!='GW',p.player_name,''),if(d.type='GW',p.player_name,'')) as 'Player Name 1',if(d.team_id=m.team_home,if(d.type!='GW',case when d.type='CY' then 'Yellow Card' when d.type='CR' then 'Red Card' when d.type='GO' then 'Goal' when d.type='GP' then 'Goal Penalty' when d.type='GW' then 'Own Goal' when d.type='PM' then 'Penalty Miss' end,''),if(d.type='GW','Own Goal','')) as 'Tipe 1',if(d.team_id=m.team_away,if(d.type!='GW',p.player_name,''),if(d.type='GW',p.player_name,'')) as 'Player Name 2',if(d.team_id=m.team_away,if(d.type!='GW',case when d.type='CY' then 'Yellow Card' when d.type='CR' then 'Red Card' when d.type='GO' then 'Goal' when d.type='GP' then 'Goal Penalty' when d.type='GW' then 'Own Goal' when d.type='PM' then 'Penalty Miss' end,''),if(d.type='GW','Own Goal','')) as 'Tipe 2' from dmatch d, `match` m, player p where m.match_id=d.match_id and p.player_id=d.player_id and m.team_home='"+cBoxKiri.SelectedValue+"' and m.team_away ='"+cBoxKanan.SelectedValue+"';";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtID);

            try
            {
                DataTable dtTanggal = new DataTable();
                sqlQuery = "select date_format(m.match_date,\"%d %M %Y\") as date, concat(m.goal_home,'-',m.goal_away) as skor from `match` m where m.match_id='" + dtID.Rows[0][0].ToString() + "';";
                sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
                sqlAdapter = new MySqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(dtTanggal);
                labelTanggalOutput.Text = dtTanggal.Rows[0][0].ToString();
                labelSkorOutput.Text = dtTanggal.Rows[0][1].ToString();
            }
            catch
            {

            }
        }
    }
}
