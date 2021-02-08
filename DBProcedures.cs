using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace SilverWPF
{
    public class DBProcedures
    {
        private SqlCommand command = new SqlCommand("", DBConnection.connection);

        private void commandConfig(string config)
        {
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "[dbo].[" + config + "]";
            command.Parameters.Clear();
        }

        public void spDoljnost_Insert(string Naimenovanie, int Oklad)
        {
            commandConfig("Doljnost_Insert");

            command.Parameters.AddWithValue("@Naimenovanie", Naimenovanie);
            command.Parameters.AddWithValue("@Oklad", Oklad);

            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void spDoljnost_Update(Int32 ID_Doljnost, string Naimenovanie, int Oklad)
        {
            commandConfig("Doljnost_Update");

            command.Parameters.AddWithValue("@ID_Doljnost", ID_Doljnost);
            command.Parameters.AddWithValue("@Naimenovanie", Naimenovanie);
            command.Parameters.AddWithValue("@Oklad", Oklad);

            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void spDoljnost_Delete(Int32 ID_Doljnost)
        {
            commandConfig("Doljnost_Delete");

            command.Parameters.AddWithValue("@ID_Doljnost", ID_Doljnost);

            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }




        public void spNomer_Insert(int Nom, string Status, string Klass, Int32 ID_Otbor)
        {
            commandConfig("Nomer_Insert");

            command.Parameters.AddWithValue("@Nom", Nom);
            command.Parameters.AddWithValue("@Status", Status);
            command.Parameters.AddWithValue("@Klass", Klass);
            command.Parameters.AddWithValue("@ID_Otbor", ID_Otbor);

            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void spNomer_Update(Int32 ID_Nomer, int Nom, string Status, string Klass, Int32 ID_Otbor)
        {
            commandConfig("Nomer_Update");

            command.Parameters.AddWithValue("@ID_Nomer", ID_Nomer);
            command.Parameters.AddWithValue("@Nom", Nom);
            command.Parameters.AddWithValue("@Status", Status);
            command.Parameters.AddWithValue("@Klass", Klass);
            command.Parameters.AddWithValue("@ID_Otbor", ID_Otbor);

            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }


        public void spNomer_Delete(Int32 ID_Nomer)
        {
            commandConfig("Nomer_Delete");

            command.Parameters.AddWithValue("@ID_Nomer", ID_Nomer);

            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }


        public void spOtbor_Insert(string Familiya, string Imya, string Otchestvo, string Pasport, int Opit, string Login, string Password, Int32 ID_Doljnost, Int32 Grafik_ID)
        {
            commandConfig("Otbor_Insert");

            command.Parameters.AddWithValue("@Familiya", Familiya);
            command.Parameters.AddWithValue("@Imya", Imya);
            command.Parameters.AddWithValue("@Otchestvo", Otchestvo);
            command.Parameters.AddWithValue("@Pasport", Pasport);
            command.Parameters.AddWithValue("@Opit", Opit);
            command.Parameters.AddWithValue("@Login", Login);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@ID_Grafik", Grafik_ID);
            command.Parameters.AddWithValue("@ID_Doljnost", ID_Doljnost);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void spOtbor_Update(Int32 ID_Otbor, string Familiya, string Imya, string Otchestvo, string Pasport, int Opit, string Login, string Password, Int32 ID_Doljnost, Int32 Grafik_ID)
        {
            commandConfig("Otbor_Update");

            command.Parameters.AddWithValue("@ID_Otbor", ID_Otbor);
            command.Parameters.AddWithValue("@Familiya", Familiya);
            command.Parameters.AddWithValue("@Imya", Imya);
            command.Parameters.AddWithValue("@Otchestvo", Otchestvo);
            command.Parameters.AddWithValue("@Pasport", Pasport);
            command.Parameters.AddWithValue("@Opit", Opit);
            command.Parameters.AddWithValue("@Login", Login);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@ID_Grafik", Grafik_ID);
            command.Parameters.AddWithValue("@ID_Doljnost", ID_Doljnost);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void spOtbor_Delete(Int32 ID_Otbor)
        {
            commandConfig("Otbor_Delete");

            command.Parameters.AddWithValue("@ID_Otbor", ID_Otbor);

            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        public void spGrafik_Insert(string Nazvanie, string Nachalo, string Konec)
        {
            commandConfig("Grafik_Insert");

            command.Parameters.AddWithValue("@Nazvanie", Nazvanie);
            command.Parameters.AddWithValue("@Nachalo", Nachalo);
            command.Parameters.AddWithValue("@Konec", Konec);

            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();

        }

        public void spGrafik_Update(Int32 ID_Grafik, string Nazvanie, string Nachalo, string Konec)
        {
            commandConfig("Grafik_Update");

            command.Parameters.AddWithValue("@ID_Grafik", ID_Grafik);
            command.Parameters.AddWithValue("@Nazvanie", Nazvanie);
            command.Parameters.AddWithValue("@Nachalo", Nachalo);
            command.Parameters.AddWithValue("@Konec", Konec);

            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();

        }

        public void spGrafik_Delete(Int32 ID_Grafik)
        {
            commandConfig("Grafik_Delete");

            command.Parameters.AddWithValue("@ID_Grafik", ID_Grafik);

            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();

        }


        public Int32 Authorization(string Login, string Password)
        {
            Int32 ID_record = 0;
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "select [dbo].[Authorization]('"
                + Login + "','" + Password + "')";
            DBConnection.connection.Open();
            ID_record = Convert.ToInt32(command.ExecuteScalar().ToString());
            DBConnection.connection.Close();
            return (ID_record);


        }
    }


}