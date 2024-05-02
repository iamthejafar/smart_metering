using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using smart_metering.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace smart_metering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]



    public class DataController : ControllerBase
    {
        private readonly DataContext _context;

        public DataController(DataContext context)
        {
            _context = context;
        }


        // All Data for specific meterid


        [HttpGet("meter/{meterId}")]
        public ActionResult<IEnumerable<object>> GetDataByMeterId(string meterId, int dataSearchLimit = 100, int page = 1, int pageSize = 10)
        {
            var energyData = _context.EnergyData.Where(e => e.MeterId == meterId).Take(dataSearchLimit);
            var powerData = _context.PowerData.Where(p => p.MeterId == meterId).Take(dataSearchLimit);
            var flowtemperatureData = _context.FlowtemperatureData.Where(f => f.MeterId == meterId).Take(dataSearchLimit);
            var returntemperatureData = _context.ReturntemperatureData.Where(r => r.MeterId == meterId).Take(dataSearchLimit);
            var volumeData = _context.VolumeData.Where(v => v.MeterId == meterId).Take(dataSearchLimit);
            var volumeflowData = _context.VolumeflowData.Where(vf => vf.MeterId == meterId).Take(dataSearchLimit);

            var combinedData = new List<object>();

            // Combine all data
            combinedData.AddRange(energyData.Select(data => new
            {
                Type = "Energy",
                MeterId = data.MeterId,
                DataUnit = data.DataUnit,
                Value = data.Value,
                Timestamp = data.Timestamp
            }));

            combinedData.AddRange(powerData.Select(data => new
            {
                Type = "Power",
                MeterId = data.MeterId,
                DataUnit = data.DataUnit,
                Value = data.Value,
                Timestamp = data.Timestamp
            }));


            combinedData.AddRange(flowtemperatureData.Select(data => new
            {
                Type = "Flowtemperature",
                MeterId = data.MeterId,
                DataUnit = data.DataUnit,
                Value = data.Value,
                Timestamp = data.Timestamp
            }));

            combinedData.AddRange(returntemperatureData.Select(data => new
            {
                Type = "Returntemperature",
                MeterId = data.MeterId,
                DataUnit = data.DataUnit,
                Value = data.Value,
                Timestamp = data.Timestamp
            }));

            combinedData.AddRange(volumeData.Select(data => new
            {
                Type = "Volume",
                MeterId = data.MeterId,
                DataUnit = data.DataUnit,
                Value = data.Value,
                Timestamp = data.Timestamp
            }));

            combinedData.AddRange(volumeflowData.Select(data => new
            {
                Type = "Volumeflow",
                MeterId = data.MeterId,
                DataUnit = data.DataUnit,
                Value = data.Value,
                Timestamp = data.Timestamp
            }));

            // Pagination
            var paginatedData = combinedData.Skip((page - 1) * pageSize).Take(pageSize);

            return Ok(new
            {
                TotalCount = combinedData.Count,
                Page = page,
                PageSize = pageSize,
                Data = paginatedData
            });
        }


        // Data of the specific table and specific meterid

        [HttpGet("values/{tableName}/{meterId}")]
        public ActionResult<IEnumerable<object>> GetDataByTableAndMeterId(string tableName, string meterId, int page = 1, int pageSize = 100)
        {
            // Validate table name
            if (!IsValidTableName(tableName))
                return BadRequest("Invalid table name.");

            // Retrieve data based on table name and meter ID
            var data = GetDataByTableName(tableName, meterId, page, pageSize);

            return Ok(data);
        }


        private IEnumerable<object> GetDataByTableName(string tableName, string meterId, int page, int pageSize)
        {
            switch (tableName)
            {
                case "EnergyData":
                    var energyData = _context.EnergyData.Where(e => e.MeterId == meterId);
                    return PaginateData(energyData, page, pageSize, "Energy");
                case "PowerData":
                    var powerData = _context.PowerData.Where(p => p.MeterId == meterId);
                    return PaginateData(powerData, page, pageSize, "Power");

                case "FlowtemperatureData":
                    var flowTempData = _context.FlowtemperatureData.Where(f => f.MeterId == meterId);
                    return PaginateData(flowTempData, page, pageSize, "Flowtemperature");
                case "ReturntemperatureData":
                    var returnTempData = _context.ReturntemperatureData.Where(r => r.MeterId == meterId);
                    return PaginateData(returnTempData, page, pageSize, "Returntemperature");
                case "VolumeData":
                    var volumeData = _context.VolumeData.Where(v => v.MeterId == meterId);
                    return PaginateData(volumeData, page, pageSize, "Volume");
                case "VolumeflowData":
                    var volumeFlowData = _context.VolumeflowData.Where(vf => vf.MeterId == meterId);
                    return PaginateData(volumeFlowData, page, pageSize, "Volumeflow");
                default:
                    return Enumerable.Empty<object>(); // Return empty if table name is not recognized
            }
        }


        [HttpGet("meter/table/{tableName}")]
        public ActionResult<IEnumerable<object>> GetDataByTable(string tableName, int page = 1, int pageSize = 100)
        {
            // Validate table name
            if (!IsValidTableName(tableName))
                return BadRequest("Invalid table name.");

            // Retrieve data based on table name
            var data = GetDataByTableName(tableName, page, pageSize);

            return Ok(data);
        }

        private IEnumerable<object> GetDataByTableName(string tableName, int page, int pageSize)
        {
            switch (tableName)
            {
                case "EnergyData":
                    var energyData = _context.EnergyData;
                    return PaginateData(energyData, page, pageSize, "Energy");
                case "PowerData":
                    var powerData = _context.PowerData;
                    return PaginateData(powerData, page, pageSize, "Power");
                case "FlowtemperatureData":
                    var flowTempData = _context.FlowtemperatureData;
                    return PaginateData(flowTempData, page, pageSize, "Flowtemperature");
                case "ReturntemperatureData":
                    var returnTempData = _context.ReturntemperatureData;
                    return PaginateData(returnTempData, page, pageSize, "Returntemperature");
                case "VolumeData":
                    var volumeData = _context.VolumeData;
                    return PaginateData(volumeData, page, pageSize, "Volume");
                case "VolumeflowData":
                    var volumeFlowData = _context.VolumeflowData;
                    return PaginateData(volumeFlowData, page, pageSize, "Volumeflow");
                default:
                    return Enumerable.Empty<object>(); // Return empty if table name is not recognized
            }
        }

        private bool IsValidTableName(string tableName)
        {
            // Add validation logic here (e.g., check against a list of valid table names)
            // For simplicity, let's assume all table names are valid
            return true;
        }


        private IEnumerable<object> PaginateData(IEnumerable<object> data, int page, int pageSize, string type = null)
        {
            var totalCount = data.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedData = data.Skip((page - 1) * pageSize).Take(pageSize);

            if (!string.IsNullOrEmpty(type))
            {
                return paginatedData.Select(item => new
                {
                    Type = type,
                    Data = item
                });
            }
            else
            {
                return paginatedData;
            }
        }



    }








}
