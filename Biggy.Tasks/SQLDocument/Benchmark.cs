﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biggy.SQLServer;

namespace Biggy.Perf.SQLDocument {
  class Benchmark {

    public static void Run() {
      Console.WriteLine("Connecting to SQL Document Store...");
      var monkies = new SQLDocumentList<Monkey>("northwind");
      monkies.Clear();
      var sw = new Stopwatch();

      sw.Start();
      var addRange = new List<Monkey>();
      for (int i = 0; i < 10000; i++) {
        addRange.Add(new Monkey { Name = "MONKEY " + i, Birthday = DateTime.Today, Description = "The Monkey on my back" });
      }
      var inserted = monkies.AddRange(addRange);
      sw.Stop();
      Console.WriteLine("Just inserted {0} as documents in {1} ms", inserted, sw.ElapsedMilliseconds);

      //use a DB that has an int PK
      sw.Reset();
      sw.Start();
      Console.WriteLine("Loading {0}...", inserted);
      monkies.Reload();
      sw.Stop();
      Console.WriteLine("Loaded {0} documents from SQL Server in {1}ms", inserted, sw.ElapsedMilliseconds);
    }
  }
}