using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahoo.Yui.Compressor;
using System.Runtime.InteropServices;
using System.IO;
using System.CodeDom;
using Microsoft.VisualStudio.Shell;

using Microsoft.Samples.VisualStudio.GeneratorSample;


namespace YUICompressorTool 
{

    [ProvideObject(typeof(CssCompressor), RegisterUsing = RegistrationMethod.CodeBase)]
    [CodeGeneratorRegistration(typeof(CssCompressor), CssCompressor.CssCustomToolName, ToolRegistration.VBCategoryString, GeneratorRegKeyName = CssCompressor.CssCustomToolName, GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(CssCompressor), CssCompressor.CssCustomToolName, ToolRegistration.CSharpCategoryString, GeneratorRegKeyName = CssCompressor.CssCustomToolName, GeneratesDesignTimeSource = true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid(CssCompressor.CssToolGuidString)]
    [ComVisible(true)]
    public class CssCompressor : BaseCodeGeneratorWithSite
    {
        public const string CssToolGuidString = "CEA6F3F3-4C40-4E87-8461-8F310F1A5C2C";
        public static Guid CssToolGuid = new Guid(CssToolGuidString);

        public const string CssCustomToolName = "YuiNetCssCompressor";
        public const string CssCustomToolDescription = "YUI.NET Css Compressor";



        protected override byte[] GenerateCode(string inputFileContent)
        {
            string output = string.Empty;
            try
            {
                var Compressor = new Yahoo.Yui.Compressor.CssCompressor();
                output = Compressor.Compress(inputFileContent);
            }
            catch (Exception e)
            {
                throw new COMException(string.Format("{0}: {1}\n{2}",
                        e.GetType().Name, e.Message, e.StackTrace));
            }
            return Encoding.ASCII.GetBytes(output);
        }

        protected override string GetDefaultExtension()
        {
            return ".min.css";
        }

        [ComRegisterFunction]
        public static void RegisterClass(Type t)
        {
            //11=2012
            //12=2013
            for (int version = 11; version <= 12; version++)
                ToolRegistration.Register(CssCustomToolName, CssCustomToolDescription, CssToolGuid, new Version(version, 0));

        }

        [ComUnregisterFunction]
        public static void UnRegisterClass(Type t)
        {
            //11=2012
            //12=2013
            for (int version = 11; version <= 12; version++)
                ToolRegistration.Unregister(CssCustomToolName, new Version(version, 0));

        }
    }
}
