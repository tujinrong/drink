//*****************************************************************************
// ユーティリティのライブラリ
//
// 概要　　：　データセット関連のユーティリティクラス
//               
// 作成　　：屠
//            2006/05/12 
//
//*****************************************************************************
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System;

namespace SafeNeeds.DySmat.Util
{ 
	/// <summary>
	/// データセット関連のユーティリティクラス
	/// </summary>
	public class DataSetUtil 
	{ 
		/// <summary>
		/// CSV出力(文字列表示)
		/// </summary>
		/// <param name="dt">対象のDataTable</param>
		public static string ToCsv(DataTable dt) 
		{ 
			StringBuilder sbRet = new StringBuilder(); 
			
			foreach (DataRow dr in dt.Rows) 
			{ 
				object[] obj = dr.ItemArray; 
				string[] str = new string[obj.Length]; 
				for (int i = 0; i < obj.Length; i++) 
				{ 
					str[i] = DataUtil.CStr(obj[i]); 
				} 
				
				sbRet.Append(string.Join(",", str)); 
				sbRet.Append(StringUtil.CRLF); 
			} 

			return sbRet.ToString(); 
		}


		/// <summary>
		/// CSV出力(文字列表示)
		/// </summary>
		/// <param name="dt">対象のDataTable</param>
        /// <param name="iRows">指定した行数</param>
		/// <returns>CSV文字列出力</returns>
		public static string ToCsv(DataTable dt, int iRows) 
		{ 
			StringBuilder sbRet = new StringBuilder(); 
			
			if (iRows <= 0)
			{
				return string.Empty;
			}
			
			int iCount = 1;
			
			foreach(DataRow dr in dt.Rows)
			{
				if (iCount > iRows)
				{
					break;
				}

				object[] obj = dr.ItemArray; 
				string[] str = new string[obj.Length]; 
				for (int i = 0; i < obj.Length; i++) 
				{ 
					str[i] = DataUtil.CStr(obj[i]); 
				} 
			
				sbRet.Append(string.Join(",", str)); 
				sbRet.Append(StringUtil.CRLF); 

				iCount++;
			}
			return sbRet.ToString(); 
		}


		/// <summary>
		/// CSV出力(文字列表示)
		/// </summary>
		/// <param name="ds">対象のDataSet</param>
		/// <returns>CSV文字列出力</returns>
		public static string ToCsv(DataSet ds) 
		{ 
			StringBuilder sbRet = new StringBuilder(); 
			
			foreach (DataRow dr in ds.Tables[0].Rows) 
			{ 
				object[] obj = dr.ItemArray; 
				string[] str = new string[obj.Length]; 
				for (int j = 0; j < obj.Length; j++) 
				{ 
					str[j] = DataUtil.CStr(obj[j]); 
				} 
			
				sbRet.Append(string.Join(",", str)); 
				sbRet.Append(StringUtil.CRLF); 
			}

			return sbRet.ToString(); 
		} 

		
		/// <summary>
		/// CSV出力(文字列表示)
		/// </summary>
		/// <param name="ds">対象のDataSet</param>
        /// <param name="rowCount">指定した行数</param>
		/// <returns>CSV文字列出力</returns>
		public static string ToCsv(DataSet ds, int rowCount) 
		{ 
			StringBuilder sbRet = new StringBuilder(); 
			
			if (rowCount <= 0)
			{
				return string.Empty;
			}

			int iCount = 1;

			foreach (DataRow dr in ds.Tables[0].Rows) 
			{ 
				if(iCount > rowCount)
				{
					break;
				}
				
				object[] obj = dr.ItemArray; 
				string[] str = new string[obj.Length]; 
				for (int j = 0; j < obj.Length; j++) 
				{ 
					str[j] = DataUtil.CStr(obj[j]); 
				} 
			
				sbRet.Append(string.Join(",", str)); 
				sbRet.Append(StringUtil.CRLF); 

				iCount++;
			}

			return sbRet.ToString(); 
		} 

		
		/// <summary>
		/// CSV出力(ファイル出力)
		/// </summary>
		/// <param name="dt">対象のDataTable</param>
        /// <param name="fileName">出力ファイル名</param>
		public static bool ToCsv(DataTable dt, string fileName)
		{
			List<string[]> al = new List<string[]>();
			
			foreach (DataRow dr in dt.Rows) 
			{ 
				object[] obj = dr.ItemArray; 
				string[] str = new string[obj.Length]; 
				for (int i = 0; i < obj.Length; i++) 
				{ 
					str[i] = DataUtil.CStr(obj[i]);
				} 

				al.Add(str);
			}

			CsvFile csv = new CsvFile();

			return csv.WriteCsv(fileName, al);
		}


