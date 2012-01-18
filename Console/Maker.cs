using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Geekors.Generator.DbSchema;
using Geekors.Generator.DbSchema.Models;
using Geekors.Generator.Template;
using System.IO;

namespace Geekors.Generator
{
    public class Maker
    {
        static void Main(string[] args)
        {

            IService service = new Service(Library.connectionStrings["ConnectionStrings"].ToString());
            Console.WriteLine("開始取得 Schema");
            Result result = service.GetResult();

            TemplateService templateService = new TemplateService();

            //var firstTest = result.Tables.FirstOrDefault();

            Console.WriteLine("開始產生");
            foreach (var item in result.Tables)
            {
                ModelMaker modelMaker = new ModelMaker(item, templateService.ModelTemplate);
                modelMaker.Start();
                modelMaker.Save();

                ServiceMaker serviceMaker = new ServiceMaker(item, templateService.ServiceTempate, "NanYiDataContext");
                serviceMaker.Start();
                serviceMaker.Save();

                ControllerMaker controllerMaker = new ControllerMaker(item, templateService.ControllerTemplate);
                controllerMaker.Start();
                controllerMaker.Save();

                View_AddMaker viewAddmaker = new View_AddMaker(item, templateService.AddTemplate);
                viewAddmaker.Start();
                viewAddmaker.Save();

                View_EditMaker viewEditmaker = new View_EditMaker(item, templateService.EditTemplate);
                viewEditmaker.Start();
                viewEditmaker.Save();

                ViewListMaker viewListmaker = new ViewListMaker(item, templateService.ListTemplate);
                viewListmaker.Start();
                viewListmaker.Save();


                View_GridMaker viewGridmaker = new View_GridMaker(item, templateService._GridTemplate);
                viewGridmaker.Start();
                viewGridmaker.Save();

                View_EditAreaMaker viewEditAreaMaker = new View_EditAreaMaker(item, templateService._EditAreaTemplate);
                viewEditAreaMaker.Start();
                viewEditAreaMaker.Save();
            }

          
            Console.WriteLine("成功");
            Console.ReadKey();
            
        }
    }
}
