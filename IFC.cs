using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.Attributes;

namespace IFC
{
    internal class IFC: IExternalApplication
    {
        private static IFC dPanel;

        public Result OnStartup(UIControlledApplication application)
        {
            RegisterDockablePanels(application);
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            throw new NotImplementedException();
        }
        public Result RegisterDockablePanels(UIControlledApplication application)
        {
            dPanel = new IFC();
            DockablePaneId dockId = new DockablePaneId(new Guid("118DFA3E-C4C6-4BE9-985B-76F645D0F6D1"));
            try
            {
                application.RegisterDockablePane(dockId, "DockPane", dPanel as IDockablePaneProvider);
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Error Message", ex.Message);
                return Result.Failed;
            }
            return Result.Succeeded;
        }
        [Transaction(TransactionMode.Manual)]
        public class ShowDockPane: IExternalCommand
        {
            public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
            {
                try
                {
                    DockablePaneId id = new DockablePaneId(new Guid("118DFA3E-C4C6-4BE9-985B-76F645D0F6D1"));
                    DockablePane dockableWindow = commandData.Application.GetDockablePane(id);
                    dockableWindow.Show();
                }
                catch(Exception ex)
                {
                    TaskDialog.Show("Info Message", ex.Message);
                }
                return Result.Succeeded;
            }

        }

    }
}
