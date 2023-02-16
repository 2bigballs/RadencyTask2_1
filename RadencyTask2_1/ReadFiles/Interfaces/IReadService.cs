﻿using RadencyTask2_1.ReadFiles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadencyTask2_1.ReadFiles.Interfaces
{
    public interface IReadService
    {
        public string ExtensionType { get; }
        List<RawPaymentTransaction> ReadFiles(IEnumerable<string> files);

    }
}
