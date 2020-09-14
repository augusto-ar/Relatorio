using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicativoRelatorio.Type
{
   public class RelatorioType
    {
        public enum TipoRelatorio
        {
            [Description("Discente X Docente por Curso")]
            DocentePorCurso = 1,
            [Description("Docente - Lista gerencial notas para direção")]
            ListaGerencialNotasDirecao = 2,
            [Description("Docente - Gerencial Notas por Curso para direção")]
            GerencialNotasDirecao = 3,
            [Description("Docente - Lista gerencial Notas por Curso para coordenadores")]
            GerencialNotaParaCoordenadores = 4,
            [Description("Docente - Gerencial comentários para direção")]
            GerencialComentários = 5
        }        
     
    }
}
