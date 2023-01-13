using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using Application = Microsoft.Office.Interop.Excel.Application;

namespace CodedDataGrouper
{
    internal class ExcelInstance
    {
        public Application application;
        public Workbooks workbooks;
        public Workbook workbook;
        public Worksheet? worksheet;

        public ExcelInstance(string filePath, ConfigurationData data, Logger logger)
        {
            application = new Application();
            application.DisplayAlerts = false;

            //get the workbooks
            workbooks = application.Workbooks;

            //open the workbook (file)
            workbook = workbooks.Open(filePath);

            //open the sheet we want to use (index starts at 1)

            try
            {
                worksheet = (Worksheet)workbook.Worksheets[data.EventsSheetName];
            }
            catch
            {
                logger.Log($"Could not find worksheet \"{data.EventsSheetName}\".");
            }
        }

        public void Save()
        {
            application.DisplayAlerts = false;
            workbook.Save();
        }

        public void Show()
        {
            application.Visible = true;
            application.DisplayAlerts = true;
        }

        public void Close()
        {
            application.DisplayAlerts = false;
            workbook.Close();
        }

        public void Dispose()
        {
            if(worksheet!= null)
            {
                Marshal.ReleaseComObject(worksheet);
            }
            Marshal.ReleaseComObject(workbook);
            Marshal.ReleaseComObject(workbooks);
            Marshal.ReleaseComObject(application);
        }
    }
}
