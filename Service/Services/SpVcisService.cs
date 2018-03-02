using System;
using System.Collections.Generic;
using Service.Contracts;
using System.Data.SqlClient;
using System.Collections;
using Service.Contracts;
using System.Data;
using DataAccess.RepositoryContracts;
using DataAccess.DAL;

namespace Service.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class SpService : ISpService
    {
        private readonly ISpRepository _spRepository;
        private readonly IUnitofWork _unitOfWork;

        public SpService(ISpRepository spRepository, IUnitofWork unitOfWork)
        {
            _spRepository = spRepository;
            _unitOfWork = unitOfWork;
        }

        #region SpVcisService Members
        public int ExcuteSp(string query, SqlParameter[] Parameters)
        {
           return _spRepository.ExcuteStoreProc(query, Parameters);
        }
        public List<dynamic> ExcuteSpAnonmious(string query, SqlParameter[] Parameters)
        {
            return _spRepository.ExcuteSpAnonmious<dynamic>(query, Parameters);        
        }

        public DataSet ExcuteSpAnonmious(string query, SqlParameter[] Parameters, int dataTableCount)
        {
            return _spRepository.ExcuteSpAnonmious(query, Parameters, dataTableCount);        
        }

                

        #endregion

    }
}
