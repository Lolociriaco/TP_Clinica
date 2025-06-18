using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Medico
    {
        private int _legajo;
        private int _dni;
        private string _nombre;
        private string _apellido;
        private string _sexo;
        private string _nacionalidad;
        private DateTime _fechaNacimiento;
        private string _direccion;
        private string _localidad;
        private string _provincia;
        private string _correoElectronico;
        private string _telefono;
        private string _especialidad;
        private string _diasYHorariosAtencion;
        private string _usuario;
        private string _contrasena;

        public Medico() { }

        public Medico(int legajo, int dni, string nombre, string apellido, string sexo,
                      string nacionalidad, DateTime fechaNacimiento, string direccion,
                      string localidad, string provincia, string correoElectronico, string telefono,
                      string especialidad, string diasYHorariosAtencion, string usuario, string contrasena)
        {
            _legajo = legajo;
            _dni = dni;
            _nombre = nombre;
            _apellido = apellido;
            _sexo = sexo;
            _nacionalidad = nacionalidad;
            _fechaNacimiento = fechaNacimiento;
            _direccion = direccion;
            _localidad = localidad;
            _provincia = provincia;
            _correoElectronico = correoElectronico;
            _telefono = telefono;
            _especialidad = especialidad;
            _diasYHorariosAtencion = diasYHorariosAtencion;
            _usuario = usuario;
            _contrasena = contrasena;
        }

        public int Legajo
        {
            get { return _legajo; }
            set { _legajo = value; }
        }

        public int DNI
        {
            get { return _dni; }
            set { _dni = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }

        public string Sexo
        {
            get { return _sexo; }
            set { _sexo = value; }
        }

        public string Nacionalidad
        {
            get { return _nacionalidad; }
            set { _nacionalidad = value; }
        }

        public DateTime FechaNacimiento
        {
            get { return _fechaNacimiento; }
            set { _fechaNacimiento = value; }
        }

        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        public string Localidad
        {
            get { return _localidad; }
            set { _localidad = value; }
        }

        public string Provincia
        {
            get { return _provincia; }
            set { _provincia = value; }
        }

        public string CorreoElectronico
        {
            get { return _correoElectronico; }
            set { _correoElectronico = value; }
        }

        public string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }

        public string Especialidad
        {
            get { return _especialidad; }
            set { _especialidad = value; }
        }

        public string DiasYHorariosAtencion
        {
            get { return _diasYHorariosAtencion; }
            set { _diasYHorariosAtencion = value; }
        }

        public string Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public string Contrasena
        {
            get { return _contrasena; }
            set { _contrasena = value; }
        }
    }

}
