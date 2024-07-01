using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebEvaluation.Models;

namespace WebEvaluation.ViewModels
{
    public class TelRptCreateViewModel
    {
        public T_Party party { get; set; }
        public T_Report report { get; set; }
        public T_EvaByStaff evaBystaff { get; set; }
    }
}