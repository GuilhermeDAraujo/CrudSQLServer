using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using System.IO;

namespace CrudSQLServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            string baseDados = Application.StartupPath + @"\db\DBSQLServer.sfd";
            string strConexao = @"DataSource = " + baseDados + "; Password = '123456'";

            SqlCeEngine db = new SqlCeEngine(strConexao);

            if (!File.Exists(baseDados))
            {
                db.CreateDatabase();
            }

            db.Dispose();

            SqlCeConnection conexao = new SqlCeConnection(strConexao);

            try
            {
                conexao.Open();
                lbResultado.Text = "Conectado com Banco de dados";
            }
            catch (Exception)
            {
                lbResultado.Text = "Erro ao conectar Banco de dados";
            }
            finally
            {
                conexao.Close();
            }
        }

        private void btnCriarTabela_Click(object sender, EventArgs e)
        {
            string baseDados = Application.StartupPath + @"\db\DBSQLServer.sfd";
            string strConexao = @"DataSource = " + baseDados + "; Password = '123456'";

            SqlCeConnection conexao = new SqlCeConnection (strConexao);

            try
            {
                conexao.Open();

                SqlCeCommand comando = new SqlCeCommand();
                comando.Connection = conexao;

                comando.CommandText = "create table pessoas (id int not null primary key, nome nvarchar(50), email nvarchar(50))";
                comando.ExecuteNonQuery();

                lbResultado.Text = "Tabela pessoas criada";
                comando.Dispose();
            }
            catch (Exception obj)
            {
                lbResultado.Text = obj.Message; 
            }
            finally
            {
                conexao.Close();
            }
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            string baseDados = Application.StartupPath + @"\db\DBSQLServer.sfd";
            string strConexao = @"DataSource = " + baseDados + "; Password = '123456'";

            SqlCeConnection conexao = new SqlCeConnection(strConexao);

            try
            {
                conexao.Open();

                SqlCeCommand comando = new SqlCeCommand();
                comando.Connection = conexao;

                int id = new Random(DateTime.Now.Millisecond).Next(0, 1000);
                string nome = lbNome.Text;
                string email = lbEmail.Text;

                comando.CommandText = "insert into pessoas values (" + id + ", '" + nome + "','" + email + "')";
                comando.ExecuteNonQuery();

                lbResultado.Text = "Dados inseridos com sucesso";
                comando.Dispose();

                txtNome.Text = "";
                txtEmail.Text = "";
            }
            catch (Exception obj)
            {
                lbResultado.Text = obj.Message;
            }
            finally
            {
                conexao.Close();
            }
        }
    }
}
