using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI.WebControls;

namespace web_api.Repositories.SQLServer
{
    public class Medicamento
    {
        private readonly SqlConnection conn;
        private readonly SqlCommand cmd;

        public Medicamento(string connectionString)
        {
            this.conn = new SqlConnection(connectionString);
            this.cmd = new SqlCommand(){Connection = this.conn};
        }


        public List<Models.Medicamento> Select()
        {
            List<Models.Medicamento> medicamentos = new List<Models.Medicamento>();


            using (this.conn)
            {
                this.conn.Open();

                using (this.cmd)
                {
                    this.cmd.CommandText = "select id, nome, datafabricacao, datavencimento from medicamento;";

                    using (SqlDataReader dr = this.cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            //Models.Medicamento medicamento = new Models.Medicamento();

                            //opcao 1

                            //medicamento.Id = (int)dr["id"];
                            //medicamento.Nome = dr["nome"].ToString();
                            //medicamento.Datafabricacao = (DateTime)dr["datafabricacao"];
                            //medicamento.Datavencimento = (DateTime)dr["datavencimento"];
                            //medicamentos.Add(medicamento);

                            //opcao 2

                            //Models.Medicamento medicamento = new Models.Medicamento((int)dr["id"], dr["nome"].ToString(),
                            //    (DateTime)dr["datafabricacao"], (DateTime)dr["datavencimento"]);
                            //medicamentos.Add(medicamento);



                            DateTime? dtvencimento;

                            if (dr.IsDBNull(dr.GetOrdinal("datavencimento")))
                            {
                                dtvencimento = null;
                            }
                            else
                            {
                               dtvencimento = (DateTime)dr["datavencimento"];
                            }


                            medicamentos.Add(new Models.Medicamento((int)dr["id"], dr["nome"].ToString(),
                                (DateTime)dr["datafabricacao"], dtvencimento));

                        }
                    }
                    
                }
            }
            
            return medicamentos;
        }

        public Models.Medicamento Select(int ID)
        {

            Models.Medicamento medicamento = null;


            using (this.conn)
            {
                this.conn.Open();

                using (this.cmd)
                {
                    this.cmd.CommandText = "select id, nome, datafabricacao, datavencimento from medicamento where id = @id;";
                    this.cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = ID;

                    using (SqlDataReader dr = this.cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            medicamento = new Models.Medicamento();

                            medicamento.Id = (int)dr["id"];
                            medicamento.Nome = dr["nome"].ToString();
                            medicamento.Datafabricacao = (DateTime)dr["datafabricacao"];

                            if (dr["datavencimento"] != DBNull.Value)
                                medicamento.Datavencimento = (DateTime)dr["datavencimento"];
                            else
                                medicamento.Datavencimento = null;
                        }
                    }
  
                }  
            }
            return medicamento;
        }

        public List<Models.Medicamento> Select(string nome)
        {
            List<Models.Medicamento> medicamentos = new List<Models.Medicamento>();

            using (this.conn)
            {
                this.conn.Open();

                using(this.cmd){

                    cmd.CommandText = "select id, nome, datafabricacao, datavencimento from medicamento where nome like @nome;";
                    cmd.Parameters.Add(new SqlParameter("@nome", System.Data.SqlDbType.VarChar)).Value = $"%{nome}%";

                    using (SqlDataReader dr = cmd.ExecuteReader()) {

                        while (dr.Read())
                        {
                            Models.Medicamento medicamento = new Models.Medicamento();

                            medicamento.Id =(int) dr["id"];
                            medicamento.Nome = dr["nome"].ToString();
                            medicamento.Datafabricacao = (DateTime)dr["datafabricacao"];


                            if (dr["datavencimento"] == DBNull.Value)
                                medicamento.Datavencimento = null;
                            else
                                medicamento.Datavencimento = (DateTime)dr["datavencimento"];


                            //if (dr.IsDBNull(dr.GetOrdinal("datavencimento")))
                            //    medicamento.Datavencimento = null;
                            //else
                            //    medicamento.Datavencimento = Convert.ToDateTime(dr["dataVencimento"]);

                            medicamentos.Add(medicamento);
                        }
                    }
                }
            }

             return medicamentos;
        }


        public bool Insert (Models.Medicamento medicamento)
        {

            using (this.conn)
            {
                this.conn.Open();

                using (this.cmd)
                {
                   
                    cmd.CommandText = "insert into medicamento (nome, datafabricacao, datavencimento) values (@nome, @datafabricacao, @datavencimento); select convert(int, @@IDENTITY);";
                    cmd.Parameters.Add(new SqlParameter("@nome", System.Data.SqlDbType.VarChar)).Value = medicamento.Nome;
                    cmd.Parameters.Add(new SqlParameter("@datafabricacao", System.Data.SqlDbType.Date)).Value = medicamento.Datafabricacao;

                    if (medicamento.Datavencimento == null)
                        cmd.Parameters.Add(new SqlParameter("@datavencimento", System.Data.SqlDbType.Date)).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add(new SqlParameter("@datavencimento", System.Data.SqlDbType.Date)).Value = medicamento.Datavencimento;

                    medicamento.Id = (int) cmd.ExecuteScalar();
                }
            }
            return medicamento.Id > 0;
        }


        public bool Update(Models.Medicamento medicamento)
        {

            int linhasAfetadas = 0;

            using (this.conn) {
                
                this.conn.Open();

                using (this.cmd)
                {
                    cmd.CommandText = "update medicamento set nome = @nome, datafabricacao = @datafabricacao, datavencimento = @datavencimento where id = @id;";
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = medicamento.Id;
                    cmd.Parameters.Add(new SqlParameter("@nome", System.Data.SqlDbType.VarChar)).Value = medicamento.Nome;
                    cmd.Parameters.Add(new SqlParameter("@datafabricacao", System.Data.SqlDbType.Date)).Value = medicamento.Datafabricacao;

                    if (medicamento.Datavencimento == null)
                        cmd.Parameters.Add(new SqlParameter("@datavencimento", System.Data.SqlDbType.Date)).Value = DBNull.Value;
                    else
                    cmd.Parameters.Add(new SqlParameter("@datavencimento", System.Data.SqlDbType.Date)).Value = medicamento.Datavencimento;

                    linhasAfetadas = cmd.ExecuteNonQuery();
                }
            }
          
            return linhasAfetadas == 1;
               
        }


        public bool Delete(int id)
        {

            int linhasAfetadas = 0;

            using (this.conn)
            {
                this.conn.Open();

                using (this.cmd){
                    cmd.CommandText = "delete medicamento where id = @id;";
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = id;

                    linhasAfetadas = cmd.ExecuteNonQuery();
                }
            }

           return linhasAfetadas == 1;
        }
        
    }
}