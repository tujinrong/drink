using System;
using System.Text;

namespace SafeNeeds.DySmat.DB
{
    /// <summary>
    /// �f�[�^�x�[�X���
    /// </summary>
    public enum EnumDatabaseType : byte
    {
        /// <summary>
        /// �f�[�^�x�[�X��Sql Server�̏ꍇ
        /// </summary>
        SQLSERVER = 0,


        /// <summary>
        /// �f�[�^�x�[�X��Oracle�̏ꍇ
        /// </summary>
        ORACLE = 1,


        /// <summary>
        /// �f�[�^�x�[�X��ACCESS�̏ꍇ
        /// </summary>
        ACCESS = 2,

        /// <summary>
        /// 
        /// </summary>
        POSTGRESQL = 3,

        /// <summary>
        /// 
        /// </summary>
        MYSQL = 4
    }

    /// <summary>
    /// �A�_�v�^�[�̎��
    /// </summary>
    public enum EnumAdapterType
    {
        /// <summary>
        /// �A�_�v�^�[��OleDB�̏ꍇ
        /// </summary>
        OLEDB = 0,

        /// <summary>
        /// �A�_�v�^�[��SQLDATA�̏ꍇ
        /// </summary>
        SQL = 1,

        /// <summary>
        /// �A�_�v�^�[��ODBC�̏ꍇ�i���ݖ��Ή��j
        /// </summary>
        ODBC = 2,

        /// <summary>
        /// �A�_�v�^�[��Oracle�̏ꍇ
        /// </summary>
        ORACLE = 3,

        /// <summary>
        /// ODP
        /// </summary>
        ODP=4
    }

    /// <summary>
    /// �v���o�C�_�[�̎��
    /// </summary>
    public enum EnumProviderType : byte
    {
        /// <summary>
        /// Provide=MS SQL SqlServer
        /// </summary>
        MS_SQL_SQLSERVER = 0,


        /// <summary>
        /// Provider=SQLOLEDB.1 (MS OleDB for SQLServer)
        /// </summary>
        MS_OLEDB_SQLSERVER = 1,

        /// <summary>
        /// Provide=MSDAORA.1 (MS OleDB for Oracle)
        /// </summary>
        MS_OLEDB_ORACLE = 2,



        /// <summary>
        /// Provide=Oracle provider by Oracle
        /// </summary>
        ORA_ORACLE = 4,

        /// <summary>
        /// Provide=Oracle provider for OLE DB
        /// </summary>
        ORA_OLEDB_ORACLE = 5,

        /// <summary>
        /// ODP
        /// </summary>
        ORA_ODP = 6,


        /// <summary>
        /// Provider=Microsoft.Jet.OLEDB.4.0
        /// </summary>
        MS_OLEDB_Jet,

        /// <summary>
        /// 
        /// </summary>
        MS_ODBC_SQLSERVER,

        /// <summary>
        /// 
        /// </summary>
        MS_ODBC_ORACLE,

        /// <summary>
        /// 
        /// </summary>
        MS_ODBC_ACCESS,

        /// <summary>
        /// 
        /// </summary>
        ORA_ODBC_ORACLE
    }

    /// <summary>
    /// DB���ڌ^
    /// </summary>
    public enum EnmItemType
    {
        /// <summary>
        /// ����`
        /// </summary>
        UnDefined,

        /// <summary>
        /// ������
        /// </summary>
        String,

        /// <summary>
        /// ���l
        /// </summary>
        Integer,

        /// <summary>
        /// ���l(����)
        /// </summary>
        Long,

        /// <summary>
        /// �\�i��
        /// </summary>
        Decimal,

        /// <summary>
        /// �_���^
        /// </summary>
        Boolean,

        /// <summary>
        /// ���t�^
        /// </summary>
        Date,

        /// <summary>
        /// ���������_
        /// </summary>
        Float,

        /// <summary>
        /// ���������_(���x����)
        /// </summary>
        Double
    }

    /// <summary>
    /// 
    /// </summary>
    public enum EnmDBObjectType
    {
        /// <summary>
        /// 
        /// </summary>
        MASTERVIVW,

        /// <summary>
        /// 
        /// </summary>
        TABLEVIVW,

        /// <summary>
        /// 
        /// </summary>
        TABLEALIASVIVW,

        /// <summary>
        /// 
        /// </summary>
        VIEW,

        /// <summary>
        /// 
        /// </summary>
        SQL,

        /// <summary>
        /// 
        /// </summary>
        RESTRICTIONSQL,

        /// <summary>
        /// 
        /// </summary>
        DYNAMICVIEW,

        /// <summary>
        /// 
        /// </summary>
        DYNAMICSQL
    }
    
    /// <summary>
    /// �\�[�g�^�C�v
    /// </summary>
    public enum EnmSortType
    {
        /// <summary>
        /// 
        /// </summary>
        NONE,

        /// <summary>
        /// 
        /// </summary>
        ASC,

        /// <summary>
        /// 
        /// </summary>
        DESC
    }

    /// <summary>
    /// �\�[�g�^�C�v
    /// </summary>
    public enum EnmOrderType
    {
        /// <summary>
        /// 
        /// </summary>
        ASC,

        /// <summary>
        /// 
        /// </summary>
        DESC
    }

    /// <summary>
    /// �������`�F�b�N�̃G���[���
    /// </summary>
    public enum EnmConsistenceType
    {
        /// <summary>
        /// �v���G���[
        /// </summary>
        FatalError,

        /// <summary>
        /// �x��
        /// </summary>
        Warning,

        /// <summary>
        /// ����
        /// </summary>
        Ignore
    }




    //public enum EnumDatabaseType : short
    //{
    //    SQLServer,
    //    Oracle,
    //    Access
    //}



    /// <summary>
    /// �ڑ����@
    /// </summary>
    public enum EnmConnectionType
    {
        /// <summary>
        /// �ڑ��Ǘ������A����ڑ����Ȃ���
        /// </summary>
        NoPooling = 0,
        
        /// <summary>
        /// ��ڑ��ŃV�X�e�����L����
        /// </summary>
        SimpleConnection = 1
    }

    /// <summary>
    /// 
    /// </summary>
    public enum EnmViewType
    {
        /// <summary>
        /// 
        /// </summary>
        TableView,

        /// <summary>
        /// 
        /// </summary>
        SqlViewType1,

        /// <summary>
        /// 
        /// </summary>
        SqlViewType2
    }
}
