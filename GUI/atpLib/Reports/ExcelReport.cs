using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atpLib.Reports
{
    abstract public class ExcelReport
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger
       (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected static ExcelReport excelReport;
        protected static readonly object lockObj = new object();

        public ExcelReport()
        {

        }

        public static bool InstanceExists()
        {
            return (excelReport != null);
        }

        abstract public string buildFileName(DateTime testTime, string serial, string temperature, string voltage, string outputDirectory);

        abstract public void createReport(DateTime testTime, string description, string serial, string temperatureString, string voltage, string outputDirectory, ICollection<string> lines);

        public void mergeExcelSheets(string[] inputFiles, string outputFile)
        {
            lock (lockObj)
            {
                try
                {
                    log.Info("Starting Excel Merge...");
                    /* verify that all the input files exists */
                    foreach (String s in inputFiles)
                    {
                        if (!File.Exists(s))
                        {
                            log.Error("the file " + s + " does not exist!");
                            return;
                        }
                    }

                    if (File.Exists(outputFile))
                    {
                        log.Info("The selected output file already exists, deleting it");
                        File.Delete(outputFile);
                    }

                    List<String> FileList = inputFiles.ToList<String>();

                    /* use the first key as anchor */
                    String anchorFile = FileList.First<String>();

                    FileList.Remove(anchorFile);

                    log.Info("The file " + anchorFile + " was selected as the base ATP");
                    File.Copy(anchorFile, outputFile);

                    var outputWb = new XLWorkbook(outputFile);
                    var outputWs = outputWb.Worksheet("Sheet1");

                    int col = 6;
                    foreach (String file in FileList)
                    {
                        log.Info("Merging file " + file);

                        var wb = new XLWorkbook(file);
                        var ws = wb.Worksheet("Sheet1");

                        int idx = 16;
                        for (; idx < 92; idx++)
                        {
                            copyCell(ref ws, ref outputWs, idx, 3, idx, col);
                            copyCell(ref ws, ref outputWs, idx, 4, idx, col + 1);
                        }

                        /* copy date */
                        copyCell(ref ws, ref outputWs, 15, 3, 15, col);
                        copyCell(ref ws, ref outputWs, 15, 4, 15, col + 1);

                        col += 3;
                        log.Info("Merging file " + file + " Done.");
                    }
                    outputWb.Save();
                    log.Info("Excel Merge... Done.");

                }
                catch (Exception ex)
                {
                    log.Error("error while merging excel sheets!", ex);
                }
            }
        }

        private void copyCell(ref IXLWorksheet inputWs, ref IXLWorksheet outputWs, params int[] points)
        {
            if (points.Length != 4)
            {
                throw new NotImplementedException();
            }
            outputWs.Cell(points[2], points[3]).Value = inputWs.Cell(points[0], points[1]).Value;
            outputWs.Cell(points[2], points[3]).Style = inputWs.Cell(points[0], points[1]).Style;
            outputWs.Cell(points[2], points[3]).DataType = inputWs.Cell(points[0], points[1]).DataType;
        }
    }
}
