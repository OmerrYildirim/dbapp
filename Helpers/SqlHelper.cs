using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace dbapp.Helpers;

 public class SqlHelper {
        private readonly SqlConnection _connection;
        private readonly SqlCommand _baseCommand;

        public SqlHelper(IConfiguration configuration) {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            _baseCommand = _connection.CreateCommand();
            _baseCommand.CommandType = CommandType.Text;
        }

        public SqlConnection OpenConnection() {
            if (_connection.State != ConnectionState.Open) {
                _connection.Open();
            }
            return _connection;
        }

        public void CloseConnection() {
            if (_connection != null && _connection.State != ConnectionState.Closed) {
                _connection.Close();
            }
        }

        public SqlCommand GetBaseCommand() {
            _baseCommand.Parameters.Clear();
            return _baseCommand;
        }

        public SqlCommand CreateCommand(string commandText) {
            SqlCommand command = GetBaseCommand();
            command.CommandText = commandText;
            return command;
        }

        public static void AddParameter(SqlCommand command, string parameterName, SqlDbType dbType, object value) {
            SqlParameter parameter = command.Parameters.Add(parameterName, dbType);
            parameter.Value = value ?? DBNull.Value;
        }

        public static int ExecuteNonQuery(SqlCommand command) {
            return command.ExecuteNonQuery();
        }

        public static object ExecuteScalar(SqlCommand command) {
            return command.ExecuteScalar();
        }

        public static SqlDataReader ExecuteReader(SqlCommand command) {
            return command.ExecuteReader();
        }

        public SqlTransaction BeginTransaction() {
            return _connection.BeginTransaction();
        }

        public static void CommitTransaction(SqlTransaction transaction) {
            transaction.Commit();
        }

        public static void RollbackTransaction(SqlTransaction transaction) {
            transaction.Rollback();
        }
    }