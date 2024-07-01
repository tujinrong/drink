using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebEvaluation.Models;

namespace WebEvaluation.ViewModels
{
    public class TelRptEvaViewModel
    {
        public T_Party party { get; set; }
        public T_Report report { get; set; }
        public T_EvaByStaff evaByStaff { get; set; }
        public T_EvaByLeader evaByLeader { get; set; }
    }
}