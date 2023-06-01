using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DZ3_Savchuk
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        private async Task LoadBooksData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    string cs = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
                    conn.ConnectionString = cs;
                    await conn.OpenAsync();

                    string query = @"SELECT b.*, c.Category, i.Izd, t.Themes, f.Format
                                     FROM books_new b
                                     LEFT JOIN Spr_kategory c ON b.kategory_id = c.id
                                     LEFT JOIN Spr_izd i ON b.izd_id = i.id
                                     LEFT JOIN Spr_themes t ON b.themes_id = t.id
                                     LEFT JOIN Spr_format f ON b.format_id = f.id";

                    SqlCommand command = new SqlCommand(query, conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    await Task.Run(() => adapter.Fill(dataTable));

                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка під час завантаження даних: " + ex.Message);
            }
        }

        private async void load_btn_Click(object sender, EventArgs e)
        {
            await LoadBooksData();
        }
    }
}
