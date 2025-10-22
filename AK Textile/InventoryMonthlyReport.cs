using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AK_Textile
{
    public partial class InventoryMonthlyReport : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SDPNF2L\MSSQLSERVER01;
                                                Initial Catalog=Textlies;
                                                Integrated Security=True");
        public InventoryMonthlyReport()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        // Method to add current month filter to your Crystal Report
        private void FilterCurrentMonthData(ReportDocument reportDocument)
        {
            // Get the current system month and year
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;

            // Add a selection formula to filter data for the current month
            reportDocument.RecordSelectionFormula =
                $"Month({{Inventory.DateAdded}}) = {currentMonth} AND " +
                $"Year({{Inventory.DateAdded}}) = {currentYear}";
        }

        // Example usage in your report loading method
        private void LoadReportWithCurrentMonthFilter()
        {
            ReportDocument reportDocument = new ReportDocument();
            reportDocument.Load("path/to/your/report.rpt");

            // Set up your database connection
            TableLogOnInfo logOnInfo = new TableLogOnInfo();
            // ... (your existing database connection logic)

            // Apply current month filter
            FilterCurrentMonthData(reportDocument);

            // Set the report document to your report viewer
            crystalReportViewer1.ReportSource = reportDocument;
        }
    }
}