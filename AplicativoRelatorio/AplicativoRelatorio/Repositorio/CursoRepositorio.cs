using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace AplicativoRelatorio.Repositorio
{
    public class CursoRepositorio
    {
        Conexao.Conexao con = new Conexao.Conexao();
        SqlCommand cmd = new SqlCommand();
    }
}
