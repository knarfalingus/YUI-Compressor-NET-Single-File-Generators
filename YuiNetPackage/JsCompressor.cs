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
using VSLangProj80; 

using Microsoft.Samples.VisualStudio.GeneratorSample;

namespace YUICompressorTool
{
    [ProvideObject(typeof(JsCompressor), RegisterUsing = RegistrationMethod.CodeBase)]
    [CodeGeneratorRegistration(typeof(JsCompressor), JsCompressor.JsCustomToolName, ToolRegistration.VBCategoryString, GeneratorRegKeyName = JsCompressor.JsCustomToolName, GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(JsCompressor), JsCompressor.JsCustomToolName, ToolRegistration.CSharpCategoryString, GeneratorRegKeyName = JsCompressor.JsCustomToolName, GeneratesDesignTimeSource = true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid(JsCompressor.JsToolGuidString)]
    [ComVisible(true)]
    public class JsCompressor : BaseCodeGeneratorWithSite
    {
        public const string JsToolGuidString = "285A1C50-72B4-40DC-8F2B-FF6A717FFD3A";
        public static Guid JsToolGuid = new Guid(JsToolGuidString);

        public const string JsCustomToolName = "YuiNetJsCompressor";
        public const string JsCustomToolDescription = "YUI.NET Javascript Compressor";
        
        protected override byte[] GenerateCode(string inputFileContent)
        {
            string output = string.Empty;
            try
            {
                var JsCompressor = new JavaScriptCompressor();
                output = JsCompressor.Compress(inputFileContent);
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
            return ".min.js";
        }

        [ComRegisterFunction]
        public static void RegisterClass(Type t)
        {
            //11=2012
            //12=2013
            for (int version = 11; version <= 12;version++)
                ToolRegistration.Register(JsCustomToolName, JsCustomToolDescription, JsToolGuid, new Version(version, 0));

        }

        [ComUnregisterFunction]
        public static void UnRegisterClass(Type t)
        {
            //11=2012
            //12=2013
            for (int version = 11; version <= 12; version++)
                ToolRegistration.Unregister(JsCustomToolName, new Version(version, 0));

        }


    }
}
