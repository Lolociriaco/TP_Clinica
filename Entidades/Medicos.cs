using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Medico
    {
        public int _id_usuario;
        public int _dni;
        public string _nombre;
        public string _apellido;
        public string _sexo;
        public string _nacionalidad;
        public DateTime _fechaNacimiento;
        public string _direccion;
        public string _localidad;
        public string _provincia;
        public string _correoElectronico;
        public string _telefono;
        public string _especialidad;
        public string _usuario;
        public string _contrasena;
        public string _repecontrasena;

        // CONSTRUCTOR
        public Medico() { }

        public Medico(int id_usuario, int dni, string nombre, string apellido, string sexo,
                      string nacionalidad, DateTime fechaNacimiento, string direccion,
                      string localidad, string provincia, string correoElectronico, string telefono,
                      string especialidad, string usuario, string contrasena, string repecontrasena)
        {
            _id_usuario = id_usuario;
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
            _usuario = usuario;
            _contrasena = contrasena;
            _repecontrasena = repecontrasena;
        }

        // GETTERS Y SETTERS
        public int Legajo
        {
            get { return _id_usuario; }
            set { _id_usuario = value; }
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
        
        public string RepeContrasena
        {
            get { return _repecontrasena; }
            set { _repecontrasena = value; }
        }
    }

}
