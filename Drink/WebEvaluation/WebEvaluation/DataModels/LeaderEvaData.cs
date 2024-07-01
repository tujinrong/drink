using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations;

namespace WebEvaluation.DataModels
{
    public class LeaderEvaData 
    {
        [Display(Name = "挙式ID")]
        public int PartyID { get; set; }

        [Display(Name = "上長評価")]
        public string LeaderEva { get; set; } 
    }
}