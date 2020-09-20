using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Reporting.WinForms;
using PrototipoRelatorio.BLL;
using PrototipoRelatorio.Type;
using System;
using System.IO;

namespace PrototipoRelatorio
{
    public class ExportarRelatorio
    {
        MontarRelatorio montar = new MontarRelatorio();
        object dataSourceSubReport = null;
        public string Relatorio(RelatorioType.TipoRelatorio tipo)
        {
            switch (tipo)
            {
                case RelatorioType.TipoRelatorio.DocentePorCurso:
                    return ExportaRelatorio(tipo);

                case RelatorioType.TipoRelatorio.ListaGerencialNotasDirecao:
                    return ExportaRelatorio(tipo);

                case RelatorioType.TipoRelatorio.GerencialNotasCursoDirecao:
                    return ExportaRelatorio(tipo);

                case RelatorioType.TipoRelatorio.GerencialNotaParaCoordenadores:
                    return "Relatorio ainda não foi desenvolvido";

                case RelatorioType.TipoRelatorio.GerencialComentários:
                    return "Relatorio ainda não foi desenvolvido";

                default:
                    return "";

            }
        }

        private string ExportaRelatorio(RelatorioType.TipoRelatorio tipo)
        {
            try
            {
                ReportViewer reportViewer = new ReportViewer();
                reportViewer.ProcessingMode = ProcessingMode.Local;
                reportViewer.LocalReport.ReportEmbeddedResource = NomeReportEmbeddedResource(tipo);

                if (tipo == RelatorioType.TipoRelatorio.DocentePorCurso)
                {
                    dataSourceSubReport =   new GerarDadosRelatorioBLL().ListaDicenteXdocenteSubReport();
                    reportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubRelatorioDocentePorCurso);
                }

                string path = @"c:\RelatorioDocentes";
                string path2 = path;

                if (Directory.Exists(path))
                    path += @"\" + tipo.ToString() + ".pdf";
                else
                {
                    Directory.CreateDirectory(path);
                    path += @"\" + tipo.ToString() + ".pdf";
                }
                    

                var dataSource = montar.DataSource(tipo);
                reportViewer.LocalReport.DataSources.Add(dataSource);
                var bytes = reportViewer.LocalReport.Render("pdf");
                System.IO.File.WriteAllBytes(path, bytes);
                return "relatorio criado em "+ path2;

            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        private void SubRelatorioDocentePorCurso(object sender, SubreportProcessingEventArgs e)
        {
            
            e.DataSources.Add(new ReportDataSource("DataSet1", dataSourceSubReport)); 

        }
        private string NomeReportEmbeddedResource(RelatorioType.TipoRelatorio tipo)
        {
            switch (tipo)
            {
                case RelatorioType.TipoRelatorio.DocentePorCurso:
                    return "PrototipoRelatorio.Relatorios.DocenteXdicentePorCurso.rdlc";

                case RelatorioType.TipoRelatorio.ListaGerencialNotasDirecao:
                    return "PrototipoRelatorio.Relatorios.GerencialNotasByDirecao.rdlc";

                case RelatorioType.TipoRelatorio.GerencialNotasCursoDirecao:
                    return "PrototipoRelatorio.Relatorios.GerencialNotasPorCursoDirecao.rdlc";

                case RelatorioType.TipoRelatorio.GerencialNotaParaCoordenadores:
                    return "";

                case RelatorioType.TipoRelatorio.GerencialComentários:
                    return "";

                default:
                    return "";

            }
        }
    }
}
