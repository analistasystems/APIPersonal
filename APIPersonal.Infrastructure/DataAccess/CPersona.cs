using APIPersonal.Domain.Persona;

using APIPersonal.Infrastructure.Connection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System.Linq;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace APIPersonal.Infrastructure.DataAccess
{
    public class CPersona : IPersona
    {
        private APIPERSONALContext _apipersonalcontext;

        public CPersona (APIPERSONALContext apipersonalcontext)
        {
            _apipersonalcontext = apipersonalcontext; 
        }
        public bool ValidaPersona(string nombre)
        {
            bool existe = true;
            
            using (var command = _apipersonalcontext.Database.GetDbConnection().CreateCommand())
            {
                try
                {
                    _apipersonalcontext.Database.OpenConnection();

                    command.CommandText = "SP_EJEMPLO_API_VALIDA_PERSONA";
                    command.CommandType = CommandType.StoredProcedure;

                    //var p = new SqlParameter("Nombre", SqlDbType.VarChar);
                    //p.Value = nombre ?? (object)DBNull.Value;
                    //command.Parameters.Add(p);

                    command.Parameters.Add(new SqlParameter("@nombre", nombre));

                    var Result = command.ExecuteReader();

                    if (Result.Read())
                    {
                        existe = Convert.ToBoolean(Result["existe"].ToString());
                    }

                    _apipersonalcontext.Database.CloseConnection();

                }
                catch (Exception ex)
                {
                    existe = true;
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    _apipersonalcontext.Database.CloseConnection();
                }
            }
            return existe;
        }
        public Persona GetPersona(string nombre)
        {
            var persona = new Persona();

            using (var command = _apipersonalcontext.Database.GetDbConnection().CreateCommand())
            {
                try
                {
                    _apipersonalcontext.Database.OpenConnection();

                    command.CommandText = "SP_EJEMPLO_API_CONSULTA_PERSONA";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@nombre", nombre));

                    var Result = command.ExecuteReader();

                    if (Result.Read())
                    {
                        if (!Result.IsDBNull("id")) { persona.Id = Convert.ToString(Result["id"]); } else { persona.Id = Convert.ToString(""); };
                        if (!Result.IsDBNull("nombre")) { persona.Nombre = Convert.ToString(Result["nombre"]); } else { persona.Nombre = Convert.ToString(""); };
                        if (!Result.IsDBNull("edad")) { persona.Edad = Convert.ToString(Result["edad"]); } else { persona.Edad = Convert.ToString(""); };
                    }

                    _apipersonalcontext.Database.CloseConnection();
                }
                catch (Exception ex)
                {
                    persona = null;
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    _apipersonalcontext.Database.CloseConnection();
                }  
            }
            return persona;
        }

       public bool InsertarPersona(Persona persona)
       {
            bool state = false;

            return state;
       }

    }
}
