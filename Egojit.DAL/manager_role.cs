using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using Egojit.DBUtility;
using Egojit.Common;

namespace Egojit.DAL
{
    /// <summary>
    /// 管理角色
    /// </summary>
    public partial class manager_role
    {
        public manager_role()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from dt_manager_role");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回角色名称
        /// </summary>
        public string GetTitle(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 role_name from dt_manager_role");
            strSql.Append(" where id=" + id);
            string title = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(title))
            {
                return "";
            }
            return title;
        }

        /// <summary>
        /// 增加一条数据,及其子表数据
        /// </summary>
        public int Add(Model.manager_role model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into dt_manager_role(");
            strSql.Append("role_name,role_type)");
            strSql.Append(" values (");
            strSql.Append("@role_name,@role_type)");
            strSql.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@role_name", SqlDbType.NVarChar,100),
					new SqlParameter("@role_type", SqlDbType.TinyInt,1),
					new SqlParameter("@ReturnValue",SqlDbType.Int)};
            parameters[0].Value = model.role_name;
            parameters[1].Value = model.role_type;
            parameters[2].Direction = ParameterDirection.Output;

            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);
            StringBuilder strSql2;
            foreach (Model.manager_role_value models in model.manager_role_values)
            {
                strSql2 = new StringBuilder();
                strSql2.Append("insert into dt_manager_role_value(");
                strSql2.Append("role_id,channel_name,channel_id,action_type)");
                strSql2.Append(" values (");
                strSql2.Append("@role_id,@channel_name,@channel_id,@action_type)");
                SqlParameter[] parameters2 = {
						new SqlParameter("@role_id", SqlDbType.Int,4),
						new SqlParameter("@channel_name", SqlDbType.NVarChar,255),
						new SqlParameter("@channel_id", SqlDbType.Int,4),
						new SqlParameter("@action_type", SqlDbType.NVarChar,100)};
                parameters2[0].Direction = ParameterDirection.InputOutput;
                parameters2[1].Value = models.channel_name;
                parameters2[2].Value = models.channel_id;
                parameters2[3].Value = models.action_type;

                cmd = new CommandInfo(strSql2.ToString(), parameters2);
                sqllist.Add(cmd);
            }
            DbHelperSQL.ExecuteSqlTranWithIndentity(sqllist);
            return (int)parameters[2].Value;
        }

        /// <summary>
        /// 更新一条数据及其子表
        /// </summary>
        public bool Update(Model.manager_role model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dt_manager_role set ");
            strSql.Append("role_name=@role_name,");
            strSql.Append("role_type=@role_type");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@role_name", SqlDbType.NVarChar,100),
					new SqlParameter("@role_type", SqlDbType.TinyInt,1)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.role_name;
            parameters[2].Value = model.role_type;

            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);
            
            //删除权限
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete from dt_manager_role_value where role_id=@role_id ");
            StringBuilder idList = new StringBuilder();
            if (model.manager_role_values != null)
            {
                foreach (Model.manager_role_value models in model.manager_role_values)
                {
                    if (models.id > 0)
                    {
                        idList.Append(models.id + ",");
                    }
                }
                string id_list = Utils.DelLastChar(idList.ToString(), ",");
                if (!string.IsNullOrEmpty(id_list))
                {
                    strSql2.Append(" and id not in(" + id_list + ")");
                }
            }
            SqlParameter[] parameters2 = {
					new SqlParameter("@role_id", SqlDbType.Int,4)};
            parameters2[0].Value = model.id;

            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            //添加权限
            if (model.manager_role_values != null)
            {
                StringBuilder strSql3;
                foreach (Model.manager_role_value models in model.manager_role_values)
                {
                    strSql3 = new StringBuilder();
                    if (models.id == 0)
                    {
                        strSql3.Append("insert into dt_manager_role_value(");
                        strSql3.Append("role_id,channel_name,channel_id,action_type)");
                        strSql3.Append(" values (");
                        strSql3.Append("@role_id,@channel_name,@channel_id,@action_type)");
                        SqlParameter[] parameters3 = {
						new SqlParameter("@role_id", SqlDbType.Int,4),
						new SqlParameter("@channel_name", SqlDbType.NVarChar,255),
						new SqlParameter("@channel_id", SqlDbType.Int,4),
						new SqlParameter("@action_type", SqlDbType.NVarChar,100)};
                        parameters3[0].Value = model.id;
                        parameters3[1].Value = models.channel_name;
                        parameters3[2].Value = models.channel_id;
                        parameters3[3].Value = models.action_type;

                        cmd = new CommandInfo(strSql3.ToString(), parameters3);
                        sqllist.Add(cmd);
                    }
                }
            }

            int rowsAffected = DbHelperSQL.ExecuteSqlTran(sqllist);
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据，及子表所有相关数据
        /// </summary>
        public bool Delete(int id)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from dt_manager_role ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete from dt_manager_role_value ");
            strSql2.Append(" where role_id=@role_id ");
            SqlParameter[] parameters2 = {
					new SqlParameter("@role_id", SqlDbType.Int,4)};
            parameters2[0].Value = id;

            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);
            int rowsAffected = DbHelperSQL.ExecuteSqlTran(sqllist);
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Egojit.Model.manager_role GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,role_name,role_type from dt_manager_role ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Egojit.Model.manager_role model = new Egojit.Model.manager_role();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                #region  父表信息
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                model.role_name = ds.Tables[0].Rows[0]["role_name"].ToString();
                if (ds.Tables[0].Rows[0]["role_type"].ToString() != "")
                {
                    model.role_type = int.Parse(ds.Tables[0].Rows[0]["role_type"].ToString());
                }
                #endregion  父表信息end

                #region  子表信息
                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append("select id,role_id,channel_name,channel_id,action_type from dt_manager_role_value ");
                strSql2.Append(" where role_id=@role_id ");
                SqlParameter[] parameters2 = {
					new SqlParameter("@role_id", SqlDbType.Int,4)};
                parameters2[0].Value = id;

                DataSet ds2 = DbHelperSQL.Query(strSql2.ToString(), parameters2);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    #region  子表字段信息
                    int i = ds2.Tables[0].Rows.Count;
                    List<Egojit.Model.manager_role_value> models = new List<Egojit.Model.manager_role_value>();
                    Egojit.Model.manager_role_value modelt;
                    for (int n = 0; n < i; n++)
                    {
                        modelt = new Egojit.Model.manager_role_value();
                        if (ds2.Tables[0].Rows[n]["id"].ToString() != "")
                        {
                            modelt.id = int.Parse(ds2.Tables[0].Rows[n]["id"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["role_id"].ToString() != "")
                        {
                            modelt.role_id = int.Parse(ds2.Tables[0].Rows[n]["role_id"].ToString());
                        }
                        modelt.channel_name = ds2.Tables[0].Rows[n]["channel_name"].ToString();
                        if (ds2.Tables[0].Rows[n]["channel_id"].ToString() != "")
                        {
                            modelt.channel_id = int.Parse(ds2.Tables[0].Rows[n]["channel_id"].ToString());
                        }
                        modelt.action_type = ds2.Tables[0].Rows[n]["action_type"].ToString();
                        models.Add(modelt);
                    }
                    model.manager_role_values = models;
                    #endregion  子表字段信息end
                }
                #endregion  子表信息end

                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,role_name,role_type ");
            strSql.Append(" FROM dt_manager_role ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}