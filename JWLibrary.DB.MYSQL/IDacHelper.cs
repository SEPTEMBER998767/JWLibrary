﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace JWLibrary.DB.MYSQL {
	public interface IDacHelper {
		#region SelectSingleEntity

		object SelectSingleEntity(string query, params DbParameter[] dbParams);

		object SelectSingleEntity(CommandType cmdType, string query, params DbParameter[] dbParams);

		T SelectSingleEntity<T>(Type type, string query, params DbParameter[] dbParams);

		T SelectSingleEntity<T>(Type type, CommandType cmdType, string query, params DbParameter[] dbParams);

		#endregion


		#region SelectMultipleEntities

		object SelectMultipleEntities(string query, params DbParameter[] dbParams);

		object SelectMultipleEntities(CommandType cmdType, string query, params DbParameter[] dbParams);

		object SelectMultipleEntities(CommandType cmdType, string query, int startIndex, int length, params DbParameter[] dbParams);

		List<T> SelectMultipleEntities<T>(Type type, string query, params DbParameter[] dbParams);

		List<T> SelectMultipleEntities<T>(Type type, CommandType cmdType, string query, params DbParameter[] dbParams);

		List<T> SelectMultipleEntities<T>(Type type, CommandType cmdType, string query, int startIndex, int legnth, params DbParameter[] dbParams);

		DataTable SelectDataTable(string query, params DbParameter[] dbParams);

		#endregion


		#region SelectScalar

		object SelectScalar(string query, params DbParameter[] dbParams);

		object SelectScalar(string query, object dbNullDefault, object nullDefault, params DbParameter[] dbParams);

		object SelectScalar(CommandType cmdType, string query, params DbParameter[] dbParams);

		object SelectScalar(CommandType cmdType, string query, object dbNullDefault, object nullDefault, params DbParameter[] dbParams);

		#endregion


		#region Execute

		int Execute(string query, params DbParameter[] dbParams);

		int Execute(CommandType cmdType, string query, params DbParameter[] dbParams);

		int ExecuteNonQuery(string query, params DbParameter[] dbParams);

		int ExecuteNonQuery(CommandType cmdType, string query, params DbParameter[] dbParams);

		#endregion


		#region Date

		DateTime GetDate();

		DateTime GetUtcDate();

		#endregion
	}
}
