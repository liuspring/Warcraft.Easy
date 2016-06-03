using System;
using System.IO;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Common
{
    public class NpoiExcelHelper
    {
        /// <summary>
        /// 读取excel到datatable中
        /// </summary>
        /// <param name="excelPath">excel地址</param>
        /// <param name="sheetIndex">sheet索引</param>
        /// <returns>成功返回datatable，失败返回null</returns>
        public static DataTable ImportExcel(string excelPath, int sheetIndex)
        {

            IWorkbook workbook = null;//全局workbook
            ISheet sheet;//sheet
            DataTable table = null;
            try
            {
                FileInfo fileInfo = new FileInfo(excelPath);//判断文件是否存在
                if (fileInfo.Exists)
                {
                    FileStream fileStream = fileInfo.OpenRead();//打开文件，得到文件流
                    switch (fileInfo.Extension)
                    {
                        //xls是03，用HSSFWorkbook打开，.xlsx是07或者10用XSSFWorkbook打开
                        case ".xls": workbook = new HSSFWorkbook(fileStream); break;
                        case ".xlsx": workbook = new XSSFWorkbook(fileStream); break;
                        default: break;
                    }
                    fileStream.Close();//关闭文件流
                }
                if (workbook != null)
                {
                    sheet = workbook.GetSheetAt(sheetIndex);//读取到指定的sheet
                    table = new DataTable();//初始化一个table

                    IRow headerRow = sheet.GetRow(0);//获取第一行，一般为表头
                    int cellCount = headerRow.LastCellNum;//得到列数

                    for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                    {
                        DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);//初始化table的列
                        table.Columns.Add(column);
                    }
                    //遍历读取cell
                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                    {
                        NPOI.SS.UserModel.IRow row = sheet.GetRow(i);//得到一行
                        DataRow dataRow = table.NewRow();//新建一个行

                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            ICell cell = row.GetCell(j);//得到cell
                            if (cell == null)//如果cell为null，则赋值为空
                            {
                                dataRow[j] = "";
                            }
                            else
                            {
                                dataRow[j] = row.GetCell(j).ToString();//否则赋值
                            }
                        }

                        table.Rows.Add(dataRow);//把行 加入到table中

                    }
                }
                return table;

            }
            catch
            {
                return table;
            }
            finally
            {
                //释放资源
                if (table != null) { table.Dispose(); }
                workbook = null;
                sheet = null;
            }
        }

    }
}
