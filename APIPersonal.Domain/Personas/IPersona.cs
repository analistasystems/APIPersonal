using System;
using System.Collections.Generic;
using System.Text;

namespace APIPersonal.Domain.Persona
{
    public interface IPersona
    {
        bool ValidaPersona(string nombre);
        Persona GetPersona( string nombre);
        bool InsertarPersona(Persona persona);
    }
}
