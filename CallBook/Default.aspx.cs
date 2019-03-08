using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallBook.MyClasses;
using CallBook.Services;

namespace CallBook
{
    public partial class Default : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            TaskDBData.GenerateDBData(15);

            if (!IsPostBack)
            {
                BindData();
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            BindData();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            BindData();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            TextBox1.Text = "";
            DropDownList1.SelectedIndex = 0;
            TextBox2.Text = "";
            BindData();
        }

        private IQueryable<T_EVENT> GetData(string sortExpression = "", SortDirection sortDirection = SortDirection.Ascending)
        {
            MyModel context = new MyModel();

            GridView1.PageSize = int.Parse(DropDownList2.SelectedValue);
            IQueryable<T_EVENT> events = null;

            string filterCaller = TextBox1.Text.Trim();
            string filterType = DropDownList1.SelectedValue;
            string filterReceiver = TextBox2.Text.Trim();

            //filters
            if (string.IsNullOrEmpty(filterType))
            {
                events = T_EVENTService.EventsByFilters(context, filterCaller, filterReceiver);
            }
            else
            {
                events = T_EVENTService.EventsByFilters(context, filterCaller, filterReceiver, filterType);
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

            return events;
        }

        private void BindData()
        {
            IQueryable<T_EVENT> events = GetData();
            GridView1.DataSource = GetData().Skip(GridView1.PageIndex * GridView1.PageSize).Take(GridView1.PageSize).ToList();
            GridView1.VirtualItemCount = events.Count();
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindData();
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            GridView gv = sender as GridView;
            ViewState["sortexpression"] = e.SortExpression;

            if (ViewState["sortdirection"] == null || ViewState["sortdirection"].ToString() == "Ascending")
            {
                ViewState["sortdirection"] = SortDirection.Descending;
            }
            else
            {
                ViewState["sortdirection"] = SortDirection.Ascending;
            }

            string a = ViewState["sortdirection"].ToString();
            this.GetData(e.SortExpression);
            BindData();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // Create the CSV file to which grid data will be exported.
            string fileName = "C:\\all_records_" + DateTime.Now.ToString("yyyyMMdd") + ".csv";
            StreamWriter sw = new StreamWriter(fileName, false);
            int pageIndex = GridView1.PageIndex;

            for (int i = 0; i < GridView1.Columns.Count - 1; i++)
            {
                sw.Write(GridView1.Columns[i] + ",");
            }
            sw.Write(sw.NewLine);

            for (int n = 0; n < GridView1.PageCount; n++)
            {
                GridView1.PageIndex = n;
                this.GetData();

                foreach (GridViewRow dr in GridView1.Rows)
                {
                    for (int i = 0; i < GridView1.Columns.Count - 1; i++)
                    {
                        string cellValue = dr.Cells[i].Text;

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

                    sw.Write(sw.NewLine);
                }
            }
            sw.Close();
            GridView1.PageIndex = pageIndex;
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView1.PageSize = Int32.Parse(DropDownList2.SelectedValue);
            GridView1.PageIndex = 0;

            this.BindData();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer'; this.style.fontWeight='bold'; ";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none',this.style.fontWeight=''; ";
                e.Row.Attributes["onclick"] = this.Page.ClientScript.GetPostBackClientHyperlink(this.GridView1, "Select$" + e.Row.RowIndex, true);
                e.Row.Cells[0].Attributes.Clear();
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();

            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowIndex == GridView1.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    row.Style.Add("cursor", "pointer");

                    for (int i = 1; i < row.Cells.Count; i++)
                    {
                        row.Cells[i].Attributes["onclick"] = string.Format("window.open('CurrentCall.aspx?Caller={0}&CallID={1}','CurrentCall', 'width=600,height=400');", ((HyperLink)row.FindControl("Caller")).Text, ((Label)row.FindControl("CallID")).Text);
                    }
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Click to select this row";
                }
            }

        }

    }
}