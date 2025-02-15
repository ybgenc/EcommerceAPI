﻿using NpgsqlTypes;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;

namespace EcommerceAPI.API.Configurations.ColumnWriters
{
    public class UsernameColumnWriter : ColumnWriterBase
    {
        public UsernameColumnWriter() : base(NpgsqlDbType.Varchar)
        {
        }

        public override object GetValue(LogEvent logEvent, IFormatProvider formatProvider = null)
        {
           var (username,value) = logEvent.Properties.FirstOrDefault(p => p.Key == "username"); 
            
            return value?.ToString() ?? null;
        }
    }
}
