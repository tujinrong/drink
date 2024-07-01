using System;
using System.Collections.Generic;
using System.Data;

using SafeNeeds.DySmat.Model;

namespace SafeNeeds.DySmat.DB
{
    /// <summary>
    /// 
    /// </summary>
    public class DBParameter
    {
        private string _ParameterName;
        private string _SourceColumn;
        private int _Size;
        private object _Value;
        private EnumDbDataType _DBType;
        private ParameterDirection _Direction = ParameterDirection.Input;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <param name="value"></param>
        public DBParameter(string parameterName, EnumDbDataType dbType, int size, object value)
        {
            this.ParameterName = parameterName;
            this.DataType = dbType;
            this.Size = size;
            this.Value = value;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="field"></param>
        public DBParameter(Y_EntityField field)
        {
            this.DataType = field.GetCommonDataType();
            this.Size = field.GetSize();
            this.SourceColumn = field.FieldName;
        }

  
        /// <summary>
        /// 
        /// </summary>
        public string ParameterName
        {
            get
            {
                return _ParameterName;
            }
            set
            {
                _ParameterName = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string SourceColumn
        {
            get
            {
                return _SourceColumn;
            }
            set
            {
                _SourceColumn = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Size
        {
            get
            {
                return _Size;
            }
            set
            {
                _Size = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public object Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public EnumDbDataType DataType
        {
            get
            {
                return _DBType;
            }
            set
            {
                _DBType = value;
            }
        }

        public ParameterDirection Direction
        {
            get
            {
                return _Direction;
            }
            set
            {
                _Direction = value;
            }
        }

     
         /// <summary>
        /// 
        /// </summary>
        /// <param name="p_fields"></param>
        /// <param name="p_adapter"></param>
        /// <param name="p_sJoin"></param>
        /// <returns></returns>
    

    
    }
}