		/// <summary>
		/// 指定した2つのDataRowを比較する(登録者CD、登録日時以外の項目)
		/// </summary>
		/// <param name="dr1">対象のDataRow1</param>
		/// <param name="dr2">対象のDataRow2</param>
		/// <returns>true: 一致; false: 不一致</returns>
		public static bool CompareDR(DataRow dr1, DataRow dr2) 
		{ 
			object[] obj1 = dr1.ItemArray; 
			object[] obj2 = dr2.ItemArray; 

			for (int i = 0; i <= obj1.Length - 1 - 2; i++) 
			{ 
				if (DataUtil.CStr(obj1[i]) != DataUtil.CStr(obj2[i])) 
				{ 
					// 2つのDataRowが異なる場合
					return false;
				} 
			} 

			// 2つのDataRowは同じの場合
			return true; 
		} 


		/// <summary>
		///指定した2つのDataTableを比較する
		/// </summary>
        /// <param name="dt1">対象のDataTable1</param>
        /// <param name="dt2">対象のDataTable2</param>
		/// <returns>true: 一致; false: 不一致</returns>
		public static bool CompareDT(DataTable dt1, DataTable dt2) 
		{ 
			//-----------------------------------------------------------------
			// 対象の存在判定
			//-----------------------------------------------------------------
			if (dt1 == null) 
			{ 
				if (dt2 == null) 
				{ 
					return true; 
				} 
				else 
				{ 
					return false; 
				} 
			} 
			else 
			{ 
				if (dt2 == null) 
				{ 
					return false; 
				} 
			} 

			//-----------------------------------------------------------------
			// 対象が存在する場合、比較する処理を行う
			//-----------------------------------------------------------------
			DataTable dtTmp1 = dt1.Copy(); 
			DataTable dtTmp2 = dt2.Copy(); 

			dtTmp1.AcceptChanges(); 
			dtTmp2.AcceptChanges(); 

			// 行数の判定
			if (dtTmp1.Rows.Count != dtTmp2.Rows.Count) 
			{ 
				return false; 
			} 

			// 列数の判定
			if (dtTmp1.Columns.Count != dtTmp2.Columns.Count)
			{
				return false;
			}

			// データ内容の判定
			for (int i = 0; i <= dtTmp1.Rows.Count - 1; i++) 
			{ 
				if (dtTmp1.Rows[i].RowState == DataRowState.Deleted ||
					dtTmp2.Rows[i].RowState == DataRowState.Deleted ||
                    dtTmp1.Rows[i].RowState == DataRowState.Detached ||
                    dtTmp2.Rows[i].RowState == DataRowState.Detached) 
				{ 
					return false; 
				} 
				for (int j = 0; j <= dtTmp1.Columns.Count - 1; j++) 
				{ 
					if (DataUtil.CStr(dtTmp1.Rows[i][j]) != DataUtil.CStr(dtTmp2.Rows[i][j])) 
					{ 
						return false; 
					} 
				} 
			} 

			return true; 
		} 


		/// <summary>
		/// 指定した2つのDataSetを比較する
		/// </summary>
		/// <param name="ds1">対象のDataSet1</param>
		/// <param name="ds2">対象のDataSet2</param>
		/// <returns></returns>
		public static bool CompareDS(DataSet ds1, DataSet ds2)
		{
			//-----------------------------------------------------------------
			// 対象の存在判定
			//-----------------------------------------------------------------
			if (ds1 == null) 
			{ 
				if (ds2 == null) 
				{ 
					return true; 
				} 
				else 
				{ 
					return false; 
				} 
			} 
			else 
			{ 
				if (ds2 == null) 
				{ 
					return false; 
				} 
			} 

			//-----------------------------------------------------------------
			// 対象が存在する場合、比較する処理を行う
			//-----------------------------------------------------------------
			DataSet dsTmp1 = ds1.Copy(); 
			DataSet dsTmp2 = ds2.Copy(); 

			dsTmp1.AcceptChanges(); 
			dsTmp2.AcceptChanges(); 

			// テーブル数の判定
			if (dsTmp1.Tables.Count != dsTmp2.Tables.Count) 
			{ 
				return false; 
			} 
			
			// テーブルを比較する
			for (int i = 0; i <= dsTmp1.Tables.Count - 1; i++)
			{
				bool bTmp = CompareDT(dsTmp1.Tables[i], dsTmp2.Tables[i]);

				if (bTmp == false)
				{
					return false;
				}
			}

			return true; 
		}


