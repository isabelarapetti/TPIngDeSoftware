using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfConsultorio.Model
{
    class Turno //: INotifyPropertyChanged
    {
        public int IdTurno { get; set; }
        public string Fecha { get; set; }
        //public int IdPaciente { get; set; }
        public string nombreCompleto { get; set; }
        //public Paciente Paciente { get; set; }
        public string Observaciones { get; set; }
        public string Estado { get; set; }
        public string Hora { get; set; }
    }
}
