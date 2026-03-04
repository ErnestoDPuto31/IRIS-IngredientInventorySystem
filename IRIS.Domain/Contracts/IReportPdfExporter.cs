using System;
using System.Collections.Generic;
using System.Text;

namespace IRIS.Domain.Contracts
{
    public interface IReportPdfExporter
    {
        void Export(ReportExportData data, string filePath);
    }
}