﻿using System;
using System.Collections.Generic;
using FlexCoreDTOs.clients;
using System.Data.SqlClient;

namespace FlexCoreDAOs.clients
{
    public class PersonPhoneDAO:GeneralDAO<PersonPhoneDTO>
    {

        public static readonly string PHONE = "telefono";
        public static readonly string PERSON_ID = "idPersona";

        private static object _syncLock = new object();
        private static PersonPhoneDAO _instance;

        public static PersonPhoneDAO getInstance()
        {
            if (_instance == null)
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new PersonPhoneDAO();
                    }
                }
            }
            return _instance;
        }

        private PersonPhoneDAO() { }

        public override void insert(PersonPhoneDTO pPhone, SqlCommand pCommand)
        {
            pCommand.Parameters.Clear();
            string tableName = "TELEFONO_PERSONA";
            string columns = String.Format("{0}, {1}", PERSON_ID, PHONE);
            string values = String.Format("@{0}, @{1}", PERSON_ID, PHONE);
            string query = getInsertQuery(tableName, columns, values);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@" + PERSON_ID, pPhone.getPersonID());
            pCommand.Parameters.AddWithValue("@" + PHONE, pPhone.getPhone());
            pCommand.ExecuteNonQuery();
        }
        public override void delete(PersonPhoneDTO pPhone, SqlCommand pCommand)
        {
            pCommand.Parameters.Clear();
            string tableName = "TELEFONO_PERSONA";
            string condition = String.Format("{0} = @{0} AND {1}=@{1}", PERSON_ID, PHONE);
            string query = getDeleteQuery(tableName, condition);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@" + PERSON_ID, pPhone.getPersonID());
            pCommand.Parameters.AddWithValue("@" + PHONE, pPhone.getPhone());
            pCommand.ExecuteNonQuery();
        }

        public override void update(PersonPhoneDTO pNewPhone, PersonPhoneDTO pPastPhone, SqlCommand pCommand)
        {
            pCommand.Parameters.Clear();
            string tableName = "TELEFONO_PERSONA";
            string values = String.Format("{0}=@nuevo{0}, {1}=@nuevo{1}", PERSON_ID, PHONE);
            string condition = String.Format("{0} = @{0}Anterior AND {1} = @{1}Anterior", PERSON_ID, PHONE);
            string query = getUpdateQuery(tableName, values, condition);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@nuevo" + PERSON_ID, pNewPhone.getPersonID());
            pCommand.Parameters.AddWithValue("@nuevo" + PHONE, pNewPhone.getPhone());
            pCommand.Parameters.AddWithValue("@" + PERSON_ID + "Anterior", pPastPhone.getPersonID());
            pCommand.Parameters.AddWithValue("@" + PHONE + "Anterior", pPastPhone.getPhone());
            pCommand.ExecuteNonQuery();
        }

        public override List<PersonPhoneDTO> search(PersonPhoneDTO pPhone, SqlCommand pCommand, int pPageNumber = 0, int pShowCount = 0, params string[] pOrderBy)
        {
            pCommand.Parameters.Clear();
            string selection = "*";
            string from = "TELEFONO_PERSONA";
            string condition = String.Format("{0} = @{0}", PERSON_ID);
            string query = getSelectQuery(selection, from, condition, pPageNumber, pShowCount, pOrderBy);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@" + PERSON_ID, pPhone.getPersonID());

            SqlDataReader reader = pCommand.ExecuteReader();
            List<PersonPhoneDTO> list = new List<PersonPhoneDTO>();

            while (reader.Read())
            {
                PersonPhoneDTO phone = new PersonPhoneDTO(pPhone.getPersonID());
                phone.setPhone(reader[PHONE].ToString());
                list.Add(phone);
            }
            reader.Close();
            return list;
        }
    }
}
