﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicativoRelatorio.Repositorio
{
   public class ProfessorRepositorio
    {
        Conexao.Conexao con = new Conexao.Conexao();
        SqlCommand cmd = new SqlCommand();

        public SqlDataReader DadosRelatorioDicenteXdocente()
        {
            ///MediaGeral = media docente;
            ///MediaQuestao = media geral
            cmd.CommandText = @"SELECT p.nome as NomeProfessor,d.nome as Disciplina, q.texto as DescricaoQuestao, 
							                mqdp.media as MediaProfessroDisciplinaQuestao,mdqc.media as MediaCurso, q.media as MediaGeral, 
                                            p.id as IdProfessor, mdqc.id_curso as idCurso
                                                FROM questao as q 
	                                            Inner join media_questao_disc_prof as mqdp on mqdp.id_questao = q.id
	                                            Inner join professor as p on p.id = mqdp.id_professor
	                                            Inner join disciplina as d on d.codigo = mqdp.cod_disc
								                Inner join media_questao_curso as mdqc on mdqc.id_questao = q.id";

            /// estabelece a conexão com o banco
            cmd.Connection = con.Conectar();

            cmd.CommandType = CommandType.Text;

            return cmd.ExecuteReader();
        }
        public SqlDataReader DadosRelatorioProfessor()
        {
            cmd.CommandText = @"SELECT p.nome as NomeProfessor, AVG(m.media)as Media FROM professor as p
                                        INNER JOIN media_questao_disc_prof as m on m.id_professor = p.id
                                        GROUP BY p.nome
                                        ORDER BY Media DESC";

            cmd.Connection = con.Conectar();

            cmd.CommandType = CommandType.Text;

            return cmd.ExecuteReader();

        }

        public SqlDataReader ListaProfessor()
        {
            cmd.CommandText = @"SELECT * FROM professor";

            cmd.Connection = con.Conectar();

            cmd.CommandType = CommandType.Text;

            return cmd.ExecuteReader();

        }
    }
}