using System;
using System.Collections.Generic;
using DataAccess.BLL;
using System.Collections;
using System.Data.SqlClient;

namespace Service.Contracts
{
    public interface ISystemSettingService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<SystemSetting> GetSystemSettings();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<SystemSetting> GetSystemSettings(Func<SystemSetting, bool> where);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SystemSetting GetSystemSettingById(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objSystemSetting"></param>
        void InsertSystemSetting(SystemSetting objSystemSetting);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objSystemSetting"></param>
        void InsertBulkSystemSetting(IEnumerable<SystemSetting> objSystemSetting);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objSystemSetting"></param>
        void UpdateSystemSetting(SystemSetting objSystemSetting);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objSystemSetting"></param>
        void DeleteSystemSetting(SystemSetting objSystemSetting);




        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<SystemSetting> ExcuteSpType(string query, SqlParameter[] Parameters);

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
        IEnumerable<SystemSetting> GetBySqlEntity(string query);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable GetBySql(string query);
        

        
    }
}
