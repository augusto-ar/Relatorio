using AplicativoRelatorio.Model;
using AplicativoRelatorio.Repositorio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicativoRelatorio.BLL
{
   public class GerarDadosRelatorioBLL
    {
        Consultas consultas = new Consultas();
        SqlDataReader dr;
        public GerarDadosRelatorioBLL()
        {

        }
        /// <summary>
        /// Lista para montar o relatorio Discente X Docente por CURSO
        /// </summary>
        /// <returns></returns>
        public List<DocenteModel> ListaDicenteXdocente()
        {
            List<DocenteModel> lista = new List<DocenteModel>();
            string query = @"SELECT p.nome as NomeProfessor,d.nome as Disciplina, q.texto as DescricaoQuestao, 
							                mqdp.media as MediaProfessroDisciplinaQuestao,mdqc.media as MediaCurso, q.media as MediaGeral, 
                                            p.id as IdProfessor, mdqc.id_curso as idCurso
                                                FROM questao as q 
	                                            Inner join media_questao_disc_prof as mqdp on mqdp.id_questao = q.id
	                                            Inner join professor as p on p.id = mqdp.id_professor
	                                            Inner join disciplina as d on d.codigo = mqdp.cod_disc
								                Inner join media_questao_curso as mdqc on mdqc.id_questao = q.id";

            try
            {
                dr = consultas.DadosdoRelatorio(query);
                while (dr.Read())
                {
                    lista.Add(new DocenteModel
                    {
                        NomeProfessor = dr.GetString(0).Trim(),
                        DescricaoDisciplina = dr.GetString(1).Trim(),
                        DescricaoQuestao = dr.GetString(2).Trim(),
                        MediaDocente = dr.GetDouble(3),
                        MediaQuestaoDiciplina = dr.GetDouble(4),
                        MediaQuestao = dr.GetDouble(5),
                        IdProfessor = dr.GetInt32(6),
                        IdCurso = dr.GetInt32(7)
                    });
                }
                return lista;
            }
            catch(Exception e)
            {
                return null;
            }
            
        }

        /// <summary>
        /// Lista para montar o relatorio Lista GERENCIAL NOTAS para DIREÇÃO
        /// </summary>
        /// <returns></returns>
        public List<ProfessorModel> ListaGerencialNotasDirecao()
        {
            string query = @"SELECT p.nome as NomeProfessor, AVG(m.media)as Media FROM professor as p
                                        INNER JOIN media_questao_disc_prof as m on m.id_professor = p.id
                                        GROUP BY p.nome
                                        ORDER BY Media DESC";

            List<ProfessorModel> lista = new List<ProfessorModel>();
            try
            {
                dr = consultas.DadosdoRelatorio(query);

                while (dr.Read())
                {
                    lista.Add(new ProfessorModel
                    {
                        IdProfessor = dr.GetInt32(0),
                        Nome = dr.GetString(1).Trim(),

                    });
                }
                return lista;
            }
            catch(Exception e)
            {
                return null;
            }
        }
        
       
    }
}
