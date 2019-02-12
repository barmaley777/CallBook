using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CallBook
{
    public partial class Default4 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetData();
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData();
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            TextBox1.Text = "";
            DropDownList1.SelectedIndex = 0;
            TextBox2.Text = "";
            GetData();
        }

        private void GetData(string sortExpression = "", SortDirection sortDirection = SortDirection.Ascending)
        {
            MyModel context = new MyModel();

            GridView1.PageSize = Int32.Parse(DropDownList2.SelectedValue);
            IQueryable<T_EVENT> events = null;

            string filterCaller = TextBox1.Text.Trim();
            string filterType = DropDownList1.SelectedValue;
            string filterReceiver = TextBox2.Text.Trim();

            //filters
            if (string.IsNullOrEmpty(filterType))
            {
                events = context.T_EVENT.Where(item => item.T_CALL.CALLER.ToString().Contains(filterCaller))
                .Where(item => item.T_CALL.RECIEVER.ToString().Contains(filterReceiver));
            }
            else
            {
                events = context.T_EVENT.Where(item => item.T_CALL.CALLER.ToString().Contains(filterCaller))
                .Where(item => item.T_EVENT_TYPE.EVENT_NAME.Equals(filterType))
                .Where(item => item.T_CALL.RECIEVER.ToString().Contains(filterReceiver));
            }

            //sorting
            if (ViewState["sortexpression"] != null)
            {
                switch (ViewState["sortexpression"].ToString().ToUpper())
                {
                    case "T_CALL.CALLER":
                        events = ViewState["sortdirection"].Equals(SortDirection.Ascending) ? events.OrderBy(item => item.T_CALL.CALLER) : events.OrderByDescending(item => item.T_CALL.CALLER);
                        break;
                    case "T_EVENT_TYPE.EVENT_NAME":
                        events = ViewState["sortdirection"].Equals(SortDirection.Ascending) ? events.OrderBy(item => item.T_EVENT_TYPE.EVENT_NAME) : events.OrderByDescending(item => item.T_EVENT_TYPE.EVENT_NAME);
                        break;
                    case "T_CALL.RECIEVER":
                        events = ViewState["sortdirection"].Equals(SortDirection.Ascending) ? events.OrderBy(item => item.T_CALL.RECIEVER) : events.OrderByDescending(item => item.T_CALL.RECIEVER);
                        break;
                    case "RECORD_DATE":
                        events = ViewState["sortdirection"].Equals(SortDirection.Ascending) ? events.OrderBy(item => item.RECORD_DATE) : events.OrderByDescending(item => item.RECORD_DATE);
                        break;
                    default:
                        events = events.OrderBy(item => item.T_CALL.CALLER);
                        break;

                }
            }
            else
            {
                events = events.OrderBy(item => item.T_CALL.CALLER);
            }

            //page index
            int recCount = events.Count();
            if ((GridView1.PageIndex * GridView1.PageSize) > recCount)
            {
                GridView1.PageIndex = recCount / GridView1.PageSize;
            }

            GridView1.DataSource = events.Skip(GridView1.PageIndex * GridView1.PageSize).Take(GridView1.PageSize).ToList();
            GridView1.VirtualItemCount = events.Count();
            GridView1.DataBind();

        }



        protected void GridView1_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetData();
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            GridView gv = sender as GridView;
            ViewState["sortexpression"] = e.SortExpression;

            if (ViewState["sortdirection"] == null)
            {
                ViewState["sortdirection"] = SortDirection.Ascending;
            }
            else
            {
                if (ViewState["sortdirection"].ToString() == "Ascending")
                {
                    ViewState["sortdirection"] = SortDirection.Descending;
                }
                else
                {
                    ViewState["sortdirection"] = SortDirection.Ascending;
                }
            }

            string a = ViewState["sortdirection"].ToString();
            this.GetData(e.SortExpression);

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //not working
            //int count1 = GridView1.PageCount;        
            //GridView1.AllowPaging = false;
            //GridView1.PageSize = GridView1.VirtualItemCount;
            //GridView1.AllowCustomPaging = false;
            //this.GetData();
            //GridView1.DataBind();


            // Create the CSV file to which grid data will be exported.
            string fileName = "C:\\all_records_" + DateTime.Now.ToString("yyyyMMdd") + ".csv";
            StreamWriter sw = new StreamWriter(fileName, false);
            int pageIndex = GridView1.PageIndex;

            int recCount = GridView1.Columns.Count;
            for (int i = 0; i < GridView1.Columns.Count - 1; i++)
            {
                sw.Write(GridView1.Columns[i]);
                if (i < recCount - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);


            for (int n = 0; n < GridView1.PageCount; n++)
            {
                GridView1.PageIndex = n;
                this.GetData();

                foreach (GridViewRow dr in GridView1.Rows)
                {
                    for (int i = 0; i < recCount - 1; i++)
                    {
                        string cellValue = dr.Cells[i].Text;

                        if (!Convert.IsDBNull(dr))
                        {
                            if (cellValue == string.Empty)
                            {
                                Control ctl = GridView1.Rows[i].Cells[i].FindControl("Caller");
                                sw.Write(((HyperLink)ctl).Text + ',');
                            }
                            else
                            {
                                sw.Write(cellValue + ',');
                           }
                        }

                    }
                    sw.Write(sw.NewLine);
                }
            }
            sw.Close();
            GridView1.PageIndex = pageIndex;
            this.GetData();
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView1.PageSize = Int32.Parse(DropDownList2.SelectedValue);
            GridView1.PageIndex = 0;

            this.GetData();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (((HyperLink)e.Row.Cells[0].FindControl("Caller")).NavigateUrl == null)
                {

                }
                else
                {
                    //this.style.cursor='hand' this.style.textDecoration='underline'
                    e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer'; this.style.fontWeight='bold'; ";
                    e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none',this.style.fontWeight=''; ";
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Attributes["onclick"] = this.Page.ClientScript.GetPostBackClientHyperlink(this.GridView1, "Select$" + e.Row.RowIndex);
                    }
                    //e.Row.Attributes["onclick"] = string.Format("window.open('CurrentCall2.aspx?Caller={0}&CallID={1}','window', resizable='yes');", ((HyperLink)e.Row.FindControl("Caller")).Text, ((Label)e.Row.FindControl("CallID")).Text);
                    e.Row.ToolTip = "Click to row to see all calls for this number.";
                }


            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData();

            int pageIndex = GridView1.PageIndex;//int.Parse((sender as LinkButton).CommandArgument);
            var rowIndex = GridView1.SelectedIndex;
            var rowSelected = GridView1.SelectedRow;
            var rowMethod = GridView1.SelectMethod;
            var rowStyle = GridView1.SelectedRowStyle;
            var rowPersistedDataKey = GridView1.SelectedPersistedDataKey;

            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowIndex == GridView1.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    row.ToolTip = string.Empty;
                    //String aaa = row.Cells[2].Text;
                    row.Style.Add("cursor", "pointer");
                    row.Attributes["onclick"] = string.Format("window.open('CurrentCall2.aspx?Caller={0}&CallID={1}','window', resizable='yes');", ((HyperLink)row.FindControl("Caller")).Text, ((Label)row.FindControl("CallID")).Text);
                    //row.Attributes["onclick"] = string.Format("window.open('CurrentCall2.aspx', 'blank', resizable='yes'); ");
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Click to select this row";

                }
            }


        }


        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VIEW")
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string dealId = lnkView.CommandArgument;
            }

        }
    }
}