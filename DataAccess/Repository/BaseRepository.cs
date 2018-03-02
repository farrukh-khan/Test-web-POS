using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using DataAccess.DAL;
using DataAccess.BLL;
using System.Collections;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Data.Common;
using System.Dynamic;


namespace DataAccess.Repository
{
    /// <summary>
    /// Abstract Entity Framework repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseRepository<T> : IRepository<T>
        where T : class
    {
        private Entities _context;
        private readonly DbSet<T> _dbset;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbFactory"></param>
        protected BaseRepository(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            _dbset = Context.Set<T>();
        }
        /// <summary>
        /// 
        /// </summary>
        protected IDbFactory DbFactory
        {
            get;
            private set;
        }


        /// <summary>
        /// 
        /// </summary>
        protected DbContext Context
        {
            get
            {
                return _context ?? (_context = DbFactory.Get());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add(T entity)
        {
            _dbset.Add(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            //_context.Entry(entity).CurrentValues.SetValues(EntityState.Modified);          
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {
            _dbset.Remove(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        public void Delete(Func<T, Boolean> where)
        {
            IEnumerable<T> objects = _dbset.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                _dbset.Remove(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetById(long id)
        {
            return _dbset.Find(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll()
        {

            return _dbset.ToList();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetMany(Func<T, bool> where)
        {
            return _dbset.Where(where);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public T Get(Func<T, Boolean> where)
        {
            return _dbset.Where(where).FirstOrDefault<T>();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<T> ExcuteSpType(string query, SqlParameter[] Parameters)
        {
            return _context.Database.SqlQuery<T>(query, Parameters);

        }
        public IEnumerable<T> ExcuteSpType<T>(string query, SqlParameter[] Parameters)
        {
            return _context.Database.SqlQuery<T>(query, Parameters);

        }


        public List<dynamic> ExcuteSpAnonmious<T>(string query, SqlParameter[] Parameters)
        {
            List<dynamic> returnList = new List<dynamic>();



            var dbCommand = _context.Database.Connection.CreateCommand();
            dbCommand.Connection.Open();

            dbCommand.CommandText = query;
            dbCommand.CommandType = System.Data.CommandType.StoredProcedure;


            var parameter1 = dbCommand.CreateParameter();
            parameter1.ParameterName = Parameters[0].ParameterName;
            parameter1.Value = Parameters[0].Value;
            dbCommand.Parameters.Add(parameter1);

            var parameter2 = dbCommand.CreateParameter();
            parameter2.ParameterName = Parameters[1].ParameterName;
            parameter2.Value = Parameters[1].Value;
            dbCommand.Parameters.Add(parameter2);

            var parameter3 = dbCommand.CreateParameter();
            parameter3.ParameterName = Parameters[2].ParameterName;
            parameter3.Value = Parameters[2].Value;
            dbCommand.Parameters.Add(parameter3);

            DbDataReader reader = dbCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                returnList.Add(SqlDataReaderToExpando(reader));
            }

            return returnList;



        }


        public DataSet ExcuteSpAnonmious(string query, SqlParameter[] Parameters, int dataTableCount)
        {
            DataSet returnList = new DataSet();



            var dbCommand = _context.Database.Connection.CreateCommand();
            dbCommand.Connection.Open();

            dbCommand.CommandText = query;
            dbCommand.CommandType = System.Data.CommandType.StoredProcedure;
            foreach (var param in Parameters)
            {
                var parameter1 = dbCommand.CreateParameter();
                parameter1.ParameterName = param.ParameterName;
                parameter1.Value = param.Value;
                dbCommand.Parameters.Add(parameter1);
            }


            LoadOption lOption = new LoadOption();
            List<DataTable> dt = new List<DataTable>();
            for (int i = 0; i < dataTableCount; i++)
            {
                DataTable dt1 = new DataTable();
                dt1.TableName = "table" + i;
                dt.Add(dt1);
                returnList.Tables.Add(dt1);
            }

            returnList.Load(dbCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection), lOption, dt.ToArray());



            return returnList;



        }


        private dynamic SqlDataReaderToExpando(DbDataReader reader)
        {
            var expandoObject = new ExpandoObject() as IDictionary<string, object>;

            for (var i = 0; i < reader.FieldCount; i++)
                expandoObject.Add(reader.GetName(i), reader[i]);

            return expandoObject;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable ExcuteSp(string query, SqlParameter[] Parameters)
        {
            return _context.Database.SqlQuery<string>(query, Parameters);

        }

        public int ExcuteStoreProc(string query, SqlParameter[] Parameters)
        {
            return _context.Database.ExecuteSqlCommand(query, Parameters);

        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<T> GetBySqlEntity(string query)
        {
            return _context.Database.SqlQuery<T>(query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable GetBySql(string query)
        {
            return _context.Database.SqlQuery<string>(query);
        }


        



       
    }
}
