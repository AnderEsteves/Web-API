using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_api.Models
{
    public class Medicamento
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public DateTime Datafabricacao { get; set; }

        public DateTime? Datavencimento { get; set; }


        public Medicamento()
        {

        }

        public Medicamento(int id, string nome, DateTime datafabricacao, DateTime? datavencimento)
        {
            this.Id = id;
            this.Nome = nome;
            this.Datafabricacao = datafabricacao;
            this.Datavencimento = datavencimento;
        }

    }
}