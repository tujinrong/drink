//*****************************************************************************
// [システム]  
// 
// [機能概要]  
//  
// [作成履歴]　2014/06/25  屠錦栄　初版 
//
// [レビュー]　2014/07/17  屠錦栄　 
//*****************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations;
using System.Linq;

using System.Data;


using SafeNeeds.DySmat.Model;


namespace SafeNeeds.DySmat
{


         public enum EnumRelationType
        {
            Type1N, TypeN1,TypeCodeName
        }

    public class Relation
    {

        EnumRelationType _Type;

        Y_Entity _Entity;

        //Y_Entity _RelaEntity;


        public string RelaName;

        public string EntityName;

        public string Alias;

        public string RelaEntityName;

        public string[] ItemNames;

        public string[] RelaItemNames;

        string _sql;


        public Relation(Y_Proj proj, string s)
        {
            string[] ss = s.Split(':');
            
            //TYPEの取得
            switch (ss[0])
            {
                case "CN": 
                    _Type = EnumRelationType.TypeCodeName;
                    break;
                case "1N":
                 _Type = EnumRelationType.Type1N;
                    break;
                case "N1":
                    _Type = EnumRelationType.TypeN1;
                    break;
                default:
                    throw new ApplicationException("");
            }

            string[] s2 = ss[1].Split('.');
            EntityName = s2[0];

            RelaName = s2[1];
            _Entity = proj.EntityList.Find(e => e.EntityName == EntityName);

            switch (_Type)
            {
                case EnumRelationType.TypeN1 :
                    Y_EntityRelaN1 relaN1 = _Entity.RelaN1List.Find(e => e.RelaName == RelaName);
                    if (relaN1==null)
                    {
                        throw new ApplicationException(string.Format("Relation未定義{0}", RelaName));
                    }
                    RelaEntityName = relaN1.RelaEntityName;
                    ItemNames = relaN1.FieldNames.Split(',');
                    RelaItemNames = relaN1.RelaIFieldNames.Split(',');
                    Alias = relaN1.Alias;
                    break;
                case EnumRelationType.Type1N:
                    Y_EntityRela1N rela1N = _Entity.Rela1NList.Find(e => e.RelaName == RelaName);
                    if (rela1N == null)
                    {
                        throw new ApplicationException(string.Format("Relation未定義{0}", RelaName));
                    }
                    RelaEntityName = rela1N.RelaEntityName;
                    Alias = rela1N.RelaEntityName;
                    ItemNames = rela1N.FieldNames.Split(',');
                    RelaItemNames = rela1N.RelaFieldNames.Split(',');
                    break;

               
            }

            string sql =  " LEFT JOIN " + RelaEntityName;
            if (Alias != RelaEntityName)
            {
                sql += " AS " + Alias;
            }

            sql += "\n ON ";
            List<string> items = new List<string>();
            for (int i = 0; i < ItemNames.Length; i++)
            {
                string entityStr = "";
                string relationStr = "";

                if (ItemNames[i].StartsWith("'"))
                {
                    entityStr = ItemNames[i];
                }
                else if (ItemNames[i].IndexOf("|*|") >= 0)
                {
                    entityStr = ItemNames[i].Replace("|*|",",");
                }
                else
                {
                    entityStr = EntityName + "." + ItemNames[i];
                }

                if (RelaItemNames[i].StartsWith("'"))
                {
                    relationStr = RelaItemNames[i];
                }
                else if (RelaItemNames[i].IndexOf("|*|") >= 0)
                {
                    relationStr = RelaItemNames[i].Replace("|*|", ",");
                }
                else
                {
                    relationStr = Alias + "." + RelaItemNames[i];
                }
                items.Add(entityStr + "=" + relationStr);
            }
            sql += string.Join("\n AND ", items.ToArray());
            _sql= sql;

        }

        public string ToSql()
        {
            return _sql;

        }

    }

}