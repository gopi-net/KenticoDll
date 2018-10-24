using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;
using System.Web;
using Bluespire.Emerge.Common.Logging;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Common;

namespace Bluespire.Emerge.CommonService.Unity
{
    /// <summary>
    /// Class responsible for instanstiating classes using Unity Application Block.
    /// </summary>
    public class EmergeUnityFactory
    {
        private static IUnityContainer _container;

        private string _configFileName = string.Empty;
        private string _containerName = string.Empty;
        private const string UNITY = "Unity";

        public EmergeUnityFactory(string configFileName, string containerName)
        {
            _configFileName = configFileName;
            _containerName = containerName;
        }

        /// <summary>
        /// Configures the Unity container with configuration from web.config file.
        /// </summary>
        private void ConfigureUnityFactory()
        {
            try
            {
                _container = new UnityContainer();
                string rootPath = HttpRuntime.AppDomainAppPath + Constants.CONFIGURATION_FOLDER + "\\";
                ConfigureContainer(rootPath, _configFileName, _containerName);
            }
            catch (NullReferenceException nullErrEx)
            {
                EmergeLogWriter.WriteError(UNITY, EventCode.EMERGE_UNITYCONFIGURATION, nullErrEx.ToString());
                throw new EmergeUnityException(nullErrEx.Message);
            }
            catch (ConfigurationErrorsException confErrEx)
            {
                EmergeLogWriter.WriteError(UNITY, EventCode.EMERGE_UNITYCONFIGURATION, confErrEx.ToString());
                throw new EmergeUnityException(confErrEx.Message);
            }
           

        }

        private void ConfigureContainer(string rootPath, string configFileName, string containerName)
        {
            try
            {
                ExeConfigurationFileMap map = new ExeConfigurationFileMap();
                try
                {
                    map.ExeConfigFilename = rootPath + configFileName; 
                }
                catch (NullReferenceException nullErrEx)
                {
                    EmergeLogWriter.WriteError(UNITY, EventCode.EMERGE_UNITYCONFIGURATION, nullErrEx.ToString());
                    throw new EmergeUnityException(nullErrEx.Message);
                }

                System.Configuration.Configuration config
                  = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

                UnityConfigurationSection sectionDAL
                  = (UnityConfigurationSection)config.GetSection(Constants.UNITY_SECTION);


                UnityConfigurationSection.CurrentSection.Configure(_container, containerName);
            }
            catch (ArgumentException argEx)
            {
                EmergeLogWriter.WriteError(UNITY, EventCode.EMERGE_UNITYCONFIGURATION, argEx.ToString());
                throw new EmergeUnityException(argEx.Message);
            }
            catch (NullReferenceException nullErrEx)
            {
                EmergeLogWriter.WriteError(UNITY, EventCode.EMERGE_UNITYCONFIGURATION, nullErrEx.ToString());
                throw new EmergeUnityException(nullErrEx.Message);
            }
            catch (ConfigurationErrorsException confErrEx)
            {
                EmergeLogWriter.WriteError(UNITY, EventCode.EMERGE_UNITYCONFIGURATION, confErrEx.ToString());
                throw new EmergeUnityException(confErrEx.Message);
            }

        }

        /// <summary>
        /// Returns the instance of an object as per Type
        /// </summary>
        /// <typeparam name="T">Interface as T</typeparam>
        /// <returns>Interface as T</returns>
        public T GetTypeInstance<T>()
        {
            try
            {
                if (_container == null)
                {
                    ConfigureUnityFactory();
                }
                return _container.Resolve<T>();
            }
            catch (ResolutionFailedException resEx)
            {
                EmergeLogWriter.WriteError(UNITY, EventCode.EMERGE_UNITYCONFIGURATION, resEx.ToString());
                throw new EmergeUnityException(resEx.Message);
            }

        }
    }
}
