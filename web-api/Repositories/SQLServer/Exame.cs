using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace web_api.Repositories.SQLServer
{
    public class Exame
    {

        private readonly SqlConnection conn;
        private readonly SqlCommand cmd;


        public Exame(string connectionString) {

            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand() {
               Connection = conn
            };
           
        }

        public  List<Models.Exame> select()
        {

            List<Models.Exame> exames = new List<Models.Exame>();

            using (conn)
            {
                conn.Open();

                using (cmd)
                {
                    cmd.CommandText = "select id, nome, data from exame;";

                    SqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        Models.Exame exame = new Models.Exame();

                        exame.Id =(int) rd["id"];
                        exame.Nome = rd["nome"].ToString();
                        exame.Data = (DateTime)rd["data"];

                        exames.Add(exame);

                    }
                }
            }

            return exames;
        }

        public Models.Exame select_(int id)
        {

            Models.Exame exame = null;

            using (conn)
            {
                conn.Open();

                using(cmd)
                {
                    cmd.CommandText = "select id, nome, [data] from exame where id = @id;";
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = id;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            exame = new Models.Exame();

                            exame.Id = (int)dr["id"];
                            exame.Nome = dr["nome"].ToString();
                            exame.Data = (DateTime)dr["data"];

                        }
                    }

                }
            }

            return exame;
        }

    }
}