using System;
using System.Text;


namespace SafeNeeds.DySmat.DB.Util
{
    /// <summary>
    /// SQL���̂��̑��N���X
    /// </summary>
    public class SqlElse
    {
        private EnumDatabaseType _DbType;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="dbType">�f�[�^�x�[�X�^�C�v</param>
        public SqlElse(EnumDatabaseType dbType)
        {
            this._DbType = dbType;
        }

        /// <summary>
        /// ���Ԃ��擾����
        /// </summary>
        /// <param name="dateItem">���t����</param>
        /// <param name="startYear">�J�n�N</param>
        /// <param name="startMonth">�J�n��</param>
        /// <returns>����</returns>
        public string GetDays(string dateItem, int startYear, int startMonth)
        {
            StringBuilder sql = new StringBuilder();
            if (this._DbType == DB.EnumDatabaseType.ACCESS)
            {
                sql.Append("year(" + dateItem + ") - " + startYear);
                sql.Append("-iif(");
                sql.Append("cint(format(" + dateItem + ",'MM')) >= " + startMonth + ",0,1)");
            }
            else if (this._DbType == DB.EnumDatabaseType.ORACLE)
            {
            }
            else if (this._DbType == DB.EnumDatabaseType.SQLSERVER)
            {
                sql.Append("year(" + dateItem + ") - " + startYear);
                sql.Append("+ case sign(MONTH(" + dateItem + ")-" + startMonth + ") when -1 then -1 else 0 end");
            }
            return sql.ToString();
        }
    }
}
