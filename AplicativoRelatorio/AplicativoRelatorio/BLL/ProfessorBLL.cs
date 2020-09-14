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
                var report = MontarRelatorioProfessor();
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

                dr = new DicenteXDocenteRepositorio().DadosRelatorioDicenteXdocente();
                dr.Read();

                while (dr.Read())
                {
                    lista.Add(new DocenteModel
                    {
                        NomeProfessor = dr.GetString(0).Trim(),
                        DescricaoQuestao = dr.GetString(1).Trim(),
                        MediaQuestao = dr.GetDouble(2),
                        MediaQuestaoDiciplina = dr.GetDouble(3),
                        MediaDocente = dr.GetDouble(4),
                        IdProfessor = dr.GetInt32(5)
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
            try
            {

                SqlDataReader dr;

                dr = new DicenteXDocenteRepositorio().DadosRelatorioProfessor();
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
        private XtraReport MontaRelatorio()
        {
            var dados = ListaDicenteXdocente();
           
            var professor = dados.Select(x => new ProfessorModel
            {
                IdProfessor = x.IdProfessor,
                Nome = x.NomeProfessor
            }).Distinct().ToList();

            var report = new DiscenteXDocentePorCurso();
            var subReport = new DiscenteXDocentePorCursoSubReport();
            subReport.DataSource = new ObjectDataSource { DataSource = dados};
            report.DataSource = new ObjectDataSource { DataSource = professor };
           
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
