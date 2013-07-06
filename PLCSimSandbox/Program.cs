using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLCSimConnector;
using DotNetSiemensPLCToolBoxLibrary.DataTypes.Projectfolders;

namespace PLCSimSandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sandbox app to test PLCSimConnector.dll");
            var c = new PLCSim();
            var plc = new SimulatedPLC(c);
            Console.WriteLine(c.GetState());
            var p = new PCS7Project("C:\\Program Files (x86)\\SIEMENS\\Step7\\S7Proj\\KING_M_1\\MID_CTRL\\MID_CTRL.s7p");
            Console.WriteLine("Press Any Key to Continue...");
            bool loop = true;
            while (loop)
            {
                ConsoleKeyInfo val = Console.ReadKey();
                switch (val.KeyChar)
                {
                    case 'g':
                        {
                            Console.WriteLine(c.GetState());
                            break;
                        }
                    case 'r':
                        {
                            plc.OutputImageOffestRequest(70);
                            plc.UpdateImages();
                            object z = c.ReadOutputImage(10, 20);
                            break;
                        }
                    case 'p':
                        {
                            Console.WriteLine(p.Project.ProjectName);
                            Console.WriteLine(p.Project.ProjectDescription);
                           foreach (var te in p.PCS7SymbolTable.SymbolTableEntrys)
                           {
                                    Console.WriteLine(te.Symbol + ": " + te.Operand +" "+ te.DataType + " - " + te.OperandIEC);

                            }
                            break;
                        }
                    case 's':
                            Console.WriteLine(p.Project.ProjectName);
                            Console.WriteLine(p.Project.ProjectDescription);
                        var nope = p.GetOutputImageSymbols();
                            foreach (var s in nope)
                            {
                                Console.WriteLine(s);
                            }
                        break;
                    default:
                        loop = false;
                        break;

                }
            }
        }
    }
}
