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
                string nome = txtNome.Text;
                string email = txtEmail.Text;

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

        private void btnProcurar_Click(object sender, EventArgs e)
        {
            dtLista.Rows.Clear();

            string baseDados = Application.StartupPath + @"\db\DBSQLServer.sfd";
            string strConexao = @"DataSource = " + baseDados + "; Password = '123456'";

            SqlCeConnection conexao = new SqlCeConnection(strConexao);

            try
            {
                string query = "select * from pessoas";

                if(txtNome.Text != "")
                {
                    query = "select * from pessoas where nome like '" + txtNome.Text + "'";
                }

                DataTable dados = new DataTable();

                SqlCeDataAdapter adaptador = new SqlCeDataAdapter(query, strConexao);

                conexao.Open();

                adaptador.Fill(dados);   
                
                foreach (DataRow linha in dados.Rows)
                {
                    dtLista.Rows.Add(linha.ItemArray);
                }
            }
            catch (Exception obj)
            {
                dtLista.Rows.Clear();
                lbResultado.Text = obj.Message;
            }
            finally
            {
                conexao.Close();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            string baseDados = Application.StartupPath + @"\db\DBSQLServer.sfd";
            string strConexao = @"DataSource = " + baseDados + "; Password = '123456'";

            SqlCeConnection conexao = new SqlCeConnection(strConexao);

            try
            {
                conexao.Open();

                SqlCeCommand comando = new SqlCeCommand();
                comando.Connection = conexao;

                int id = (int)dtLista.SelectedRows[0].Cells[0].Value;
                comando.CommandText = "delete from pessoas where id = '" + id + "' ";
                comando.ExecuteNonQuery();

                lbResultado.Text = "Dados excluídos com sucesso";
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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string baseDados = Application.StartupPath + @"\db\DBSQLServer.sfd";
            string strConexao = @"DataSource = " + baseDados + "; Password = '123456'";

            SqlCeConnection conexao = new SqlCeConnection(strConexao);

            try
            {
                conexao.Open();

                SqlCeCommand comando = new SqlCeCommand();
                comando.Connection = conexao;

                int id = (int)dtLista.SelectedRows[0].Cells[0].Value;

                comando.CommandText = "update pessoas set nome = '" + txtNome.Text + "', email = '" + txtEmail.Text + "' where id like '" + id + "'";
                comando.ExecuteNonQuery();

                lbResultado.Text = "Dados editados com sucesso";
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
