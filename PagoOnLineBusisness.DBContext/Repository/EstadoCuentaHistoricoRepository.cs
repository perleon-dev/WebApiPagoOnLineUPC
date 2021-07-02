﻿using Dapper;
using PagoOnLineBusisness.DBContext.Base;
using PagoOnLineBusisness.DBContext.Interface;
using PagoOnLineBusisness.DBEntity.Base;
using PagoOnLineBusisness.DBEntity.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PagoOnLineBusisness.DBContext.Repository
{
    public class EstadoCuentaHistoricoRepository : BaseRepository, IEstadoCuentaHistoricoRepository
    {

        public ResponseBase EstadoCuentaHistorico(string idcontribuyente, DateTime fdesde, DateTime fhasta, int retorno)
        {
            var returnEntity = new ResponseBase();

            try
            {
                using (var db = GetSqlConnection())
                {
                    const string sql = @"usp_estadocuentahistorico";

                    var p = new DynamicParameters();
                    p.Add(name: "@idcontribuyente",value: idcontribuyente, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@desde", value:fdesde, dbType: DbType.DateTime, direction: ParameterDirection.Input);
                    p.Add(name: "@hasta", value:fhasta, dbType: DbType.DateTime, direction: ParameterDirection.Input);
                    p.Add(name: "@resultado",value:retorno, dbType: DbType.Int32, direction: ParameterDirection.Output);

                    db.Query<EntityEstadoCuenta>(sql: sql, param: p, commandType: CommandType.StoredProcedure).FirstOrDefault();

                    int idresultado = p.Get<int>("@resultado");

                    if (idresultado > 0)
                    {
                        returnEntity.isSuccess = true;
                        returnEntity.errorCode = "0000";
                        returnEntity.errorMessage = string.Empty;
                        returnEntity.data = new
                        {
                            idresultado = idresultado
                        };
                    }
                    else
                    {
                        returnEntity.isSuccess = false;
                        returnEntity.errorCode = "0000";
                        returnEntity.errorMessage = string.Empty;
                        returnEntity.data = null;
                    }
                }
            }
            catch (Exception ex)
            {
                returnEntity.isSuccess = false;
                returnEntity.errorCode = "0001";
                returnEntity.errorMessage = ex.Message;
                returnEntity.data = null;
            }

            return returnEntity;
        }
      
    }
}