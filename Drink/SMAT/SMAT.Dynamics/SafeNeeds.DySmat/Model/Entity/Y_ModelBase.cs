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

namespace SafeNeeds.DySmat.Model
{
    public class DyModelBase
    {
        public const int NAME_LEN = 40;
        public const int DESC_LEN = 100;

        public const int TYPE_LEN = 10;
        public const int GROUP_LEN = 20;
        public const int OPR_LEN = 10;


        //LikeHead, LikeMiddle, =,>=,<= in, Between
        public const string OPR_LH = "LikeHead";
        public const string OPR_LK = "Like";
        public const string OPR_EQ = "=";
        public const string OPR_LE = "<=";
        public const string OPR_GE = ">=";
        public const string OPR_BE = "BETWEEN";
        public const string OPR_IN = "in";
        public const string OPR_NI = "not in";
    }
  

}