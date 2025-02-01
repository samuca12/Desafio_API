using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Repository
{
    public class CadastroRepository
    {
        public Task<CadastroModel> listar()
        {
            using (var conn = new SqlConnection)
        }
    }
}
