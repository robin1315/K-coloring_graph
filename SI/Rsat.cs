using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace SI.rsat
{
    public class Rsat
    {
        #region Private fields

        private List<string> _rsatReponse;

        #endregion

        #region Properties

        public bool IsSatisfiable
        {
            get
            {
                return IsProblemSatisfiable();
            }
        }

        public string ResultVariables
        {
            get { return GetRsatResultVariables(); }
        }

        #endregion

        #region Methods

        public void Run()
        {
            _rsatReponse = new List<string>();

            try
            {
                var start = new ProcessStartInfo
                {
                    FileName = "rsat_2.01_win.exe",
                    Arguments = "problem.cnf -s",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                };

                using (Process p = Process.Start(start))
                {
                    if (p != null)
                        using (StreamReader reader = p.StandardOutput)
                        {
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                _rsatReponse.Add(line);
                            }
                        }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }

        private bool IsProblemSatisfiable()
        {
            bool problemSolved = false;
            if (_rsatReponse.Count > 0)
            {
                foreach (var line in _rsatReponse)
                {
                    if (line.Contains("SATISFIABLE"))
                    {
                        problemSolved = true;
                        break;
                    }
                }
            }
            return problemSolved;
        }

        private string GetRsatResultVariables()
        {
            string result = string.Empty;

            foreach (var line in _rsatReponse)
            {
                if (line.StartsWith("v"))
                {
                    result = line;
                    break;
                }
            }

            return result;
        }

        #endregion
    }
}
