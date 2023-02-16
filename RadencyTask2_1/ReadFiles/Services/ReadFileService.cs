﻿using Microsoft.Extensions.Options;
using RadencyTask2_1.Models;
using RadencyTask2_1.PathConfig;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadencyTask2_1.ReadFiles.Services
{
    public class ReadFileService
    {
        private readonly ReadFileCreator _readFileCreator;
        private readonly AppSettings _appSettings;

        public ReadFileService(ReadFileCreator readFileCreator, IOptions<AppSettings> appSettings)
        {
            _readFileCreator = readFileCreator;
            _appSettings = appSettings.Value;
        }

        public void ReadFiles()
        {
            var filesList = Directory.GetFiles(_appSettings.ToRead).GroupBy(x => Path.GetExtension(x));
            foreach (var file in filesList)
            {

                var readService = _readFileCreator.CreateReadService(file.Key);
                if (readService == null)
                {
                    continue;
                }
                readService.ReadFiles(file);

            }
        }




    }

}