		/// <summary>
		/// 指定した2つのDataTableを比較して、変更部分のデータを抽出する
		/// </summary>
		/// <param name="dtOrginal">編集する前のDataTable</param>
		/// <param name="dtCurrent">編集する後のDataTable</param>
		/// <returns>変更したDataRow</returns>
		public static DataRow[] GetDiffDR(DataTable dtOrginal, DataTable dtCurrent) 
		{ 
//			DataTable dtCur = dtCurrent.Copy(); 
//			DataTable dtOrg = dtOrginal.Copy(); 
//
//			dtCur.AcceptChanges(); 
//			dtOrg.AcceptChanges(); 
//
//			//-----------------------------------------------------------------
//			// 編集後のDataSetのDataRow毎に処理する
//			//-----------------------------------------------------------------
//			DataTable dtResult = dtOrginal.Clone(); 
//			DataRow drTmp; 
//			SortedList slDsOld = new SortedList(); 
//			foreach (DataRow dr in dsCur.Tables[0].Rows) 
//			{ 
//				string sKey = string.Empty; 
//
//				if (!(slDsOld.Contains(sKey))) 
//				{ 
//					// 編集前のDataSetに含まない場合、AddNewで結果DataSetに追加する
//					drTmp = dsResult.Tables[0].NewRow(); 
//					drTmp.BeginEdit(); 
//					for (int j = 0; j <= dsCur.Tables[0].Columns.Count - 1; j++) 
//					{ 
//						drTmp.ItemArray[j] = dr.ItemArray[j]; 
//					} 
//					drTmp.EndEdit(); 
//					dsResult.Tables[0].Rows.Add(drTmp); 
//				} 
//				else 
//				{ 
//					// 編集前のDataSetにある場合
//					drTmp = ((DataRow)(slDsOld[sKey])); 
//					drTmp = (new DataSet()).Tables[0].Rows[0];
//					if (CompareDR(dr, drTmp) == false) 
//					{ 
//						// 編集前のDataSetの対応DataRowと違う場合、Modifyで結果DataSetに追加する
//						dr.BeginEdit(); 
//						dr.ItemArray[0] = drTmp.ItemArray[0]; 
//						dr.EndEdit(); 
//						dsResult.Tables[0].ImportRow(dr); 
//					} 
//					slDsOld.Remove(sKey); 
//				} 
//			} 
//
//			// 編集前DataSetの残りDataRowが編集後DataSetにないはずなので、Deleteで結果DataSetに追加する
//			for (int i = 0; i <= slDsOld.Count - 1; i++) 
//			{ 
//				drTmp = ((DataRow)(slDsOld[slDsOld.GetKey(i)])); 
//				drTmp = (new DataSet()).Tables[0].Rows[0];
//				drTmp.Delete(); 
//				dsResult.Tables[0].ImportRow(drTmp); 
//			} 
//
//			return dsResult; 

			return new DataRow[7];
		} 


