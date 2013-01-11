using System;
using System.Data;
using System.Collections.Generic;
using Egojit.Model;

namespace Egojit.BLL
{
	/// <summary>
	/// ϵͳģ��
	/// </summary>
	public partial class sys_model
	{
        private readonly Egojit.DAL.sys_model dal = new Egojit.DAL.sys_model();
		public sys_model()
		{}
		#region  Method

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(int id)
		{
			return dal.Exists(id);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int Add(Egojit.Model.sys_model model)
		{
            return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public bool Update(Egojit.Model.sys_model model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public bool Delete(int id)
		{
			
			return dal.Delete(id);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public Egojit.Model.sys_model GetModel(int id)
		{
			return dal.GetModel(id);
		}

		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}

		#endregion  Method
	}

    /// <summary>
    /// ϵͳģ�Ͳ˵�
    /// </summary>
    public partial class sys_model_nav
    {
        private readonly Egojit.DAL.sys_model_nav dal = new DAL.sys_model_nav();
        public sys_model_nav()
        { }
        #region  Method
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        #endregion  Method
    }
}

