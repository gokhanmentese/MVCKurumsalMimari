using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Core.Exceptions;
using System.Reflection;
using Core.Attributes;
using Core.Extensions;


namespace Core.DataAccess.AdoNet
{
    public class AnProductRepositoryBase<TEntity> : IEntityRepository<TEntity>
      where TEntity : class, IEntity, new()

    {
        private readonly string _connectionString;
        public AnProductRepositoryBase(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<K> ExecuteReaderToListGeneric<K>(SqlCommand cmd)
        {
            List<K> ret = new List<K>();
            using (SqlConnection cnn = new SqlConnection(_connectionString))
            {
                SqlDataReader dr = null;
                try
                {
                    cmd.Connection = cnn;
                    cnn.Open();
                    cmd.CommandTimeout = 0;

                    dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                    while (dr.Read())
                    {
                        ret.Add(genericFiller<K>(dr));
                    }
                }
                catch (Exception ex)
                {
                    throw new SqlServerException(ex.Message);
                }
                finally
                {
                    if (dr != null)
                    {
                        if (!dr.IsClosed)
                            dr.Close();
                        dr = null;
                        if (cmd.Connection.State == System.Data.ConnectionState.Open)
                            cmd.Connection.Close();
                    }
                }
            }
            return ret;
        }

        private T genericFiller<T>(SqlDataReader dr)
        {
            Type a = typeof(T);

            
            //if (a.BaseType.Name != typeof(TEntity).Name)
            //    throw new Exception("Base Entity classından inherit değil.");

            object o = Activator.CreateInstance(a);
            foreach (var prop in a.GetProperties())
            {
                if (prop.SetMethod != null)
                {
                    EntityAttributes ea =  (EntityAttributes)prop.GetCustomAttribute(typeof(EntityAttributes));
                    if (ea != null)
                        prop.SetValue(o, dr.fillerSetter(ea.PropertyName, prop.PropertyType));
                }
            }
            return (T)o;
        }

        protected object ExecuteScalar(SqlCommand cmd)
        {
            cmd.Connection = new SqlConnection(_connectionString);
            try
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Closed)
                    cmd.Connection.Open(); cmd.CommandTimeout = 0;
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new SqlServerException(ex.Message);
            }
            finally
            {
                if (cmd != null)
                {
                    if (cmd.Connection != null)
                        if (cmd.Connection.State == System.Data.ConnectionState.Open) { cmd.Connection.Close(); cmd.Connection.Dispose(); cmd.Connection = null; }
                    cmd.Dispose();
                }
                cmd = null;
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            #region Sorgu
            string cmdstr = "select * from Account";
            #endregion

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = cmdstr;
            cmd.CommandTimeout = 0;


            return ExecuteReaderToListGeneric<TEntity>(cmd);
        }

        public TEntity Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }

}