		/// <summary>
		/// 指定した2つのDataSetを比較して、変更部分のデータを抽出する
		/// </summary>
        /// <param name="dtOrginal">編集する前のDataSet</param>
        /// <param name="dtCurrent">編集する後のDataSet</param>
		/// <returns>変更したDataSet</returns>
		public static DataTable GetDiffDT(DataTable dtOrginal, DataTable dtCurrent) 
		{ 
			DataTable dtCur = dtCurrent.Copy(); 
			DataTable dtOrg = dtOrginal.Copy(); 

			dtCur.AcceptChanges(); 
			dtOrg.AcceptChanges(); 

			//-----------------------------------------------------------------
			// 編集後のDataSetのDataRow毎に処理する
			//-----------------------------------------------------------------
			DataTable dtResult = dtOrginal.Clone(); 
			DataRow drTmp; 
			SortedList slDsOld = new SortedList(); 
			foreach (DataRow dr in dtCur.Rows) 
			{ 
				string sKey = string.Empty; 

				if (!(slDsOld.Contains(sKey))) 
				{ 
					// 編集前のDataSetに含まない場合、AddNewで結果DataSetに追加する
					drTmp = dtResult.NewRow(); 
					drTmp.BeginEdit(); 
					for (int j = 0; j <= dtCur.Columns.Count - 1; j++) 
					{ 
						drTmp.ItemArray[j] = dr.ItemArray[j]; 
					} 
					drTmp.EndEdit(); 
					dtResult.Rows.Add(drTmp); 
				} 
				else 
				{ 
					// 編集前のDataSetにある場合
					drTmp = ((DataRow)(slDsOld[sKey])); 
					drTmp = (new DataTable()).Rows[0];
					if (CompareDR(dr, drTmp) == false) 
					{ 
						// 編集前のDataSetの対応DataRowと違う場合、Modifyで結果DataSetに追加する
						dr.BeginEdit(); 
						dr.ItemArray[0] = drTmp.ItemArray[0]; 
						dr.EndEdit(); 
						dtResult.ImportRow(dr); 
					} 
					slDsOld.Remove(sKey); 
				} 
			} 

			// 編集前DataSetの残りDataRowが編集後DataSetにないはずなので、Deleteで結果DataSetに追加する
			for (int i = 0; i <= slDsOld.Count - 1; i++) 
			{ 
				drTmp = ((DataRow)(slDsOld[slDsOld.GetKey(i)])); 
				drTmp = (new DataTable()).Rows[0];
				drTmp.Delete(); 
				dtResult.ImportRow(drTmp); 
			} 

			return dtResult; 
		} 


		/// <summary>
		/// フィールドごとに同じ値を設定します。
		/// </summary>
		/// <param name="ds">指定するデータセット</param>
		/// <param name="colNames">フィールド名</param>
		/// <param name="colValues">フィールド値</param>
		public static void SetColumnValue(DataSet ds, string[] colNames, object[] colValues)
		{
			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				for (int i = 0; i < colNames.Length; i++)
				{
					if (dr[colNames[i]] == System.DBNull.Value){
						dr[colNames[i]] = colValues[i];
					}
				}
			}
		}


