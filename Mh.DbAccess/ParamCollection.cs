using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Mh.DbAccess
{
    public class ParamCollection : IDisposable
    {
        #region < Vars >
        List<SqlParameter> _SqlParams = null;

        public SqlParameter this[string ParamAdi]
        {
            get
            {
                foreach (SqlParameter item in _SqlParams)
                {
                    if (item.ParameterName == ParamAdi)
                    {
                        return item;
                    }
                }

                return null;
            }
        }

        public SqlParameter this[int ParamIndex]
        {
            get
            {
                return _SqlParams[ParamIndex];
            }
        }

        public List<SqlParameter> Ver_SqlParams
        {
            get
            {
                return _SqlParams;
            }
        }

        public int Count
        {
            get
            {
                return _SqlParams.Count;
            }
        }
        #endregion

        #region < Inits >
        public ParamCollection()
        {
            _SqlParams = new List<SqlParameter>();
        }

        protected virtual void Dispose(bool disposing)
        {
            _SqlParams.Clear();
            _SqlParams = null;

            //GC.Collect();
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

        #region < Methods >
        public void Add(string name, object value)
        {
            _SqlParams.Add(new SqlParameter() { ParameterName = name, Value = value });
        }
        public void Add(string name, object value, ParameterDirection paramDirecitoion)
        {
            _SqlParams.Add(new SqlParameter() { ParameterName = name, Value = value, Direction = paramDirecitoion });
        }
        public void Add(SqlParameter param)
        {
            _SqlParams.Add(param);
        }
        //public void Add(string ParamAdi, object ParamDegeri, SqlDbType ParamDbType,
        //   ParameterDirection ParamYonu = ParameterDirection.Input, int ParamSize = 50,
        //   byte TamKisim = 0, byte OndalikKisim = 0)
        //{
        //    SqlParameter SqlParam = new SqlParameter(ParamAdi, ParamDegeri);
        //    SqlParam.Direction = ParamYonu;
        //    SqlParam.Size = ParamSize;
        //    SqlParam.SqlDbType = ParamDbType;
        //    SqlParam.Precision = TamKisim;
        //    SqlParam.Scale = OndalikKisim;

        //    _SqlParams.Add(SqlParam);
        //}
        //public void Add(string ParamAdi, SqlDbType ParamDbType, ParameterDirection ParamYonu = ParameterDirection.Input, int ParamSize = 50,
        //     byte TamKisim = 0, byte OndalikKisim = 0)
        //{
        //    Add(ParamAdi, null, ParamDbType, ParamYonu, ParamSize, TamKisim, OndalikKisim);
        //}

        public void ChangeValue(string paramName, object newValue)
        {
            bool Bulundu = false;
            foreach (SqlParameter item in _SqlParams)
            {
                if (item.ParameterName == paramName)
                {
                    item.Value = newValue;
                    Bulundu = true;
                    break;
                }
            }

            if (Bulundu == false)
            {
                throw new Exception("Sql Paramertre değeri değiştirilken bir hata oluştur. Parametre bulunamadı.Yer: Degistir_Param()");
            }
        }

        public void Delete(string paramName)
        {
            int silinecek_index = -1;
            for (int i = 0; i < _SqlParams.Count; i++)
            {
                if (_SqlParams[i].ParameterName == paramName)
                {
                    silinecek_index = i;
                    break;
                }
            }

            _SqlParams.RemoveAt(silinecek_index);
        }
        public void Clear()
        {
            _SqlParams.Clear();
        }
        #endregion
    }
}