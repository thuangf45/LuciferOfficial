using LuciferCore.Attributes;
using LuciferCore.Database;
using LuciferCore.Handler.Database;
using LuciferCore.Manager.Database;
using LuciferCore.Manager.Cache;
using System.Data;
using static LuciferCore.Core.Simulation;
using LuciferCore.Extensions;
using LuciferCore.Manager.Log;

namespace Test.core.Handler
{
    public class NewDatabaseHandler : DatabaseHandlerBase
    {
        public override string Table => "yourtable";

        [Safe]
        [RateLimiter(10, 5)]
        [DatabaseJob(DbJobType.Select)]
        protected override async void SelectHandle([Data] List<object> data)
        {
            //string sql = "EXEC Read_User";
            //var dt = await DbCrud.ReadAsync(data, sql, CommandType.StoredProcedure);
            //var table = GetModel<CacheManager>().GetCacheTable(Table);
            //foreach(DataRow row in dt.GetFirstTable().Rows)
            //{
            //    table.AddColdRow(row);
            //}
            GetModel<LogManager>().LogSystem(this, "Select Handle OK");
        }


        [Safe]
        [DatabaseJob(DbJobType.Insert)]
        [RateLimiter(10, 5)]
        protected override async void InsertHandle([Data] List<object> data)
        {
            //using var context = GetModel<DbContextManager>().CreateContext();
            //string sql = "EXEC Create_User";
            //var table = GetModel<CacheManager>().GetCacheTable(Table);
            //table.AddColdRows(data);
            //var response = await DbCrud.CreateAsync(data, sql, CommandType.StoredProcedure);
            //var success = response.GetScalarValue<bool>() ? true : false;
            GetModel<LogManager>().LogSystem(this, "Insert Handle OK");
        }


        [Safe]
        [DatabaseJob(DbJobType.Update)]
        [RateLimiter(10, 5)]
        protected override async void UpdateHandle([Data] List<object> data)
        {
            //string sql = "EXEC Update_User";         
            //var table = GetModel<CacheManager>().GetCacheTable(Table);
            //table.AddColdRows(data);
            //var response = await DbCrud.UpdateAsync(data, sql, CommandType.StoredProcedure);
            //var success = response.GetScalarValue<bool>() ? true : false;
            GetModel<LogManager>().LogSystem(this, "Update Handle OK");
        }

        [Safe]
        [DatabaseJob(DbJobType.Delete)]
        [RateLimiter(10, 5)]
        protected override async void DeleteHandle([Data] List<object> data)
        {
            //string sql = "EXEC Delete_User";         
            //var table = GetModel<CacheManager>().GetCacheTable(Table);
            //table.AddColdRows(data);
            //var response = await DbCrud.DeleteAsync(data, sql, CommandType.StoredProcedure);
            //var success = response.GetScalarValue<bool>() ? true : false;

            GetModel<LogManager>().LogSystem(this, "Delete Handle OK");

        }

    }
}