		/// <summary>
		/// フィールドごとに同じ値を設定します。
		/// </summary>
		/// <param name="ds">指定するデータセット</param>
		/// <param name="colName">フィールド名</param>
		/// <param name="colValue">フィールド値</param>
		public static void SetColumnValue(DataSet ds, string colName, object colValue)
		{
			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				if (dr[colName] == System.DBNull.Value)
				{
					dr[colName] = colValue;
				}
			}
		}


        ///// <summary>
        ///// DataRowの状態を変更します。
        ///// </summary>
        ///// <param name="dtOrg">変更前のDataTable</param>
        ///// <param name="dtDes">変更後のDataTable</param>
        ///// <param name="keyNames"></param>
        //public static void ChangeDataRowStateOld(DataTable dtOrg,  DataTable dtDes, string[] keyNames)
        //{
        //    Hashtable htKeyValue = new Hashtable();
        //    ArrayList alDelKeyValue = new ArrayList();
			
        //    int iRowNo = 0;

        //    foreach(DataRow dr in dtOrg.Rows)
        //    {
        //        string sKeyValue = ToKeyString(dr, keyNames);

        //        htKeyValue.Add(sKeyValue, iRowNo);

        //        iRowNo++;
        //    }

        //    foreach(DataRow dr in dtDes.Rows)
        //    {
        //        string sKeyValue = ToKeyString(dr, keyNames);

        //        alDelKeyValue.Add(sKeyValue);

        //        if (!htKeyValue.Contains(sKeyValue))
        //        {
        //            DataRow drTmp = dtOrg.NewRow();
        //            for (int i = 0; i < dtDes.Columns.Count; i++)
        //            {
        //                drTmp[i] = dr[i];
        //            }
        //            dtOrg.Rows.Add(drTmp);
        //        }
        //        else
        //        {
        //            int irow = (int)htKeyValue[sKeyValue];

        //            dtOrg.Rows[irow].BeginEdit();
					
        //            for (int i = 0; i < dtDes.Columns.Count; i++)
        //            {
        //                dtOrg.Rows[irow][i] = dr[i];
        //            }
					
        //            dtOrg.Rows[irow].EndEdit();
        //        }
        //    }

        //    foreach(DataRow dr in dtOrg.Rows)
        //    {
        //        string sKeyValue = ToKeyString(dr, keyNames);

        //        if (!alDelKeyValue.Contains(sKeyValue))
        //        {
        //            dr.Delete();
        //        }
        //    }
        //}

        /// <summary>
        /// DataRowの状態を変更します。
        /// </summary>
        /// <param name="dtOld">変更前のDataTable</param>
        /// <param name="dtNew">変更後のDataTable</param>
        /// <param name="keyNames"></param>
        public static void ChangeDataRowState(DataTable dtOld, DataTable dtNew, string[] keyNames)
        {
            Dictionary<string, DataRow> dicOld = new Dictionary<string,DataRow>();
            Dictionary<string, DataRow> dicNew = new Dictionary<string, DataRow>();

            //旧DataSetの辞書を作成
            foreach (DataRow dr in dtOld.Rows)
            {
                dicOld.Add(ToKeyString(dr, keyNames), dr);
            }
            //新Datasetの辞書を作成
            foreach (DataRow dr in dtNew.Rows)
            {
                dicNew.Add(ToKeyString(dr, keyNames), dr);
            }

            foreach (string key in dicNew.Keys)
            {

                if (!dicOld.ContainsKey(key))
                {
                    //Ｏｒｇに存在しない場合、追加する
                    dtOld.Rows.Add(dicNew[key].ItemArray);
                }
                else
                {
                    //存在する場合、Ｏｒｇを更新にする
                    //int irow = (int)OrgKeyNoDic[sKeyValue];
                    DataRow rowOrg = dicOld[key];
                    DataRow rowDes = dicNew[key];
                    
                    if (IsDataRowEquals(rowOrg, rowDes))
                    {
                        //内容が一致する場合、更新しない
                        rowOrg.AcceptChanges();
                    }
                    else
                    {
                        //一致しない場合、新DataSet内容をセット
                        if (rowOrg.RowState == DataRowState.Added)
                        {
                            rowOrg.AcceptChanges();
                        }
                        rowOrg.BeginEdit();
                        for (int i = 0; i < dtNew.Columns.Count; i++)
                        {
                            rowOrg[i] = rowDes[i];
                        }
                        rowOrg.EndEdit();
                    }
                    
                }
            }

            //旧DataSetを遍歴
            foreach (string key in dicOld.Keys)
            {
                if (!dicNew.ContainsKey(key))
                {
                    //新DataSetに存在しない場合、削除
                    dicOld[key].Delete();
                }
            }

        }


        /// <summary>
        /// DataRowの状態を変更します。
        /// </summary>
        /// <param name="dtOld">変更前のDataTable</param>
        /// <param name="dtNew">変更後のDataTable</param>
        /// <param name="i_ColumnName">項目名</param>
        /// <param name="keyNames"></param>
        public static void ChangeDataRowState(DataTable dtOld, DataTable dtNew, string[] keyNames, params string[] i_ColumnName)
        {
            Dictionary<string, DataRow> dicOld = new Dictionary<string, DataRow>();
            Dictionary<string, DataRow> dicNew = new Dictionary<string, DataRow>();

            //旧DataSetの辞書を作成
            foreach (DataRow dr in dtOld.Rows)
            {
                if (dicOld.ContainsKey(ToKeyString(dr, keyNames)) == false)
                {
                    dicOld.Add(ToKeyString(dr, keyNames), dr);
                }
            }
            //新Datasetの辞書を作成
            foreach (DataRow dr in dtNew.Rows)
            {
                if (dicNew.ContainsKey(ToKeyString(dr, keyNames)) == false)
                {
                    dicNew.Add(ToKeyString(dr, keyNames), dr);
                }
            }

            foreach (string key in dicNew.Keys)
            {

                if (!dicOld.ContainsKey(key))
                {
                    //Ｏｒｇに存在しない場合、追加する
                    dtOld.Rows.Add(dicNew[key].ItemArray);
                }
                else
                {
                    //存在する場合、Ｏｒｇを更新にする
                    //int irow = (int)OrgKeyNoDic[sKeyValue];
                    DataRow rowOrg = dicOld[key];
                    DataRow rowDes = dicNew[key];

                    if (IsDataRowEquals(rowOrg, rowDes, i_ColumnName))
                    {
                        //内容が一致する場合、更新しない
                        rowOrg.AcceptChanges();
                    }
                    else
                    {
                        //一致しない場合、新DataSet内容をセット
                        if (rowOrg.RowState == DataRowState.Added)
                        {
                            rowOrg.AcceptChanges();
                        }

                        List<string> lstColumnName = new List<string>();
                        lstColumnName.AddRange(i_ColumnName);

                        rowOrg.BeginEdit();
                        for (int i = 0; i < dtNew.Columns.Count; i++)
                        {
                            if (lstColumnName.Contains(dtNew.Columns[i].ColumnName) == false)
                            {
                                rowOrg[i] = rowDes[i];
                            }
                        }
                        rowOrg.EndEdit();
                    }

                }
            }

            //旧DataSetを遍歴
            foreach (string key in dicOld.Keys)
            {
                if (!dicNew.ContainsKey(key))
                {
                    //新DataSetに存在しない場合、削除
                    dicOld[key].Delete();
                }
            }

        }

        private static bool IsDataRowEquals(DataRow dr1, DataRow dr2)
        {
            for (int i = 0; i < dr1.ItemArray.Length; i++)
            {
                object o1 = dr1[i];
                object o2 = dr2[i];
                if (o1.GetType() != o2.GetType()) return false;
                if (o1.ToString() != o2.ToString()) return false;
            }
            return true;
        }


        private static bool IsDataRowEquals(DataRow dr1, DataRow dr2,params string[] i_ColumnName)
        {
            List<string> lstColumnName = new List<string>();
            lstColumnName.AddRange(i_ColumnName);

            for (int i = 0; i < dr1.ItemArray.Length; i++)
            {
                if (lstColumnName.Contains(dr1.Table.Columns[i].ColumnName) == false)
                {
                    object o1 = dr1[i];
                    object o2 = dr2[i];
                    if (o1.GetType() != o2.GetType()) return false;
                    //if (o1.ToString() != o2.ToString()) return false;
                    if (!o1.Equals(o2)) return false;
                }
            }
            return true;
        }

		private static string ToKeyString(DataRow dr, string[] keyNames)
		{
			string sKeyValue = string.Empty;
			for (int i = 0; i < keyNames.Length; i++)
			{
				if (i >0) 
				{
					sKeyValue += "|";
				}
				sKeyValue += dr[keyNames[i]].ToString();
			}
			return sKeyValue;

		}

        /// <summary>
        /// 依据指定的DataRow对象，构造出相应类型的Model实例，利用DataRow中的数据进行填充并返回
        /// </summary>
        /// <param name="i_Row">DataRow原型实例</param>
        /// <param name="i_Type">Model类型</param>
        /// <returns>>填充后的Model对象</returns>
        public static object DataRowToModel(DataRow i_Row, System.Type i_Type)
        {
            //类型实例化
            object model = System.Activator.CreateInstance(i_Type);
            DataTable dt = i_Row.Table;

            //获取类型的字段数组

            System.Reflection.FieldInfo[] fieldInfos = i_Type.GetFields(BindingFlags.Instance | BindingFlags.Public);


            foreach (System.Reflection.FieldInfo fieldInfo in fieldInfos)
            {
                //屏蔽非字段信息
                if (fieldInfo.MemberType != System.Reflection.MemberTypes.Field)
                    continue;
                if (dt.Columns.Contains(fieldInfo.Name))    //数据行中存在此字段信息的场合
                {
                    if (fieldInfo.FieldType == typeof(int))
                        fieldInfo.SetValue(model, DataUtil.CInt(i_Row[fieldInfo.Name]));
                    else if (fieldInfo.FieldType == typeof(int?))
                        fieldInfo.SetValue(model, DataUtil.CIntN(i_Row[fieldInfo.Name]));
                    else if (fieldInfo.FieldType == typeof(decimal))
                        fieldInfo.SetValue(model, DataUtil.CDec(i_Row[fieldInfo.Name]));
                    else if (fieldInfo.FieldType == typeof(decimal?))
                        fieldInfo.SetValue(model, DataUtil.CDecN(i_Row[fieldInfo.Name]));
                    else if (fieldInfo.FieldType == typeof(DateTime))
                        fieldInfo.SetValue(model, DataUtil.CDate(i_Row[fieldInfo.Name]));
                    else if (fieldInfo.FieldType == typeof(DateTime?))
                        fieldInfo.SetValue(model, DataUtil.CDateN(i_Row[fieldInfo.Name]));
                    else if (fieldInfo.FieldType == typeof(Single))
                        fieldInfo.SetValue(model, DataUtil.CFloat(i_Row[fieldInfo.Name]));
                    else if (fieldInfo.FieldType == typeof(Double))
                        fieldInfo.SetValue(model, DataUtil.CDbl(i_Row[fieldInfo.Name]));
                    else
                    {
                        string s = DataUtil.CStr(i_Row[fieldInfo.Name]);
                        fieldInfo.SetValue(model, s);
                    }

                }
            }

            return model;
        }

        /// <summary>
        /// 依据指定的DataTable对象，构造出相应类型的Model实例，在利用DataTable中第一行数据作为
        /// 模型数据源进行填充并返回
        /// TO调用者：此方法将只利用数据库表中的第一行作为模型实例的数据源

        /// </summary>
        /// <param name="i_Table">DataTable原型对象</param>
        /// <param name="i_Type">Model类型</param>
        /// <returns>填充后的Model对象</returns>
        public static object DataTableToModel(DataTable i_Table, System.Type i_Type)
        {
            if (i_Table.Rows.Count == 0) return null;

            DataRow dr = i_Table.Rows[0];
            Assembly assembly = Assembly.GetAssembly(i_Type);
            object oo = assembly.CreateInstance(i_Type.FullName);

            System.Reflection.FieldInfo[] fieldInfos = i_Type.GetFields(BindingFlags.Instance | BindingFlags.Public);
            foreach (System.Reflection.FieldInfo fieldInfo in fieldInfos)
            {
                if (fieldInfo.MemberType != System.Reflection.MemberTypes.Field)
                    continue;

                if (i_Table.Columns.Contains(fieldInfo.Name))
                {
                    if (fieldInfo.FieldType == typeof(int))
                        fieldInfo.SetValue(oo, DataUtil.CInt(dr[fieldInfo.Name]));
                    else if (fieldInfo.FieldType == typeof(decimal))
                        fieldInfo.SetValue(oo, DataUtil.CDec(dr[fieldInfo.Name]));
                    else if (fieldInfo.FieldType == typeof(DateTime))
                        fieldInfo.SetValue(oo, DataUtil.CDate(dr[fieldInfo.Name]));
                    else if (fieldInfo.FieldType == typeof(Single))
                        fieldInfo.SetValue(oo, DataUtil.CFloat(dr[fieldInfo.Name]));
                    else if (fieldInfo.FieldType == typeof(Double))
                        fieldInfo.SetValue(oo, DataUtil.CDbl(dr[fieldInfo.Name]));
                    else
                    {
                        string s = DataUtil.CStr(dr[fieldInfo.Name]);
                        fieldInfo.SetValue(oo, s);
                    }

                }
            }
            return oo;

        }

        protected string GetModelType(string fullname, string modelName)
        {
            string s = fullname.Replace("System.String", "string");
            s = s.Replace("System.Collections.Generic.Dictionary`2", "Dictionary");
            s = s.Replace("System.Collections.Generic.List`1", "List");
            s = s.Replace(modelName, "Model");

            int p = 0;
            while (true)
            {
                p = s.IndexOf(", ");
                if (p > 0)
                {
                    int p2 = s.IndexOf("]", p);
                    s = s.Remove(p, p2 - p);
                }
                else break;
            }


            if (s == "List[[Model]]")
            {
                return "List<Model>";
            }
            else if (s == "Dictionay[[string],[List[Model]]]")
            {
                return "Dictionay<string,List<Model>>";
            }
            else if (s == "Dictionay[[string],[Dictionay[[string],[Model]]]]")
            {
                return "Dictionay<string,Dictionary<string,Model>>";
            }
            else
            {
                throw new ApplicationException("");
            }
        }
	} 
}
