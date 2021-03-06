﻿using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace JWLibrary.DB.MYSQL.Module {
	public class MySqlDataBinder<T> : IDataBinder<T> {
		private T _instance;
		private IDataReader _reader;

		public MySqlDataBinder(Type type, IDataReader reader) {
			_instance = (T)Assembly.GetAssembly(type).CreateInstance(type.FullName, true);
			_reader = reader;
		}

		#region IDataBinder<T> 멤버

		public T DataBind() {
			MySqlDataBinderAttribute binderAttr = null;
			object[] customAttr = null;

			try {
				foreach (FieldInfo field in _instance.GetType().GetFields()) {
					customAttr = null;
					binderAttr = null;
					customAttr = field.GetCustomAttributes(typeof(MySqlDataBinderAttribute), false);

					if (customAttr != null && customAttr.Length > 0)
						binderAttr = customAttr[0] as MySqlDataBinderAttribute;

					if (binderAttr != null && _reader[binderAttr.FieldName] != null && !String.IsNullOrEmpty(_reader[binderAttr.FieldName].ToString()))
						field.SetValue(_instance, _reader[binderAttr.FieldName]);
				}

				foreach (PropertyInfo property in _instance.GetType().GetProperties()) {
					customAttr = null;
					binderAttr = null;
					customAttr = property.GetCustomAttributes(typeof(MySqlDataBinderAttribute), false);

					if (customAttr != null && customAttr.Length > 0)
						binderAttr = customAttr[0] as MySqlDataBinderAttribute;

					try {
						//_reader[]에서 바인더 필드네임이 없을 경우 계속적 오류로 속도가 떨어지게 되므로
						//컬럼명 비교로 변경한다.
						//Exception보다는 빠르겠지.
						foreach (DataRow row in _reader.GetSchemaTable().Rows) {
							if (row["ColumnName"].ToString() == binderAttr.FieldName) {
								if (binderAttr != null && _reader[binderAttr.FieldName] != null && !String.IsNullOrEmpty(_reader[binderAttr.FieldName].ToString())) {
									property.SetValue(_instance, _reader[binderAttr.FieldName], null);
									break;
								}

							}
						}
					}
					catch (Exception e) {
						Debug.WriteLine(string.Format("Message : {0}, Stack : {1}", e.Message, e.StackTrace));
					}
				}
			}
			catch (Exception e) {
				throw e;
			}

			return _instance;
		}

		#endregion
	}
}
