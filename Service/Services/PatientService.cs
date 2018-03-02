using System;
using System.Collections.Generic;
using DataAccess.BLL;
using DataAccess.DAL;
using DataAccess.RepositoryContracts;
using Service.Contracts;
using System.Data.SqlClient;
using System.Collections;


namespace Service.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _PatientRepository;
        private readonly IUnitofWork _unitOfWork;

        public PatientService(IPatientRepository appsRepository, IUnitofWork unitOfWork)
        {
            _PatientRepository = appsRepository;
            _unitOfWork = unitOfWork;
        }
        #region IPatienttService Members

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Patient> GetPatients()
        {
            return _PatientRepository.GetAll();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Patient> GetPatients(Func<Patient, bool> where)
        {
            if (@where == null) throw new ArgumentException("where");
            return _PatientRepository.GetMany(where);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Patient GetPatientById(long id)
        {
            return _PatientRepository.GetById(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objPatient"></param>
        public void InsertPatient(Patient objPatient)
        {
            _PatientRepository.Add(objPatient);
            _unitOfWork.Commit();
        }


        public void InsertBulkPatient(IEnumerable<Patient> objPatient)
        {
            foreach (var item in objPatient)
            {
                _PatientRepository.Add(item);
            }
            _unitOfWork.Commit();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="objPatient"></param>
        public void UpdatePatient(Patient objPatient)
        {
            _PatientRepository.Update(objPatient);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objPatient"></param>
        public void DeletePatient(Patient objPatient)
        {
            _PatientRepository.Delete(objPatient);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<Patient> ExcuteSpType(string query, SqlParameter[] Parameters)
        {
            return _PatientRepository.ExcuteSpType(query, Parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable ExcuteSp(string query, SqlParameter[] Parameters)
        {
            return _PatientRepository.ExcuteSp(query, Parameters);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<Patient> GetBySqlEntity(string query)
        {
            return _PatientRepository.GetBySqlEntity(query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable GetBySql(string query)
        {
            return _PatientRepository.GetBySql(query);
        }

        #endregion

    }
}
