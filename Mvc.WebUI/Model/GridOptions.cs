using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.WebUI.Model
{
    public class GridOptions
    {
        public string GridId { get; set; }
        public Type SourceType { get; set; }

        public bool sourceDefined { get; set; }

        public object[] Parameters { get; set; }

        public string[] DisplayColumnsName { get; set; }

        public string[] ValueColumnsName { get; set; }

        public IList Source { get; set; }

        public List<CustomGridColumns> commandColumns { get; set; }

        public string[] ColumnWidths { get; set; }

        public bool IsNEwButton { get; set; }

        public PageLink NewButtonUrl { get; set; }

        public GridHeader GridHeader { get; set; }

        public bool IsGridHeader { get; set; }

        public bool IsPrivate { get; set; }

        public Guid? TaskId { get; set; }
    }

    public class GridOptionsManager
    {
        private readonly GridOptions _gridOptions;

        public GridOptionsManager(GridOptions gridOptions)
        {
            _gridOptions = gridOptions;
        }

        public GridControl GetClass()
        {
            try
            {
                GridControl gridControl = new GridControl();
                gridControl.Id = _gridOptions.GridId;
                gridControl.IsNEwButton = _gridOptions.IsNEwButton;
                gridControl.NewButtonUrl = _gridOptions.NewButtonUrl;
                gridControl.GridHeader = _gridOptions.GridHeader;
                gridControl.IsGridHeader = _gridOptions.IsGridHeader;

                this.Validations();

                object obj = Activator.CreateInstance(_gridOptions.SourceType);

                gridControl.AoColumnDefs = PrepareColumnDefs();

                if ((_gridOptions.sourceDefined && _gridOptions.Source != null))
                {
                    StringBuilder filter = new StringBuilder();
                    StringBuilder filterText = new StringBuilder();

                    foreach (string f in _gridOptions.DisplayColumnsName)
                    {
                        filter.AppendLine("<th>" + f + "</th>");
                        filterText.AppendLine("<th></th>");
                    }

                    if (_gridOptions.commandColumns != null && _gridOptions.commandColumns.Count > 0)
                    {
                        foreach (CustomGridColumns item in _gridOptions.commandColumns)
                        {
                            filter.AppendLine("<th>" + item.DisplayColumnName + "</th>");
                            filterText.AppendLine("<th></th>");
                        }
                    }

                    gridControl.Filter = filterText.ToString();
                    gridControl.Header = filter.ToString();
                    gridControl.Footer = filter.ToString();

                    StringBuilder body = new StringBuilder();
                    PropertyInfo[] oInf;
                    PropertyInfo inf;

                    foreach (var o in _gridOptions.Source)
                    {
                        oInf = o.GetType().GetProperties();

                        body.AppendLine("<tr>");
                        foreach (string v in _gridOptions.ValueColumnsName)
                        {
                            inf = oInf.ToList().Where(a => a.Name.Equals(v)).First();
                            //body.AppendLine("<td>"+inf.GetValue(o) == null ? "" : inf.GetValue(o).ToString() +"</td>");
                            body.AppendLine("<td>" +
                               (inf.GetValue(o) != null ? inf.GetValue(o).ToString() : "")
                                + "</td>");
                        }

                        if (_gridOptions.commandColumns != null && _gridOptions.commandColumns.Count > 0)
                        {
                            foreach (CustomGridColumns item in _gridOptions.commandColumns)
                            {
                                if (item.ActionFunction != null && item.ActionFunction.Length > 0)
                                {
                                    body.AppendLine("<td>");
                                    for (int i = 0; i < item.ActionFunction.Length; i++)
                                    {
                                        MethodInfo m = obj.GetType().GetMethod(item.ActionFunction[i], BindingFlags.Instance | BindingFlags.Public);
                                        if (m != null)
                                        {
                                            object donus = m.Invoke(o, null);
                                            body.Append("&nbsp;");
                                            if (donus != null && donus is string)
                                            {
                                                body.Append(donus.ToString());
                                            }
                                        }
                                    }
                                    body.AppendLine("</td>");
                                }
                                else
                                    body.AppendLine("<td></td>");
                            }
                        }

                        body.AppendLine("</tr>");
                    }

                    gridControl.Body = body.ToString();
                }
                else
                    throw new Exception("Source function değeri girilmemiş.");

                return gridControl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string PrepareColumnDefs()
        {

            string jstring = "";
            int columnCount = (_gridOptions.DisplayColumnsName != null ? _gridOptions.DisplayColumnsName.Length : 0) +
                (_gridOptions.commandColumns != null ? _gridOptions.commandColumns.Count : 0);

            if (_gridOptions.ColumnWidths == null || (_gridOptions.ColumnWidths != null && _gridOptions.ColumnWidths.Length == 0))
            {

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < columnCount; i++)
                {

                    sb.Append("{");
                    sb.AppendFormat("\"aTargets\":[{0}],", i.ToString());

                    sb.Append("'sWidth':'auto'");
                    sb.Append("}");
                    if ((columnCount - 1) != i)
                        sb.Append(",").AppendLine();

                }

                jstring = "[" + sb.ToString() + "];";
                string java1 = @"var aoColumnDefs_" + _gridOptions.GridId + " = " + jstring;
                //this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), this.ClientID + "_gridScript", java1, true);
                //ViewState["java1" + this.ClientID] = java1;
                return java1;

            }

            if (columnCount > 0)
            {
                StringBuilder sb = new StringBuilder();
                string clsName = "";
                for (int i = 0; i < columnCount; i++)
                {
                    sb.Append("{");
                    sb.AppendFormat("'aTargets':[{0}],", i.ToString());
                    clsName = GetClassName(_gridOptions.ColumnWidths[i]);
                    if (clsName != "")
                    {
                        sb.AppendFormat("'sClass':'{0}',", clsName);
                    }
                    sb.AppendFormat("'sWidth':'{0}'", _gridOptions.ColumnWidths[i].Split(';')[0]);
                    if (_gridOptions.ColumnWidths[i].Split(';').Length == 3)
                    {
                        if (_gridOptions.ColumnWidths[i].Split(';')[2] != null && _gridOptions.ColumnWidths[i].Split(';')[2] != "")
                        {
                            if (_gridOptions.ColumnWidths[i].Split(';')[2].Split(':')[0] == "visible")
                            {
                                //string[] roles = BLayer.AgcMembership.GetRolesForUser(this.Page.User.Identity.Name);
                                //string whois = _gridOptions.ColumnWidths[i].Split(';')[2].Split(':')[1];
                                //switch (whois)
                                //{
                                //    case "admin":
                                //        if (roles.Contains(Utility.Roles.Partner.ToString()) || roles.Contains(Utility.Roles.Katilimci.ToString()))
                                //        {
                                //            sb.Append(getVisibility("false"));
                                //        }
                                //        else
                                //            sb.Append(getVisibility("true"));
                                //        break;
                                //    case "user":
                                //        if (roles.Contains(Utility.Roles.Admin.ToString()) || roles.Contains(Utility.Roles.SatisTemsilcisi.ToString()))
                                //        {
                                //            sb.Append(getVisibility("false"));
                                //        }
                                //        else
                                //            sb.Append(getVisibility("true"));
                                //        break;
                                //    default:
                                //        sb.Append(getVisibility("false"));
                                //        break;
                                //}
                            }
                        }
                    }
                    else
                    {

                        sb.Append(getVisibility("true"));
                    }
                    sb.Append("}");
                    if ((columnCount - 1) != i)
                        sb.Append(",").AppendLine();
                }
                jstring = "[" + sb.ToString() + "];";
            }

            string java = @"var aoColumnDefs_" + _gridOptions.GridId + " = " + jstring;
            return java;
        }

        protected void Validations()
        {
            if (_gridOptions.DisplayColumnsName == null)
                throw new Exception("DisplayColumnName null olamaz");
            if (_gridOptions.ValueColumnsName == null)
                throw new Exception("ValueColumnsName null olamaz");
            if (_gridOptions.DisplayColumnsName != null && _gridOptions.DisplayColumnsName.Length <= 0)
                throw new Exception("DisplayColumnName length sıfırdan büyük değil");
            if (_gridOptions.ValueColumnsName != null && _gridOptions.ValueColumnsName.Length <= 0)
                throw new Exception("ValueColumnsName length sıfırdan büyük değil");

            if (_gridOptions.ValueColumnsName.Length != _gridOptions.DisplayColumnsName.Length)
                throw new Exception("ValueColumnsName ile DisplayColumnsName uzunlukları eşit değil");

            if (_gridOptions.SourceType == null)
                throw new Exception("SourceType null olamaz");
        }

        protected string getVisibility(string obj)
        {
            return ",'bVisible': '" + obj + "'";
        }

        protected string GetClassName(string obj)
        {
            if (obj.Split(';').Length > 1)
            {
                switch (obj.Split(';')[1].ToLower())
                {
                    case "left":
                        return "gColLeft";
                    case "right":
                        return "gColRight";
                    case "center":
                        return "gColCenter";
                    default:
                        return "gColLeft";
                }
            }
            else
                return "";
        }
    }
}
