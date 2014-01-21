using System;
using System.Linq;
using DotNetSiemensPLCToolBoxLibrary.DataTypes.Projectfolders.Step7V5;
using DotNetSiemensPLCToolBoxLibrary.Projectfiles;

namespace PLCSimConnector
{
    public class PCS7Project
    {
        private String pcs7ProjectFile;
 
        public PCS7Project() 
        {
        }
        public PCS7Project(string projectFile){
            
            //Project = new Step7ProjectV5(projectFile,false);
            File = projectFile;
        }
        public String File { 
            get { return pcs7ProjectFile; }
            set 
            { 
                pcs7ProjectFile = value;
                Project = new Step7ProjectV5(value, false);
            }
        }
        public Step7ProjectV5 Project;
        private SymbolTable pcs7SymbolTable;

        public SymbolTable PCS7SymbolTable
        {
            get 
            {
                if (pcs7SymbolTable != null) return pcs7SymbolTable;
                pcs7SymbolTable = (SymbolTable)Project.S7ProgrammFolders[0].SymbolTable;
                return pcs7SymbolTable;
            }
        }

        public string[,] GetOutputImageSymbolsOperands()
        {
            var returnString = new string[PCS7SymbolTable.SymbolTableEntrys.Count, 2];
            int i = 0;
            foreach (var symEntry in PCS7SymbolTable.SymbolTableEntrys)
            {
                returnString[i, 0] = symEntry.Symbol;
                returnString[i, 1] = symEntry.OperandIEC;
                i++;
            }

            return returnString;
        }

        public string[] GetOutputImageSymbols()
        {
            string[] returnString = PCS7SymbolTable.SymbolTableEntrys.Where(sym => sym.OperandIEC.First().Equals('Q')).OrderBy(sym => sym.Symbol).Select(sym => sym.Symbol).ToArray();

            return returnString;
        }

        public string[] GetDataSymbols()
        {
            string[] returnString = PCS7SymbolTable.SymbolTableEntrys.Where(sym => true).OrderBy(sym => sym.Symbol).Select(sym => sym.Symbol).ToArray();

            return returnString;
        }
    }

}
