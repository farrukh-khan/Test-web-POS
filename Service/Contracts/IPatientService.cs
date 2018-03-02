using System;
using System.Collections.Generic;
using DataAccess.BLL;
using System.Collections;
using System.Data.SqlClient;

namespace Service.Contracts
{
    public interface IPatientService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Patient> GetPatients();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Patient> GetPatients(Func<Patient, bool> where);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Patient GetPatientById(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objPatient"></param>
        void InsertPatient(Patient objPatient);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objPatient"></param>
        void InsertBulkPatient(IEnumerable<Patient> objPatient);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objPatient"></param>
        void UpdatePatient(Patient objPatient);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objPatient"></param>
        void DeletePatient(Patient objPatient);




        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<Patient> ExcuteSpType(string query, SqlParameter[] Parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable ExcuteSp(string query, SqlParameter[] Parameters);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<Patient> GetBySqlEntity(string query);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable GetBySql(string query);
        

        
    }
}
