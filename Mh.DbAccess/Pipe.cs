using System;
using System.Data;
using System.Data.SqlClient;

namespace Mh.DbAccess
{
    public class Pipe: IDisposable
    {
        #region < Vars >
        ParamCollection _SqlParams = new ParamCollection();

        SqlConnection _connection = null;
        SqlCommand _cmd = null;
        SqlDataAdapter _adp = null;

        string _sqlString = null;
        public string SqlString
        {
            get
            {
                return _sqlString;
            }
            set
            {
                _sqlString = value;
            }
        }

        public ParamCollection SqlParams
        {
            get
            {
                return _SqlParams;
            }
        }

        public SqlParameter GetParam(string paramName)
        {
            return _SqlParams[paramName];
        }
        public object GetCmdParamValue(string paramName)
        {
            return _cmd.Parameters[paramName].Value;
        }

        public SqlCommand cmd
        {
            get
            {
                return _cmd;
            }
        }

        public SqlDataAdapter adp
        {
            get
            {
                return _adp;
            }
        }

        public SqlConnection connection
        {
            get
            {
                return _connection;
            }
        }
        #endregion

        #region < Inits >
        public Pipe()
        {
            _connection = Connection.GetConn();
        }
        public Pipe(string sqlString)
        {
            _sqlString = sqlString;

            _connection = Connection.GetConn();
        }

        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (_connection != null)
                {
                    CloseConnecition();
                    _connection.Dispose();
                    _connection = null;
                }

                if (_cmd != null)
                {
                    _cmd.Dispose();
                    _cmd = null;
                }

                if (_adp != null)
                {
                    _adp.Dispose();
                    _adp = null;
                }

                if (_SqlParams != null)
                {
                    _SqlParams.Dispose();
                }

                //GC.Collect();
            }
            catch { }
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

        #region < Methods >
        public void AddParam(string name, object value)
        {
            _SqlParams.Add(new SqlParameter() { ParameterName = name, Value = value });
        }
        public void AddParam(string name, object value, ParameterDirection paramDirecitoion)
        {
            _SqlParams.Add(new SqlParameter() { ParameterName = name, Value = value, Direction = paramDirecitoion });
        }
        public void AddParam(SqlParameter param)
        {
            _SqlParams.Add(param);
        }

        public void ChangeParamValue(string paramName, object newValue)
        {
            _SqlParams.ChangeValue(paramName, newValue);
        }

        public void DeleteParam(string paramName)
        {
            _SqlParams.Delete(paramName);
        }
        public void ClearParam()
        {
            _SqlParams.Clear();
        }

        void AddParams(ref SqlCommand _cmd)
        {
            if (_SqlParams.Count > 0)
            {
                _cmd.Parameters.Clear();
                foreach (SqlParameter item in _SqlParams.Ver_SqlParams)
                {
                    _cmd.Parameters.Add(item.ParameterName, item.SqlDbType, item.Size);
                    _cmd.Parameters[item.ParameterName].Value = item.Value;
                    _cmd.Parameters[item.ParameterName].Direction = item.Direction;
                    _cmd.Parameters[item.ParameterName].Precision = item.Precision;
                    _cmd.Parameters[item.ParameterName].Scale = item.Scale;
                }
            }
        }
        void AddParams(ref SqlDataAdapter _adp)
        {
            if (_SqlParams.Count > 0 && _adp.SelectCommand != null)
            {
                _adp.SelectCommand.Parameters.Clear();
                foreach (SqlParameter item in _SqlParams.Ver_SqlParams)
                {
                    _adp.SelectCommand.Parameters.Add(item.ParameterName, item.SqlDbType, item.Size);
                    _adp.SelectCommand.Parameters[item.ParameterName].Value = item.Value;
                    _adp.SelectCommand.Parameters[item.ParameterName].Direction = item.Direction;
                }
            }
        }

        void OpenConnection()
        {
            if (_connection.State != ConnectionState.Open) _connection.Open();
        }
        void CloseConnecition()
        {
            if (_connection.State != ConnectionState.Closed) _connection.Close();
        }

        public int ExecuteNonQuery(bool useParam = true, CommandType commandType = CommandType.Text)
        {
            int Sonuc = -1;

            if (string.IsNullOrEmpty(SqlString))
                throw new Exception("No sql query found: ExecuteNonQuery");

            OpenConnection();

            using (_cmd = new SqlCommand(SqlString, _connection))
            {
                if (useParam)
                {
                    AddParams(ref _cmd);
                }

                _cmd.CommandType = commandType;
                Sonuc = _cmd.ExecuteNonQuery();
            }

            CloseConnecition();


            return Sonuc;
        }

        public object ExecuteScalar( bool useParam = true, CommandType commandType = CommandType.Text)
        {
            object Sonuc = null;

            if (string.IsNullOrEmpty(SqlString))
                throw new Exception("No sql query found: ExecuteScalar");

            OpenConnection();

            using (_cmd = new SqlCommand(SqlString, _connection))
            {
                if (useParam)
                {
                    AddParams(ref _cmd);
                }

                _cmd.CommandType = commandType;
                Sonuc = _cmd.ExecuteScalar();
            }

            CloseConnecition();

            return Sonuc;
        }

        public SqlDataReader ExecuteReader(bool useParam = true, CommandType commandType = CommandType.Text)
        {
            if (string.IsNullOrEmpty(SqlString))
                throw new Exception("No sql query found: ExecuteReader");

            SqlDataReader oku = null;

            OpenConnection();
            using (_cmd = new SqlCommand(SqlString, _connection))
            {
                if (useParam)
                {
                    AddParams(ref _cmd);
                }

                _cmd.CommandType = commandType;
                oku = _cmd.ExecuteReader();
            }

            //Kapat_Baglanti();

            return oku;
        }

        public DataSet GetDataSet(bool useParam = true, CommandType commandType = CommandType.Text)
        {
            if (string.IsNullOrEmpty(SqlString))
                throw new Exception("No sql query found: Ver_DataSet()");

            DataSet Sonuc = new DataSet();

            OpenConnection();
            using (_adp = new SqlDataAdapter(SqlString, _connection))
            {
                if (useParam)
                {
                    AddParams(ref _adp);
                }
                _adp.SelectCommand.CommandType = commandType;
                _adp.Fill(Sonuc);
            }

            CloseConnecition();

            return Sonuc;
        }

        public DataTable GetDataTable(bool useParam = true, CommandType commandType = CommandType.Text)
        {
            if (string.IsNullOrEmpty(SqlString))
                throw new Exception("No sql query found: Ver_DataTable()");

            DataTable Sonuc = new DataTable();

            OpenConnection();
            using (_adp = new SqlDataAdapter(SqlString, _connection))
            {
                if (useParam)
                {
                    AddParams(ref _adp);
                }

                _adp.SelectCommand.CommandType = commandType;
                _adp.Fill(Sonuc);
            }

            CloseConnecition();

            return Sonuc;
        }
        #endregion
    }
}