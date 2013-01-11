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
    /// �û���(�ȼ�)
    /// </summary>
    public partial class user_groups
    {
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from dt_user_groups");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// ���ر�������
        /// </summary>
        public string GetTitle(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 title from dt_user_groups");
            strSql.Append(" where id=" + id);
            string title = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(title))
            {
                return "";
            }
            return title;
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(Model.user_groups model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into dt_user_groups(");
            strSql.Append("title,grade,upgrade_exp,amount,point,discount,is_default,is_upgrade,is_lock)");
            strSql.Append(" values (");
            strSql.Append("@title,@grade,@upgrade_exp,@amount,@point,@discount,@is_default,@is_upgrade,@is_lock)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@title", SqlDbType.NVarChar,100),
					new SqlParameter("@grade", SqlDbType.Int,4),
					new SqlParameter("@upgrade_exp", SqlDbType.Int,4),
					new SqlParameter("@amount", SqlDbType.Decimal,5),
					new SqlParameter("@point", SqlDbType.Int,4),
					new SqlParameter("@discount", SqlDbType.Int,4),
					new SqlParameter("@is_default", SqlDbType.TinyInt,1),
					new SqlParameter("@is_upgrade", SqlDbType.TinyInt,1),
					new SqlParameter("@is_lock", SqlDbType.TinyInt,1)};
            parameters[0].Value = model.title;
            parameters[1].Value = model.grade;
            parameters[2].Value = model.upgrade_exp;
            parameters[3].Value = model.amount;
            parameters[4].Value = model.point;
            parameters[5].Value = model.discount;
            parameters[6].Value = model.is_default;
            parameters[7].Value = model.is_upgrade;
            parameters[8].Value = model.is_lock;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Update(Model.user_groups model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dt_user_groups set ");
            strSql.Append("title=@title,");
            strSql.Append("grade=@grade,");
            strSql.Append("upgrade_exp=@upgrade_exp,");
            strSql.Append("amount=@amount,");
            strSql.Append("point=@point,");
            strSql.Append("discount=@discount,");
            strSql.Append("is_default=@is_default,");
            strSql.Append("is_upgrade=@is_upgrade,");
            strSql.Append("is_lock=@is_lock");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@title", SqlDbType.NVarChar,100),
					new SqlParameter("@grade", SqlDbType.Int,4),
					new SqlParameter("@upgrade_exp", SqlDbType.Int,4),
					new SqlParameter("@amount", SqlDbType.Decimal,5),
					new SqlParameter("@point", SqlDbType.Int,4),
					new SqlParameter("@discount", SqlDbType.Int,4),
					new SqlParameter("@is_default", SqlDbType.TinyInt,1),
					new SqlParameter("@is_upgrade", SqlDbType.TinyInt,1),
					new SqlParameter("@is_lock", SqlDbType.TinyInt,1),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.title;
            parameters[1].Value = model.grade;
            parameters[2].Value = model.upgrade_exp;
            parameters[3].Value = model.amount;
            parameters[4].Value = model.point;
            parameters[5].Value = model.discount;
            parameters[6].Value = model.is_default;
            parameters[7].Value = model.is_upgrade;
            parameters[8].Value = model.is_lock;
            parameters[9].Value = model.id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from dt_user_groups ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public Model.user_groups GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,title,grade,upgrade_exp,amount,point,discount,is_default,is_upgrade,is_lock from dt_user_groups ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Egojit.Model.user_groups model = new Egojit.Model.user_groups();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["title"] != null && ds.Tables[0].Rows[0]["title"].ToString() != "")
                {
                    model.title = ds.Tables[0].Rows[0]["title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["grade"] != null && ds.Tables[0].Rows[0]["grade"].ToString() != "")
                {
                    model.grade = int.Parse(ds.Tables[0].Rows[0]["grade"].ToString());
                }
                if (ds.Tables[0].Rows[0]["upgrade_exp"] != null && ds.Tables[0].Rows[0]["upgrade_exp"].ToString() != "")
                {
                    model.upgrade_exp = int.Parse(ds.Tables[0].Rows[0]["upgrade_exp"].ToString());
                }
                if (ds.Tables[0].Rows[0]["amount"] != null && ds.Tables[0].Rows[0]["amount"].ToString() != "")
                {
                    model.amount = decimal.Parse(ds.Tables[0].Rows[0]["amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["point"] != null && ds.Tables[0].Rows[0]["point"].ToString() != "")
                {
                    model.point = int.Parse(ds.Tables[0].Rows[0]["point"].ToString());
                }
                if (ds.Tables[0].Rows[0]["discount"] != null && ds.Tables[0].Rows[0]["discount"].ToString() != "")
                {
                    model.discount = int.Parse(ds.Tables[0].Rows[0]["discount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_default"] != null && ds.Tables[0].Rows[0]["is_default"].ToString() != "")
                {
                    model.is_default = int.Parse(ds.Tables[0].Rows[0]["is_default"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_upgrade"] != null && ds.Tables[0].Rows[0]["is_upgrade"].ToString() != "")
                {
                    model.is_upgrade = int.Parse(ds.Tables[0].Rows[0]["is_upgrade"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_lock"] != null && ds.Tables[0].Rows[0]["is_lock"].ToString() != "")
                {
                    model.is_lock = int.Parse(ds.Tables[0].Rows[0]["is_lock"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// ȡ��Ĭ�����ʵ��
        /// </summary>
        public Model.user_groups GetDefault()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id from dt_user_groups where is_lock=0 order by is_default desc,id asc");
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj != null)
            {
                return GetModel(Convert.ToInt32(obj));
            }
            return null;
        }

        /// <summary>
        /// ���ݾ���ֵ�������������ʵ��
        /// </summary>
        public Model.user_groups GetUpgrade(int group_id, int exp)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id from dt_user_groups");
            strSql.Append(" where is_lock=0 and is_upgrade=1 and grade>(select grade from dt_user_groups where id=" + group_id + ") and upgrade_exp<=" + exp);
            strSql.Append(" order by grade asc");
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj != null)
            {
                return GetModel(Convert.ToInt32(obj));
            }
            return null;
        }

        /// <summary>
        /// ���ǰ��������
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" id,title,grade,upgrade_exp,amount,point,discount,is_default,is_upgrade,is_lock ");
            strSql.Append(" FROM dt_user_groups ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

    }
}