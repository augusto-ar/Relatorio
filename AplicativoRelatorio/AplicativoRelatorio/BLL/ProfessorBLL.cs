using DevExpress.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraReports.UI;
using AplicativoRelatorio.Model;
using System.IO;
using AplicativoRelatorio.Repositorio;
using AplicativoRelatorio.Relatorios;
using DevExpress.DataAccess.ObjectBinding;

namespace AplicativoRelatorio.BLL
{
    public class ProfessorBLL
    {
        public ProfessorBLL()
        {

        }

        public string ExportaRelatorio()
        {
            try
            {
                var report = MontaRelatorioDocente();
                string reportPath = @"C:\Users\QG\Desktop\Test.pdf";
                report.ExportToPdf(reportPath);

                return "Relatorio criado em  "+ reportPath;

            }
            catch(Exception e)
            {

            }
            
            return "";

        }

        public List<DocenteModel> ListaDicenteXdocente()
        {
            List<DocenteModel> lista = new List<DocenteModel>();
            try
            {

                SqlDataReader dr;

                dr = new ProfessorRepositorio().DadosRelatorioDicenteXdocente();
                dr.Read();

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
            catch (Exception e)
            {
                return null;
            }
        }

        public List<ProfessorModel> ListaProfessor()
        {
            List<ProfessorModel> lista = new List<ProfessorModel>();

            SqlDataReader dr;

            dr = new ProfessorRepositorio().ListaProfessor();
            dr.Read();

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


        public List<ProfessorModel> ListaGerencialNotasDirecao()
        {
            List<ProfessorModel> lista = new List<ProfessorModel>();
            try
            {

                SqlDataReader dr;

                dr = new ProfessorRepositorio().DadosRelatorioProfessor();
                dr.Read();

                while (dr.Read())
                {
                    lista.Add(new ProfessorModel
                    {
                        Nome = dr.GetString(0).Trim(),
                        Media = dr.GetDouble(1)                     
                    });
                }
                return lista;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        private XtraReport MontaRelatorioDocente()
        {
            var dados = ListaDicenteXdocente();
           
            var professor = dados.Select(x => new
            {
                x.IdProfessor,
                Nome = x.NomeProfessor,
                Disciplina = x.DescricaoDisciplina,
                 x.IdCurso,
            }).Distinct().ToList();

            var report = new DiscenteXDocentePorCurso();
            var subReport = new DiscenteXDocentePorCursoSubReport();
            subReport.DataSource = new ObjectDataSource { DataSource = dados};
            report.DataSource = new ObjectDataSource { DataSource = professor };
            ((XRSubreport)report.FindControl("xrSubreport1", true)).ReportSource = subReport;
            return report;
        }

        private XtraReport MontarRelatorioProfessor()
        {
            var listaprofessor = ListaProfessor();
            var report = new GerencialNotasDirecao();

            report.DataSource = new ObjectDataSource { DataSource = listaprofessor };

            return report;

        }

    }
}
