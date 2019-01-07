using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace UpworkProject.utilities
{
    class ReadExcel
    {
        public String data;

        //this method will accept two parameter. First parameter shows which
        //row need to be read and second parameter(heading) shows which colomn needs
        // to be read.
        public String getExcelFile(int i, String data)
        {

            //Create COM Objects. Create a COM object for everything that is referenced
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"E:\Freelancing\UpworkProject\UpworkProject\data\testdata.xlsx");
            Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;

            int colCount = xlRange.Columns.Count;

            for (int j = 1; j <= colCount; j++)
            {
                if(xlRange.Cells[1, j].Value2.ToString().Equals(data))
                {
                    this.data = xlRange.Cells[i, j].Value2.ToString();
                }
            }
            
            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

            return this.data;
        }
    }
}
