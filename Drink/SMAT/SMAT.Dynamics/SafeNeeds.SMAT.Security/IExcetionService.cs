//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading;

//namespace SafeNeeds.SMAT.Core
//{
//    /// <summary>
//    /// 異常処理サービスインタフェース 
//    /// </summary>
//    public interface IExcetionService
//    {
//        bool DefaultConfirm { get;}
//        bool DefaultExitSystem { get;}

//        bool DoException(Exception ex, string Msg);
//        bool DoException(Exception ex, string Msg, bool doConfirm, bool isExit);

//        /// <summary>
//        /// システム例外発生時処理
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="e"></param>
//        void Application_Exception(object sender, ThreadExceptionEventArgs e);

//    }

//}
