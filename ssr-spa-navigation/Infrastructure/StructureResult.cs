using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ssr_spa_navigation.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ssr_spa_navigation.Infrastructure.Repositories;

namespace ssr_spa_navigation.Infrastructure
{
    public abstract class StructureResult : Attribute, IResultFilter
    {
        private readonly IContentRepository _contentRepository;

        private List<StuctureRequirement> _requirements;

        protected StructureResult(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
            _requirements = new List<StuctureRequirement>();
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {

        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (!string.IsNullOrEmpty(context.HttpContext.Request.Query["ns"]))
                return;

            var objectResult = context.Result as ObjectResult;
            var viewResult = context.Result as ViewResult;
            var jobj = new JObject();

            if (viewResult != null && viewResult.Model != null)
            {
                jobj = JObject.FromObject(viewResult.Model);
            }

            // Check requirements
            foreach (var requirement in _requirements)
            {
                if (string.IsNullOrEmpty(context.HttpContext.Request.Query[requirement.Alias]))
                {
                    var val = requirement.function.Invoke(null);
                    jobj.Add(requirement.StoreProperty, JToken.FromObject(val));
                }
            }

            if (viewResult != null)
            {
                #region 
                if (context.ActionDescriptor.RouteValues["controller"].ToLower() == "offers" && context.ActionDescriptor.RouteValues["action"].ToLower() == "index")
                {
                    jobj.Add("ignoreFirstOffersCall", true);
                }
                if (context.ActionDescriptor.RouteValues["controller"].ToLower() == "offers" && context.ActionDescriptor.RouteValues["action"].ToLower() == "details")
                {
                    jobj.Add("ignoreFirstArticleCall", true);
                }

                if (context.ActionDescriptor.RouteValues["controller"].ToLower() == "home" && context.ActionDescriptor.RouteValues["action"].ToLower() == "index")
                {
                    jobj.Add("ignoreFirstHomeCall", true);
                }

                if (context.ActionDescriptor.RouteValues["controller"].ToLower() == "league")
                {
                    jobj.Add("ignoreFirstLeagueCall", true);
                }
                #endregion

                JObject initialData = null;
                if (viewResult.ViewData["INITIAL_STATE"] != null)
                {
                    initialData = JObject.Parse(viewResult.ViewData["INITIAL_STATE"].ToString());
                    jobj.Merge(initialData, new JsonMergeSettings
                    {
                        // union array values together to avoid duplicates
                        MergeArrayHandling = MergeArrayHandling.Union
                    });
                }

                viewResult.ViewData = new ViewDataDictionary(new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(),
                    new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary()) { { "INITIAL_STATE", jobj.ToString() } };
            }
            else if (objectResult != null)
            {
                var notFirstIteration = objectResult.Value != null && JObject.FromObject(objectResult.Value).ContainsKey("s");
                JToken previousValue = null;

                if (notFirstIteration)
                {
                    previousValue = JObject.FromObject(objectResult.Value)["d"];
                    jobj.Merge(JObject.FromObject(objectResult.Value)["s"], new JsonMergeSettings
                    {
                        // union array values together to avoid duplicates
                        MergeArrayHandling = MergeArrayHandling.Union
                    });
                }

                objectResult.Value = new
                {
                    d = !notFirstIteration ? objectResult.Value : previousValue,
                    s = jobj
                };
            }
        }

        protected void AddStructureRequirement(StuctureRequirement requirement)
        {
            _requirements.Add(requirement);
        }
    }

    public class DefaultStructureResult : StructureResult
    {
        public DefaultStructureResult(IContentRepository contentRepository) : base(contentRepository)
        {
            StuctureRequirement headerRequirement = new StuctureRequirement();
            headerRequirement.StoreProperty = "header";
            headerRequirement.Alias = "h";
            headerRequirement.function = (x) => contentRepository.GetHeaderContent("Sports Betting");

            StuctureRequirement footerRequirement = new StuctureRequirement();
            footerRequirement.StoreProperty = "footer";
            footerRequirement.Alias = "f";
            footerRequirement.function = (x) => contentRepository.GetFooterContent();

            StuctureRequirement sidebarRequirement = new StuctureRequirement();
            sidebarRequirement.StoreProperty = "sidebar";
            sidebarRequirement.Alias = "s";
            sidebarRequirement.function = (x) => contentRepository.GetSports();

            AddStructureRequirement(headerRequirement);
            AddStructureRequirement(footerRequirement);
            AddStructureRequirement(sidebarRequirement);
        }
    }

    public class NoSidebarStructureResult : StructureResult
    {
        public NoSidebarStructureResult(IContentRepository contentRepository) : base(contentRepository)
        {
            StuctureRequirement headerRequirement = new StuctureRequirement();
            headerRequirement.StoreProperty = "header";
            headerRequirement.Alias = "h";
            headerRequirement.function = (x) => contentRepository.GetHeaderContent("Sports Betting");

            StuctureRequirement footerRequirement = new StuctureRequirement();
            footerRequirement.StoreProperty = "footer";
            footerRequirement.Alias = "f";
            footerRequirement.function = (x) => contentRepository.GetFooterContent();

            AddStructureRequirement(headerRequirement);
            AddStructureRequirement(footerRequirement);
        }
    }

    public class LiveStoiximaStructureResult : StructureResult
    {

        public LiveStoiximaStructureResult(IContentRepository contentRepository) : base(contentRepository)
        {
            StuctureRequirement liveStoiximaRequirement = new StuctureRequirement();
            liveStoiximaRequirement.StoreProperty = "live";
            liveStoiximaRequirement.Alias = "l";
            liveStoiximaRequirement.function = (x) => contentRepository.GetLiveBet();

            AddStructureRequirement(liveStoiximaRequirement);
        }

    }
}
